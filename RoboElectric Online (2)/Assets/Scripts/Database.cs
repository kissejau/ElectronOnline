using System;
using System.IO;
using System.Collections.Generic;
using System.Data.SqlClient;
using UnityEngine;
using Mirror;

public class Database : NetworkBehaviour
{
    public static string GetAppDataPath()
    {
        return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    }
    public static void WriteToFile(string FileName, string data)
    {
        if (!Directory.Exists(GetAppDataPath() + @"\SubFolder"))
        {
            Directory.CreateDirectory(GetAppDataPath() + @"\SubFolder");
            print("dir not exists, created");
        }
        else
        {
            print("dir exists");
        }
        if (!File.Exists(GetAppDataPath() + @"\SubFolder" + FileName))
        {
            File.Create(GetAppDataPath() + @"\SubFolder" + FileName).Close();
            print("file not exists, created");
        }
        {
            print("file exists");
        }
        var path = (GetAppDataPath() + @"\SubFolder" + FileName);
        StreamWriter sw = new StreamWriter(path, false);
        sw.Write(data);
        sw.Close();
    }
    public static string ReadFromFile(string FileName)
    {
        if (!Directory.Exists(GetAppDataPath() + @"\SubFolder"))
        {
            Directory.CreateDirectory(GetAppDataPath() + @"\SubFolder");
            print("dir not exists, created");
        }
        else
        {
            print("dir exists");
        }
        if (!File.Exists(GetAppDataPath() + @"\SubFolder" + FileName))
        {
            File.Create(GetAppDataPath() + @"\SubFolder" + FileName).Close();
            print("file not exists, created");
        }
        {
            print("file exists");
        }
        var path = (GetAppDataPath() + @"\SubFolder" + FileName);
        StreamReader sr = new StreamReader(path);
        var ret = sr.ReadToEnd();
        sr.Close();
        return ret;
    }
}

