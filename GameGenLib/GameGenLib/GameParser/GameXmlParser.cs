using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using GameGenLib.GameEntities;
using GameGenLib.Logics;
using GameGenLib.Logics.Cells;

namespace GameGenLib.GameParser {
    public class GameXmlParser {
        private const string FieldNodeName = "Field";
        private const string LogicsNodeName = "Logics";
        private const string LogicNodeName = "Logic";
        private const string PlayersNodeName = "Players";
        private const string PlayerNodeName = "Player";
        private const string PlayerFigureNodeName = "PlayerFigure";
        private const string FiguresNodeName = "Figures";
        private const string FigureNodeName = "Figure";
        private const string EndGameNodeName = "EndGame";
        private const string NextMoveEventNodeName = "NextMoveEvent";
        private const string RulesNodeName = "Rules";

        private const string SizeAttributeName = "Size";
        private const string NameAttributeName = "Name";
        private const string PossibleMovesAttributeName = "PossibleMoves";
        private const string TypeAttributeName = "Type";
        private const string PlayerWinConditionNodeName = "PlayerWinCondition";
        private const string FieldPropertyNodeName = "FieldProperty";
        private const string MoveActionAttributeName = "MoveAction";
        private const string InitFieldNodeName = "InitField";

        private readonly XDocument doc;
        private readonly PropertiesMapping propertiesMapping;
        private readonly GlobalCollections globalCollections = new GlobalCollections();
        private readonly IDictionary<string, LogicAgregator> logics = new Dictionary<string, LogicAgregator>();
        private readonly IDictionary<string, FigureType> figureTypes = new Dictionary<string, FigureType>();

        private int playersCount = 0;
        private readonly IDictionary<string, int> playerNames = new Dictionary<string, int>();

        public GameXmlParser(TextReader rulesXmlReader) {
            doc = XDocument.Load(rulesXmlReader);
            if (doc == null) {
                throw new FileLoadException("Could not load xml rules.");
            }

            propertiesMapping = new PropertiesMapping();
        }

        public GameContext Parse() {
            GameField field = ParseField();
            ParseLogicsNames();
            ParseFigureTypes();
            IList<GamePlayer> players = ParsePlayers();
            GameRules gameRules = ParseRules();

            GameContext gameContext = new GameContext(field, players, gameRules);
            ParseLogics(gameContext);
            InitPropertiesForAllGameEntities(gameContext);
            return gameContext;
        }

        private void InitPropertiesForAllGameEntities(GameContext context) {
            for (int i = 0; i < context.Field.Size; ++i) {
                for (int j = 0; j < context.Field.Size; ++j) {
                    context.Field.GetCell(i, j).InitPropertiesForType(propertiesMapping.GetCellPropsCount());
                }
            }
            context.Field.InitPropertiesForType(propertiesMapping.GetFieldPropsCount());
            foreach (var player in context.Players) {
                player.InitPropertiesForType(propertiesMapping.GetPlayerPropsCount());
                player.SetProperty(propertiesMapping.GetPlayerPropertyIndex("Name"), player.Name);
                foreach (var playerFigure in player.PlayerFigures) {
                    playerFigure.InitPropertiesForType(propertiesMapping.GetFigurePropsCount());
                }
            }
            
        }

        private void ParseLogics(GameContext context) {
            var logicsNode = doc.Root.Element(LogicsNodeName);
            new LogicsParser(logicsNode, logics, propertiesMapping, globalCollections, context).ParseLogics();
        }

        private GameRules ParseRules() {
            var rulesNode = doc.Root.Element(RulesNodeName);
            var nextMoveNode = rulesNode.Element(NextMoveEventNodeName);
            var endGameNode = rulesNode.Element(EndGameNodeName);
            var initFieldNode = rulesNode.Element(InitFieldNodeName);

            IDictionary<int, int> playersToWinProperties = new Dictionary<int, int>();
            foreach (var winConditionNode in endGameNode.Elements(PlayerWinConditionNodeName)) {
                string playerName = winConditionNode.Attribute(NameAttributeName).Value;
                string propertyName = winConditionNode.Attribute(FieldPropertyNodeName).Value;
                playersToWinProperties[GetPlayerName(playerName)] = propertiesMapping.GetFieldPropertyIndex(propertyName);
            }
            ILogic nextMoveEvent = logics[nextMoveNode.Attribute(NameAttributeName).Value];
            ILogic initFieldLogic = logics[initFieldNode.Attribute(NameAttributeName).Value];
            return new GameRules(initFieldLogic, nextMoveEvent, playersToWinProperties);
        }

