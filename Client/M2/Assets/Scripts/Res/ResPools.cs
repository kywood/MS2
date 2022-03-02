using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MDefine;
using static Defines;
using static Player;
using static PlayerManager;

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
        private string _PrefabsPath;
        private string _PrefabsName;
        private int _CreateCount;

        public string PrefabsPath { get { return _PrefabsPath + "/" + _PrefabsName;  } }
        public string PrefabsName { get { return _PrefabsName;  } }
        public int CreateCount { get { return _CreateCount; } }

        public ResInfo(string path, string name, int c_count)
        {
            _PrefabsPath = path;
            _PrefabsName = name;
            _CreateCount = c_count;
        }
    }
    static partial class GConst
    {
        public static readonly List<ResInfo> ResPrefabs = new List<ResInfo>()
        {
            {
                new ResInfo("Prefabs", "NBubble", 140 )
                
            }
        };
    }
}

public class ResPools : SingletonMonoBehaviour<ResPools>
{


    //public GameObject bubbleParents;

    private Dictionary<E_PLAYER_TYPE , Dictionary<eResType, Pool>> PoolList = new Dictionary<E_PLAYER_TYPE, Dictionary<eResType, Pool>>();
    //public Dictionary<eResType, Pool> PoolList = new Dictionary<eResType, Pool>();

    private void AddResMap(E_PLAYER_TYPE player_type , eResType res_type , Pool pool )
    {
        if( PoolList.ContainsKey(player_type) )
        {
            PoolList[player_type].Add(res_type, pool);
        }
        else
        {
            Dictionary<eResType, Pool> pool_map = new Dictionary<eResType, Pool>()
            {
                {  res_type , pool }
            };

            PoolList.Add(player_type, pool_map);
        }

    }

    protected override void OnAwake()
    {
        for (int i = 0; i < (int)eResType.MAX; i++)
        {
            if( i == (int)eResType.Bubble)
            {
                foreach(Player p in PlayerManager.Instance.GetPlayers() )
                {
                    //GameObject newObj = new GameObject(((eResType)i).ToString());
                    GameObject newObj = Util.AddChild(p.BubblePool, new GameObject(((eResType)i).ToString()));
                    Pool newPool = newObj.AddComponent<Pool>();
                    newPool.MakePool(newObj, GConst.ResPrefabs[i].PrefabsPath, GConst.ResPrefabs[i].CreateCount);

                    if( p.PlayerType == E_PLAYER_TYPE.MY_PLAYER )
                    {
                        AddResMap(E_PLAYER_TYPE.MY_PLAYER, (eResType)i, newPool);
                    }
                    else
                    {
                        AddResMap(E_PLAYER_TYPE.PEER, (eResType)i, newPool);
                    }

                }
            }
            else
            {
                //GameObject newObj = new GameObject(((eResType)i).ToString());
                GameObject newObj = Util.AddChild( gameObject  , new GameObject(((eResType)i).ToString()));
                Pool newPool = newObj.AddComponent<Pool>();
                newPool.MakePool(newObj, GConst.ResPrefabs[i].PrefabsPath, GConst.ResPrefabs[i].CreateCount);
                AddResMap(E_PLAYER_TYPE.COMMON, (eResType)i, newPool);
            }

            
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

    public bool IsStopAllBubble(E_PLAYER_TYPE player_type)
    {
        Pool pool = GetPool(player_type, eResType.Bubble);

        foreach (int k in pool.ResList.Keys)
        {
            if ((pool.ResList[k].GetComponent<CSBubble>()).GetMoving() != E_MOVING_STATE.STOP)
            {
                return false;
            }
        }

        return true;
    }


    //public bool IsActiveAll(eResType resType , bool active )
    //{
    //    Pool pool = GetPool(resType);
    //    //if ( pool == null )
    //    //{
    //    //    return false;
    //    //}

    //    foreach( int k in pool.ResList.Keys)
    //    {
    //        if( pool.ResList[k].activeSelf != active)
    //        {
    //            return false;
    //        }
    //    }

    //    return true;

    //}

    public Pool GetPool(E_PLAYER_TYPE player_type , eResType res_type)
    {
        if (PoolList.ContainsKey(player_type))
        {
            if( PoolList[player_type].ContainsKey(res_type) )
            {
                return PoolList[player_type][res_type];
            }
        }

        return null;
    }
}
