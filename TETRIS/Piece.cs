using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace TETRIS
{
    public class Piece
    {
        private Point currPosition;
        private Point[] currShape;
        private Brush Couleur;
        private bool rotate;
        public Piece()
        {
            currPosition = new Point(0, 0);
            Couleur = Brushes.Transparent;
            currShape = setRandomFormes();

        }
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
            return currShape;
        }
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
                for (int i = 0; i < currShape.Length; i++)
                {
                    double x = currShape[i].X;
                    currShape[i].X = currShape[i].Y * -1;
                    currShape[i].Y = x;
                }
            }
        }
        private Point[] setRandomFormes()
        {
            Random rand = new Random();
            switch (rand.Next() % 7)
            {
                case 0: // I
                    rotate = true;
                    Couleur = Brushes.Cyan;
                    return new Point[] {
                        new Point(0,0),
                        new Point(-1,0),
                        new Point(1,0),
                        new Point(2,0),
                    };
                case 1: // J
                    rotate = true;
                    Couleur = Brushes.Blue;
                    return new Point[] {
                        new Point(1,-1),
                        new Point(-1,0),
                        new Point(0,0),
                        new Point(1,0),
                    };
                case 2: // L
                    rotate = true;
                    Couleur = Brushes.Orange;
                    return new Point[] {
                        new Point(0,0),
                        new Point(-1,0),
                        new Point(1,0),
                        new Point(1,-1),
                    };
                case 3: // Carré
                    rotate = false;
                    Couleur = Brushes.Yellow;
                    return new Point[] {
                        new Point(0,0),
                        new Point(0,1),
                        new Point(1,0),
                        new Point(1,1),
                    };
                case 4: // S
                    rotate = true;
                    Couleur = Brushes.Green;
                    return new Point[] {
                        new Point(0,0),
                        new Point(-1,0),
                        new Point(0,-1),
                        new Point(1,0),
                    };
                case 5: // T
                    rotate = true;
                    Couleur = Brushes.Purple;
                    return new Point[] {
                        new Point(0,0),
                        new Point(-1,0),
                        new Point(0,1),
                        new Point(1,1),
                    };
                case 6: // Z
                    rotate = true;
                    Couleur = Brushes.Red;
                    return new Point[] {
                        new Point(0,0),
                        new Point(-1,0),
                        new Point(0,1),
                        new Point(1,1),
                    };
                default:
                    return null;

            }
        }

    }
}
