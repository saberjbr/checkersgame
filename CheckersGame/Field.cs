using System;
using System.Collections.Generic;
using System.Text;

namespace CheckersGame
{
    public class Field
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Field(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Field(int[] pair)
        {
            this.X = pair[0];
            this.Y = pair[1];
        }

        public override string ToString()
        {
            return $"({this.X},{this.Y})";
        }

        public FieldColor Color()
        {
            return ((this.X % 2) != (this.Y % 2)) ? FieldColor.White : FieldColor.Black;
        }


        override public bool Equals(object other)
        {
            var otherField = other as Field;
            if (otherField == null)
                return false;
            return this.X == otherField.X && this.Y == otherField.Y;
        }

        override public int GetHashCode()
        {
            return this.X.GetHashCode() + this.Y.GetHashCode();
        }
    }
}
