using System;
using System.Collections.Generic;
using System.Text;

namespace CaroV1
{
    class Player
    {
        private string name;
        private int numWin;

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public int NumWin
        {
            get
            {
                return numWin;
            }

            set
            {
                numWin = value;
            }
        }

        public Player(string s, int num)
        {
            name = s;
            numWin = num;
        }
    }
}
