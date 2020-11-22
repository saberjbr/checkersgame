using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CheckersGame
{
    public class Engine
    {
        private readonly int DEFAULT_DEPTH = 6; // 2 Play = 1
        public int Depth { get; }
        public Board StartingBoard { get; set; }

        public Engine(Board board)
        {
            this.Depth = DEFAULT_DEPTH;
            this.StartingBoard = (Board)board.Clone();
        }

        public Play ScoreMoves()
        {
          
            var movesTree = new TreeNode(new Play(0, -1, -1, -1, -1, this.StartingBoard));

            AddToTree(movesTree, 1);

            var root = movesTree.Root();

            return root.Minimax().Value;
        }

        public void AddToTree(TreeNode parent, int depth)
        {
            if (depth > this.Depth)
            {
                return;
            }

            foreach (var movesForPawn in parent.Value.boardAfterMove.NextMoves())
            {
                foreach (var move in movesForPawn.Value)
                {
                    
                    var ply = new Play(depth, movesForPawn.Key.X, movesForPawn.Key.Y, move[0], move[1], new Board(parent.Value.boardAfterMove, movesForPawn.Key, move[0], move[1]));
                    var newChild = parent.AddChild(ply);
                    AddToTree(newChild, depth + 1);
                }
            }
        }


    }
}
