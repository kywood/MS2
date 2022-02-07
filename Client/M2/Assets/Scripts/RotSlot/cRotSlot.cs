using System;
using System.Collections.Generic;
using System.Text;

namespace RotSlot
{
    public interface IEntityID
    {
        public int GetID();
    }

    public abstract class cID : IEntityID
    {
        int mID;
        public cID(int id)
        {
            mID = id;
        }

        public int GetID()
        {
            return mID;
        }
    }

    public class cSlot<T> : cID where T : class
    {
        T mItem = null;
        cColsSlot<T> mParent;

        public cSlot(cColsSlot<T> parent , int id)
            : base(id)
        {
            mParent = parent;
        }
        public void Set(T item)
        {
            mItem = item;
        }

        public cColsSlot<T> GetParent()
        {
            return mParent;
        }

        

        public int GetParentID()
        {
            return mParent.GetID();
        }

        public T GetItem()
        {
            return mItem;
        }

        public void ReSet()
        {
            mItem = null;
        }
        public override string ToString()
        {
            if (mItem == null)
                return "E";

            return mItem.ToString();
        }
    }

    //odd-to-even 
    public enum E_COLS_TYPE
    {
        NONE = -1,
        ODD,      // 홀
        EVEN,     // 짝
        MAX
    }

    public class cColsSlot<T> : cID where T : class
    {
        List<cSlot<T>> mSlots = new List<cSlot<T>>();

        E_COLS_TYPE mColsType = E_COLS_TYPE.ODD;

        public cColsSlot(int id, int slot_nums)
            : base(id)
        {
            if (slot_nums % 2 == 0)
            {
                //짝수다
                mColsType = E_COLS_TYPE.EVEN;
            }

            for (int i = 0; i < slot_nums; i++)
            {
                mSlots.Add(new cSlot<T>( this , i));
            }
        }

        public E_COLS_TYPE GetColsType()
        {
            return mColsType;

            //if ( mSlots.Count % 2 == 0 )
            //{
            //    return E_COLS_TYPE.EVEN;
            //}

            //return E_COLS_TYPE.ODD;
        }

        public T GetItemByIDX(int slot_idx)
        {
            cSlot<T> slot = GetSlotByIDX( slot_idx);

            if( slot == null )
            {
                return null;
            }

            return slot.GetItem();

        }

        public cSlot<T> GetSlotByIDX(int slot_idx)
        {
            if( slot_idx < 0 || 
                slot_idx >= mSlots.Count )
            {
                return null;
            }

            //if( slot_idx >= mSlots.Count )
            //    return null;

            return mSlots[slot_idx];
        }

        public cSlot<T> GetSlotByID(int slot_id)
        {
            foreach (cSlot<T> slot in mSlots)
            {
                if (slot.GetID() == slot_id)
                    return slot;
            }

            return null;
        }

        public int GetCount()
        {
            return mSlots.Count;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("ID: ");
            sb.Append(GetID());
            sb.Append(" ");

            for (int i = 0; i < mSlots.Count; i++)
            {
                if (i != 0)
                    sb.Append(",");

                sb.Append(mSlots[i].ToString());
            }
            return sb.ToString();
        }
    }


    enum E_SLOT_CHK_DIR
    {
        LEFT = 0,
        RIGHT = 1,
        UP_LEFT = 2,
        UP_RIGHT = 3,
        DOWN_LEFT = 4,
        DOWN_RIGHT = 5,
        MAX
    }

    public class cRotSlot<T> where T : class
    {
        int mRowCnt;
        int mColCnt;

        cRotQueue<cColsSlot<T>> mRotQueue = null;

        public cRotSlot( int row_cnt , int cols_cnt )
        {
            Init( row_cnt , cols_cnt );
        }

        void Init(int row_cnt, int cols_cnt)
        {
            mRowCnt = row_cnt;
            mColCnt = cols_cnt;

            mRotQueue = new cRotQueue<cColsSlot<T>>(mRowCnt);
            MakeSlots();
        }

