using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownScript : MonoBehaviour
{

    [SerializeField] 
    private TMP_Dropdown dropdown;
 
    [SerializeField] 
    private TextMeshProUGUI density;

    [SerializeField] 
    private TextMeshProUGUI heatCapacity;

    [SerializeField] 
    private DataCollector data;

    void Start()
    {
        dropdown.onValueChanged.AddListener((v) => {
            density.text = Constants.Instance.liquids[v].x.ToString("0.00");
            heatCapacity.text = Constants.Instance.liquids[v].y.ToString("0.00");
        });
    }
}
