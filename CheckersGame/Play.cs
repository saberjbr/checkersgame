using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersGame
{
    public class Play : IComparable
    {
        public int level;
        public int fromX;
        public int fromY;
        public int toX;
        public int toY;
        public int Score { get; }

        public Board boardAfterMove { get; }

        public Play(int level, int x, int y, int x2, int y2, Board board)
        {
            this.level = level;
            this.fromX = x;
            this.fromY = y;
            this.toX = x2;
            this.toY = y2;
            this.Score = board.CurrentScore();
            this.boardAfterMove = (Board)board.Clone();
        }

        public override string ToString()
        {
            if (this.fromX == -1 && this.fromY == -1)
            {
                return $"Root: {level} Score ({this.boardAfterMove.CurrentScore()})";
            }
            else
            {
                
                return $"From ({this.fromX},{this.fromY}) => TO ({this.toX},{this.toY})";
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Play otherPly = obj as Play;
            if (otherPly != null)
                return this.Score.CompareTo(otherPly.Score);
            else
                throw new ArgumentException("error");
        }
    }
}
