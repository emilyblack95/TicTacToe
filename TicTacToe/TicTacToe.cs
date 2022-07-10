using System.Diagnostics;

namespace TicTacToe
{
    /// <summary>
    /// About:
    /// TicTacToe Console Application with M,N,K variation
    /// Solutions that were scrapped:
    /// - Using a multidimensional matrix (whole board isn't needed for win condition)
    /// This game assumes:
    /// - All input is valid
    /// - No duplicate moves are entered
    /// Going foward:
    /// - Abstract classes a bit more, use interfaces
    /// Notes:
    /// - https://en.wikipedia.org/wiki/M,n,k-game
    /// </summary>
    public class TicTacToe
    {
        private static long _M = 3;
        private static long _N = 3;
        private static long _K = 3;
        private static bool _PLAYAGAIN = true;

        public static void Main()
        {
            while (_PLAYAGAIN)
            {
                #region Variables

                Player PlayerOne = new();
                Player PlayerTwo = new();
                int round = 0;
                bool continuePlaying = true;

                Stopwatch watch = Stopwatch.StartNew();
                string? input;

                #endregion

                #region Gather Input

                Console.WriteLine("--------- WELCOME TO THE M,N,K VARIATION OF TIC TAC TOE ---------");
                Console.WriteLine();
                Console.WriteLine("--------- ENTER THE NUMBER OF M ROWS (DEFAULT IS 3) ---------");
                Console.WriteLine();

                input = Console.ReadLine();
                _M = input == string.Empty ? _M : long.Parse(input!);

                Console.WriteLine();
                Console.WriteLine("--------- ENTER THE NUMBER OF N COLUMNS (DEFAULT IS 3) ---------");
                Console.WriteLine();

                input = Console.ReadLine();
                _N = input == string.Empty ? _N : long.Parse(input!);

                Console.WriteLine();
                Console.WriteLine("--------- ENTER THE WINNING THRESHOLD K (DEFAULT IS 3) ---------");
                Console.WriteLine();

                input = Console.ReadLine();
                _K = input == string.Empty ? _K : long.Parse(input!);

                #endregion

                #region Start Game

                watch.Restart();
                
                // While we're in a playing state or we've hit our max
                while(continuePlaying)
                {
                    #region Player One Moves

                    if(!PlayTurn(PlayerOne, PlayerTwo, true))
                    {
                        PrintStatus(PlayerOne, PlayerTwo, round+1);
                        break;
                    }

                    #endregion

                    #region Player Two Moves

                    continuePlaying = PlayTurn(PlayerTwo, PlayerOne, false);

                    #endregion

                    #region Print Status

                    round++;
                    PrintStatus(PlayerOne, PlayerTwo, round);

                    #endregion
                }

                watch.Stop();
                Console.WriteLine($"Elapsed milliseconds: {watch.ElapsedMilliseconds}");
                Console.WriteLine($"Elapsed timespan: {watch.Elapsed}");
                Console.WriteLine($"Elapsed ticks: {watch.ElapsedTicks}");
                Console.WriteLine();

                #endregion

                #region Play Again?

                Console.WriteLine("--------- DO YOU WANT TO PLAY AGAIN? (YES/NO) ---------");
                Console.WriteLine();
                input = Console.ReadLine();

                if (!string.Equals(input, "yes", StringComparison.OrdinalIgnoreCase))
                {
                    _PLAYAGAIN = false;
                }

                Console.WriteLine();

                #endregion
            }
        }

        /// <summary>
        /// Plays turn for current player.
        /// </summary>
        /// <param name="current">Current player</param>
        /// <param name="opponent">Other player</param>
        /// <returns>Returns if the game should continue.</returns>
        public static bool PlayTurn(Player current, Player opponent, bool isPlayerOne)
        {
            bool playAgain = true;

            while (playAgain)
            {
                #region Receive New Move From Player

                string? input = string.Empty;
                while (input == string.Empty)
                {
                    Console.WriteLine();

                    if (isPlayerOne)
                    {
                        Console.WriteLine("--------- ENTER THE NEXT MOVE PLAYER ONE (FORMAT: x y) ---------");
                    }
                    else
                    {
                        Console.WriteLine("--------- ENTER THE NEXT MOVE PLAYER TWO (FORMAT: x y) ---------");
                    }
                    
                    Console.WriteLine();
                    input = Console.ReadLine();

                    if (input == string.Empty)
                    {
                        Console.WriteLine("--------- Please enter a valid x y move ---------");
                    }
                }

                long[] parsed = input!.Split(' ').Select(n => (long)Convert.ToDouble(n)).ToArray();
                current.Moves.Add(new(parsed[0], parsed[1]));

                #endregion

                #region Check for Win

                if (current.CheckState(_M, _N, _K))
                {
                    Console.WriteLine();
                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");

                    if (isPlayerOne)
                    {
                        Console.WriteLine("--------- PLAYER ONE HAS WON! ---------");
                    }
                    else
                    {
                        Console.WriteLine("--------- PLAYER TWO HAS WON! ---------");
                    }

                    Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
                    Console.WriteLine();
                    return false;
                }

                #endregion

                #region Check for Draw

                if (_M * _N == (current.Moves.Count + opponent.Moves.Count))
                {
                    Console.WriteLine();
                    Console.WriteLine("--------- BOTH PLAYERS HAVE DRAWN! ---------");
                    Console.WriteLine();
                    return false;
                }

                #endregion

                #region Undo?

                Console.WriteLine();

                if (isPlayerOne)
                {
                    Console.WriteLine("--------- WOULD YOU LIKE TO UNDO YOUR MOVE PLAYER ONE? (YES/NO) ---------");
                }
                else
                {
                    Console.WriteLine("--------- WOULD YOU LIKE TO UNDO YOUR MOVE PLAYER TWO? (YES/NO) ---------");
                }

                Console.WriteLine();
                input = Console.ReadLine();

                if (!string.Equals(input, "yes", StringComparison.OrdinalIgnoreCase))
                {
                    playAgain = false;
                }
                else
                {
                    current.UndoMove();
                }

                #endregion
            }

            return true;
        }

        /// <summary>
        /// Prints current status of board with current round.
        /// </summary>
        public static void PrintStatus(Player PlayerOne, Player PlayerTwo, int round)
        {
            Console.WriteLine();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine("--------- CURRENT STATE OF BOARD ---------");
            Console.WriteLine();
            Console.WriteLine($"ROUND: {round}");
            Console.WriteLine();
            Console.WriteLine("PLAYER ONE'S MOVES:");
            Console.WriteLine();
            Console.WriteLine(string.Join(",", PlayerOne.Moves));
            Console.WriteLine();
            Console.WriteLine("PLAYER TWO'S MOVES:");
            Console.WriteLine();
            Console.WriteLine(string.Join(",", PlayerTwo.Moves));
            Console.WriteLine();
            Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            Console.WriteLine();
        }
    }
}