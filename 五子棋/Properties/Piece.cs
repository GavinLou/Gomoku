using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 五子棋
{
    abstract class Piece : PictureBox
    {
        private static readonly int Piece_Size = 18;
        public Piece(int x, int y)
        {
            this.BackColor = Color.Transparent;
            this.Location = new Point(x - Piece_Size/2, y - Piece_Size/2);
            this.Size = new Size(18, 18);
        }
    }
}