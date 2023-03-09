using System;
using System.Threading;

namespace DonkeyGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.WindowHeight = 20;
            Console.WindowWidth = 40;
            Console.BufferHeight = Console.WindowHeight;
            Console.BufferWidth = Console.WindowWidth;

            int carPosition = Console.WindowWidth / 2;
            int score = 0;

            while (true)
            {
                Console.Clear();

                // Draw road
                Console.WriteLine("============");
                for (int i = 0; i < Console.WindowHeight - 2; i++)
                {
                    Console.WriteLine("|          |");
                }
                Console.WriteLine("============");

                // Draw car
                Console.SetCursorPosition(carPosition, Console.WindowHeight - 2);
                Console.Write("##");

                // Draw donkey
                Random rand = new Random();
                int donkeyPosition = rand.Next(1, Console.WindowWidth - 1);
                Console.SetCursorPosition(donkeyPosition, Console.WindowHeight - 4);
                Console.Write("oo");

                // Update score
                Console.SetCursorPosition(0, 0);
                Console.Write($"Score: {score}");

                // Check for collision
                if (donkeyPosition == carPosition || donkeyPosition == carPosition + 1)
                {
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 4, Console.WindowHeight / 2);
                    Console.Write("GAME OVER!");
                    Console.ReadLine();
                    return;
                }

                // Move car
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                    if (keyInfo.Key == ConsoleKey.LeftArrow)
                    {
                        carPosition--;
                        if (carPosition < 1)
                        {
                            carPosition = 1;
                        }
                    }
                    else if (keyInfo.Key == ConsoleKey.RightArrow)
                    {
                        carPosition++;
                        if (carPosition > Console.WindowWidth - 3)
                        {
                            carPosition = Console.WindowWidth - 3;
                        }
                    }
                }

                // Increase score
                score++;

                // Slow down game loop
                Thread.Sleep(100);
            }
        }
    }
}
