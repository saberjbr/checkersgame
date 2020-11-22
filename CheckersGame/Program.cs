using System;
using System.IO;

namespace CheckersGame
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("----- Welcome to Checkers Game -----");
            Console.ResetColor();
            Console.WriteLine("Play on 8x8 board");
            var board = new Board();
            StartGame(board);
        }



        static void StartGame(Board board)
        {
            int moves = 1;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Play: #0");
            Console.ResetColor();
            board.PrintToOut();
            while (!board.IsGameOver() && moves < 100)
            {
                var engine = new Engine(board);
                var bestMove = engine.ScoreMoves();
                board = bestMove.boardAfterMove;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Play: #{moves}");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(bestMove);
                Console.ResetColor();


                board.PrintToOut();

                moves++;
            }

            if (board.IsGameOver())
            {
                if (board.Turn == PawnColor.Black)
                {
                    Console.WriteLine("Game over. White won");
                }
                else
                {
                    Console.WriteLine("Game over. Black won");
                }
            }
        }
    }
}
