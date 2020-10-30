using System;
using System.Diagnostics.Tracing;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading;

namespace Cross_O
{
    class Program
    {
        static void Main(string[] args)
        {
            CrossGame();
            ContinueOrExitGame();
            Console.Clear();
        }

        static void CrossGame()
        {
            string player1 = null;
            string player2 = null;
            int count = 0;
            int countX = 0;
            int countO = 0;
            string Xaxis = null;
            string Yaxis = null;
            int Yc = 0;
            int Xc = 0;
            int winner = 0;
            string[,] board = new string[,]
            
            {
                {" ","|", "1","|", "2","|", "3"},
                {"--","--","--", "--", "--", " ", " "},
                {"A","|", " ","|", " ","|", " "},
                {"--","--","--", "--", "--", " ", " "},
                {"B","|", " ","|", " ","|", " "},
                {"--","--","--", "--", "--", " ", " "},
                {"C","|", " ","|", " ","|", " "},
            };

            Introduce(ref player1, ref player2); //Предлагает игрокам ввести имена или оставить дефолтные. Есть проверка на ввод Да/Нет
            Console.Clear();
            while(count <= 9)
            {
                BoardDisplay(board);  //Отображения поля на данный момент
                Console.WriteLine();
                count++;
                AskAndPut(board, count, ref countX, ref countO, player1, player2, Xaxis, ref Yaxis, ref Xc, ref Yc); //Ввод координат игроками по очереди
                Console.Clear();

                //Проверка на выигрышные комбинации
                if(countX >= 3 || countO >= 3)
                {
                    VictoryConditions(board, ref winner, countX, countO);
                    if(winner == 1)
                    {
                        BoardDisplay(board);
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"***\\.....{player1} won!.....//***");
                        Console.ResetColor();
                        break;
                    }
                    if(winner == 2)
                    {
                        BoardDisplay(board);
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"***\\.....{player2} won!.....//***");
                        Console.ResetColor();
                        break;
                    }
                    else if(winner != 1 && winner != 2 && count == 9)
                    {
                        BoardDisplay(board);
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("No one won. Come back again!");
                        Console.ResetColor();
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
        static void BoardDisplay(string[,] board)
        {
            int height = board.GetLength(0);
            int width = board.GetLength(1);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Write(board[y, x] + " ");
                }
                Console.WriteLine();
            }
        }
        static void Introduce(ref string player1, ref string player2)
        {
            //Предлагает игрокам ввести имена. Есть проверка на введение либо Y, либо N
            //Если игроки соглашаются ввести имя, записывает имена в переменные Player. Иначе перемю просто Player 1/2 
            Console.WriteLine("Do you want to enter a name for 1st player? Y/N");
            string answer = Console.ReadLine();
            answer = answer.ToUpper();
            while(answer != "Y" || answer != "N")
            {
                
                if (answer == "Y")
                {
                    Console.Write("1st player's name: ");
                    player1 = Console.ReadLine();
                    break;
                }
                else if (answer == "N")
                {
                    player1 = "Player 1";
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid answer.Enter Y or N");
                    answer = Console.ReadLine();
                    answer = answer.ToUpper();
                }
            }

            Console.WriteLine("Do you want to enter a name for 2nd player? Y/N");
            answer = Console.ReadLine();
            answer = answer.ToUpper();
            while (answer != "Y" || answer != "N")
            {
                if (answer == "Y")
                {
                    Console.Write("2nd player's name: ");
                    player2 = Console.ReadLine();
                    break;
                }
                else if (answer == "N")
                {
                    player2 = "Player 2";
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid answer.Enter Y or N");
                    answer = Console.ReadLine();
                    answer = answer.ToUpper();
                }
            }
        }
        static void AskAndPut(string[,] board, int count, ref int countX, ref int countO, string player1, string player2, string Xaxis, 
            ref string Yaxis, ref int Xc, ref int Yc)
        {
            if (count % 2 == 0)
            {
                while (true)
                {
                    Console.WriteLine($"{player2}, please enter your coordinate Y: ");
                    Yaxis = Console.ReadLine();
                    Yaxis = Yaxis.ToUpper();
                    YCheck(ref Yaxis);
                    Ycoordinate(Yaxis, ref Yc);

                    Console.WriteLine($"{player2}, please enter your coordinate X: ");
                    Xaxis = Console.ReadLine();
                    XCheck(ref Xaxis);
                    Xc = int.Parse(Xaxis) * 2;

                    if (board[Yc, Xc] == " ")
                    {
                        board[Yc, Xc] = "O";
                        countO++;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("This square is occupied!");
                    }
                }
            }
            else
            {
                while (true)
                {
                    Console.WriteLine($"{player1}, please enter your coordinate Y: ");
                    Yaxis = Console.ReadLine();
                    Yaxis = Yaxis.ToUpper();
                    YCheck(ref Yaxis);
                    Ycoordinate(Yaxis, ref Yc);

                    Console.WriteLine($"{player1}, please enter your coordinate X: ");
                    Xaxis = Console.ReadLine();
                    XCheck(ref Xaxis);
                    Xc = int.Parse(Xaxis) * 2;

                    if (board[Yc, Xc] == " ")
                    {
                        board[Yc, Xc] = "X";
                        countX++;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("This square is occupied!");
                    }
                }
            }
        }
        static void YCheck(ref string Yaxis)
        {
                while (Yaxis != "A" & Yaxis != "B" & Yaxis != "C")
                {
                    Console.WriteLine("Invalid answer! Must be a, b or c. Enter again:");
                    Yaxis = Console.ReadLine();
                    Yaxis = Yaxis.ToUpper();
                }
        }
        static int Ycoordinate(string Yaxis, ref int Yc)
        {
            switch (Yaxis)
            {
                case "A":
                    Yc = 2;
                    return Yc;
                case "B":
                    Yc = 4;
                    return Yc;
                case "C":
                    Yc = 6;
                    return Yc;
            }
            return Yc;
        }
        static void XCheck(ref string Xaxis)
        {
            while (Xaxis != "1" && Xaxis != "2" && Xaxis != "3")
            {
                Console.WriteLine("Invalid answer for coordinate X! Must be 1, 2 or 3. Enter again:");
                Xaxis = Console.ReadLine();
            }
        }
        static void VictoryConditions(string[,] board, ref int winner, int countX, int countO)
        {
            if(board[2, 2] == "X" && board[2,2] == board[2,4] && board[2, 4] == board[2,6] && countX >= 3)
            {
                winner = 1;
            }
            else if(board[4, 2] == "X" && board[4,2] == board[4, 4] && board[4,4] == board[4, 6] && countX >= 3)
            {
                winner = 1; ;
            }
            else if (board[6, 2] == "X" && board[6, 2] == board[6, 4] && board[6, 4] == board[6, 6] && countX >= 3)
            {
                winner = 1;
            }
            else if (board[2, 2] == "X" && board[2, 2] == board[4, 2] && board[4, 2] == board[6, 2] && countX >= 3)
            {
                winner = 1;
            }
            else if (board[2, 4] == "X" && board[2, 4] == board[4, 4] && board[4, 4] == board[6, 4] && countX >= 3)
            {
                winner = 1;
            }
            else if (board[2, 6] == "X" && board[2, 6] == board[4, 6] && board[4, 6] == board[6, 6] && countX >= 3)
            {
                winner = 1;
            }
            else if (board[2, 2] == "X" && board[2, 2] == board[4, 4] && board[4, 4] == board[6, 6] && countX >= 3)
            {
                winner = 1;
            }
            else if (board[6, 2] == "X" && board[6, 2] == board[4, 4] && board[4, 4] == board[2, 6] && countX >= 3)
            {
                winner = 1;
            }
            else if (board[2, 2] == "O" && board[2, 2] == board[2, 4] && board[2, 4] == board[2, 6] && countO >= 3)
            {
                winner = 2;
            }
            else if (board[4, 2] == "O" && board[4, 2] == board[4, 4] && board[4, 4] == board[4, 6] && countO >= 3)
            {
                winner = 2;
            }
            else if (board[6, 2] == "O" && board[6, 2] == board[6, 4] && board[6, 4] == board[6, 6] && countO >= 3)
            {
                winner = 2;
            }
            else if (board[2, 2] == "O" && board[2, 2] == board[4, 2] && board[4, 2] == board[6, 2] && countO >= 3)
            {
                winner = 2;
            }
            else if (board[2, 4] == "O" && board[2, 4] == board[4, 4] && board[4, 4] == board[6, 4] && countO >= 3)
            {
                winner = 2;
            }
            else if (board[2, 6] == "O" && board[2, 6] == board[4, 6] && board[4, 6] == board[6, 6] && countO >= 3)
            {
                winner = 2;
            }
            else if (board[2, 2] == "O" && board[2, 2] == board[4, 4] && board[4, 4] == board[6, 6] && countO >= 3)
            {
                winner = 2;
            }
            else if (board[6, 2] == "O" && board[6, 2] == board[4, 4] && board[4, 4] == board[2, 6] && countO >= 3)
            {
                winner = 2; 
            }
            else
            {
                winner = 0;
            }
        }
        static void ContinueOrExitGame()
        {
            string answer = null;
                do
                {
                    Console.WriteLine("Press A to play again or press Q to quit the program");
                    answer = Console.ReadLine();
                    answer = answer.ToUpper();
                     if (answer == "A")
                     {
                        CrossGame();
                     }
                     else if (answer == "Q")
                     {
                        Environment.Exit(0);
                     }
                }
                while (answer != "Q" || answer != "A");
        }
    }
}
