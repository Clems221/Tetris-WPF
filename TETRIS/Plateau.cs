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
        private int Lignes;
        private int Colonnes;
        private int Score;
        private int LignesRemplies;
        private Piece Piece;
        private Label[,] BlockControls;

        static private Brush Nocolor = Brushes.Transparent;
        static private Brush GrayColor = Brushes.Gray;


        public Plateau(Grid TetrisGrid)
        {
            Lignes = TetrisGrid.RowDefinitions.Count;
            Colonnes = TetrisGrid.ColumnDefinitions.Count;
            Score = 0;
            LignesRemplies = 0;

            BlockControls = new Label[Colonnes, Lignes];
            for (int i = 0; i < Colonnes; i++)
            {
                for (int j = 0; j < Lignes; j++)
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
            Piece = new Piece();
            pieceDessin();
        }
        public int getScore()
        {
            return Score;
        }
        public int getLignes()
        {
            return LignesRemplies;
        }
        private void pieceDessin()
        {
            Point Position = Piece.getPosition();
            Point[] Shape = Piece.getForme();
            Brush Color = Piece.getCouleur();
            foreach (Point S in Shape)
            {
                BlockControls[(int)(S.X + Position.X) + ((Colonnes / 2) - 1),
                    (int)(S.Y + Position.Y) + 2].Background = Color;
            }
        }
        private void currJeuSuppr()
        {
            Point Position = Piece.getPosition();
            Point[] Forme = Piece.getForme();
            foreach (Point S in Forme)
            {
                BlockControls[(int)(S.X + Position.X) + ((Colonnes / 2) - 1),
                    (int)(S.Y + Position.Y) + 2].Background = Nocolor;
            }
        }
        private void CheckLignes()
        {
            bool rempli;
            for (int i = Lignes - 1; i > 0; i--)
            {
                rempli = true;
                for (int j = 0; j < Colonnes; j++)
                {
                    if (BlockControls[j, i].Background == Nocolor)
                    {
                        rempli = false;
                    }
                }

                if (rempli)
                {
                    SupprLigne(i);
                    Score += 120;
                    LignesRemplies += 1;
                    i++;
                }
            }
        }
        private void SupprLigne(int row)
        {
            for (int i = row; i > 2; i--)
            {
                for (int j = 0; j < Colonnes; j++)
                {
                    BlockControls[j, i].Background = BlockControls[j, i - 1].Background;
                }
            }
        }
        public void PieceMovLeft()
        {
            Point Position = Piece.getPosition();
            Point[] Shape = Piece.getForme();
            bool move = true;
            currJeuSuppr();
            foreach (Point S in Shape)
            {
                if (((int)(S.X + Position.X) + ((Colonnes / 2) - 1) - 1) < 0)
                {
                    move = false;
                }
                else if (BlockControls[((int)(S.X + Position.X) + ((Colonnes / 2) - 1) - 1),
                    (int)(S.Y + Position.Y) + 2].Background != Nocolor)
                {
                    move = false;
                }
            }
            if (move)
            {
                Piece.movLeft();
                pieceDessin();
            }
            else
            {
                pieceDessin();
            }
        }
        public void PieceMovRight()
        {
            Point Position = Piece.getPosition();
            Point[] Shape = Piece.getForme();
            bool move = true;
            currJeuSuppr();
            foreach (Point S in Shape)
            {
                if (((int)(S.X + Position.X) + ((Colonnes / 2) - 1) + 1) >= Colonnes)
                {
                    move = false;
                }
                else if (BlockControls[((int)(S.X + Position.X) + ((Colonnes / 2) - 1) + 1),
                    (int)(S.Y + Position.Y) + 2].Background != Nocolor)
                {
                    move = false;
                }
            }
            if (move)
            {
                Piece.movRight();
                pieceDessin();
            }
            else
            {
                pieceDessin();
            }
        }
        public void PieceMovDown()
        {
            Point Position = Piece.getPosition();
            Point[] Shape = Piece.getForme();
            bool move = true;
            currJeuSuppr();
            foreach (Point S in Shape)
            {
                if (((int)(S.Y + Position.Y) + 2 + 1) >= Lignes)
                {
                    move = false;
                }
                else if (BlockControls[((int)(S.X + Position.X) + ((Colonnes / 2) - 1)),
                    (int)(S.Y + Position.Y) + 2 + 1].Background != Nocolor)
                {
                    move = false;
                    // Ne marche pas !
                    if (S.X + Position.X == 0 && S.Y + Position.Y == 0)
                    {
                        partiePerdue();
                    }
                }
            }
            if (move)
            {
                Piece.movDown();
                pieceDessin();
            }
            else
            {
                pieceDessin();
                CheckLignes();
                Piece = new Piece();
            }
        }
        public void PieceRotation()
        {
            Point Position = Piece.getPosition();
            Point[] S = new Point[4];
            Point[] Shape = Piece.getForme();
            bool move = true;
            Shape.CopyTo(S, 0);
            currJeuSuppr();
            for (int i = 0; i < S.Length; i++)
            {
                double x = S[i].X;
                S[i].X = S[i].Y * -1;
                S[i].Y = x;
                if (((int)((S[i].Y + Position.Y) + 2)) >= Lignes)
                {
                    move = false;
                }
                else if (((int)(S[i].X + Position.X) + ((Colonnes / 2) - 1)) < 0)
                {
                    move = false;
                }
                //problème lors de la rotation au bord droit
                else if (BlockControls[((int)(S[i].X + Position.X) + ((Colonnes / 2) - 1)),
                    (int)(S[i].Y + Position.Y) + 2].Background != Nocolor)
                {
                    move = false;
                }
                else if (((int)(S[i].X + Position.X) + ((Colonnes / 2) - 1)) >= Lignes)
                {
                    move = false;
                }
                
            }
            if (move)
            {
                Piece.movRotate();
                pieceDessin();
            }
            else
            {
                pieceDessin();
            }
        }

        public void partiePerdue ()
        {
            MessageBox.Show("GAME OVER\n\nSCORE:" + Score.ToString("   000000") +
                            "\nLIGNES:" + LignesRemplies.ToString("   000000"));
            Application Tetris = Application.Current;
            Tetris.Shutdown();
        }
    }
}