        void MakeSlots()
        {
            for (int i = 0; i < mRowCnt; i++)
            {
                cColsSlot<T> cs = new cColsSlot<T>(i, (i % 2 == 0) ? mColCnt : mColCnt - 1);
                mRotQueue.Set(i, cs);
            }
        }
        public cColsSlot<T> GetColsSlotByIDX(int idx)
        {
            return mRotQueue.GetItemByIDX(idx);
        }
        protected cColsSlot<T> GetColsSlotByIDX(cPoint<int> point)
        {
            return mRotQueue.GetItemByIDX(point.y);
        }

        protected cColsSlot<T> GetColsSlotByID(cPoint<int> point)
        {
            return mRotQueue.GetItemByID(point.y);
        }

        protected int GetColsSlotIDXByID(cPoint<int> point)
        {
            return mRotQueue.GetItemIDXByID(point.y);
        }

        public cPoint<int> ID2IDX(cPoint<int> point)
        {
            return mRotQueue.ID2IDX(point);
        }

        public int ID2IDX(int id)
        {
            return mRotQueue.ID2IDX(new cPoint<int>(0, id)).y;
        }

        public int GetColsSlotCount()
        {
            return mRotQueue.GetCount();
        }

        public cSlot<T> GetSlotByIDX(int x, int y)
        {
            return GetSlotByIDX(new cPoint<int>(x, y));
        }

        protected cSlot<T> GetSlotByID(int x, int y)
        {
            return GetSlotByID(new cPoint<int>(x, y));
            //cColsSlot<T> colsSlot = mRotQueue.GetItemByIDX(y);
            //return colsSlot == null ? null : colsSlot.GetSlotByIDX(x);
        }

        public cSlot<T> GetSlotByIDX(cPoint<int> point)
        {
            cColsSlot<T> colsSlot = GetColsSlotByIDX(point);// mRotQueue.GetItemByIDX(point.y);

            if (colsSlot == null)
                return null;

            cSlot<T> slot = colsSlot.GetSlotByIDX(point.x);

            if (slot == null)
            {
                return null;
            }

            return slot;
        }

        protected cSlot<T> GetSlotByID(cPoint<int> point)
        {
            cColsSlot<T> colsSlot = GetColsSlotByID(point);// mRotQueue.GetItemByIDX(point.y);

            if (colsSlot == null)
                return null;

            cSlot<T> slot = colsSlot.GetSlotByID(point.x);

            if (slot == null)
            {
                return null;
            }

            return slot;
        }

        //protected cSlot<T> GetSlot(int x, int y)
        //{
        //    cColsSlot<T> colsSlot = mRotQueue.GetItemByIDX(y);
        //    return colsSlot == null ? null : colsSlot.GetSlotByIDX(x);
        //}
        //protected cSlot<T> GetSlot(cPoint<int> point)
        //{
        //    cColsSlot<T> colsSlot = GetColsSlot(point);// mRotQueue.GetItemByIDX(point.y);
        //    return colsSlot == null ? null : colsSlot.GetSlotByIDX(point.x);
        //}

        public void SetItemByIDX(int x, int y, T item)
        {
            GetSlotByIDX(x, y).Set(item);
        }

        public void SetItemByIDX(cPoint<int> point, T item = null)
        {
            GetSlotByIDX(point.x, point.y).Set(item);
        }

        
        public void SetItemByID(cPoint<int> point, T item = null)
        {
            GetSlotByID(point.x, point.y).Set(item);
        }

        public void SetItemByID(int cols_id, int slot_id, T item)
        {
            try
            {
                mRotQueue.GetItemByID(cols_id).GetSlotByID(slot_id).Set(item);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return;
        }

        //public void Print()
        //{
        //    //Debug.Log("Ready OnEnter");

        //    Console.WriteLine(mRotQueue.ToString());
        //}


        public String ToString()
        {
            return mRotQueue.ToString();
        }


        public void ForWard()
        {
            mRotQueue.ForWard();
        }
        public void BackWard()
        {
            mRotQueue.BackWard();
        }
    }


}
