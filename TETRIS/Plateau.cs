using System.Threading;
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
        static private Brush BlackColor = Brushes.Black;
        private bool gameFin = false;
        #region création de la grille de jeu
        //Nous avons besoin : lignes, colonnes , score, lignes remplies et quelques effets
        public Plateau(Grid TetrisGrid)
        {
            //État actuel des valeurs , les lignes et les colonnes sont à compter,
            //elles ont déjà été déclarés dans le fichier XAML.
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
                    BlockControls[i, j].BorderBrush = BlackColor;
                    BlockControls[i, j].BorderThickness = new Thickness(0.3, 0.3, 0.3, 0.3);
                    Grid.SetRow(BlockControls[i, j], j);
                    Grid.SetColumn(BlockControls[i, j], i);
                    TetrisGrid.Children.Add(BlockControls[i, j]);
                }
            }
            Piece = new Piece();
            pieceDessin();
        }
        #endregion
        #region récupèrer le nombre de lignes et score pour affichage
        public int getScore()
        {
            return Score;
        }
        public int getLignes()
        {
            return LignesRemplies;
        }
        #endregion
        #region Représenter les pièces
        private void pieceDessin()
        {
            Point Position = Piece.getPosition();
            Point[] Forme = Piece.getForme();
            Brush Color = Piece.getCouleur();
            foreach (Point S in Forme)
            {
                BlockControls[(int)(S.X + Position.X) + ((Colonnes / 2) - 1),
                              (int)(S.Y + Position.Y) + 2].Background = Color;
            }
        }
        #endregion
        #region Supression de lignes

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
        //Méthode pour contrôler les lignes
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
                    Score += 100;
                    LignesRemplies += 1;
                    i++;
 
                }
            }
        }
        //Méthode pour supprimer une ligne remplie
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
        #endregion
        #region Mouvements pièces
        //Méthodes de vérification pour décaler les pièces vers Gauche/Droite
        public void PieceMovLeft()
        {
            Point Position = Piece.getPosition();
            Point[] Forme = Piece.getForme();
            bool move = true;
            currJeuSuppr();
            foreach (Point S in Forme)
            {
                //Si on touche le bord du plateau
                if (((int)(S.X + Position.X) + ((Colonnes / 2) - 1) - 1) < 0)
                {
                    move = false;
                }
                //Sinon si on touche une autre pièce
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
            Point[] Forme = Piece.getForme();
            bool move = true;
            currJeuSuppr();
            foreach (Point S in Forme)
            {
                //Si on touche le bord du plateau
                if (((int)(S.X + Position.X) + ((Colonnes / 2) - 1) + 1) >= Colonnes)
                {
                    move = false;
                }
                //Sinon si on touche une autre pièce
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
        //Méthode pour vérifier si le mouvement vers le bas est possible
        public void PieceMovDown()
        {
            Point Position = Piece.getPosition();
            Point[] Forme = Piece.getForme();
            bool move = true;
            currJeuSuppr();
            foreach (Point S in Forme)
            {
                //La pièce est en bas du plateau
                if (((int)(S.Y + Position.Y) + 2 + 1) >= Lignes)
                {
                    move = false;
                }
                //Si pièce en dessous
                else if (BlockControls[((int)(S.X + Position.X) + ((Colonnes / 2) - 1)),
                    (int)(S.Y + Position.Y) + 2 + 1].Background != Nocolor)
                {
                    move = false;
                    //Si la pièce touche une autre au niveau de la première ligne
                    if (S.X + Position.X == 0 && S.Y + Position.Y == -1)
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
        //Méthode de vérification pour tourner la pièce
        public void PieceRotation()
        {
            Point Position = Piece.getPosition();
            Point[] S = new Point[4];
            Point[] Forme = Piece.getForme();
            bool move = true;
            Forme.CopyTo(S, 0);
            currJeuSuppr();
            for (int i = 0; i < S.Length; i++)
            {
                double x = S[i].X;
                S[i].X = S[i].Y * -1;
                S[i].Y = x;
                //Si on touche le bas du plateau
                if (((int)((S[i].Y + Position.Y) + 2)) >= Lignes)
                {
                    move = false;
                }
                //Si la pièce dépasse du plateau sur la gauche
                else if (((int)(S[i].X + Position.X) + ((Colonnes / 2) - 1)) < 0)
                {
                    move = false;
                }
                //Si la pièce dépasse du plateau sur la droite
                else if (((int)(S[i].X + Position.X) + ((Colonnes / 2) - 1)) >= Colonnes)
                {
                    move = false;
                }
                //Si la pièce touche une autre pièce
                else if (BlockControls[((int)(S[i].X + Position.X) + ((Colonnes / 2) - 1)),
                    (int)(S[i].Y + Position.Y) + 2].Background != Nocolor)
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
        #endregion
        public void partiePerdue()
        {
            if (gameFin == false)
            {
                gameFin = true;
                MessageBox.Show("Partie perdue ! \n\nVotre score : " + Score.ToString("000000") +
                            "\nNombre de lignes remplies : " + LignesRemplies.ToString("000000"));
                Application Tetris = Application.Current;
                Tetris.Shutdown();
            }

        }
    }
}