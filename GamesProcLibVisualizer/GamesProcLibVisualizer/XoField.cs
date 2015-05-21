using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using XO_GamesProcLib;

namespace GamesProcLibVisualizer {
    enum Player { X, O }

    class XoCell {
        public XoCell() {
            IsEmpty = true;
        }
        public bool IsEmpty { get; set; }
        public Player Player { get; set; }
    }

    class XoField  : IField{
        private double _cellWidthPx;
        private double _cellHeightPx;
        private Player _player;
        private GamesWindow.PlayerType _xPlayerType;
        private GamesWindow.PlayerType _oPlayerType;
        private bool _isGameEnd;

        private XO_GameProcessor _xoGameProcessor;

        /// <summary>
        /// Is used if two ai players have different difficulty
        /// </summary>
        private XO_GameProcessor _xoGameProcessorSecondInstance;

        /// <summary>
        /// Number of cells on field side. Total number of cells is FieldSize * FieldSize.
        /// </summary>
        public const int FieldSize = 36;
        private const double GamePieceSizePx = 12;
        public List<XoCell> Cells { get; private set; }
        public double WidthPx { get; private set; }
        public double HeightPx { get; private set; }

        public XoField(int widthPx, int heightPx, bool firstPlayerIsHuman, bool secondPlayerIsHuman,
                       GamesWindow.AiDifficulty firstAiDifficulty, GamesWindow.AiDifficulty secondAiDifficulty) {
            
            InitializeGameProcessor(firstPlayerIsHuman, secondPlayerIsHuman, firstAiDifficulty, secondAiDifficulty);

            _xPlayerType = firstPlayerIsHuman ? GamesWindow.PlayerType.Human : GamesWindow.PlayerType.Computer;
            _oPlayerType = secondPlayerIsHuman ? GamesWindow.PlayerType.Human : GamesWindow.PlayerType.Computer;
            WidthPx = widthPx;
            HeightPx = heightPx;
            Cells = new List<XoCell>(FieldSize * FieldSize);
            for (int i = 0; i < FieldSize * FieldSize; ++i)
                Cells.Add(new XoCell());

            _cellWidthPx = WidthPx / FieldSize;
            _cellHeightPx = HeightPx / FieldSize;

            _player = Player.X;
        }

        ~XoField() {
            if (_xoGameProcessor!=null)
                _xoGameProcessor.Dispose();
            if (_xoGameProcessorSecondInstance != null) {
                _xoGameProcessorSecondInstance.Dispose();
            }
        }

        private void InitializeGameProcessor(bool firstPlayerIsHuman, bool secondPlayerIsHuman,
                                             GamesWindow.AiDifficulty firstAiDifficulty, GamesWindow.AiDifficulty secondAiDifficulty) {
            if (firstPlayerIsHuman && secondPlayerIsHuman)
                _xoGameProcessor = new XO_GameProcessor(0);

            int difficulty = (int)firstAiDifficulty;
            if (!firstPlayerIsHuman) {
                _xoGameProcessor = new XO_GameProcessor(difficulty);
            }
            difficulty = (int)secondAiDifficulty;
            if (!secondPlayerIsHuman && firstPlayerIsHuman) {
                _xoGameProcessor = new XO_GameProcessor(difficulty);
            }

            if (!firstPlayerIsHuman && !secondPlayerIsHuman && firstAiDifficulty != secondAiDifficulty) {
                _xoGameProcessorSecondInstance = new XO_GameProcessor(difficulty);
            }
        }

        private int[] GetEvaluatedPossibleMoves(XO_GameProcessor processor) {
            int size;
            IntPtr moves = processor.evaluatePossibleMoves(out size);
            int[] result = new int[size];
            Marshal.Copy(moves, result, 0, size);
            processor.freeMemory();
            return result;
        }

        public bool TryDoMove(double xPx, double yPx) {
            int x = (int) (xPx/_cellWidthPx);
            int y = (int) (yPx/_cellHeightPx);
            int index = y*FieldSize + x;
            
            
            if (x < 0 || x >= FieldSize || y < 0 || y >= FieldSize)
                return false;

            if (!Cells[index].IsEmpty) 
                return false;

            DoMove(index);
            return true;
        }

