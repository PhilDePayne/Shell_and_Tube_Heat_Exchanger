using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleScript : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private DataCollector data;

    // Start is called before the first frame update
    void Start()
    {
        data = FindObjectOfType<DataCollector>();
    }
}
