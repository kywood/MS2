using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDefine;
using static Defines;

namespace MDefine
{
    public enum eResType
    {
        None = -1,

        Bubble,

        MAX
    }

    public class ResInfo
    {
        public string PrefabsPath { get; set; }
        public int CreateCount { get; set; }
    }
    static partial class GConst
    {
        public static readonly List<ResInfo> ResPrefabs = new List<ResInfo>()
        {
            {
                new ResInfo()
                {
                    PrefabsPath = "Prefabs/NBubble",
                    CreateCount = 70,
                }
            }
        };
    }
}

public class ResPools : SingletonMonoBehaviour<ResPools>
{
    public Dictionary<eResType, Pool> PoolList = new Dictionary<eResType, Pool>();
    protected override void OnAwake()
    {
        for (int i = 0; i < (int)eResType.MAX; i++)
        {
            //GameObject newObj = new GameObject(((eResType)i).ToString());
            GameObject newObj = Util.AddChild(gameObject, new GameObject(((eResType)i).ToString()));
            Pool newPool = newObj.AddComponent<Pool>();
            newPool.MakePool(newObj, GConst.ResPrefabs[i].PrefabsPath, GConst.ResPrefabs[i].CreateCount);
            PoolList.Add((eResType)i, newPool);
        }
    }

    //protected override void OnStart()
    //{

    //}

    //public void Destory()
    //{

    //}

    //bool CheckPaticleType(eResType ParticleResType)
    //{
    //    if (ParticleResType >= eResType.Paticle_Block &&
    //        ParticleResType <= eResType.Paticle_Ball)
    //    {
    //        return true;
    //    }

    //    return false;
    //}

    //public bool EmiitPaticle(eResType ParticleResType, Vector3 vPos, Color color)
    //{
    //    if (CheckPaticleType(ParticleResType) == false)
    //    {
    //        return false;
    //    }

    //    Pool pool = GetPool(ParticleResType);
    //    GameObject Paticle = pool.GetAbleObject();
    //    if (Paticle == null)
    //    {
    //        return false;
    //    }
    //    Paticle.transform.position = vPos;
    //    Paticle.GetComponent<ParticleSystem>().startColor = color;
    //    return true;
    //}

    public bool IsStopAllBubble()
    {
        Pool pool = GetPool(eResType.Bubble);

        foreach (int k in pool.ResList.Keys)
        {
            if ((pool.ResList[k].GetComponent<CSBubble>()).GetMoving() != E_MOVING_STATE.STOP)
            {
                return false;
            }
        }

        return true;
    }


    public bool IsActiveAll(eResType resType , bool active )
    {
        Pool pool = GetPool(resType);
        //if ( pool == null )
        //{
        //    return false;
        //}

        foreach( int k in pool.ResList.Keys)
        {
            if( pool.ResList[k].activeSelf != active)
            {
                return false;
            }
        }

        return true;

    }

    public Pool GetPool(eResType resType)
    {
        if (PoolList.ContainsKey(resType))
        {
            return PoolList[resType];
        }

        return null;
    }
}
