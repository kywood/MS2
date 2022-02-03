using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class UtilFile
{
    public static bool Exists(string filename)
    {
        string path = PathForDocumentsFile(filename);
        return File.Exists(path);
    }

    public static void WriteByteArrayToFile(string filename, byte[] byteData)
    {
        string path = PathForDocumentsFile(filename);
        FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
        file.Seek(0, SeekOrigin.Begin);
        file.Write(byteData, 0, byteData.Length);
        file.Close();
    }

    public static byte[] ReadByteArrayFromFile(string filename)
    {
        byte[] fileBytes = null;
        string path = PathForDocumentsFile(filename);

        if (File.Exists(path))
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);

            fileBytes = new byte[file.Length];
            file.Read(fileBytes, 0, fileBytes.Length);

            file.Close();
        }

        return fileBytes;
    }

    public static void WriteStringToFile(string filename, string str)
    {
        string path = PathForDocumentsFile(filename);
        FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);

        StreamWriter sw = new StreamWriter(file);
        sw.Write(str);
        sw.Close();
        file.Close();
    }


    public static string ReadStringFromFile(string filename, string defaultString = "")
    {
        string path = PathForDocumentsFile(filename);

        if (File.Exists(path))
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(file);

            string str = null;
            str = sr.ReadToEnd();

            sr.Close();
            file.Close();

            return str == null ? defaultString : str;
        }
        else
        {
            return defaultString;
        }
    }

    public static string PathForDocumentsFile(string filename)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
			string path = Application.persistentDataPath;
			return Path.Combine(path, filename);
			/*
            string path = Application.dataPath.Substring(0, Application.dataPath.Length - 5);
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(Path.Combine(path, "Documents"), filename);
			*/
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            string path = Application.persistentDataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            path = Path.Combine(path, "files");
            return Path.Combine(path, filename);
        }
        else
        {
            string path = Application.dataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(path, filename);
        }
    }
    public static bool StreamWrite( object Obj , string FileName )
    {
        //StreamWriter sWriter = new StreamWriter(Application.persistentDataPath + "/data.bin");
        StreamWriter sWriter = new StreamWriter(Application.persistentDataPath + FileName);
        BinaryFormatter bin = new BinaryFormatter();
        bin.Serialize(sWriter.BaseStream, Obj);
        sWriter.Close();
        return true;
    }

    public static bool StreamRead<T>( string FileName ,ref T ReadStream)
    {
        string path = Application.persistentDataPath + FileName;
        if (File.Exists(path) == false )
        {
            return false;
        }

        StreamReader sr = new StreamReader(path);
        BinaryFormatter bin = new BinaryFormatter();
        ReadStream = (T)bin.Deserialize(sr.BaseStream);
        sr.Close();
        return true;
    }

}
