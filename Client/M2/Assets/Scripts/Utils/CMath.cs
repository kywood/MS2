using UnityEngine;
using System.Collections;

public static class CMath
{
    public static Vector2 ConvertV3toV2(Vector3 v3)
    {
        return new Vector2(v3.x, v3.y);
    }

    public static float GetAngle(Vector2 v1, Vector2 v2)
    {
        return GetAngle(v1.x, v1.y, v2.x, v2.y);
    }

    public static float GetAngle(float x1, float y1, float x2, float y2)
    {
        float dx = x2 - x1;
        float dy = y2 - y1;

        return AdjustAngle(Mathf.Atan2(dy, dx) * Mathf.Rad2Deg);

        //float rad= Mathf.Atan2(dy, dx);
        //float degree = (rad * 180.0f) / Mathf.PI;
        //return degree;
    }
    public static bool IsAdjustAngle(float Angle)
    {
        if (Angle < 0.0f || Angle > 360.0f)
        {
            return true;
        }
        return false;
    }

    public static float AdjustAngle(float Angle)
    {
        float RetAngle = Angle;
        if (Angle < 0.0f)
        {
            RetAngle = 360.0f - ( (360.0f + Mathf.Abs(Angle)) % 360.0f) ;
        }
        else if (Angle >= 360.0f)
        {
            RetAngle = (360.0f + Angle) % 360.0f;
        }

        //if (IsAdjustAngle(RetAngle))
        //{
        //    RetAngle = AdjustAngle(RetAngle);
        //}

        return RetAngle;
    }


    // HACK 20200812
    //public static float AdjustAngle(float Angle)
    //{
    //    float RetAngle = Angle;
    //    if (Angle < 0.0f)
    //    {
    //        RetAngle = 360.0f + Angle;
    //    }
    //    else if (Angle >= 360.0f)
    //    {
    //        RetAngle = Angle - 360.0f;
    //    }

    //    if (IsAdjustAngle(RetAngle))
    //    {
    //        RetAngle = AdjustAngle(RetAngle);
    //    }

    //    return RetAngle;
    //}

    public static Vector2 AngleToPoint2(float Angle)
    {
        return new Vector2(Mathf.Cos(Angle) * Mathf.Deg2Rad , Mathf.Sin(Angle) * Mathf.Deg2Rad);
    }

    public static Vector3 AngleToPoint3(float Angle)
    {
        return new Vector3(Mathf.Cos(Angle) * Mathf.Deg2Rad, Mathf.Sin(Angle) * Mathf.Deg2Rad , 0f);
    }
}
