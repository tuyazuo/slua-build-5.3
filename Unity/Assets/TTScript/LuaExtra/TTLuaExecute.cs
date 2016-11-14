using UnityEngine;
using System.Collections;

public class TTLuaExecute : MonoBehaviour {

    public string luaFilePath = string.Empty;

    void Start()
    {
        TTLuaMain.Instance.DoFile(luaFilePath);
    }
}
