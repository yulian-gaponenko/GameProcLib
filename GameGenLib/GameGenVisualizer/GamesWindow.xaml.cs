using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GamesProcLibVisualizer {
    /// <summary>
    /// Interaction logic for GamesWindow.xaml
    /// </summary>
    public partial class GamesWindow : Window {
        public const int FieldSizePx = 680;
        private IField _field;
        private Timer _timer;
        private Game _game;
        private PlayerType _firstPlayerType;
        private PlayerType _secondPlayerType;
        private AiDifficulty _firstAiDifficulty;
        private AiDifficulty _secondAiDifficulty;
        private GameState _state;
        private Rectangle _blockedScreen;

        enum Game { XsOs, Checkers }
        enum GameState { NotStarted, HumanTurn, AiTurn }
        public enum PlayerType { Human, Computer }
        public enum AiDifficulty { Easy, Normal, Hard }

        public delegate void GameEndEvent(string gameEndMessage);

        //---------------------------WindowInitializing-------------------------------------------
        //----------------------------------------------------------------------------------------

        public GamesWindow() {
            InitializeComponent();

            XoCanvas.Width = FieldSizePx;
            XoCanvas.Height = FieldSizePx;

            _game = Game.XsOs;
            _state = GameState.NotStarted;
            _firstPlayerType = PlayerType.Human;
            _firstAiDifficulty = AiDifficulty.Normal;
            _secondPlayerType = PlayerType.Computer;
            _secondAiDifficulty = AiDifficulty.Normal;

            InitializeField(true, false, _firstAiDifficulty, _secondAiDifficulty);

            _blockedScreen = new Rectangle {
                Height = 680,
                Width=680,
                StrokeThickness= 0,
                Fill = Brushes.LightGray,
                Opacity = 0.6
            };
            Canvas.SetZIndex(_blockedScreen, 99);
        }

        private void InitializeField(bool firstPlayerIsHuman, bool secondPlayerIsHuman, AiDifficulty firstAiDifficulty, AiDifficulty secondAiDifficulty) {
            switch (_game) {
                case Game.XsOs:
                    _field = new XoField(FieldSizePx, FieldSizePx);
                    _field.GameEndAction = message => {
                        if (_timer != null) {
                            _timer.Dispose();
                            _timer = null;
                        }
                        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                            new Action(() => ChangeGameState(GameState.NotStarted)));
                        MessageBox.Show(message);
                    };
                    break;
                case Game.Checkers:
                    break;
            }
        }
        private void ChangeGameState(GameState gameState) {
            _state = gameState;

            _blockedScreen.Visibility = GameState.NotStarted == _state ? Visibility.Visible : Visibility.Hidden;
        }

        protected override void OnRender(DrawingContext dc) {
            if (_field != null)
                _field.Draw(XoCanvas.Children);
            XoCanvas.Children.Add(_blockedScreen);
        }


        //--------------------------------WindowEvents--------------------------------------------
        //----------------------------------------------------------------------------------------

        private void XoCanvas_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            if (_state == GameState.NotStarted || _state == GameState.AiTurn)
                return;

            var position = e.GetPosition(XoCanvas);
            bool changesDone = _field.TryDoMove((int) position.X, (int) position.Y);
            if (changesDone) {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                            new Action(InvalidateVisual));
            }

//            changesDone = _field.LetAiPlayersDoMove();
//            if (changesDone) {
//                InvalidateVisual();
//            }
        }

        private void NewGameButton_OnClick(object sender, RoutedEventArgs e) {
            if (_timer != null) {
                _timer.Dispose();
                _timer = null;
            }

            bool firstIsHuman = _firstPlayerType == PlayerType.Human;
            bool secondIsHuman = _secondPlayerType == PlayerType.Human;
            InitializeField(firstIsHuman, secondIsHuman, _firstAiDifficulty, _secondAiDifficulty);
//            if (!firstIsHuman && !secondIsHuman) {
//                _timer = new Timer(state => 
//                    {
//                        lock(this) {
//                            _field.LetAiPlayersDoMove();
//                            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Background,
//                                new Action(InvalidateVisual));
//                        }
//                    },
//                    null, 0, 1000);
//                ChangeGameState(GameState.AiTurn);

//            } else {
                ChangeGameState(GameState.HumanTurn);
//            }
            InvalidateVisual();
        }

        private void GameComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            switch (GameComboBox.SelectedIndex) {
                case 0:
                    _game = Game.XsOs;
                    break;
                case 1:
                    _game = Game.Checkers;
                    break;
            }
        }

        private void Player1TypeComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            switch (Player1TypeComboBox.SelectedIndex) {
                case 0:
                    _firstPlayerType = PlayerType.Human;
                    break;
                case 1:
                    _firstPlayerType = PlayerType.Computer;
                    _firstAiDifficulty = AiDifficulty.Easy;
                    break;
                case 2:
                    _firstPlayerType = PlayerType.Computer;
                    _firstAiDifficulty = AiDifficulty.Normal;
                    break;
                case 3:
                    _firstPlayerType = PlayerType.Computer;
                    _firstAiDifficulty = AiDifficulty.Hard;
                    break;
            }
        }

        private void Player2TypeComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e) {
            switch (Player2TypeComboBox.SelectedIndex) {
                case 0:
                    _secondPlayerType = PlayerType.Human;
                    break;
                case 1:
                    _secondPlayerType = PlayerType.Computer;
                    _secondAiDifficulty = AiDifficulty.Easy;
                    break;
                case 2:
                    _secondPlayerType = PlayerType.Computer;
                    _secondAiDifficulty = AiDifficulty.Normal;
                    break;
                case 3:
                    _secondPlayerType = PlayerType.Computer;
                    _secondAiDifficulty = AiDifficulty.Hard;
                    break;
            }
        }
    }
}
