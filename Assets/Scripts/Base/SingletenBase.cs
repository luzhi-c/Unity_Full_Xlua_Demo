using UnityEngine;

public class SingletenBase<T> : MonoBehaviour where T:SingletenBase<T>
{
    private static T m_Instance;

    public static T GetInstance() {

        if (m_Instance == null) {
            GameObject go = new GameObject(typeof(T).Name);
            m_Instance = go.AddComponent<T>();
            DontDestroyOnLoad(go);
            m_Instance.Init();
        }
        return m_Instance;
    }

    public virtual void Init() {

    }
}
