using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingScoreCard.Model
{
    /// <summary>
    /// A bowling frame
    /// 
    /// A frame consists of one to three delivers where you try to knock down
    /// all of the pins
    /// 
    /// The tenth frame is the lone exception where you can bowl three delivers
    /// and score up to 30 pin fall.
    /// 
    /// A frame where it takes one deliver to knock down all 10 pins is a
    /// strike.
    /// 
    /// A frame where it takes two delivers to knock down all 10 pins is a
    /// spare.
    /// 
    /// A frame where you fail to knock down all 10 frames is an open frame.
    /// 
    /// TODO: A frame where you leave non consecutive pins up after the first
    /// delivery is a split.  The exception to the split is if the head pin
    /// (pin #1) is still standing.
    /// </summary>
    public class Frame
    {
        #region "Parameters"

        private List<int> delivery = new List<int>();
        private int carryOver = 0;
        private int numCarryOver = 0;
        private int strikes = 0;
        private bool spare = false;
        private bool split = false;
        private int frameNumber = 0;

        #endregion

        #region "Constructors"

        /// <summary>
        /// Blank constructor
        /// </summary>
        public Frame()
        {
            //Intentially left blank.
        }

        /// <summary>
        /// Define frame number on constructor.
        /// </summary>
        /// <param name="value"></param>
        public Frame(int value)
        {
            frameNumber = value;
        }

        #endregion

        #region "Methods"

        /// <summary>
        /// When we have a strike or a spare we will add the value of the next
        /// frames deliver to this frames total score.
        /// </summary>
        /// <param name="value"></param>
        public void AddCarryOverValue(int value)
        {
            if (numCarryOver > 0)
            {
                carryOver = value;
                numCarryOver += -1;
            }
        }

        /// <summary>
        /// We want to record a delivery.
        /// </summary>
        /// <param name="value">Is number of pin falls.</param>
        public void RecordDelivery(int value) {
            delivery.Add(value);
            
            //One delviery ten pins is a strike.
            if ((value == 10) && (delivery.Count() == 1))
            {
                strikes = 1;
                if (frameNumber != 10)
                {
                    numCarryOver = 2;
                }
            }

            //Two deliver ten pin fall is a spare.
            if ((value == 10) && (delivery.Count() == 2))
            {
                spare = true;
                if (frameNumber != 10)
                {
                    numCarryOver = 1;
                }
            }

            if (frameNumber == 10)
            {

                if ((value == 20) && (delivery.Count() == 2))
                {
                    strikes += 1;
                }

                if (((value == 30) || (value == 20)) && (delivery.Count() == 3))
                {
                    strikes += 1;
                }
            }
        }

        /// <summary>
        /// We want to record a delivery.
        /// </summary>
        /// <param name="value">Number of pin fall</param>
        /// <param name="isSplit">Determine if the first delivery was a split</param>
        private void RecordDelivery(int value, bool isSplit)
        {
            split = isSplit;
            RecordDelivery(value);
        }

        /// <summary>
        /// We want to know if this frame was a spare.
        /// </summary>
        public bool IsSpare()
        {
            return spare;
        }

        /// <summary>
        /// Return the number of strikes.
        /// 
        /// It is possible to get up to 3 striks in a frame.
        /// </summary>
        /// <returns>The number of strikes.</returns>
        public int NumberOfStrikes()
        {
            return strikes;
        }

        /// <summary>
        /// Return the value the frames total pin fall plus any carry over.
        /// </summary>
        /// <returns>deliver sum and carry over value.</returns>
        public int TotalPinFall()
        {
            return delivery.Sum() + carryOver;
        }

        #endregion

    }
}
