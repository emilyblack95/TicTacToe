namespace TicTacToe
{
    /// <summary>
    /// Represents a point on the TicTacToe board.
    /// </summary>
    public class Point
    {
        /// <summary>
        /// X coord.
        /// </summary>
        public long X { get; set; }

        /// <summary>
        /// Y coord.
        /// </summary>
        public long Y { get; set; }

        /// <summary>
        /// Overloaded constructor.
        /// </summary>
        /// <param name="x">x coord</param>
        /// <param name="y">y coord</param>
        public Point(long x, long y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return "{" + X + "," + Y + "}";
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() ^ Y.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            return obj != null && (obj as Point)!.X == X && (obj as Point)!.Y == Y;
        }
    }
}
