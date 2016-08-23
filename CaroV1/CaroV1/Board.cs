using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CaroV1
{
    class Board
    {

        int h = 21;
        Cell[,] board = new Cell[22,22];

        public Board()
        {
            int i, j;

            //khoi tao bien 
            i = 0;
            for (j = 0; j <= h; j++)
            {
                board[i, j] = new Cell();
                board[i, j].X = board[i, j].Id = - 1;
            }
            i = h;
            for (j = 0; j <= h; j++)
            {
                board[i, j] = new Cell();
                board[i, j].X = board[i, j].Id = -1;
            }
            j = 0;
            for (i = 0; i <= h; i++)
            {
                board[i, j] = new Cell();
                board[i, j].X = board[i, j].Id = -1;
            }
            j = h;
            for (i = 0; i <= h; i++)
            {
                board[i, j] = new Cell();
                board[i, j].X = board[i, j].Id = -1;
            }

            //khoi tao ban co
            for (i = 1; i < h; i++)
                for (j = 1; j < h; j++)
                {
                    board[i, j] = new Cell();
                    board[i, j].X = 0;
                    board[i, j].Y = 0;
                    board[i, j].Id = 0;

                }
        }

        public bool isLocked(Cell p)
        {       

            if (board[p.X,p.Y].Id != 0) return true;
            else return false;
        }

        public void makeMove(Cell p)
        {
            board[p.X, p.Y].X = p.X;
            board[p.X, p.Y].Y = p.Y;
            board[p.X, p.Y].Id = p.Id;

         /*   Console.WriteLine("pX " + p.X.ToString());
            Console.WriteLine("pY " + p.Y.ToString());
            Console.WriteLine("pId " + p.Id.ToString());
            for (int i = 1; i <= 20; i++)
            {
                for (int j = 1; j <= 20; j++) Console.Write(board[i, j].Id);
                Console.WriteLine();
            }
            Console.WriteLine(); */
        }

        public void unMove(Cell p)
        {
            board[p.X, p.Y].X = 0;
            board[p.X, p.Y].Y = 0;
            board[p.X, p.Y].Id = 0;
        }

        public List<Cell> checkDoc(int a, int b)
        {

            int isBlocked = 0;
            List<Cell> res = new List<Cell>();
            Cell cell = new Cell(board[a, b].Id, a, b);
            res.Add(cell);

            int c = a + 1;
            int count = 1;
            while (c <= a + 4 && board[c, b].X != -1 && board[a, b].Id == board[c, b].Id)
            {
                cell = new Cell(board[a, b].Id, c, b);
                res.Add(cell);
                count++;
                c++;
            }
            if (board[c, b].Id != 0) isBlocked++;

            c = a - 1;
            while (c >= a - 4 && board[c, b].X != -1 && board[a, b].Id == board[c, b].Id)
            {
                cell = new Cell(board[a, b].Id, c, b);
                res.Add(cell);
                count++;
                c--;
            }
            if (board[c, b].Id != 0) isBlocked++;
            cell = new Cell(isBlocked, c, b);
            res.Add(cell);

            return res;
        }

        public List<Cell> checkNgang(int a, int b)
        {
            int isBlocked = 0;
            List<Cell> res = new List<Cell>();
            Cell cell = new Cell(board[a, b].Id, a, b);
            res.Add(cell);

            int c = b + 1;
            int count = 1;
            while (c <= b + 4 && board[a, c].X != -1 && board[a, b].Id == board[a, c].Id)
            {
                cell = new Cell(board[a, b].Id, a, c);
                res.Add(cell);
                count++;
                c++;
            }
            if (board[a, c].Id != 0) isBlocked++;

            c = b - 1;
            while (c >= b - 4 && board[a, c].X != -1 && board[a, b].Id == board[a, c].Id)
            {
                cell = new Cell(board[a, b].Id, a, c);
                res.Add(cell);
                count++;
                c--;
            }
            if (board[a, c].Id != 0) isBlocked++;
            cell = new Cell(isBlocked, c, b);
            res.Add(cell);

            return res;
        }

        public List<Cell> checkCheo1(int a, int b)
        {
            int isBlocked = 0;
            List<Cell> res = new List<Cell>();
            Cell cell = new Cell(board[a, b].Id, a, b);
            res.Add(cell);

            int count = 1;
            int c = a + 1;
            int d = b - 1;
            while (c <= a + 4 && d >= b - 4 && board[c, d].X != -1 && board[a, b].Id == board[c, d].Id)
            {
                cell = new Cell(board[a, b].Id, c, d);
                res.Add(cell);
                count++;
                c++;
                d--;
            }
            if (board[c, d].Id != 0) isBlocked++;

            c = a - 1;
            d = b + 1;
            while (c >= a - 4 && d <= b + 4 && board[c, d].X != -1 && board[a, b].Id == board[c, d].Id)
            {
                cell = new Cell(board[a, b].Id, c, d);
                res.Add(cell);
                count++;
                c--;
                d++;
            }
            if (board[c, d].Id != 0) isBlocked++;
            cell = new Cell(isBlocked, c, b);
            res.Add(cell);

            return res;
        }

        public List<Cell> checkCheo2(int a, int b)
        {
            int isBlocked = 0;
            List<Cell> res = new List<Cell>();
            Cell cell = new Cell(board[a, b].Id, a, b);
            res.Add(cell);

            int count = 1;
            int c = a + 1;
            int d = b + 1;
            while (c <= a + 4 && d <= b + 4 && board[c, d].X != -1 && board[a, b].Id == board[c, d].Id)
            {
                cell = new Cell(board[a, b].Id, c, d);
                res.Add(cell);
                count++;
                c++;
                d++;
            }
            if (board[c, d].Id != 0) isBlocked++;

            c = a - 1;
            d = b - 1;
            while (c >= a - 4 && d >= b - 4 && board[c, d].X != -1 && board[a, b].Id == board[c, d].Id)
            {
                cell = new Cell(board[a, b].Id, c, d);
                res.Add(cell);
                count++;
                c--;
                d--;

            }
            if (board[c, d].Id != 0) isBlocked++;
            cell = new Cell(isBlocked, c, b);
            res.Add(cell);

            return res;

        }
        public List<Cell> checkBoard(int a, int b)
        {

            for (int i = 1; i <= 20; i++)
            {
                for (int j = 1; j <= 20; j++) Console.Write(board[i, j].Id);
                Console.WriteLine();
            }

            List<Cell> res = new List<Cell>();

            res = checkNgang(a, b);      if (res.Count >= 6) return res;
            res = checkDoc(a, b);        if (res.Count >= 6) return res;
            res = checkCheo1(a, b);      if (res.Count >= 6) return res;
            res = checkCheo2(a, b);

            return res;
        }
/*
        public int checkHoa(int a, int b)
        {
            int h = 21, count = 0;
            if (checkBoard(a, b) == 0)
            {
                for (int i = 1; i < h; i++)
                    for (int j = 1; j < h; j++)
                    {
                        if (board[i, j].Id != 0)
                            count++;
                    }
                return (count == (h - 1) * (h - 1) ? 1 : 0;

            }
        }

*/
        public void initBoard()
        {
            int i, j;

            //khoi tao bien 
            i = 0;
            for (j = 0; j <= h; j++)
            {
                board[i, j] = new Cell();
                board[i, j].X = board[i, j].Id = -1;
            }
            i = h;
            for (j = 0; j <= h; j++)
            {
                board[i, j] = new Cell();
                board[i, j].X = board[i, j].Id = -1;
            }
            j = 0;
            for (i = 0; i <= h; i++)
            {
                board[i, j] = new Cell();
                board[i, j].X = board[i, j].Id = -1;
            }
            j = h;
            for (i = 0; i <= h; i++)
            {
                board[i, j] = new Cell();
                board[i, j].X = board[i, j].Id = -1;
            }

            //khoi tao ban co
            for (i = 1; i < h; i++)
                for (j = 1; j < h; j++)
                {
                    board[i, j] = new Cell();
                    board[i, j].X = 0;
                    board[i, j].Y = 0;
                    board[i, j].Id = 0;

                }
        }

    }
}
