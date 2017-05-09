using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class FileManager
{
    private static readonly string DEFAULT_PASS = "LIGA_FACENS";

    public static bool SaveData(string content, string path, string fileName)
    {
        try
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);


            File.WriteAllText(path + fileName, content, System.Text.Encoding.UTF8);

            Debug.Log("Arquivo gravado em " + (path + fileName));

            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Fail to write file: " + e.Message);
            return false;
        }
    }

    public static bool SaveData(string content, string path, string fileName, bool encrypt)
    {
        try
        {
            if (encrypt)
                content = AES.Encrypt(content, DEFAULT_PASS);
            return SaveData(content, path, fileName);
        }
        catch (Exception e)
        {
            Debug.LogError("Fail to write file: " + e.Message);
            return false;
        }
    }

    public static void Delete(string path, string file)
    {
        try
        {
            if (File.Exists(path + file))
            {
                File.Delete(path + file);
            }
        }
        catch
        {
            //todo - nothing is necessary....
        }
    }

    internal static void SaveData(string serialized, string cACHE_DIR, object lEAGUE_TEAMS_CACHED)
    {
        throw new NotImplementedException();
    }

    public static string LoadData(string path, string fileName)
    {
        try
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            if (!File.Exists(path + fileName))
                return "";

            return File.ReadAllText(path + fileName, System.Text.Encoding.UTF8);
        }
        catch (Exception e)
        {
            Debug.LogError("Fail to load: " + e.Message);
            return string.Empty;
        }
    }

    public static string LoadData(string path, string fileName, bool isEncrypted)
    {
        string data = LoadData(path, fileName);

        if (isEncrypted)
        {
            try
            {
                if (!string.IsNullOrEmpty(data))
                {
                    return AES.Decrypt(data, DEFAULT_PASS);
                }

                return "";
            }
            catch
            {
                if (!string.IsNullOrEmpty(data))
                    return data;
                else
                    return string.Empty;
            }
        }

        return data;
    }

    public static bool CheckIfFileExistsInPath(string path, string fileName)
    {
        return File.Exists(path + fileName);
    }

    /// <summary>
    ///  Returns how many files exist
    /// </summary>
    /// <param name="path"></param>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static int LoadCountFile(string path, string fileName)
    {
        int count = 0;
        bool hasDiretory = Directory.Exists(path);
        if (!hasDiretory)
        {
            Directory.CreateDirectory(path);
            return 0;
        }
        count++;
        while (File.Exists(path + fileName + count))
            count++;

        return count;
    }

    public static byte[] ConvertToByteArray(object obj)
    {
        byte[] bytes;

        IFormatter formatter = new BinaryFormatter();
        using (MemoryStream stream = new MemoryStream())
        {
            formatter.Serialize(stream, obj);
            bytes = stream.ToArray();
        }

        return bytes;
    }

    public static object ConvertToObject(byte[] bytes)
    {
        IFormatter formatter = new BinaryFormatter();
        object myObject;

        using (MemoryStream stream = new MemoryStream(bytes))
        {
            myObject = formatter.Deserialize(stream);
        }

        return myObject;
    }

}
