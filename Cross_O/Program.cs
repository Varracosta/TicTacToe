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
            CrossGame();           //Start of the Game. The program is designed for two real people and doesn't offer to play with Machine
            ContinueOrExitGame();  //Program offers to play again or exit the game
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

            Introduce(ref player1, ref player2); //Offers players to type their names 
                                                 //or to leave default ones, you can chose by pressing Y/N
            Console.Clear();
            while(count <= 9)
            {
                BoardDisplay(board);  //Displays the Field at the moment
                Console.WriteLine();
                count++;
                //Players take turn in entering coordinates. Choice will be displayed on the Field, Console will be cleared each time
                AskAndPut(board, count, ref countX, ref countO, player1, player2, Xaxis, ref Yaxis, ref Xc, ref Yc); 
                Console.Clear();

                //After at least one of the players will make 3 moves, the program will start to check the field on winner combinatons
                //If there will be a combination, the game will be stoped, and the name of the winner will be displayed
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
        {//Displaying the board at the real game moment
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
            //Offers players to type their names or to leave default ones, you can chose by pressing Y/N
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
            //Asking for coordinates. The program checks if the square is occupied. If it is, the program will offer to enter another 
            //coordinates. If its free, the program accepts the choice
            //Also, each player has their own counter to calculate moves
            if (count % 2 == 0)  //determine whose turn to play
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
            //Checking that Y coordinate is acceptible. If player puts anything but required, 
            //the program asks for coordinate until recieves the proper answer  
                while (Yaxis != "A" & Yaxis != "B" & Yaxis != "C")
                {
                    Console.WriteLine("Invalid answer! Must be a, b or c. Enter again:");
                    Yaxis = Console.ReadLine();
                    Yaxis = Yaxis.ToUpper();
                }
        }
        static int Ycoordinate(string Yaxis, ref int Yc)
        {//Determining Y coordinates according to positions on the field
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
        {//Checking that X coordinate is acceptible. If player puts anything but required, 
         //the program asks for coordinate until recieves the proper answer  
            while (Xaxis != "1" && Xaxis != "2" && Xaxis != "3")
            {
                Console.WriteLine("Invalid answer for coordinate X! Must be 1, 2 or 3. Enter again:");
                Xaxis = Console.ReadLine();
            }
        }
        static void VictoryConditions(string[,] board, ref int winner, int countX, int countO)
        {   //Comparing existing positions with victory combinations, taking into account personal player's counters to determine
            //whose combination is winning
            //(Probably the bulkiest version of Victory Combinations, but I couldn't create anything more simple and elegant...)
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
            //Offers to play again or to exit the program
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
