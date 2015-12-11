using System;
using System.Windows;
using System.Windows.Media;

namespace TETRIS
{
    public class Piece
    {
        private Point currPosition;
        private Point[] currForme; //on récupère les valeurs X et Y de notre plateau
        private Brush Couleur;
        private bool rotate;
        public static Random rand = new Random();

        //Créér une pièce
        public Piece()
        {
            currPosition = new Point(0, -1);
            Couleur = Brushes.Transparent;
            currForme = setRandomFormes();
        }

        #region Récupèrer la couleur, la position et la forme de la pièce
        public Brush getCouleur()
        {
            return Couleur;
        }
        public Point getPosition()
        {
            return currPosition;
        }
        public Point[] getForme()
        {
            return currForme;
        }
        #endregion

        #region Méthodes de mouvement des pièces
        public void movLeft()
        {
            currPosition.X -= 1;
        }
        public void movRight()
        {
            currPosition.X += 1;
        }
        public void movDown()
        {
            currPosition.Y += 1;
        }
        public void movRotate()
        {
            if (rotate)
            {
                for (int i = 0; i < currForme.Length; i++)
                {
                    double x = currForme[i].X;
                    currForme[i].X = currForme[i].Y * -1;
                    currForme[i].Y = x;
                }
            }
        }
        #endregion

        #region Générer les formes de chaque pièces
        private Point[] setRandomFormes()
        {

            switch (Piece.rand.Next(0, 7))
            {
                case 0: // I
                    rotate = true;
                    Couleur = Brushes.Cyan;
                    return new Point[] {
                        new Point(0,-1),
                        new Point(-1,-1),
                        new Point(1,-1),
                        new Point(2,-1)
                    };
                case 1: // J
                    rotate = true;
                    Couleur = Brushes.Blue;
                    return new Point[] {
                        new Point(-1,-1),
                        new Point(-1,0),
                        new Point(0,0),
                        new Point(1,0)
                    };
                case 2: // L
                    rotate = true;
                    Couleur = Brushes.Orange;
                    return new Point[] {
                        new Point(0,0),
                        new Point(-1,0),
                        new Point(1,0),
                        new Point(1,-1)
                    };
                case 3: // Carré
                    rotate = false;
                    Couleur = Brushes.Yellow;
                    return new Point[] {
                        new Point(0,0),
                        new Point(0,-1),
                        new Point(1,0),
                        new Point(1,-1)
                    };
                case 4: // T
                    rotate = true;
                    Couleur = Brushes.Purple;
                    return new Point[] {
                        new Point(0,0),
                        new Point(-1,0),
                        new Point(0,-1),
                        new Point(1,0),
                    };
                case 5: // T
                    rotate = true;
                    Couleur = Brushes.Red;
                    return new Point[] {
                        new Point(0,0),
                        new Point(-1,0),
                        new Point(0,1),
                        new Point(1,1),
                    };
                case 6: // Z
                    rotate = true;
                    Couleur = Brushes.Green;
                    return new Point[] {
                   new Point(0,0),
                        new Point(1,0),
                        new Point(0,1),
                        new Point(-1,
                        1),
                    };
                default:
                    return null;

            }
        }
        #endregion

    }
}