        public bool LetAiPlayersDoMove() {
            if (_player == Player.X && _xPlayerType == GamesWindow.PlayerType.Computer) {
                int[] moves = GetEvaluatedPossibleMoves(_xoGameProcessor);
                DoMove(moves[0]);
                return true;
            }
            if (_player == Player.O && _oPlayerType == GamesWindow.PlayerType.Computer) {
                int[] moves = GetEvaluatedPossibleMoves(_xoGameProcessorSecondInstance ?? _xoGameProcessor);
                DoMove(moves[0]);
                return true;
            }
            return false;
        }

        private void DoMove(int index) {
            if (_isGameEnd)
                return;

            Cells[index].IsEmpty = false;
            Cells[index].Player = _player;

            if (_xoGameProcessor != null) {
                _xoGameProcessor.doMove(index);
            }
            if (_xoGameProcessorSecondInstance != null) {
                _xoGameProcessorSecondInstance.doMove(index);
            }

            CheckForGameEnd();
            _player = Player.X == _player ? Player.O : Player.X;
        }

        private void CheckForGameEnd() {
            if (_xoGameProcessor.isGameEnd()) {
                _isGameEnd = true;
                GameEndAction((Player.X == _player ? "'X'" : "'O'") + " WINS");
            }
        }

        public void Draw(UIElementCollection fieldCanvas) {
            fieldCanvas.Clear();
            for (int i = 0; i < FieldSize + 1; ++i) {
                fieldCanvas.Add(new Line { X1 = 0, Y1 = i * _cellHeightPx, X2 = WidthPx, Y2 = i * _cellHeightPx, Stroke = Brushes.Black, StrokeThickness = 1 });
            }
            for (int i = 0; i < FieldSize + 1; ++i) {
                fieldCanvas.Add(new Line { X1 = i * _cellWidthPx, Y1 = 0, X2 = i * _cellWidthPx, Y2 = HeightPx, Stroke = Brushes.Black, StrokeThickness = 1 });
            }

            for (int i = 0; i < Cells.Count; ++i) {
                if (!Cells[i].IsEmpty) {
                    if (Cells[i].Player == Player.X)    DrawX(i, fieldCanvas);
                    else                                DrawO(i, fieldCanvas);
                }
            }
        }

        public GamesWindow.GameEndEvent GameEndAction { get; set; }

        private void DrawX(int index, UIElementCollection fieldCanvas) {
            double xPx = (index % FieldSize) * _cellWidthPx + _cellWidthPx/2;
            double yPx = (index / FieldSize) * _cellHeightPx + _cellHeightPx/2;
            Line l1 = new Line { X1 = xPx - GamePieceSizePx / 2, Y1 = yPx - GamePieceSizePx / 2, X2 = xPx + GamePieceSizePx / 2, Y2 = yPx + GamePieceSizePx / 2, 
                Stroke = Brushes.Black, StrokeThickness = 1.5 };

            Line l2 = new Line { X1 = xPx - GamePieceSizePx / 2, Y1 = yPx + GamePieceSizePx / 2, X2 = xPx + GamePieceSizePx / 2, Y2 = yPx - GamePieceSizePx / 2,
                Stroke = Brushes.Black, StrokeThickness = 1.5 };

            fieldCanvas.Add(l1);
            fieldCanvas.Add(l2);
        }
         
        private void DrawO(int index, UIElementCollection fieldCanvas) {
            double xPx = (index % FieldSize) * _cellWidthPx + _cellWidthPx / 2;
            double yPx = (index / FieldSize) * _cellHeightPx + _cellHeightPx / 2;

            Ellipse e = new Ellipse { Height = GamePieceSizePx, Width = GamePieceSizePx, Stroke = Brushes.Black, StrokeThickness = 1.5};
            fieldCanvas.Add(e);
            Canvas.SetLeft(e, xPx - GamePieceSizePx / 2);
            Canvas.SetTop(e, yPx - GamePieceSizePx / 2);
        }
    }
}
