using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Server.Game
{


    class Bubble
    {
        E_BUBBLE_TYPE _bubbleType;

        public E_BUBBLE_TYPE BubbleType { 
            get { return _bubbleType; }
            set { _bubbleType = value; }
        }

        public Bubble(int seed)
        {
            Random rand = new Random(unchecked((int)DateTime.Now.Ticks) + seed);
            _bubbleType = (E_BUBBLE_TYPE)rand.Next((int)E_BUBBLE_TYPE.RED, (int)E_BUBBLE_TYPE.PURPLE + 1);
            Thread.Sleep(5);
        }
        public override string ToString()
        {
            return _bubbleType.ToString();
        }

    }

 
    class BubbleCols 
    {
        List<Bubble> _cols = new List<Bubble>();

        public List<Bubble> Cols { get { return _cols; } }

        public BubbleCols()
        {
            for (int i = 0; i < BubbleMap.G_BUBBLE_COL_COUNT; i++)
            {
                _cols.Add(new Bubble(i));
            }
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
