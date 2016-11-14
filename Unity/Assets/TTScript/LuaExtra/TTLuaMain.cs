using System;
using UnityEngine;
using System.Collections;
using LuaInterface;
using SLua;
using TinyTeam.Debuger;

public class TTLuaMain : TTSingleton<TTLuaMain>
{
    LuaSvr luasvr = null;
    LuaFunction LuaGobalBrocast;

    private bool m_isBind;
    private string m_dofile;
    protected override void Init()
    {
        TTDebuger.Log("init");
        DontDestroyOnLoad(gameObject);

        float to = Time.realtimeSinceStartup;
        LuaState.loaderDelegate = LuaLoader.LoadFile;
        luasvr = new LuaSvr();
        luasvr.init(null, () =>
        {
            TTDebuger.Log("Game Start Cost:" + (Time.realtimeSinceStartup - to) + " s");
            OnBindComplete();
        }, LuaSvrFlag.LSF_3RDDLL);
    }

    void OnApplicationFocus(bool focusStatus) { }

    void Update()
    {
        for (int i = 0; i < updateList.size;)
        {
            if (updateList[i].isEnd())
            {
                updateList.RemoveAt(i);
            }
            else
            {
                updateList[i].Update();
                i++;
            }
        }
    }

    void OnBindComplete()
    {
        m_isBind = true;

        //LuaDLL.lua_pushcfunction(luasvr.luaState.L, new LuaCSFunction(push_cjson));
        //LuaDLL.lua_call(luasvr.luaState.L, 0, 1);
        //LuaDLL.lua_setglobal(luasvr.luaState.L, "cjson");

        //LuaDLL.lua_pushcfunction(luasvr.luaState.L, new LuaCSFunction(push_lpeg));
        //LuaDLL.lua_call(luasvr.luaState.L, 0, 1);
        //LuaDLL.lua_setglobal(luasvr.luaState.L, "lpeg");

        //LuaDLL.lua_pushcfunction(luasvr.luaState.L, new LuaCSFunction(push_sproto_core));
        //LuaDLL.lua_call(luasvr.luaState.L, 0, 1);
        //LuaDLL.lua_setglobal(luasvr.luaState.L, "sprotocore");

        ///DO main.lua
        luasvr.luaState.doFile("main");
        LuaGobalBrocast = luasvr.luaState.getFunction("LuaGobalBrocast");

        if (!string.IsNullOrEmpty(m_dofile) && m_dofile != "main")
            DoFile(m_dofile);
    }


    public void DoFile(string file)
    {
        if (!m_isBind)
        {
            m_dofile = file;
            return;
        }
        if (luasvr != null && luasvr.luaState != null && !string.IsNullOrEmpty(file))
        {
            luasvr.luaState.doFile(file);
        }
        else
        {
            TTDebuger.LogError("Cant DoFile:" + file + ",Lua not ready.");
        }
    }

    public LuaState GetLuaState()
    {
        return luasvr != null ? luasvr.luaState : null;
    }

    #region messenger event to lua

    public void BrocastToLua(string eventName)
    {
        //TTDebuger.Log("C# BrocastToLua:" + eventName);
        if (LuaGobalBrocast != null)
        {
            LuaGobalBrocast.call(eventName);
        }
        else
        {
            TTDebuger.LogError("LuaGobalBrocast is Null.");
        }
    }

    public void BrocastToLua(string eventName, object arg1)
    {
        //TTDebuger.Log("C# BrocastToLua:" + eventName);
        if (LuaGobalBrocast != null)
        {
            LuaGobalBrocast.call(eventName, arg1);
        }
        else
        {
            TTDebuger.LogError("LuaGobalBrocast is Null.");
        }
    }

    public void BrocastToLua(string eventName, object arg1, object arg2)
    {
        //TTDebuger.Log("C# BrocastToLua:" + eventName);
        if (LuaGobalBrocast != null)
        {
            LuaGobalBrocast.call(eventName, arg1, arg2);
        }
        else
        {
            TTDebuger.LogError("LuaGobalBrocast is Null.");
        }
    }

    public void BrocastToLua(string eventName, object arg1, object arg2, object arg3)
    {
        //TTDebuger.Log("C# BrocastToLua params:" + eventName);
        if (LuaGobalBrocast != null)
        {
            LuaGobalBrocast.call(eventName, arg1, arg2, arg3);
        }
        else
        {
            TTDebuger.LogError("LuaGobalBrocast is Null.");
        }
    }

    public void BrocastToLua(string eventName, object arg1, object arg2, object arg3, object arg4)
    {
        //TTDebuger.Log("C# BrocastToLua params:" + eventName);
        if (LuaGobalBrocast != null)
        {
            LuaGobalBrocast.call(eventName, arg1, arg2, arg3, arg4);
        }
        else
        {
            TTDebuger.LogError("LuaGobalBrocast is Null.");
        }
    }

    #endregion

    #region UpdateLogic delegate

    public BetterList<UpdateLogic> updateList = new BetterList<UpdateLogic>();

    public interface UpdateLogic
    {
        bool isEnd();
        void Update();
    }

    public class LuaUpdate : UpdateLogic
    {
        public LuaFunction func;
        public bool isEnded = false;

        private LuaUpdate() { }

        public LuaUpdate(LuaFunction f)
        {
            this.func = f;
        }

        public bool isEnd()
        {
            return isEnded;
        }

        public void Update()
        {
            if (func != null)
            {
                isEnded = !((bool)func.call());
            }
            else
            {
                isEnded = true;
            }
        }
    }

    /// <summary>
    /// 添加一个Update回调给lua里面
    /// 需要每次回调都返回true or false来判断是否继续执行
    /// </summary>
    public void AddUpdate(LuaFunction func)
    {
        updateList.Add(new LuaUpdate(func));
    }


    #endregion
}
