
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;
    using System;
    using System.IO;
    using System.Threading;
    using TinyTeam.Debuger;

public class TTYieldWWWW : CustomYieldInstruction
{
    public WWW www;
    private string m_error = string.Empty;
    private float timeout = 0f;
    float timer = 0;

    public string error
    {
        get { return m_error; }
    }

    public bool isError
    {
        get { return !string.IsNullOrEmpty(m_error); }
    }

    public byte[] bytes
    {
        get
        {
            if (www != null && string.IsNullOrEmpty(m_error))
            {
                return www.bytes;
            }
            else
            {
                return null;
            }
        }
    }

    public string text
    {
        get
        {
            if (www != null && string.IsNullOrEmpty(m_error))
            {
                return www.text;
            }
            else
            {
                return string.Empty;
            }
        }
    }

    public override bool keepWaiting
    {
        get { return Running(); }
    }

    public TTYieldWWWW(string url, float timeout)
    {
        this.www = new WWW(url);
        this.timeout = timeout <= 0f ? 10.0f : timeout;
        this.timer = 0f;
    }

    public bool Running()
    {
        // timeout control
        bool timeOut = false;
        if (!www.isDone)
        {
            if ( /*www.progress <= 0.0f && */timer > timeout)
            {
                timeOut = true;
                m_error = "www timeout:" + timeout;
            }
            else
            {
                timer += Time.deltaTime;
                return true;
            }
        }

        // when failed should dispose the www.
        if (timeOut)
        {
            try
            {
                www.Dispose();
            }
            catch (Exception e)
            {
                m_error = e.Message;
            }
        }
        else
        {
            if (!string.IsNullOrEmpty(www.error))
            {
                m_error = www.error;
                Debug.LogError("YieldWWW error=" + www.error);

                try
                {
                    www.Dispose();
                    www = null;
                }
                catch (Exception e)
                {
                    Debug.LogError("www.Dispose exception=" + e.Message);
                }
            }
        }

        return false;
    }

}