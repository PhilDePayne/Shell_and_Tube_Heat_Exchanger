using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour
{
    public List<Vector3> liquids = new List<Vector3>();

    public static Constants Instance { get; private set; }
    private void Awake() 
    { 
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this;
        } 
    }

    void Start()
    {
        liquids.Add(new Vector3(1200.00f, 2400.00f, 1.0f));
        liquids.Add(new Vector3(700.0f, 2000.0f, 0.1f));
        liquids.Add(new Vector3(995.0f, 4200.0f, 0.001f));
    }
}