        private void ParseFigureTypes() {
            foreach (var figureNode in doc.Root.Element(FiguresNodeName).Elements(FigureNodeName)) {
                string figureTypeName = figureNode.Attribute(NameAttributeName).Value;
                string possibleMoves = figureNode.Attribute(PossibleMovesAttributeName).Value;
                string moveAction = figureNode.Attribute(MoveActionAttributeName).Value;
                LogicAgregator moveActionLogic = logics[moveAction];
                LogicAgregator possibleMovesLogic = logics[possibleMoves];
                possibleMovesLogic.AddPostLogic(new CellsCopier(globalCollections.LocalBuffer, globalCollections.CurrFigurePossibleMoves));
                moveActionLogic.AddPreLogic(new CellsCopier(globalCollections.CurrMoveCells, globalCollections.LocalBuffer));
                figureTypes[figureTypeName] = new FigureType(possibleMovesLogic, moveActionLogic);
            }
            
        }

        private IList<GamePlayer> ParsePlayers() {
            var playersNode = doc.Root.Element(PlayersNodeName);
            IList<GamePlayer> players = new List<GamePlayer>();
            foreach (var playerNode in playersNode.Elements(PlayerNodeName)) {
                IList<GameFigure> playerFigures = ParsePlayerFigures(playerNode);
                string value = playerNode.Attribute(NameAttributeName).Value;
                GamePlayer gamePlayer = new GamePlayer(GetPlayerName(value), playerFigures);
                players.Add(gamePlayer);
            }

            return players;
        }

        private IList<GameFigure> ParsePlayerFigures(XElement playerNode) {
            IList<GameFigure> playerFigures = new List<GameFigure>();
            foreach (var playerFigureNode in playerNode.Elements(PlayerFigureNodeName)) {
                string type = playerFigureNode.Attribute(TypeAttributeName).Value;
                playerFigures.Add(new GameFigure(figureTypes[type]));
            }
            return playerFigures;
        }

        private void ParseLogicsNames() {
            var logicsNode = doc.Root.Element(LogicsNodeName);
            foreach (var logicName in logicsNode.Elements(LogicNodeName)
                                                .Select(node => node.Attribute(NameAttributeName).Value)) {
                logics[logicName] = new LogicAgregator();
            }
        }

        private GameField ParseField() {
            var fieldElement = doc.Root.Element(FieldNodeName);
            string sizeString = fieldElement.Attribute(SizeAttributeName).Value;

//            var properties = fieldElement.Element(PropertiesNodeName);
//            if (properties != null) {
//                foreach (var propertyElement in properties.Elements()) {
//                    propertiesMapping. propertyElement.Name
//                }
//            }
            return new GameField(Convert.ToInt32(sizeString));
        }

        private int GetPlayerName(string playerName) {
            int player;
            if (playerNames.TryGetValue(playerName, out player)) {
                return player;
            }
            playerNames[playerName] = playersCount;
            return playersCount++;
        }

        internal class GlobalCollections {
            public readonly CellsCollectionHolder LocalBuffer = new CellsCollectionHolder();
            public readonly CellsCollectionHolder LastLogicResult = new CellsCollectionHolder();
            public readonly CellsCollectionHolder CurrFigurePossibleMoves = new CellsCollectionHolder();
            public readonly CellsCollectionHolder CurrMoveCells = new CellsCollectionHolder();
        }

        /*
            <Game>
                <Field>
                <Logics>
            </Game>

            <Field Size="INT"/>

            <Logics>
                <Logic> +
            </Logics>

            <Logic>
                ( <AddCells> | <SetProperty> | <Do> | <ApplyPattern> | <If> ) *
            </Logic>

            <AddCells>
                <WithProperty> *
            </AddCells>

            <WithProperty Name="STRING" Value="STRING"/>
        */
    }
}