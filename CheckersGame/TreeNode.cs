using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CheckersGame
{

    public class TreeNode
    {
        private Play _value;
        private List<TreeNode> _children = new List<TreeNode>();

        public TreeNode(Play value)
        {
            this._value = value;
        }

        public TreeNode this[int i]
        {
            get { return _children[i]; }
        }

        public TreeNode Minimax(bool maxLevel = true)
        {
            if (this.Children.Count == 0)
            {
                return this;
            }

            var ordered = this.Children.OrderBy(node => node.ScoreAlphaBeta(maxLevel));

            if (maxLevel)
            {
                return ordered.Last();
            }
            else
            {
                return ordered.First();
            }
        }

        public int ScoreAlphaBeta(bool maxLevel, int alpha = int.MinValue, int beta = int.MaxValue)
        {
            if (this.Children.Count == 0)
            {
                return this.Value.boardAfterMove.CurrentScore();
            }

            if (maxLevel)
            {
                foreach (var child in this.Children)
                {
                    alpha = Math.Max(alpha, child.ScoreAlphaBeta(false, alpha, beta));
                    if (alpha >= beta)
                    {
                        return alpha;
                    }
                }
                return alpha;
            }
            else
            {
                foreach (var child in this.Children)
                {
                    beta = Math.Min(beta, child.ScoreAlphaBeta(false, alpha, beta));
                    if (alpha >= beta)
                    {
                        return beta;
                    }
                }

                return beta;
            }

        }

        public int Score(bool maxLevel)
        {
            if (this.Children.Count == 0)
            {
                return this.Value.boardAfterMove.CurrentScore();
            }

            var ordered = this.Children.OrderBy(node => node.Value.boardAfterMove.CurrentScore());

            if (maxLevel)
            {
                return ordered.Last().Score(false);
            }
            else
            {
                return ordered.First().Score(true);
            }
        }

        public bool IsRoot()
        {
            return this.Parent == null;
        }

        public TreeNode Parent { get; private set; }

        public TreeNode Root()
        {
            var node = this;
            while (node.Parent != null)
            {
                node = node.Parent;
            }
            return node;
        }

        public Play Value { get { return _value; } }

        public ReadOnlyCollection<TreeNode> Children
        {
            get { return _children.AsReadOnly(); }
        }

        public TreeNode AddChild(Play value)
        {
            var node = new TreeNode(value) { Parent = this };
            _children.Add(node);
            return node;
        }

        public TreeNode[] AddChildren(params Play[] values)
        {
            return values.Select(AddChild).ToArray();
        }

        public bool RemoveChild(TreeNode node)
        {
            return _children.Remove(node);
        }

        public void Traverse(Action<Play> action)
        {
            action(Value);
            foreach (var child in Children)
                child.Traverse(action);
        }

        public void Print()
        {
            Console.WriteLine($"{Value} Expected Score = {this.Score(true)}");
            foreach (var child in Children)
                child.Print();
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public IEnumerable<Play> Flatten()
        {
            return new[] { Value }.Concat(_children.SelectMany(x => x.Flatten()));
        }
    }
}
