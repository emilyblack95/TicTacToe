namespace TicTacToe
{
    /// <summary>
    /// Player class that exists on a given TicTacToe board.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// List of moves.
        /// </summary>
        public HashSet<Point> Moves { get; set; }

        /// <summary>
        /// Current PlayerState on the board.
        /// </summary>
        public PlayerState State { get; set; }

        /// <summary>
        /// The different win conditions: Diagonal Left Win, Diagonal Right Win, Vertical Win, Horizontal Win
        /// </summary>
        private static readonly List<(Point, Point)> WinConditions = new() { (new(-1, -1), new(1, 1)), (new(1, -1), new(-1, 1)), (new(-1, 0), new(1, 0)), (new(0, -1), new(0, 1)) };

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Player()
        {
            Moves = new();
        }

        /// <summary>
        /// Undo last move if any moves exist.
        /// </summary>
        public void UndoMove()
        {
            // Want to undo if we have moves to undo, and if the current state is still playing
            if (Moves.Count > 0 && State == PlayerState.Playing)
            {
                Moves.Remove(Moves.Last());
            }
        }

        /// <summary>
        /// The state is checked after add a move to determine if the player has won.
        /// </summary>
        /// <param name="m">Number of rows.</param>
        /// <param name="n">Number of columns.</param>
        /// <param name="k">K number of consecutive cells to win.</param>
        /// <returns>True if player has won.</returns>
        public bool CheckState(long m, long n, long k)
        {
            // run algorithm against winconditions
            foreach(var cell in Moves)
            {
                // Diagonal Left Win Check, Diagonal Right Win Check, Vertical Win, Horizontal Win
                if (CountNeighbors(WinConditions[0], cell, m, n, k) >= k ||
                    CountNeighbors(WinConditions[1], cell, m, n, k) >= k ||
                    CountNeighbors(WinConditions[2], cell, m, n, k) >= k ||
                    CountNeighbors(WinConditions[3], cell, m, n, k) >= k)
                {
                    State = PlayerState.Win;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Counts the number of neighbors around any given cell on the board. Given a pair of cardinal directions.
        /// </summary>
        /// <param name="direction">pair of cardinal direction (Diagonal Left, Diagonal Right, Vertical, Horizontal)</param>
        /// <param name="cell">current cell we're on.</param>
        /// <param name="m">Number of rows.</param>
        /// <param name="n">Number of columns.</param>
        /// <param name="k">K number of consecutive cells to win.</param>
        /// <returns></returns>
        public int CountNeighbors((Point, Point) direction, Point cell, long m, long n, long k)
        {
            // Start off at 1 since our current cell is considered the "first" point
            int counter = 1;
            Point calcPointOne = new(cell.X, cell.Y);
            Point calcPointTwo = new(cell.X, cell.Y);
            bool keepComputing = true;

            // Count in first direction
            while(keepComputing)
            {
                // Compute second point
                calcPointOne.X += direction.Item1.X;
                calcPointOne.Y += direction.Item1.Y;

                // If our new calculated point is within the m n bounds and is contained in moves
                if (calcPointOne.X < m && calcPointOne.Y < n && Moves.Contains(calcPointOne))
                {
                    counter++;
                }
                else
                {
                    keepComputing = false;
                }

                // If we've hit K occurences, consider it a win and return
                if (counter == k)
                {
                    State = PlayerState.Win;
                    return counter;
                }
            }

            keepComputing = true;

            // Count in second direction
            while (keepComputing)
            {
                // Compute second point
                calcPointTwo.X += direction.Item2.X;
                calcPointTwo.Y += direction.Item2.Y;

                // If our new calculated point is within the m n bounds and is contained in moves
                if(calcPointTwo.X < m && calcPointTwo.Y < n && Moves.Contains(calcPointTwo))
                {
                    counter++;
                }
                else
                {
                    keepComputing = false;
                }

                // If we've hit K occurences, consider it a win and return
                if(counter == k)
                {
                    State = PlayerState.Win;
                    return counter;
                }
            }

            return counter;
        }
    }
}
