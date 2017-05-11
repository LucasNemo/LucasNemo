using UnityEngine;
using System.Collections;

public abstract class SingletonBehaviour<T> : MonoBehaviour 
    where T : MonoBehaviour
{
    private static T m_instance = null;

    public static T Instance
    {
        get
        {

            if (m_instance == null)
            {
                m_instance =  (T) FindObjectOfType(typeof(T));

                if ( m_instance == null)
                {
                    var instanceName =  "__" + typeof(T).ToString();
                    var go = new GameObject(instanceName);
                    m_instance = go.AddComponent<T>();
                    DontDestroyOnLoad(go);
                }
            }

            return m_instance;
        }
    }

    void OnApplicationQuit()
    {
        m_instance = null;
    }

    public void Remove()
    {
        Destroy(gameObject);
        OnApplicationQuit();
    }

}
