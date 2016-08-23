using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CaroV1
{
    public partial class MainGame : Form
    {
        Button[,] button = new Button[21, 21];
        Player p1 = new Player("", 0);
        Player p2 = new Player("", 0);
        Controllor control = new Controllor();
        Board board = new Board();
        Computer com = new Computer();

        //Hàm tạo mảng button
        private void CreateArrayButton()
        {
            int top = 20; //Giá  trị Location Top
            for (int i = 1; i <= 20; i++)
            {
                int Left = 200; //Giá trị Location Left
                for (int j = 1; j <= 20; j++)
                {
                    button[i, j] = new Button();
                    //Thêm cài đặt cho button
                    button[i, j].Name = "";
                    //bt.Text = string.Format("Button [{0},{1}]", i, j);
                    button[i,j].Tag = string.Format("{0},{1},", i, j);
                    button[i, j].Size = new Size(40, 40);
                    button[i, j].Left = Left += 40;
                    button[i, j].Top = top;
                    button[i, j].Enabled = false;
                    button[i, j].Font = new Font("Tahoma", 15, FontStyle.Regular);

                    //Tạo sự kiên cho button và gán tới hàm sử lý sự kiện
                    button[i, j].Click += new EventHandler(Button_Click);
                    this.Controls.Add(button[i, j]);
                }
                top += 40;
            }
        }

        //Lấy tọa độ cột của button bị click
        private int getX(Button b)
        {
            int num = 0;
            string s = b.Tag.ToString();
            for (int i = 0; s[i] != ','; i++) num = num * 10 + (s[i] - '0');
            return num;
        }

        //Lấy tọa độ dòng của button bị click
        private int getY(Button b)
        {
            int num = 0, i = 0;
            string s = b.Tag.ToString();
            for (; s[i] != ','; i++) ;   i++;

            for (; s[i] != ','; i++) num = num * 10 + (s[i] - '0');
            return num;
        }

        private void makeGraphicMove(Cell c)
        {
            if (c.Id == 1) button[c.X, c.Y].ForeColor = Color.Red;
            else button[c.X, c.Y].ForeColor = Color.Blue;

            button[c.X, c.Y].Text = c.ParseID();
        }

        public void makeResult(int ID)
        { 
            if (ID == 1) MessageBox.Show(p1.Name + " thắng !", "AlphaCaro");
            else if (ID == 2)
            {
                if (control.IsOnePlayer) MessageBox.Show("Tao thắng !", "AlphaCaro");
                else MessageBox.Show(p2.Name + " thắng !", "AlphaCaro");
            }
            else MessageBox.Show("Hòa, làm tiếp bàn nữa đi !", "AlphaCaro");
        }

        //Nhận nước đi
        private void Button_Click(object sender, EventArgs e)
        {
          //  MessageBox.Show(control.IsWin.ToString());
            if (control.IsWin) return ;

            List<Cell> res = new List<Cell>();
            Cell c = new Cell(control.IdTurn, getX((Button)sender), getY((Button)sender));
            if (board.isLocked(c)) return;
            if (board.isLocked(c) == false)
            {                
                makeGraphicMove(c);
                board.makeMove(c);
              //  MessageBox.Show(c.X.ToString(),"X");
             //   MessageBox.Show(c.Y.ToString(),"Y");
             //   MessageBox.Show(c.Id.ToString(),"Id");

                res = board.checkBoard(c.X, c.Y);
            //    MessageBox.Show(res.Count.ToString());
                if (res.Count >= 6) 
                {
                    
                    control.IsWin = true;                    

                    //Make a win line 
                    for (int i = 0; i < 5; i++)
                    {
                        Cell tmpCell = res[i];
                        button[tmpCell.X, tmpCell.Y].ForeColor = Color.Green;
                        button[tmpCell.X, tmpCell.Y].Font = new Font("Tahoma", 17, FontStyle.Bold);
                    }
                    makeResult(c.Id);
                    if (c.Id == 1) p1.NumWin += 1; else p2.NumWin += 1;
                    lblNumWin1.Text = "Số trận thắng: " + p1.NumWin;
                    lblNumWin2.Text = "Số trận thắng: " + p2.NumWin;
                }
                control.changeTurn();
                control.NumMove += 1;
            }

            if (control.NumMove == 400)
            {
                makeResult(0);
                control.IsWin = true;
                return ;
            }

            //Computer chơi
            if (control.IsOnePlayer && !control.IsWin) ComputerMove();            
        }

        public void ComputerMove()
        {
            Cell c = new Cell();
            List<Cell> res = new List<Cell>();

            c = com.findNextMove(board);
            makeGraphicMove(c);
            board.makeMove(c);
            res = board.checkBoard(c.X, c.Y);
            if (res.Count >= 6)
            {
                control.IsWin = true;

                //Make a win line 
                for (int i = 0; i < 5; i++)
                {
                    Cell tmpCell = res[i];
                    button[tmpCell.X, tmpCell.Y].ForeColor = Color.Green;
                    button[tmpCell.X, tmpCell.Y].Font = new Font("Tahoma", 17, FontStyle.Bold);

                }
                makeResult(c.Id);
                if (c.Id == 1) p1.NumWin += 1; else p2.NumWin += 1;
                lblNumWin1.Text = "Số trận thắng: " + p1.NumWin;
                lblNumWin2.Text = "Số trận thắng: " + p2.NumWin;
            }
            control.changeTurn();
            control.NumMove += 1;

            if (control.NumMove == 400)
            {
                makeResult(0);
                control.IsWin = true;
                return;
            }
        }

        public void initGame()
        {
            btnRestart.Enabled = true;

            //initGraphicBoard
            for (int i = 1; i <= 20; i++)
                for (int j = 1; j <= 20; j++)
                {
                    button[i, j].Text = "";
                    button[i, j].Enabled = true;
                    button[i, j].Font = new Font("Tahoma", 15, FontStyle.Regular);
                }

            control.IsWin = false;
            board.initBoard();
            control.setPlayFirst(control.MoveFirst);
            
        }

        private void btnOnePlayer_Click(object sender, EventArgs e)
        {
            string promptValue = Prompt.ShowDialog("Nhập tên của người chơi", "AlphaCaro");
            p1.Name = promptValue;
            p1.NumWin = 0;

            p2.Name = "AlphaCaro";
            p2.NumWin = 0;

            lblName1.Text = "Tên người chơi 1:\n\n        " + p1.Name + " (X)";
            lblName2.Text = "Tên người chơi 2:\n\n        " + p2.Name + " (O)";

            lblNumWin1.Text = "Số trận thắng: " + p1.NumWin;
            lblNumWin2.Text = "Số trận thắng: " + p2.NumWin;
            
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn đi trước", "AlphaCaro", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                control.setPlayFirst(1);
                control.MoveFirst = 1;
            }
            else if (dialogResult == DialogResult.No)
            {
                control.setPlayFirst(2);
                control.MoveFirst = 2;
            }
            control.IsOnePlayer = true;

            initGame();
            if (control.IdTurn == 2)
            {
                Cell c = new Cell(2, 10, 10);
                makeGraphicMove(c);
                board.makeMove(c);
                
             
                control.changeTurn();
                control.NumMove += 1;
            }

        }

        private void btnTwoPlayer_Click(object sender, EventArgs e)
        {
            string promptValue = Prompt.ShowDialog("Nhập tên của người chơi 1", "AlphaCaro");
            p1.Name = promptValue;
            p1.NumWin = 0;

            promptValue = Prompt.ShowDialog("Nhập tên của người chơi 2", "AlphaCaro");
            p2.Name = promptValue;
            p2.NumWin = 0;

            lblName1.Text = "Tên người chơi 1:\n\n        " + p1.Name + " (X)";
            lblName2.Text = "Tên người chơi 2:\n\n        " + p2.Name + " (O)";

            lblNumWin1.Text = "Số trận thắng: " + p1.NumWin;
            lblNumWin2.Text = "Số trận thắng: " + p2.NumWin;

            
            control.setPlayFirst(1);
            control.MoveFirst = 1;
            control.IsOnePlayer = false;
            
            initGame();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            initGame();
        }

        public MainGame()
        {
            InitializeComponent();
            CreateArrayButton();                
            
        }

      
    }
}
