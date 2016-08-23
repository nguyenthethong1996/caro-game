using System;
using System.Collections.Generic;
using System.Text;

namespace CaroV1
{
    class Computer
    {
        private int oo = 1000000000;

        public Computer()
        {

        }

        private int calc(List<Cell> res, int mode )
        {
            int num = res.Count - 1;
            int numBlocked = res[res.Count - 1].Id;    //Số con đã chặn đường (1 hoặc 2)

            if (mode == 1)   // Attack
            { 
                if (num >= 5) return oo;
                if (numBlocked == 2) return 0;
                if (num == 4 && numBlocked == 0) return 10000;
                if (num == 4 && numBlocked == 1) return 2000;
                if (num == 3 && numBlocked == 0) return 1800;
                if (num == 3 && numBlocked == 1) return 700;
                if (num == 2 && numBlocked == 0) return 20;
                if (num == 2 && numBlocked == 1) return 15;
                if (num == 1 && numBlocked == 0) return 10;
                if (num == 1 && numBlocked == 1) return 5;
                return 0;
            }
            else   //  Defend
            {
                if (num >= 5) return oo / 2;
                if (numBlocked == 2) return 0;
                if (num == 4 && numBlocked == 0) return 10000 - 1;
                if (num == 4 && numBlocked == 1) return 2000 - 1;
                if (num == 3 && numBlocked == 0) return 1800 - 1;
                if (num == 3 && numBlocked == 1) return 700 - 1;
                if (num == 2 && numBlocked == 0) return 20 - 1;
                if (num == 2 && numBlocked == 1) return 15 - 1;
                if (num == 1 && numBlocked == 0) return 10 - 1;
                if (num == 1 && numBlocked == 1) return 5 - 1;
                return 0;
            }
        }

        private int evaluate(Board b, Cell c, int mode)
        {
            int num = 0;
            List<Cell> res = new List<Cell>();

            res = b.checkNgang(c.X, c.Y);
            num += calc(res,mode);

            res = b.checkDoc(c.X, c.Y);
            num += calc(res, mode);

            res = b.checkCheo1(c.X, c.Y);
            num += calc(res, mode);

            res = b.checkCheo2(c.X, c.Y);
            num += calc(res, mode);

            return num;
        }    

        public Cell findNextMove(Board b)
        {
            List<Cell> res = new List<Cell>();
            Cell bestCell = new Cell();
            int scoreAttack = 0;
            int scoreDefend = 0;
            int totalScore = 0;
            int bestScore = 0;

            for(int i = 1; i <= 20; i++)
            {
                for(int j = 1; j <= 20; j++)                    
                {
                    Cell c = new Cell(2, i, j);
                    if (!b.isLocked(c))
                    {
                        b.makeMove(c);
                        scoreAttack = evaluate(b, c, 1);
                        b.unMove(c);

                        c = new Cell(1, i, j);
                        b.makeMove(c);
                        scoreDefend = evaluate(b, c, 2);
                        b.unMove(c);

                        totalScore = scoreAttack + scoreDefend;
                        if (bestScore < totalScore)
                        {
                            bestScore = totalScore;
                            bestCell = new Cell(2, i, j);
                        }
                    }
                }

            }            
            return bestCell;
        }
    }
}
