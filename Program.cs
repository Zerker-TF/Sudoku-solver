using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku_solver
{
    internal class Program
    {


        class SudokuSolver
        {
            static int[,] board = new int[9, 9];
            static int[,] BColor = new int[9, 9];

            static void Main(string[] args)
            {
                // Enter the puzzle here, using 0 for unknown cells
                board = new int[,] {
                        {0, 0, 0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0, 0, 0}
                                   };
                BColor = new int[,] {
                        {0, 0, 0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0, 0, 0},
                        {0, 0, 0, 0, 0, 0, 0, 0, 0}
                                   };

                Console.Title = "Solve this Sudoku!";

                int num = 1;
                while (num >= 0)
                {
                    Console.Write("Ingrese un valor entre 1-9: ");
                    int Num = Convert.ToInt32(Console.ReadLine());
                    if (Num <= 9 && num >= 1)
                    {
                        Console.Write("\nEn que fila?: ");
                        int row = Convert.ToInt32(Console.ReadLine());
                        Console.Write("\nEn que columna?: ");
                        int col = Convert.ToInt32(Console.ReadLine());
                        board[col - 1, row - 1] = Num;
                        BColor[col - 1, row - 1] = 1;
                        Console.Clear();
                    }
                    
                   
                    num = Num;
                    Console.Clear();
                    
                }

                Console.WriteLine("\nInitial Board:\n");
                PrintBoard();

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("\nSolution:");


                if (SolveSudoku(0, 0)) //solve the board
                {
                    PrintBoard();
                }
                else
                {
                    Console.WriteLine("No solution found.");
                }
                Console.ReadLine();
            }

            static bool SolveSudoku(int row, int col)
            {
                
                if (row == 9 ) //si llegamos a la fila 9 columna 9, es el fin del sudoku
                {
                    return true;
                    
                }
                //move to the next row/colum
                int nextRow = col == 8 ? row + 1 : row;
                int nextCol = (col + 1) % 9;

                if (board[row,col] != 0) //skip filled cells
                {
                    return SolveSudoku(nextRow, nextCol);
                }

                //if any 0 is found, check the surrounding numbers to find a valid value for said place
                for (int num = 1; num <= 9; num++)
                {
                    if (IsValid(row, col, num))
                    {
                        board[row, col] = num; //try to place num in (row,col)
                        if (SolveSudoku(nextRow, nextCol))
                        {
                            return true;
                        }
                        board[row, col] = 0; //for backtracking if no solution is found
                    }
                }

                return false;
            }

            static bool IsValid(int row, int col, int num)
            {
                // Check row and column
                for (int i = 0; i < 9; i++)
                {
                    if (board[row, i] == num || board[i,col] == num)
                    {
                        return false;
                    }
                }

                //check 3x3 box
                int squareRow = (row / 3) * 3; // looks for the top-left corner of the box
                int squareCol = (col / 3) * 3;
                for (int i = squareRow; i < squareRow + 3; i++)
                {
                    for (int j = squareCol; j < squareCol + 3; j++)
                    {
                        if (board[i,j] == num)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }

           
            static void PrintBoard()
            {
                for (int row = 0; row < 9; row++)
                {
                    for (int col = 0; col < 9; col++)
                    {
                        
                        if (BColor[row,col] == 1)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.Write(board[row, col] + " ");
                            if (col == 2 || col == 5)
                            {
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.Write("| ");
                            }
                           
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write(board[row, col] + " ");
                            if (col == 2 || col == 5)
                            {
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.Write("| ");
                            }
                            
                        }               
                    }
                    if (row == 2 || row == 5)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("\n_____________________");
                    }
                   
                    Console.WriteLine();
                }
               
            }

        }
    }
}