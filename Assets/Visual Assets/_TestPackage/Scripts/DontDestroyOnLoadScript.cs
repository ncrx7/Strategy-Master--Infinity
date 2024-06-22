using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    private static DontDestroyOnLoad instance;

    // Singleton pattern to ensure only one instance is created
    public static DontDestroyOnLoad Instance
    {
        get { return instance; }
    }

    public int levelconstant = 1;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            // Prevents the creation of multiple instances
            Destroy(gameObject);
            return;
        }

        instance = this;

        // Don't destroy the GameObject when loading new scenes
        DontDestroyOnLoad(gameObject);
    }

    // Other functions and properties of the singleton class can be added here
}

