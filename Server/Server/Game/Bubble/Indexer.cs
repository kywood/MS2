using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Game
{
    class Indexer
    {
        public int Id 
        {
            get;
            set;
        }
        public int Offset { get; set; } = 0;

        BubbleMap _bubbleMap;

        public Indexer(int id , BubbleMap bubbleMap)
        {
            this.Id = id;
            _bubbleMap = bubbleMap;
        }

        public override string ToString()
        {
            return $"ID : {Id} Offset : {Offset}";
        }

    }
}
