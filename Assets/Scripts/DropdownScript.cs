using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownScript : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown dropdown;
 
    [SerializeField] private TextMeshProUGUI density;

    [SerializeField] private TextMeshProUGUI heatCapacity;

    // Start is called before the first frame update
    void Start()
    {
        dropdown.onValueChanged.AddListener((v) => {
            density.text = "test";
            heatCapacity.text = "test";
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
