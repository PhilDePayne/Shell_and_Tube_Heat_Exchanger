using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SlidebarScript : MonoBehaviour
{
    [SerializeField] private Slider slider;

    [SerializeField] private TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener((v)=>{
            text.text = v.ToString("0.00");
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
