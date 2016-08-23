using System;
using System.Collections.Generic;
using System.Text;


namespace CaroV1
{
    class Cell
    {
        private int id;
        private int x;
        private int y;

        public Cell()
        {
            Id = X = Y = 0;
        }

        public Cell(int id1, int x1, int y1)
        {
            Id = id1;
            X = x1;
            Y = y1;
        }

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public string ParseID()
        {
            if (id == 1) return "X"; else return "O";
        }
    }
}

