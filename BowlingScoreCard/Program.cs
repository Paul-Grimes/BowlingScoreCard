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
        /// <summary>
        /// We want all of the logic for a bowling game defined here
        /// 
        /// Things I am not concerned with, what pin was left up, was it a split
        /// and other shit that has nothing to do with actually keeping score.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //Moved the code to a function so I can see what the fuck is going on.
            PrintHeader();

            //We need a variable that will determine how many carry overs we have.
            //You get a carry over when you bowl a strike or make a spare.
            //The next delivery is added to your last frame on a spare.
            //The next two deliveries are added to your current frame on a spare.
            int carryOvers = 0;

            //The last frame has a potential of 3 deliverals instead of the typical 2.
            int lastFrame = 1;

            //A bowling frame is consisted of one to three deliveries which will always
            //be a hole number less then ten.
            List<List<int>> game = new List<List<int>>();

            //A game has ten frames.  We want to stop after we complete the 10th frame.
            while (game.Count() < 10)
            {

                //Deterimin if this is the last frame to complete.
                lastFrame = (game.Count == 9) ? 0 : 1;

                List<int> frame = new List<int>();

                //Start the current frame.
                while ((frame.Count + lastFrame) < 3)
                {
                    
                    Console.WriteLine("#{0} Delivery of Frame {1}", frame.Count() + 1, game.Count() + 1);
                    Console.Write("Enter score:");

                    int delivery = Convert.ToInt32(Console.ReadLine());
                    frame.Add(delivery);

                    //Check to see if we had a carryover from an earlier frame.
                    if (carryOvers > 0)
                    {
                        //If you have two or more carry overs you need to 
                        //go back two frames to update the score.
                        if ((carryOvers > 1) && (game.Count() > 2))
                        {
                            game[game.Count() - 2].Add(delivery);
                            carryOvers += -1;
                        }

                        //Update the last frame that you had a mark in.
                        game[game.Count() - 1].Add(delivery);
                        carryOvers += -1;

                    }

                    //You threw a strike, finish this frame.
                    if ((frame.Count == 1) && (frame.Sum() == 10) && (game.Count() != 9))
                    {
                        lastFrame = 2;
                        continue;
                    }//You did not spare or strike the tenth frame.
                    else if ((game.Count() == 9) && (frame.Count == 2) && (frame.Sum() < 10))
                    {
                        lastFrame = 1;
                        continue;
                    }

                }

                game.Add(frame);

                //See if you made a strike or a spare.
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

            PrintFooter(game);

        }

        static void PrintHeader()
        {
            Console.WriteLine("Enter only numbers:");
            Console.WriteLine("There is no input validation.");
            Console.WriteLine("----------------------------");
            Console.WriteLine("");
        }

        static void PrintFooter(List<List<int>> game)
        {
            Console.WriteLine("");
            Console.WriteLine(string.Format("You bowled a {0}", CurrentScore(game).ToString()));
            Console.ReadLine();
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
