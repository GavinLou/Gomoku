using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using 五子棋.Properties;

namespace 五子棋
{
    public partial class Form1 : Form
    {
        private layout board= new layout();      //變得可以運用 layout 程式碼 變數與 函式

        private Piecetype secpiecetype = Piecetype.Black;   
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)    //滑鼠點擊事件函式
        {

            Piece piece = board.Put(e.X, e.Y, secpiecetype);
            if (piece != null)               //如果沒東西
            {
                this.Controls.Add(piece);
                if (secpiecetype == Piecetype.Black)       //黑白棋輪流放
                    secpiecetype = Piecetype.White;
                else if (secpiecetype == Piecetype.White)
                    secpiecetype = Piecetype.Black;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)    //游標的座標
        {
            if (board.CorrectPoint(e.X,e.Y))    
            {
                this.Cursor = Cursors.Hand;     //當true 屬標變成 食指指向 
            }else
            {
                this.Cursor = Cursors.Default;  
            }

        }
    }
}
