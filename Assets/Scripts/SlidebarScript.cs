using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlidebarScript : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private DataCollector data;

    // Start is called before the first frame update
    void Start()
    {
        data = FindObjectOfType<DataCollector>();
        slider.onValueChanged.AddListener((v)=>{
            Debug.Log(v);
            text.text = v.ToString("0.00");
            data.Collect();
        });
    }

    // Update is called once per frame
    void Update()
    {
        if(data == null) Debug.Log("null");
    }
}
