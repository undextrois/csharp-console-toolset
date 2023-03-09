using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowHeight = 20;
            Console.WindowWidth = 60;

            int playerX = 25;
            int playerY = Console.WindowHeight - 1;

            int score = 0;
            int level = 1;
            int lives = 3;

            bool gameOver = false;

            List<Alien> aliens = new List<Alien>();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    aliens.Add(new Alien(j * 5 + 5, i * 2 + 2));
                }
            }

            while (!gameOver)
            {
                Console.Clear();

                // Draw player
                Console.SetCursorPosition(playerX, playerY);
                Console.Write("▲");

                // Draw aliens
                foreach (Alien alien in aliens)
                {
                    Console.SetCursorPosition(alien.X, alien.Y);
                    Console.Write("O");
                }

                // Draw score, level, and lives
                Console.SetCursorPosition(0, Console.WindowHeight - 1);
                Console.Write("Score: " + score + "  Level: " + level + "  Lives: " + lives);

                // Move player
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.LeftArrow && playerX > 0)
                    {
                        playerX--;
                    }
                    else if (key.Key == ConsoleKey.RightArrow && playerX < Console.WindowWidth - 1)
                    {
                        playerX++;
                    }
                    else if (key.Key == ConsoleKey.Spacebar)
                    {
                        // Shoot a bullet
                        Bullet bullet = new Bullet(playerX, playerY - 1);
                        while (bullet.Y > 0)
                        {
                            Console.SetCursorPosition(bullet.X, bullet.Y);
                            Console.Write("|");

                            // Check for collision with aliens
                            foreach (Alien alien in aliens.ToList())
                            {
                                if (bullet.X == alien.X && bullet.Y == alien.Y)
                                {
                                    aliens.Remove(alien);
                                    score += 10;
                                }
                            }

                            bullet.Y--;

                            System.Threading.Thread.Sleep(50);

                            Console.SetCursorPosition(bullet.X, bullet.Y);
                            Console.Write(" ");
                        }
                    }
                }

                // Move aliens
                foreach (Alien alien in aliens)
                {
                    if (alien.X == 0 || alien.X == Console.WindowWidth - 1)
                    {
                        foreach (Alien a in aliens)
                        {
                            a.Y++;
                        }
                        break;
                    }

                    alien.X += level;
                }

                // Check for collision with player
                foreach (Alien alien in aliens.ToList())
                {
                    if (alien.Y == playerY && Math.Abs(alien.X - playerX) < 2)
                    {
                        lives--;
                        aliens.Remove(alien);
                    }
                }

                if (aliens.Count == 0)
                {
                    level++;
                    for (int i = 0; i < 4; i++)
                    {
                        for (int j = 0; j
