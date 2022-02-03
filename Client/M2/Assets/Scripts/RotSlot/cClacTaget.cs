using System;
using System.Collections.Generic;
using System.Text;

namespace RotSlot
{

    public enum E_CALC_STATE
    {
        NONE = 0,
        COMPLETE = 1,
        MAX
    }

    public class cCalcTarget : cPoint<int>
    {
        // 상태
        E_CALC_STATE _calcState = E_CALC_STATE.NONE;

        int _key;

        public E_CALC_STATE calcState
        {
            get { return _calcState; }
            set { _calcState = value; }
        }

        public int Key
        {
            get { return _key; }
            set { _key = value; }
        }

        public cCalcTarget(int key , cPoint<int> pos)
            : base(pos.x, pos.y)
        {
            calcState = E_CALC_STATE.NONE;
            this.Key = key;
        }

        public cCalcTarget( cPoint<int> pos , E_CALC_STATE calc_state )
            :base(pos.x,pos.y)
        {
            calcState = calc_state;
        }

        public void SetComplete()
        {
            calcState = E_CALC_STATE.COMPLETE;
        }

        public bool SamePos( cPoint<int> pos )
        {
            if (x == pos.x && y == pos.y)
                return true;

            return false;
        }
    }

    class cCalcQueue
    {
        Dictionary< int , cCalcTarget> mCalcQueue = new Dictionary<int  , cCalcTarget>();
        public bool IsContain( cPoint<int> pos  )
        {
            foreach( int k in mCalcQueue.Keys )
            {
                if (mCalcQueue[k].SamePos(pos))
                    return true;
            }
            return false;
        }

        public Dictionary< int , cCalcTarget> GetQueue()
        {
            return mCalcQueue;
        }

        private int GetNextKey()
        {
            int keyMax = -1;
            foreach (int k in mCalcQueue.Keys)
            {
                if (k > keyMax)
                    keyMax = k;
            }
            return ++keyMax;
        }
        public int Add( cPoint<int> pos )
        {
            if(IsContain( pos ))
            {
                return -1;                
            }

            int key = GetNextKey();
            mCalcQueue.Add(key, new cCalcTarget(key ,pos));
            return key;
        }
        public bool IsComplete()
        {
            foreach (int k in mCalcQueue.Keys)
            {
                if (mCalcQueue[k].calcState != E_CALC_STATE.COMPLETE)
                    return false;
            }
            return true;
        }

        public cCalcTarget Pop()
        {
            foreach (int k in mCalcQueue.Keys)
            {
                if (mCalcQueue[k].calcState != E_CALC_STATE.COMPLETE)
                    return mCalcQueue[k];
            }

            return null;
        }

        public int GetCalcStateCount(E_CALC_STATE calcs_tate)
        {
            int retVal = 0;

            foreach (int k in mCalcQueue.Keys)
            {
                if (mCalcQueue[k].calcState == calcs_tate)
                    retVal++;
            }

            return retVal;
        }

    }
}
