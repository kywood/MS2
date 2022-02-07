using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RotSlot
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

    public class cBubbleHelper
    {
        //public static cBubble Factory(cBubble cbubble)
        //{
        //    if (type == E_BUBBLE_TYPE.RED)
        //        return new cBubbleRed(id);
        //    else if (type == E_BUBBLE_TYPE.BLUE)
        //        return new cBubbleBlue(id);
        //    else if (type == E_BUBBLE_TYPE.YELLOW)
        //        return new cBubbleYellow(id);
        //    else if (type == E_BUBBLE_TYPE.GREEN)
        //        return new cBubbleGreen(id);
        //    else if (type == E_BUBBLE_TYPE.PURPLE)
        //        return new cBubblePurple(id);

        //    return null;
        //}


        public static cBubble Factory(cPoint<int> id , E_BUBBLE_TYPE type = E_BUBBLE_TYPE.NONE )
        {
            if(type == E_BUBBLE_TYPE.NONE)
            {
                type = ConstData.GetNextBubbleType();

                //type = (E_BUBBLE_TYPE)UnityEngine.Random.Range((int)E_BUBBLE_TYPE.NONE + 1,
                //                                                (int)E_BUBBLE_TYPE.MAX - 1);
            }

            if (type == E_BUBBLE_TYPE.RED)
                return new cBubbleRed(id);
            else if (type == E_BUBBLE_TYPE.BLUE)
                return new cBubbleBlue(id);
            else if (type == E_BUBBLE_TYPE.YELLOW)
                return new cBubbleYellow(id);
            else if (type == E_BUBBLE_TYPE.GREEN)
                return new cBubbleGreen(id);
            else if (type == E_BUBBLE_TYPE.PURPLE)
                return new cBubblePurple(id);
            else if (type == E_BUBBLE_TYPE.SPECIAL)
                return new cBubblePurple(id);

            return null;
        }
    }

    public  class cBubble
    {
        E_BUBBLE_TYPE mType;

        cPoint<int> mID;

        protected void SetBubbleType (E_BUBBLE_TYPE type , cPoint<int> id)
        {
            mType = type;
            mID = id;

            //Debug.Log("===" + mType + " ========== ");
        }

        //HACK
        public cBubble()
        {

        }

        public cBubble(cBubble cb)
        {
            this.mType = cb.mType;
            mID = cb.mID;
        }


        public override string ToString()
        {
            return mType.ToString();
        }

        public E_BUBBLE_TYPE GetBubbleType()
        {
            return mType;
        }

        public bool IsSameType( cBubble bubble )
        {
            if (bubble == null)
                return false;

            if (mType == bubble.GetBubbleType())
                return true;

            return false;
        }

        public cPoint<int> GetID()
        {
            return mID;
        }

        public bool IsEqID(cPoint<int> id)
        {
            if(mID.x == id.x && mID.y == id.y)
            {
                return true;
            }

            return false;
        }
    }

    public class cBubbleRed : cBubble
    {
        public cBubbleRed(cPoint<int> id)
        {
            SetBubbleType(E_BUBBLE_TYPE.RED , id);
        }
    }
    public class cBubbleBlue : cBubble
    {
        public cBubbleBlue(cPoint<int> id)
        {
            SetBubbleType(E_BUBBLE_TYPE.BLUE , id);
        }
               
    }

    class cBubbleYellow : cBubble
    {
        public cBubbleYellow(cPoint<int> id)
        {
            SetBubbleType(E_BUBBLE_TYPE.YELLOW , id);
        }
    }

    public class cBubbleGreen : cBubble
    {
        public cBubbleGreen(cPoint<int> id)
        {
            SetBubbleType(E_BUBBLE_TYPE.GREEN , id);
        }
    }

    public class cBubblePurple : cBubble
    {
        public cBubblePurple(cPoint<int> id)
        {
            SetBubbleType(E_BUBBLE_TYPE.PURPLE , id);
        }
    }

    public class cBubbleSpecial : cBubble
    {
        public cBubbleSpecial(cPoint<int> id)
        {
            SetBubbleType(E_BUBBLE_TYPE.SPECIAL, id);
        }
    }

}
