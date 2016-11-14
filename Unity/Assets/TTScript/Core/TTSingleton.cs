using UnityEngine;

public class TTSingleton<T> : MonoBehaviour where T : TTSingleton<T>
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("(singleton)" + typeof(T));
                instance = go.AddComponent<T>();
                instance.Init();
            }
            return instance;
        }
    }

    protected virtual void Init() { }

    void OnDestroy()
    {
        instance = null;
        Debug.Log(">>>>>> on destroy:" + gameObject.name);
    }
}