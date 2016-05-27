using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using GameGenLib;
using GameGenLib.GameParser;

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

        private GameContext _xoGameProcessor;

        public int FieldSize;
        private const double GamePieceSizePx = 12;
        public List<XoCell> Cells { get; private set; }
        public double WidthPx { get; private set; }
        public double HeightPx { get; private set; }

        public XoField(int widthPx, int heightPx) {
            
            InitializeGameProcessor();
            bool firstPlayerIsHuman = true;
            bool secondPlayerIsHuman = true;

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

        private void InitializeGameProcessor() {
            _xoGameProcessor = new GameXmlParser(File.OpenText("../../RulesXOEnhanced.xml")).Parse();
            _xoGameProcessor.StartGame();
            FieldSize = _xoGameProcessor.Field.Size;
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

        private void DoMove(int index) {
            if (_isGameEnd)
                return;

            Cells[index].IsEmpty = false;
            Cells[index].Player = _player;

            if (_xoGameProcessor != null) {
                var size = _xoGameProcessor.Field.Size;
                _xoGameProcessor.SelectPossibleMove(index % size, index / size);
            }
            CheckForGameEnd();
            _player = Player.X == _player ? Player.O : Player.X;
        }

        private void CheckForGameEnd() {
            if (_xoGameProcessor.GetEndOfGameStatus() > -1) {
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
