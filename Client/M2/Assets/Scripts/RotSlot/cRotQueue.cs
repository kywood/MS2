using System;
using System.Collections.Generic;
using System.Text;

namespace RotSlot
{


    //   T   cColsSlot<T>
    public class cRotQueue<T> where T : class  , IEntityID 
    {
        T[] mQueue;

        public cRotQueue( int size )
        {
            mQueue = new T[size];
        }
        public void Set( int idx , T val )
        {
            mQueue[idx] = val;
        }


        public int GetCount()
        {
            return mQueue.Length;
        }

        
        public T GetItemByID(int id)
        {
            for( int i = 0; i < mQueue.Length; i++ )
            {
                if (mQueue[i].GetID() == id)
                    return mQueue[i];
            }

            return null;
        }

        public int GetItemIDXByID(int id)
        {
            for (int i = 0; i < mQueue.Length; i++)
            {
                if (mQueue[i].GetID() == id)
                    return i;
            }

            throw new IndexOutOfRangeException();
        }

        public cPoint<int> ID2IDX(cPoint<int> point)
        {
            for (int i = 0; i < mQueue.Length; i++)
            {
                if (mQueue[i].GetID() == point.y)
                    return new cPoint<int>(point.x, i);
            }

            throw new IndexOutOfRangeException();
        }

        // T    cColsSlot<T>
        public T GetItemByIDX(int idx)
        {
            if (idx < 0 || idx >= mQueue.Length)
                return null;

            return mQueue[idx];
        }



        public void ForWard()
        {
            T last = mQueue[mQueue.Length - 1];

            for (int i = mQueue.Length - 1; i > 0; i--)
            {
                mQueue[i] = mQueue[i - 1];
            }

            mQueue[0] = last;
        }

        public void BackWard()
        {
            T first = mQueue[0];

            for (int i = 0; i < mQueue.Length - 1; i++)
            {
                mQueue[i] = mQueue[i + 1];
            }

            mQueue[mQueue.Length - 1] = first;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for( int i = 0; i < mQueue.Length; i++ )
            {
                if (i != 0)
                    sb.Append("\n");

                sb.Append("IDX : " + i + " V : " + mQueue[i].ToString());
            }

            return sb.ToString();
        }


        
    }
}
