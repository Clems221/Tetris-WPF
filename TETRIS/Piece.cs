﻿using System;
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
        private Point[] currForme; //on récupère les valeurs X et Y de notre plateau
        private Brush Couleur;
        private bool rotate;
        public Piece()
        {
            currPosition = new Point(0, 0);
            Couleur = Brushes.Transparent;
            currForme = setRandomFormes();
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
            return currForme;
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
                for (int i = 0; i < currForme.Length; i++)
                {
                    double x = currForme[i].X;
                    currForme[i].X = currForme[i].Y * -1;
                    currForme[i].Y = x;
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
                    Couleur = Brushes.HotPink;
                    return new Point[] {
                        new Point(0,0),
                        new Point(-1,0),
                        new Point(1,0),
                        new Point(1,-1)
                    };
                case 3: // Carré
                    rotate = false;
                    Couleur = Brushes.Gold;
                    return new Point[] {
                        new Point(0,0),
                        new Point(0,-1),
                        new Point(1,0),
                        new Point(1,-1)
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
                        new Point(0,-1),
                        new Point(1,0)
                    };
                default:
                    return null;

            }
        }

    }
}
