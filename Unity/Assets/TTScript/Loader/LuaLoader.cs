using UnityEngine;
using System.Collections;
using System.IO;
using TinyTeam.Debuger;

public class LuaLoader {


    public static byte[] LoadFile(string fileName)
    {
        if (Application.isEditor)
        {
            if (true)
            {
                string path = "Assets/Lua/" + fileName.Replace('.', '/') + ".lua";
                string fullPath = Application.dataPath + "/Lua/" + fileName.Replace('.', '/') + ".lua";
                if (!File.Exists(fullPath)) return null;
                byte[] ret = File.ReadAllBytes(path);
                if (ret.Length == 0)
                {
                    TTDebuger.LogError("Cant Load .lua : " + path);
                }
                return ret;
            }
            //else
            //{
                
            //}
        }
        return null;
    }
}
