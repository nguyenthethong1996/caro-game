using System;
using System.Collections.Generic;
using System.Text;

namespace CaroV1
{
    class Controllor
    {
        public Controllor()
        {

        }

        private int idTurn;
        private bool isOnePlayer;
        private bool isWin;
        private int numMove;
        private int moveFirst;

        public int IdTurn
        {
            get
            {
                return idTurn;
            }

            set
            {
                idTurn = value;
            }
        }

        public bool IsOnePlayer
        {
            get
            {
                return isOnePlayer;
            }

            set
            {
                isOnePlayer = value;
            }
        }

        public bool IsWin
        {
            get
            {
                return isWin;
            }

            set
            {
                isWin = value;
            }
        }

        public int NumMove
        {
            get
            {
                return numMove;
            }

            set
            {
                numMove = value;
            }
        }

        public int MoveFirst
        {
            get
            {
                return moveFirst;
            }

            set
            {
                moveFirst = value;
            }
        }

        public void setPlayFirst(int num)
        {
            IdTurn = num;
        }

        public void changeTurn()
        {
            if (IdTurn == 1) IdTurn = 2;
            else IdTurn = 1;
        }
    }
}
