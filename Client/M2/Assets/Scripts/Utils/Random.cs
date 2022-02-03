using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Text;
using System;

public partial class Util
{

    public static int Rand(int min, int max)
    {
        return UnityEngine.Random.Range(min, max );
    }

    public static float Rand(float min, float max)
    {
        return UnityEngine.Random.Range(min, max);
    }


    //private static System.Random random = new System.Random();


    static public void ShuffleList<E>(ref List<E> randomList)
    {
        List<E> inputList = new List<E>();
        foreach (E e in randomList)
        {
            inputList.Add(e);
        }
        randomList.Clear();

        //System.Random r = new System.Random(rSeed);
        int randomIndex = 0;

        while (inputList.Count > 0)
        {
            randomIndex = Rand(0, inputList.Count);
            randomList.Add(inputList[randomIndex]);
            inputList.RemoveAt(randomIndex);
        }
    }

    static public void ShuffleList<E>(ref E[] randomList)
    {
        E[] inputList = new E[0];
        foreach (E e in randomList)
        {
            AddArray(ref inputList, e);
        }
        randomList = new E[0];

        //System.Random r = new System.Random(rSeed);
        int randomIndex = 0;

        while (inputList.Length > 0)
        {
            randomIndex = Rand(0, inputList.Length);
            AddArray(ref randomList, inputList[randomIndex]);
            RemoveArray(ref inputList, randomIndex);
        }
    }



    public static T GetRandomItem<T>(Dictionary<T, int> items)
    {
        int sum = 0;
        var em = items.GetEnumerator();
        while (em.MoveNext())
        {
            sum += em.Current.Value;
        }
        int cumulatedProbability = Rand(0,sum);

        foreach (var item in items)
            if ((cumulatedProbability -= item.Value) < 0)
                return item.Key;

        throw new InvalidOperationException();
    }

    public static T GetRandomItem<T>(Dictionary<T, float> items)
    {
        float sum = 0.0f;
        var em = items.GetEnumerator();
        while (em.MoveNext())
        {
            sum += em.Current.Value;
        }
        float cumulatedProbability = Rand(0.0f, sum);// random.Next(sum);

        foreach (var item in items)
            if ((cumulatedProbability -= item.Value) < 0)
                return item.Key;

        throw new InvalidOperationException();
    }

    public static int GetRandomItemIndex(float[] items)
    {
        float sum = 0.0f;

        for(int i = 0 ; i < items.Length ; i++)
        {
            sum += items[i];
        }

        float cumulatedProbability = Rand(0.0f, sum);// random.Next(sum);

        int idx = 0;
        foreach (var item in items)
        {
            if ((cumulatedProbability -= item) < 0)
            {
                return idx;
            }
            idx++;
        }

        throw new InvalidOperationException();
    }

    public static int GetRandomItemIndex(List<float> items)
    {
        float sum = 0.0f;
        var em = items.GetEnumerator();
        while (em.MoveNext())
        {
            sum += em.Current;
        }
        float cumulatedProbability = Rand(0.0f, sum);// random.Next(sum);

        int idx = 0;
        foreach (var item in items)
        {
            if ((cumulatedProbability -= item) < 0)
            {
                return idx;
            }
            idx++;
        }

        throw new InvalidOperationException();
    }

    public static bool RandRatio(float ratio)
    {
        float seed = Rand(0.0f, 100.0f);

        if (seed < ratio)
            return true;

        return false;
    }

    ////확률함수
    //public static bool RandRatio100(int ratio)
    //{
    //    int seed = Rand(0, 101);

    //    if (seed < ratio)
    //        return true;

    //    return false;
    //}
}
