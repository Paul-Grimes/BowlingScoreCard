using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoreCard
{
    /// <summary>
    /// Prototype
    /// 
    /// Build a console application that will keep
    /// your bowling game score. 
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            PrintHeader();

            int carryOvers = 0;
            int lastFrame = 1;


            List<List<int>> game = new List<List<int>>();

            while (game.Count() < 10)
            {

                lastFrame = (game.Count == 9) ? 0 : 1;

                List<int> frame = new List<int>();

                while ((frame.Count + lastFrame) < 3)
                {
                    
                    Console.WriteLine("#{0} Delivery of Frame {1}", frame.Count() + 1, game.Count() + 1);
                    Console.Write("Enter score:");

                    int delivery = Convert.ToInt32(Console.ReadLine());
                    frame.Add(delivery);

                    if (carryOvers > 0)
                    {
                        if ((carryOvers > 1) && (game.Count() > 2))
                        {
                            game[game.Count() - 2].Add(delivery);
                            carryOvers += -1;
                        }
                        game[game.Count() - 1].Add(delivery);
                        carryOvers += -1;

                    }

                    if ((frame.Count == 1) && (frame.Sum() == 10) && (game.Count() != 9))
                    {
                        lastFrame = 2;
                        continue;
                    }
                    else if ((game.Count() == 9) && (frame.Count == 2) && (frame.Sum() < 9))
                    {
                        lastFrame = 1;
                        continue;
                    }

                }

                game.Add(frame);

                if ((game.Count) != 10)
                {

                    if ((frame.Count == 1) && (frame.Sum() == 10))
                    {
                        carryOvers += 2;
                    }
                    else if (frame.Sum() == 10)
                    {
                        carryOvers = 1;
                    }
                }

                Console.WriteLine("Current Score: {0}:", CurrentScore(game));                

            }

            Console.WriteLine("");
            Console.WriteLine(string.Format("You bowled a {0}", CurrentScore(game).ToString()));
            Console.ReadLine();

        }

        static void PrintHeader()
        {
            Console.WriteLine("Enter only numbers:");
            Console.WriteLine("There is no input validation.");
            Console.WriteLine("----------------------------");
            Console.WriteLine("");
        }

        static int CurrentScore(List<List<int>> game)
        {
            int score = 0;
            foreach (List<int> i in game)
            {
                score += i.Sum();
            }

            return score;
        }
    }
}
