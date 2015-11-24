using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TETRIS
{

    public class Plateau
    {
        private int Rows;
        private int Cols;
        private int Score;
        private int LignesRemplies;
        private Piece currJeu;
        private Label[,] BlockControls;

        static private Brush Nocolor = Brushes.Transparent;
        static private Brush GrayColor = Brushes.Gray;


        public Plateau(Grid TetrisGrid)
        {
            Rows = TetrisGrid.RowDefinitions.Count;
            Cols = TetrisGrid.ColumnDefinitions.Count;
            Score = 0;
            LignesRemplies = 0;

            BlockControls = new Label[Cols, Rows];
            for (int i = 0; i < Cols; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    BlockControls[i, j] = new Label();
                    BlockControls[i, j].Background = Nocolor;
                    BlockControls[i, j].BorderBrush =GrayColor;
                    BlockControls[i, j].BorderThickness = new Thickness(1, 1, 1, 1);
                    Grid.SetRow(BlockControls[i, j], j);
                    Grid.SetColumn(BlockControls[i, j], i);
                    TetrisGrid.Children.Add(BlockControls[i, j]);
                }
            }
            currJeu = new Piece();
            currJeuDessin();
        }
        public int getScore()
        {
            return Score;
        }
        public int getLignes()
        {
            return LignesRemplies;
        }
        private void currJeuDessin()
        {
            Point Position = currJeu.getCurrPosition();
            Point[] Shape = currJeu.getCurrShape();
            Brush Color = currJeu.getCurrColor();
            foreach (Point S in Shape)
            {
                BlockControls[(int)(S.X + Position.X) + ((Cols / 2) - 1),
                    (int)(S.Y + Position.Y) + 2].Background = Color;
            }
        }
        private void currJeuSuppr()
        {
            Point Position = currJeu.getCurrPosition();
            Point[] Shape = currJeu.getCurrShape();
            foreach (Point S in Shape)
            {
                BlockControls[(int)(S.X + Position.X) + ((Cols / 2) - 1),
                    (int)(S.Y + Position.Y) + 2].Background = Nocolor;
            }
        }
        private void CheckRows()
        {
            bool rempli;
            for (int i = Rows - 1; i > 0; i--)
            {
                rempli = true;
                for (int j = 0; j < Cols; j++)
                {
                    if (BlockControls[j, i].Background == Nocolor)
                    {
                        rempli = false;
                    }
                }

                if (rempli)
                {
                    SupprRow(i);
                    Score += 120;
                    LignesRemplies += 1;
                }
            }
        }
        private void SupprRow(int row)
        {
            for (int i = row; i > 2; i--)
            {
                for (int j = 0; j < Cols; j++)
                {
                    BlockControls[j, i].Background = BlockControls[j, i - 1].Background;
                }
            }
        }
        public void CurrJeuMovLeft()
        {
            Point Position = currJeu.getCurrPosition();
            Point[] Shape = currJeu.getCurrShape();
            bool move = true;
            currJeuSuppr();
            foreach (Point S in Shape)
            {
                if (((int)(S.X + Position.X) + ((Cols / 2) - 1) - 1) < 0)
                {
                    move = false;
                }
                else if (BlockControls[((int)(S.X + Position.X) + ((Cols / 2) - 1) - 1),
                    (int)(S.Y + Position.Y) + 2].Background != Nocolor)
                {
                    move = false;
                }
            }
            if (move)
            {
                currJeu.movLeft();
                currJeuDessin();
            }
            else
            {
                currJeuDessin();
            }
        }
        public void CurrJeuMovRight()
        {
            Point Position = currJeu.getCurrPosition();
            Point[] Shape = currJeu.getCurrShape();
            bool move = true;
            currJeuSuppr();
            foreach (Point S in Shape)
            {
                if (((int)(S.X + Position.X) + ((Cols / 2) - 1) + 1) >= Cols)
                {
                    move = false;
                }
                else if (BlockControls[((int)(S.X + Position.X) + ((Cols / 2) - 1) + 1),
                    (int)(S.Y + Position.Y) + 2].Background != Nocolor)
                {
                    move = false;
                }
            }
            if (move)
            {
                currJeu.movRight();
                currJeuDessin();
            }
            else
            {
                currJeuDessin();
            }
        }
        public void CurrJeuMovDown()
        {
            Point Position = currJeu.getCurrPosition();
            Point[] Shape = currJeu.getCurrShape();
            bool move = true;
            currJeuSuppr();
            foreach (Point S in Shape)
            {
                if (((int)(S.Y + Position.Y) + 2 + 1) >= Rows)
                {
                    move = false;
                }
                else if (BlockControls[((int)(S.X + Position.X) + ((Cols / 2) - 1)),
                    (int)(S.Y + Position.Y) + 2 + 1].Background != Nocolor)
                {
                    move = false;
                    if (((int)(S.Y + Position.Y) + 2 + 1) <= 1)
                    {
                        MessageBox.Show("GAME OVER\n\nSCORE:" + Score.ToString("   000000") +
                            "\nLIGNES:" + LignesRemplies.ToString("   000000"));
                        Application Tetris = Application.Current;
                        Tetris.Shutdown();
                    }
                }
            }
            if (move)
            {
                currJeu.movDown();
                currJeuDessin();
            }
            else
            {
                currJeuDessin();
                CheckRows();
                currJeu = new Piece();
            }
        }
        public void CurrJeuMovRotate()
        {
            Point Position = currJeu.getCurrPosition();
            Point[] S = new Point[4];
            Point[] Shape = currJeu.getCurrShape();
            bool move = true;
            Shape.CopyTo(S, 0);
            currJeuSuppr();
            for (int i = 0; i < S.Length; i++)
            {
                double x = S[i].X;
                S[i].X = S[i].Y * -1;
                S[i].Y = x;
                if (((int)((S[i].Y + Position.Y) + 2)) >= Rows)
                {
                    move = false;
                }
                else if (((int)(S[i].X + Position.X) + ((Cols / 2) - 1)) < 0)
                {
                    move = false;
                }
                else if (((int)(S[i].X + Position.X) + ((Cols / 2) - 1)) >= Rows)
                {
                    move = false;
                }
                else if (BlockControls[((int)(S[i].X + Position.X) + ((Cols / 2) - 1)),
                    (int)(S[i].Y + Position.Y) + 2].Background != Nocolor)
                {
                    move = false;
                }
            }
            if (move)
            {
                currJeu.movRotate();
                currJeuDessin();
            }
            else
            {
                currJeuDessin();
            }
        }

    }
}

