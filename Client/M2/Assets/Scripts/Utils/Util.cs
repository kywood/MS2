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
    const int POOL_TABLE_LENGTH = 1024;
    static int[] m_poolTable = new int[POOL_TABLE_LENGTH];
    static int[] m_poolTable2 = new int[POOL_TABLE_LENGTH];
    public static StringBuilder m_stringBuilder = new StringBuilder();
	static string[] currency = {
		"", "K", "M", "B", "T",
		"aa", "bb", "cc", "dd", "ee", "ff", "gg", "hh", "ii", "jj", "kk", "ll", "mm", "nn", "oo", "pp", "qq", "rr", "ss", "tt", "uu", "vv", "ww", "xx", "yy", "zz",
		"AA", "BB", "CC", "DD", "EE", "FF", "GG", "HH", "II", "JJ", "KK", "LL", "MM", "NN", "OO", "PP", "QQ", "RR", "SS", "TT", "UU", "VV", "WW", "XX", "YY", "ZZ",
		"aaa", "bbb", "ccc", "ddd", "eee", "fff", "ggg", "hhh", "iii", "jjj", "kkk", "lll", "mmm", "nnn", "ooo", "ppp", "qqq", "rrr", "sss", "ttt", "uuu", "vvv", "www", "xxx", "yyy", "zzz",
	};

    	
	public static string CurrencyValue(long number, int LiCount)
	{
		string strNum = number.ToString ();
		int index = (strNum.Length - 1) / 3;
		if (index < 0) {
			index = 0;
		}
		
		long Li = 0;
		for (int i = 0; i < index; i++)
		{
			Li = number % 1000;
			number = number / 1000;
		}
		
		if (index == 0) {
			return number.ToString ();
		} else {
			if (LiCount == 0)
			{
				return string.Format("{0}{1}", number.ToString(), currency[index]);
			}
			else if (LiCount == 1)
			{
				int numLi = (int)Li / 100;
				return string.Format("{0}.{1:0}{2}", number.ToString(), numLi, currency[index]);
			}
			else if (LiCount == 2)
			{
				int numLi = (int)Li / 10;
				return string.Format("{0}.{1:00}{2}", number.ToString(), numLi, currency[index]);
			}
			else
			{
				int numLi = (int)Li;
				return string.Format("{0}.{1:000}{2}", number.ToString(), numLi, currency[index]);
			}
		}
	}

    // Start is called before the first frame update

    //  For case 1 I use Camera.main.ScreenToWorldPoint(Input.mousePosition) and it seems to work okay for my purposes.
    //  For case 2 I use RectTransformUtility.WorldToScreenPoint(Camera.main, obj.transform.position);
    //  For case 3 I use Camera.main.ScreenToWorldPoint(rectTransform.transform.position) and it seems to give good results.


    public static Vector3 CanvasChildToWorldPosition(Camera cam, Vector3 pos)
    {
        Vector3 vpos = cam.ScreenToWorldPoint(pos);

        return vpos;
    }
    public static Vector3 WorldToCanavsPosition(Camera cam , Vector3 worldPos , GameObject canvas )
    {
        RectTransform CanvasRect = canvas.GetComponent<RectTransform>();

        //then you calculate the position of the UI element
        //0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0. Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.

        Vector2 ViewportPosition = cam.WorldToViewportPoint(worldPos);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

        //now you can set the position of the ui element
     
     //   Debug.Log($" X : {WorldObject_ScreenPosition.x} , Y:{WorldObject_ScreenPosition.y}");

        return new Vector3(WorldObject_ScreenPosition.x , WorldObject_ScreenPosition.y , 0);
    }


	public static void StringBuilderClear()
	{
		m_stringBuilder.Remove(0, m_stringBuilder.Length);
	}

    public static string StringFormat(string format, params object[] args)
    {
		StringBuilderClear ();
        return m_stringBuilder.AppendFormat(format, args).ToString();
    }

    public static string IntToCommaString(int number)
    {
        return number.ToString("n0");
    }

    public static float DeltaValue( float StartValue , float EndValue , float Flow , float Max)
    {
        return Mathf.Lerp(StartValue, EndValue, Flow / Max);
    }

    public static float FlowValue( float flowTime , float MaxTime = 1.0f , float MaxValue = 1.0f)
    {
        if( flowTime > MaxTime )
        {
            flowTime = MaxTime;
        }

        return MaxValue * flowTime / MaxTime;
    }

    public static DateTime DateTimeParse(string dateTime)
    {
        return DateTime.Parse(dateTime);
    }

    public static void InitTransform(Transform trans)
    {
        trans.localPosition = Vector3.zero;
        trans.localRotation = Quaternion.identity;
        trans.localScale = Vector3.one;
    }
    
    public static float Distance(GameObject a, GameObject b)
    {
        return Mathf.Abs(Vector3.Distance(a.transform.position, b.transform.position));
    }

    public static float Distance(Transform a, Transform b)
    {
        return Mathf.Abs(Vector3.Distance(a.position, b.position));
    }

    public static float Distance(Vector3 a, Vector3 b)
    {
        return (a - b).magnitude;
    }

    public static float Distance(Vector2 a, Vector2 b)
    {
        return Mathf.Abs(Vector2.Distance(a, b));
    }

    public static bool FloatEqual(float x, float y, float d)
    {
        return x - d <= y && y <= x + d;
    }

    public static Color NewColor( int hex_color )
    {
        float a = 1.0f;
        float r = 1.0f;
        float g = 1.0f;
        float b = 1.0f;

        //FFDB00

        r = (float)( (hex_color >> 16) & 0x0000FF ) / 255.0f;  // 00 00 FF
        g = (float)((hex_color >> 8) & 0x0000FF) / 255.0f;   // 00 FF DB and 00 00 FF      -->  FF & F0  =  F0
        b = (float)(hex_color & 0x0000FF) / 255.0f;   // 00 FF DB and 00 00 FF      -->  FF & F0  =  F0

        return new Color(r, g, b, a);
    }

    public static bool IsProbability100(float percent)
    {
        float rnd = Rand(0.0f, 100.0f);
        if (percent == 0.0f)
        {
            return false;
        }
        if (rnd <= percent)
        {
            return true;
        }
        return false;
    }

    public static bool IsProbability10000(int percent)
    {
        int rnd = Rand(0, 10000);
        if (percent == 0)
        {
            return false;
        }
        if (rnd <= percent)
        {
            return true;
        }
        return false;
    }

    public static bool IsProbability100000000(uint percent)
    {
        int rnd = Rand(0, 100000000);
        if (percent == 0)
        {
            return false;
        }
        if (rnd <= percent)
        {
            return true;
        }
        return false;
    }


    static public int GetMax(int[] priorities)
    {
        System.Array.Sort(priorities);
        return priorities[priorities.Length - 1];
    }

    //static public int _rSeed = (int)DateTime.Now.Ticks;

    //static public int rSeed
    //{
    //    get
    //    {
    //        if( _rSeed+1 >= int.MaxValue - 1)
    //        {
    //            _rSeed = 0;
    //        }
    //        return _rSeed++;
    //    }
    //}



    public static int GetPriority(int[] priorities)
    {
        int sum = 0;
        for (int i = 0; i < priorities.Length; ++i)
        {
            sum += priorities[i];
        }

        if (sum <= 0)
            return 0;

        int num = Rand(1, sum);

        sum = 0;
        for (int i = 0; i < priorities.Length; ++i)
        {
            int start = sum;
            sum += priorities[i];
            if (start < num && num <= sum)
            {
                return i;
            }
        }

        return 0;
    }

    public static int GetPriority(List<int> priorities)
    {
        return GetPriority(priorities.ToArray());
    }

    public static int RandArrayInt(int[] pick, int pickCount, int[] pool, int poolCount)
    {
        if (pickCount > poolCount)
        {
            pickCount = poolCount;
        }

        if (poolCount > 0 && pickCount > 0)
        {
            int tableCount = 0;
            for (int i = 0; i < pickCount; i++)
            {
                m_poolTable2[tableCount] = RandExceptIntArray(0, poolCount - 1, m_poolTable2, tableCount);
                //Util.Log("m_poolTable2[" + tableCount + "]:" + m_poolTable2[tableCount]);
                tableCount++;
            }

            for (int i = 0; i < pickCount; i++)
            {
                pick[i] = pool[m_poolTable2[i]];
            }
        }

        return pickCount;
    }

    public static int RandExceptIntArray(int start, int end, int[] exceptNumArray)
    {
        return RandExceptIntArray(start, end, exceptNumArray, exceptNumArray.Length);
    }

    public static int RandExceptIntArray(int start, int end, int[] exceptNumArray, int poolCount)
    {
        int count = end - start + 1;
        int num;
        int tableCount = 0;
        for (int i = 0; i < count; i++)
        {
            num = start + i;

            if (!IsIncludePool(num, exceptNumArray, poolCount))
            {
                //Util.Log("m_poolTable[" + tableCount + "]:" + m_poolTable[tableCount]);
                m_poolTable[tableCount] = num;
                tableCount++;
            }
        }

        if (tableCount == 0)
        {
            return 0;
        }

        int rnd = UnityEngine.Random.Range(0, tableCount);

        return m_poolTable[rnd];
    }

    public static int RandExceptInt(int start, int end, int exceptNum)
    {
        int count = end - start + 1;
        int num;
        int tableCount = 0;
        for (int i = 0; i < count; i++)
        {
            num = start + i;
            if (num != exceptNum)
            {
                m_poolTable[tableCount] = num;
                tableCount++;
            }
        }

        if (tableCount == 0)
        {
            return 0;
        }

        int rnd = UnityEngine.Random.Range(0, tableCount);

        return m_poolTable[rnd];
    }

    public static bool IsIncludePool(int num, int[] poolArray)
    {
        return IsIncludePool(num, poolArray, poolArray.Length);
    }
    public static bool IsIncludePool(int num, int[] poolArray, int poolCount)
    {
        bool ret = false;
        for (int i = 0; i < poolCount; i++)
        {
            //Util.Log("num:" + num + ", poolArray[" + i + "]:" + poolArray[i]);
            if (poolArray[i] == num)
            {
                ret = true;
                break;
            }
        }
        return ret;
    }

    static public GameObject AddChildWithOutScaleLayer(GameObject parent, GameObject child)
    {
        if (child == null)
        {
            return null;
        }
        //GameObject go = GameObject.Instantiate(prefab) as GameObject;
#if UNITY_EDITOR
        UnityEditor.Undo.RegisterCreatedObjectUndo(child, "Create Object");
#endif
        if (child != null && parent != null)
        {
            Transform t = child.transform;
            t.parent = parent.transform;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            //t.localScale = Vector3.one;
            //child.layer = parent.layer;
        }
        return child;
    }

    static public GameObject AddChildWithOutScale(GameObject parent, GameObject child)
    {
        if (child == null)
        {
            return null;
        }
        //GameObject go = GameObject.Instantiate(prefab) as GameObject;
#if UNITY_EDITOR
        UnityEditor.Undo.RegisterCreatedObjectUndo(child, "Create Object");
#endif
        if (child != null && parent != null)
        {
            Transform t = child.transform;
            t.parent = parent.transform;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            //t.localScale = Vector3.one;
            child.layer = parent.layer;
        }
        return child;
    }

    static public GameObject AddChild(GameObject parent, GameObject child)
    {
        if( child == null)
        {
            return null;
        }
        //GameObject go = GameObject.Instantiate(prefab) as GameObject;
#if UNITY_EDITOR
        UnityEditor.Undo.RegisterCreatedObjectUndo(child, "Create Object");
#endif
        if (child != null && parent != null)
        {
            Transform t = child.transform;
            t.parent = parent.transform;
            t.localPosition = Vector3.zero;
            t.localRotation = Quaternion.identity;
            t.localScale = Vector3.one;
            child.layer = parent.layer;
        }
        return child;
    }


    public static GameObject FindChildObject(GameObject go, string name)
    {
        foreach (Transform tr in go.transform)
        {
            if (tr.name.Equals(name))
            {
                return tr.gameObject;
            }
            else
            {
                GameObject find = FindChildObject(tr.gameObject, name);
                if (find != null)
                {
                    return find;
                }                    
            }
        }

        return null;
    }

    public static void ChAngleayersRecursively(Transform trans, string name)
    {
        trans.gameObject.layer = LayerMask.NameToLayer(name);
        foreach (Transform child in trans)
        {
            ChAngleayersRecursively(child, name);
        }
    }

    public static string TimeToString(float time)
    {
        if (time < 0)
        {
            time = 0;
        }

        int iTime = Mathf.FloorToInt(time);
        int iMin = iTime / 60;
        int iSec = iTime - iMin * 60;
        int hSec = Mathf.FloorToInt((time - iTime) * 100.0f);

        string text = Util.StringFormat("{0:00}:{1:00}:{2:00}", iMin, iSec, hSec);
        return text;
    }

    public static string DeviceUniqueIdentifier()
    {
        return SystemInfo.deviceUniqueIdentifier;
    }

    public static void ChangeShader(GameObject go, string name)
    {
        if (go == null)
        {
            return;
        }

        Renderer[] renderers = go.GetComponentsInChildren<Renderer>(true);

        if (renderers != null)
        {
            Shader newShader = Shader.Find(name);

            for (int i = 0; i < renderers.Length; i++)
            {
                if (renderers[i].materials != null)
                {
                    for (int j = 0; j < renderers[i].materials.Length; j++)
                    {
                        renderers[i].materials[j].shader = newShader;
                    }
                }
            }
        }
    }

    public static void RefreshShader(GameObject go)
    {
        if (go == null)
        {
            return;
        }

        Renderer[] renderers = go.GetComponentsInChildren<Renderer>(true);

        if (renderers != null)
        {
            for (int i = 0; i < renderers.Length; i++)
            {
                Material[] sharedMaterials = renderers[i].sharedMaterials;
                if (sharedMaterials != null)
                {
                    for (int j = 0; j < sharedMaterials.Length; j++)
                    {
                        if (sharedMaterials[j].shader != null)
                        {
                            string shaderName = sharedMaterials[j].shader.name;
                            Shader newShader = Shader.Find(shaderName);

                            if (newShader != null)
                            {
                                renderers[i].sharedMaterials[j].shader = newShader;
                            }
                        }
                    }
                }
            }
        }

    } 

    // class need [System.Serializable]
    public static T CopyData<T>(object src)
    {
        MemoryStream memStream = new MemoryStream();
        IFormatter formatter = new BinaryFormatter();
        formatter.Serialize(memStream, src);
        memStream.Seek(0, 0);
        return (T)formatter.Deserialize(memStream);
    }

    public static T ToEnum<T>(string str)
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        foreach (T t in A)
        {
            if (t.ToString() == str)
                return t;
        }
        return default(T);
    }

    public static string GetFirstDirectory(string path)
    {
        path = path.Remove(0, System.IO.Path.GetPathRoot(path).Length);	// Remove Root

        while (path.Contains("/") || path.Contains("\\"))
        {
            path = System.IO.Path.GetDirectoryName(path); // Remove One Depth
        }

        return path;
    }

    public static string GetFirstDirectoryName(string path)
    {
        char[] sep = new char[] { '\\', '/' };
        string[] array = path.Split(sep);
        if (array.Length == 0)
            return path;
        return array[0];
    }

    public static void Swap<T>(ref T lhs, ref T rhs)
    {
        T temp;
        temp = lhs;
        lhs = rhs;
        rhs = temp;
    }

    public static void Swap<T>(ref List<T> list, int i, int j)
    {
        T tmp = list[i];
        list[i] = list[j];
        list[j] = tmp;
    }

    public static int AddArray<T>(ref T[] array, T elem)
    {
        int len = array.Length;
        System.Array.Resize(ref array, len + 1);
        array[len] = elem;

        return len + 1;
    }

    public static void AddArray<T>(ref T[] array, T[] elem)
    {
        foreach (T e in elem)
        {
            AddArray(ref array, e);
        }
    }

    public static void AddArray<T>(ref T[] array, T[] elem, int len)
    {
        int cnt = 0;
        foreach (T e in elem)
        {
            AddArray(ref array, e);
            ++cnt;
            if (cnt >= len)
                break;
        }
    }

    public static void RemoveArray<T>(ref T[] array, int index)
    {
        T[] tmp = new T[array.Length];
        for (int i = 0; i < tmp.Length; ++i)
        {
            tmp[i] = array[i];
        }

        array = new T[0];
        for (int i = 0; i < tmp.Length; ++i)
        {
            if (i != index)
                AddArray(ref array, tmp[i]);
        }
    }

    public static void RemoveArray<T>(ref T[] array, int index, int count)
    {
        T[] tmp = new T[array.Length];
        for (int i = 0; i < tmp.Length; ++i)
        {
            tmp[i] = array[i];
        }

        array = new T[0];
        for (int i = 0; i < index; ++i)
        {
            AddArray(ref array, tmp[i]);
        }

        for (int i = index + count; i < tmp.Length; ++i)
        {
            AddArray(ref array, tmp[i]);
        }
    }

    public static T FindArray<T>(T[] array, T elem)
    {
        foreach (T t in array)
        {
            if (t.Equals(elem))
            {
                return t;
            }
        }
        return default(T);
    }

    public static bool CheckNum(string value)
    {
        foreach (char c in value)
        {
            if (System.Char.IsNumber(c))
            {
                return true;
            }
        }

        return false;
    }

    public static bool ByteToBool(byte value)
    {
        return value == 0 ? false : true;
    }

    public static float UIntToFloatSec(uint sec1000)
    {
        return (float)sec1000 / 1000.0f;
    }

    public static T ParseAsEnum<T>(string value)
    {
        System.Type templateType = typeof(T);
        if (!string.IsNullOrEmpty(value))
        {
            if (!CheckNum(value))
            {
                if (templateType.IsEnum)
                {
                    try
                    {
                        return (T)System.Enum.Parse(templateType, value);
                    }
                    catch
                    {
                        Util.Log("Error : Found not " + templateType.ToString() + "(" + value + ")");
                        return default(T);
                    }
                }
            }
        }

        Util.Log("Error : " + templateType.ToString() + "(" + value + ")");
        return default(T);
    }

    // 콘솔창의 Log를 지운다.
    public static void ClearLog()
    {
#if UNITY_EDITOR
        var logEntries = System.Type.GetType("UnityEditorInternal.LogEntries,UnityEditor.dll");
        var clearMethod = logEntries.GetMethod("Clear", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public);
        clearMethod.Invoke(null, null);
#endif
    }

    public static void OpenFolder(string fullPath)
    {
#if UNITY_EDITOR
        System.Diagnostics.Process.Start("explorer.exe", fullPath);
#endif
    }

    public static void CopyFolder(string sourceFolder, string destFolder)
    {
#if UNITY_EDITOR
        if (!Directory.Exists(destFolder))
        {
            Directory.CreateDirectory(destFolder);
        }
        string[] files = Directory.GetFiles(sourceFolder);
        string[] folders = Directory.GetDirectories(sourceFolder);

        foreach (string file in files)
        {
            string name = Path.GetFileName(file);
            string dest = Path.Combine(destFolder, name);
            File.Copy(file, dest);
        }

        foreach (string folder in folders)
        {
            string name = Path.GetFileName(folder);
            string dest = Path.Combine(destFolder, name);
            CopyFolder(folder, dest);
        }
#endif
    }

    public static void Log(object message)
    {
#if UNITY_EDITOR
        Debug.Log("[Log]:" + message);
#endif
    }

    public static void LogWarning(object message)
    {
#if UNITY_EDITOR
        Debug.LogWarning("[LogWarning]:" + message);
#endif
    }

    public static void LogError(object message)
    {
#if UNITY_EDITOR
        Debug.LogError("[LogError]:" + message);
#endif
    }

    public static void LogState(object message)
    {
#if UNITY_EDITOR
        Debug.Log("[State]" + message);
#endif
    }

    public static void LogPacket(object message)
    {
#if UNITY_EDITOR
        Debug.Log("[Packet]" + message);
#endif
    }

    public static void Assert(bool condition, string msg = "")
    {
#if UNITY_EDITOR
        if (!condition)
        {
            if (msg != "")
            {
                Util.Log(msg);
            }

            throw new Exception();
        }
#endif
    }

    public static bool AssertIf(bool expr, string msg = "")
    {
        Util.Assert(expr, msg);
        if (!expr)
            return false;

        return true;
    }

    public static GameObject RemoveComponent<T>(GameObject gameObject) where T : MonoBehaviour
    {
        T obj = gameObject.GetComponent<T>();
        if (obj == null)
            return null;

        UnityEngine.Object.Destroy(obj);
        obj = null;

        return gameObject;
    }
}
