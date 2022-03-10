using Google.Protobuf.Protocol;
using Server.Game;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Game
{


    public enum E_BUBBLE_TYPE
    {
        NONE = 0,
        RED = 1,
        BLUE = 2,
        YELLOW = 3,
        GREEN = 4,
        PURPLE = 5,
        SPECIAL = 6,
        MAX
    }
    class BubbleMap
    {
        public static readonly int G_BUBBLE_COL_COUNT = 8;
        public static readonly int G_COLS_SLOT_BUFFER_SIZE = 100;
        public static readonly int G_COLS_SLOT_RE_SIZE_RANGE = 75;

        BubbleCols[] colsArray = new BubbleCols[G_COLS_SLOT_BUFFER_SIZE];

        Dictionary<int, Indexer> _indexers = new Dictionary<int, Indexer>();

        //object _lock = new object();

        public void Init()
        {
            for(int i = 0; i < G_COLS_SLOT_BUFFER_SIZE; i++)
            {
                colsArray[i] = new BubbleCols();
            }
        }

        public bool AddIndexer( int id )
        {
            //lock (_lock)
            {
                if (_indexers.ContainsKey(id))
                    return false;

                _indexers.Add(id, new Indexer(id, this));
                return true;
            }
        }
        int GetMinOffSet()
        {
            int minOffset = G_COLS_SLOT_BUFFER_SIZE;

            foreach(Indexer indexer in _indexers.Values )
            {
                if(minOffset > indexer.Offset )
                {
                    minOffset = indexer.Offset;
                }
            }
            return minOffset;
        }

        int GetMaxOffSet()
        {
            int maxOffset = 0;

            foreach (Indexer indexer in _indexers.Values)
            {
                if (maxOffset < indexer.Offset)
                {
                    maxOffset = indexer.Offset;
                }
            }
            return maxOffset;
        }

        public BubbleCols Next(int indexerId )
        {
            int maxOffset = 0;

            //lock(_lock)
            {
                maxOffset= GetMaxOffSet();
                //재할당
                if( maxOffset >= G_COLS_SLOT_RE_SIZE_RANGE)
                {
                    int minOffset = GetMinOffSet();

                    Clean(minOffset, maxOffset);

                }

                return colsArray[_indexers[indexerId].Offset++];
            }
        }

        public void Clean( int minOffset , int maxOffset )
        {
            Array.Copy(colsArray, minOffset, colsArray, 0, colsArray.Length - minOffset);

            //재활당
            for( int i = colsArray.Length - minOffset; i<colsArray.Length; i++)
            {
                colsArray[i] = new BubbleCols();
            }

            foreach( Indexer indexer in _indexers.Values)
            {
                indexer.Offset -= minOffset;
            }

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (Indexer indexer in _indexers.Values)
            {
                sb.Append(indexer.ToString()).Append("\n");
            }

            int loopCnt = 0;
            foreach (BubbleCols bs in colsArray)
            {
                if (loopCnt++ != 0)
                    sb.Append("\n");

                sb.Append(bs.ToString());
            }

            return sb.ToString();
        }


    }
}
