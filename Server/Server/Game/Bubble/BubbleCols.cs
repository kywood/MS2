using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Server.Game
{


    class Bubble
    {
        E_BUBBLE_TYPE _bubbleType;

        static int seed = 0 ;

        object _lock = new object();

        public E_BUBBLE_TYPE BubbleType { 
            get { return _bubbleType; }
            set { _bubbleType = value; }
        }

        public int getSeed()
        {
            return seed++;
        }       

        public Bubble()
        {
            Random rand = new Random(unchecked((int)DateTime.Now.Ticks) + seed++);
            _bubbleType = (E_BUBBLE_TYPE)rand.Next((int)E_BUBBLE_TYPE.RED, (int)E_BUBBLE_TYPE.PURPLE + 1);
            //Thread.Sleep(5);
            if (seed > 1000000)
                seed = 0;
        }

        public static E_BUBBLE_TYPE GetBubbleType()
        {
            //E_BUBBLE_TYPE bt;
            Random rand = new Random(unchecked((int)DateTime.Now.Ticks) + seed++);

            if (seed > 1000000)
                seed = 0;

            return (E_BUBBLE_TYPE)rand.Next((int)E_BUBBLE_TYPE.RED, (int)E_BUBBLE_TYPE.PURPLE + 1);
            //return bt;
            
        }

        public override string ToString()
        {
            return _bubbleType.ToString();
        }

    }

 
    class BubbleCols 
    {
        //static int seed = 0;

        List<Bubble> _cols = new List<Bubble>();

        public List<Bubble> Cols { get { return _cols; } }

        public BubbleCols()
        {
            for (int i = 0; i < BubbleMap.G_BUBBLE_COL_COUNT; i++)
            {
                _cols.Add(new Bubble());
            }

            //if (seed > 100000)
            //    seed = 0;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            int loopCnt = 0;
            foreach (Bubble bb in _cols)
            {
                if (loopCnt++ != 0)
                    sb.Append(" , ");

                sb.Append(bb.ToString());
            }

            return sb.ToString();

        }

    }
}
