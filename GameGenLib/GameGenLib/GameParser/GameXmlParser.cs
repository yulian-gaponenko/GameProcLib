﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using GameGenLib.GameEntities;
using GameGenLib.Logics;

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

        private readonly XDocument doc;
        private readonly PropertiesMapping propertiesMapping;
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
            return gameContext;
        }

        private void ParseLogics(GameContext context) {
            var logicsNode = doc.Root.Element(LogicsNodeName);
            new LogicsParser(logicsNode, logics, propertiesMapping, context).ParseLogics();
        }

        private GameRules ParseRules() {
            var rulesNode = doc.Root.Element(RulesNodeName);
            var nextMoveNode = rulesNode.Element(NextMoveEventNodeName);
            var endGameNode = rulesNode.Element(EndGameNodeName);

            IDictionary<int, int> playersToWinProperties = new Dictionary<int, int>();
            foreach (var winConditionNode in endGameNode.Elements(PlayerWinConditionNodeName)) {
                string playerName = winConditionNode.Attribute(NameAttributeName).Value;
                string propertyName = winConditionNode.Attribute(FieldPropertyNodeName).Value;
                playersToWinProperties[GetPlayerName(playerName)] = propertiesMapping.GetFieldPropertyIndex(propertyName);
            }
            ILogic nextMoveEvent = logics[nextMoveNode.Attribute(NameAttributeName).Value];
            return new GameRules(nextMoveEvent, playersToWinProperties);
        }

        private void ParseFigureTypes() {
            foreach (var figureNode in doc.Root.Element(FiguresNodeName).Elements(FigureNodeName)) {
                string figureTypeName = figureNode.Attribute(NameAttributeName).Value;
                string possibleMoves = figureNode.Attribute(PossibleMovesAttributeName).Value;
                string moveAction = figureNode.Attribute(MoveActionAttributeName).Value;
                figureTypes[figureTypeName] = new FigureType(logics[possibleMoves], logics[moveAction]);
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