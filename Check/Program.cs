using System;

namespace Check
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter coordinate");
            string Y = null;
            do
            {
                Y = Console.ReadLine();
                Y = Y.ToUpper();
                Console.WriteLine("Invalid answer. Enter coordiate Y: ");
            }
            while (Y != "A" && Y != "B");
        }
    }
}
