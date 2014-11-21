using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramonEngine
{
    public class LCGRandom
    {
        private int state { get; set; }

        public LCGRandom() 
        { 
            state = 0; 
        }

        public LCGRandom(int seed) 
        { 
            state = seed; 
        }

        /// <summary> Get a random short based on the LCG algoritm. </summary>
        /// <returns> A 16bit Int. </returns>
        public short NextShort()
        {
            const int multilier = 214013;
            const int increment = 2531011;
            const int modules = int.MaxValue;

            state = multilier * state + increment;

            return (short)((state & modules) >> 16);
        }
    }
}
