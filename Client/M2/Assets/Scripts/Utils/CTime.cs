using UnityEngine;
using System.Collections;
using System.Text;


public partial class Util
{
    public static void Pause()
    {
        Time.timeScale = 0.0f;
    }

    public static void Resume()
    {
        Time.timeScale = 1.0f;
    }

    public static int GetNowTime()
    {
        System.DateTime now = System.DateTime.Now.ToLocalTime();
        System.TimeSpan span = (now - new System.DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
        int nowTime = (int)span.TotalSeconds;

        return nowTime;
    }

    public static string SecToReverseHH24MISSS(int span_sec , int max_sec)
    {
        int rsec = max_sec - span_sec;

        System.TimeSpan t = System.TimeSpan.FromSeconds(rsec);

        string ret = string.Format("{0:D2}:{1:D2}:{2:D2}",
                            t.Hours,
                            t.Minutes,
                            t.Seconds);

        return ret;

    }

}

