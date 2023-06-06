using Microsoft.TeamFoundation.Work.WebApi;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋.Properties
{
    class layout
    {
        private static readonly Point NoPoint = new Point(-1, -1);  //代表錯誤位子
        private static readonly int border = 39;    //棋盤邊界長度
        private static readonly int distance = 25;  //兩個交叉點間距
        private static readonly int range = 6;      //可點擊範圍

        private Piece[,] pieces = new Piece[19, 19]; //棋子座標
        public bool CorrectPoint(int x, int y)        //確認游標位子是否在正確範圍
        {
            Point NowPoint = FindCorrectPlace(x, y);

            if (NowPoint == NoPoint)
                return false;
            if (pieces[NowPoint.X, NowPoint.Y] != null)
                return false;
            return true;
        }

        public Piece Put(int x, int y, Piecetype type)
        {
            Point NowPoint = FindCorrectPlace(x, y);

            if (NowPoint == NoPoint)
                return null;
            if (pieces[NowPoint.X, NowPoint.Y] != null)
                return null;

            Point nodepoint = node(NowPoint);                    //如果偵測都通過回傳最接近游標的放棋點
            if (type == Piecetype.Black)
                pieces[NowPoint.X, NowPoint.Y] = new BlackPiece(nodepoint.X, nodepoint.Y);
            else if (type == Piecetype.White)
                pieces[NowPoint.X, NowPoint.Y] = new WhitePiece(nodepoint.X, nodepoint.Y);

            Checkwinner(NowPoint.X, NowPoint.Y);

            return pieces[NowPoint.X, NowPoint.Y];
        }
        public void Checkwinner(int x, int y)
        {
            Type type = pieces[x,y].GetType();
            for (int j = 0; j < 19; j++)
            {
                for (int i = 0; i < 15; i++)
                {
                    var flag = true;
                    for (int k = 0; k < 5; k++)
                    {
                        if (pieces[i + k, j] == null || pieces[i + k, j].GetType() != type)
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                        System.Windows.Forms.MessageBox.Show(type + "  win");
                }
            }
            for (int j = 0; j < 19; j++)
            {
                for (int i = 0; i < 15; i++)
                {
                    var flag = true;
                    for (int k = 0; k < 5; k++)
                    {
                        if (pieces[j, i + k] == null || pieces[j, i + k].GetType() != type)
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                        System.Windows.Forms.MessageBox.Show(type + "  win");
                }
            }
            for (int j = 4; j < 19; j++)
            {
                for (int i = 0; i < 15; i++)
                {
                    var flag = true;
                    for (int k = 0; k < 5; k++)
                    {
                        if (pieces[j - k, i + k] == null || pieces[j - k, i + k].GetType() != type)
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                        System.Windows.Forms.MessageBox.Show(type + "  win");
                }
            }
            for (int j = 15; j >= 0; j--)
            {
                for (int i = 15; i >= 0; i--)
                {
                    var flag = true;
                    for (int k = 0; k < 5; k++)
                    {
                        if (pieces[j + k, i + k] == null || pieces[j + k, i + k].GetType() != type)
                        {
                            flag = false;
                            break;
                        }
                    }
                    if (flag)
                        System.Windows.Forms.MessageBox.Show(type + "  win");
                }
            }
        }

        private Point node(Point NowPoint)      //計算出離點擊範圍最近的節點的實際座標
        {
            Point nodepoint = new Point();
            nodepoint.X = NowPoint.X * distance + border;
            nodepoint.Y = NowPoint.Y * distance + border;
            return nodepoint;
        }

        private Point FindCorrectPlace(int x, int y)   //如果兩軸都在範圍內回傳座標 
        {
            int PointX = FindCorrectPlace(x);
            if (PointX == -1)
                return NoPoint;

            int PointY = FindCorrectPlace(y);
            if (PointY == -1)
                return NoPoint;
            return new Point(PointX, PointY);
        }
        private int FindCorrectPlace(int pos)     //計算出是否在可以點擊的範圍
        {
            if (pos < border - distance)
                return -1;

            pos -= border;

            int quotient = pos / distance;
            int remainder = pos % distance;

            if (remainder <= range)
                return quotient;
            else if (remainder >= distance - range)
                return quotient + 1;
            else
                return -1;
        }
    }
}
