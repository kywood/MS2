using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CBezier : MonoBehaviour
{

    public Transform[] points;
    //Vector3[] blacks;  
    public float t;

    public GameObject target;
    //LineRenderer line_render;

    // Use this for initialization  
    void Start()
    {
        //this.line_render = gameObject.GetComponent<LineRenderer>();
        //this.line_render.SetVertexCount(32);
    }


    // 0.0 >= t <= 1.0 her be magic and dragons  
    public Vector3 GetPointAtTime(float t)
    {
        float u = 1f - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * this.points[0].position; //first term  
        p += 3 * uu * t * this.points[1].position; //second term  
        p += 3 * u * tt * this.points[2].position; //third term  
        p += ttt * this.points[3].position; //fourth term  

        return p;

    }


    Vector3 draw(float t)
    {
        Vector3[] blacks = new Vector3[this.points.Length];
        for (int i = 0; i < this.points.Length; ++i)
        {
            blacks[i] = this.points[i].position;
        }

        int count = this.points.Length - 1;
        while (count >= 1)
        {
            for (int i = 0; i < count; ++i)
            {
                blacks[i] = blacks[i] + (blacks[i + 1] - blacks[i]) * t;
            }

            --count;
        }

        return blacks[0];
    }


    void draw_line(Vector3 start, Vector3 to, Color color)
    {
        Debug.DrawLine(start, to, color);
    }


    //void draw_linerenderer()
    //{
    //    Vector3 prev = this.points[0].position;
    //    this.line_render.SetPosition(0, prev);
    //    for (int i = 0; i <= 30; ++i)
    //    {
    //        Vector3 to = draw(i / 30.0f);
    //        this.line_render.SetPosition(i + 1, to);
    //        prev = to;
    //    }
    //}


    // Update is called once per frame  
    void Update()
    {

        Vector3 prev = this.points[0].position;
        for (int i = 0; i <= 30; ++i)
        {
            //Vector3 to = draw(i / 30.0f);  
            Vector3 to = GetPointAtTime(i / 30.0f);
            draw_line(prev, to, Color.white);
            prev = to;
        }

        draw_line(this.points[0].position, this.points[1].position, Color.green);
        draw_line(this.points[2].position, this.points[3].position, Color.green);

        //draw_linerenderer();
    }
}