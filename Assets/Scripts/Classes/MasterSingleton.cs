using UnityEngine;
using Debug = UnityEngine.Debug;

/// <summary>
/// Inherit from this component to easily create a singleton gameobject.
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class MasterSingleton<T> : MonoBehaviour where T : MasterSingleton<T>
{
    private static T _instance;

    /// <summary>
    /// Get the instance of this singleton.
    /// </summary
    public static T Instance
    {        
        get
        {            
            if (_instance == null)
            {
                Debug.LogWarning($"No instance of {typeof(T).Name} present in scene");                                
            }
            return _instance;
        }
    }

    public static bool HasInstance
    {        
        get { return _instance != null; }
    }

    private void Register()
    {
        if (_instance != null)
        {
            Debug.Log($"More than one singleton object of type {typeof(T).Name} exists.");

            // Check if gameobject only contains Transform and this component
            if (GetComponents<Component>().Length == 2)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(this);
            }

            return;
        }

        _instance = (T)this;
    }

    // If no other monobehaviour request the instance in an awake function
    // executing before this one, no need to search the object.
    protected virtual void Awake()
    {
        Register();
    }

    protected virtual void OnEnable()
    {
        // In case of code-reload, this should restore the single instance
        if (_instance == null)
        {
            Register();
        }
    }
}