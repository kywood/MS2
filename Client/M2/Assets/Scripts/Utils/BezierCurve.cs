using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class BezierCurve
{

    // 0.0 >= t <= 1.0 her be magic and dragons  
    public static Vector3 GetPointAtTime(Transform[] points , float t)
    {
        float u = 1f - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * points[0].position; //first term  
        p += 3 * uu * t * points[1].position; //second term  
        p += 3 * u * tt * points[2].position; //third term  
        p += ttt * points[3].position; //fourth term  

        return p;

    }

    public static Vector3 GetPointAtTimeLocal(List<Transform> points, float t)
    {
        float u = 1f - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * points[0].localPosition; //first term  
        p += 3 * uu * t * points[1].localPosition; //second term  
        p += 3 * u * tt * points[2].localPosition; //third term  
        p += ttt * points[3].localPosition; //fourth term  

        return p;
    }

    public static Vector3 GetPointAtTimeLocal(Transform[] points, float t)
    {
        float u = 1f - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * points[0].localPosition; //first term  
        p += 3 * uu * t * points[1].localPosition; //second term  
        p += 3 * u * tt * points[2].localPosition; //third term  
        p += ttt * points[3].localPosition; //fourth term  

        return p;

    }

}
