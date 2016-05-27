using System.Collections.Generic;
using GameGenLib.GameEntities;
using GameGenLib.Logics;
using GameGenLib.Logics.Cells;

namespace GameGenLib {
    public class GameContext {

        private int prevPlayerIndex = 0;
        private int currentPlayerIndex = 0;
        private int endOfGame = -1;

        public GameContext(GameField field, IList<GamePlayer> players, GameRules gameRules) {
            Field = field;
            Players = players;
            GameRules = gameRules;
            CurrPlayer = new PlayerHolder();
            CurrFigure = new FigureHolder();
        }

        public GameField Field { get; }

        public IList<GamePlayer> Players { get; }
        public GameRules GameRules { get; set; }
        public PlayerHolder CurrPlayer { get; set; }
        public FigureHolder CurrFigure { get; set; }
        public CellsCollectionHolder CurrFigurePossibleMoves { get; set; }
        public CellsCollectionHolder CurrMoveCells { get; set; }


        public void StartGame() {
            GameRules.InitFieldLogic.Execute(null);
            NextMove();
        }

        public void SelectPossibleMove(int x, int y) {
            var move = CurrFigurePossibleMoves.Cells.ToCellsSequences().FindSingleSequenceByEndCell(Field.GetCell(x, y));
            if (move != null) {
                MakeMove(move);
            }
        }

        public int GetEndOfGameStatus() {
            return endOfGame;
        }

        private void NextMove() {
            NextPlayer();
            if (CurrPlayer.Player.PlayerFigures.Count == 1) {
                CurrFigure.Figure = CurrPlayer.Player.PlayerFigures[0];
                CalcPossibleMoves();
            }

        }

        private void CalcPossibleMoves() {
            CurrFigure.Figure.Type.PossibleMovesLogic.Execute(null);
        }

        private void MakeMove(CellsSequences move) {
            CurrMoveCells.Cells = move;
            CurrFigure.Figure.Type.MoveActionLogic.Execute(null);
            GameRules.NextMoveEvent.Execute(null);
            CheckIfEndOfGame();
            NextMove();
        }

        private void CheckIfEndOfGame() {
            int prevPlayerWinProp = Field.GetProperty(GameRules.PlayerWinProperty[prevPlayerIndex]);
            if (prevPlayerWinProp > 0) {
                endOfGame = prevPlayerIndex;
            }
        }

        private void NextPlayer() {
            prevPlayerIndex = currentPlayerIndex;
            CurrPlayer.Player = Players[currentPlayerIndex];
            currentPlayerIndex = (currentPlayerIndex + 1) % Players.Count;
        }
    }
}
