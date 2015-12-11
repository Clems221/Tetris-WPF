using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace TETRIS
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer Timer;
        Plateau monPlateau;
        public MainWindow()
        {
            InitializeComponent();
        }
        void MainWindow_Initialized(object sender, EventArgs e)
        {
            Timer = new DispatcherTimer();
            Timer.Tick += new EventHandler(GameLoaded);
            Timer.Interval = new TimeSpan(0, 0, 0, 0, 600);
            GameStart();
        }

        //Propriétés du jeu
        private void GameStart()
        {
            PlateauTetris.Children.Clear();
            monPlateau = new Plateau(PlateauTetris);

            Timer.Start();
        }
        private void GameLoaded(object sender, EventArgs e)
        {
            Score.Content = monPlateau.getScore().ToString("000000");
            Lignes.Content = monPlateau.getLignes().ToString("000000");
            monPlateau.PieceMovDown();
        }
        private void GamePause()
        {
            if (Timer.IsEnabled)
            {
                Timer.Stop();
            }
            else
            {
                Timer.Start();
            }
        }

        //Interaction clavier utilisateur
        private void HandleKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    if (Timer.IsEnabled) monPlateau.PieceMovLeft();
                    break;
                case Key.Right:
                    if (Timer.IsEnabled) monPlateau.PieceMovRight();
                    break;
                case Key.Down:
                    if (Timer.IsEnabled) monPlateau.PieceMovDown();
                    break;
                case Key.Up:
                    if (Timer.IsEnabled) monPlateau.PieceRotation();
                    break;
                case Key.F2:
                    GameStart();
                    break;
                case Key.F3:
                    GamePause();
                    break;
                case Key.Escape:
                    Application Tetris = Application.Current;
                    Tetris.Shutdown();
                    break;
                default:
                    break;
            }
        }
        private void PlaySound()
        {
            var uri = new Uri("TETRIS.mp3");
            var player = new MediaPlayer();
            player.Open(uri);
            player.Play();
        }
    }
}
