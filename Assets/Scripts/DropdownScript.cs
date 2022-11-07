using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DropdownScript : MonoBehaviour
{
    public List<Vector3> liquids = new List<Vector3>(); //TODO: przeniesc do innego miejsca
    [SerializeField] private TMP_Dropdown dropdown;
 
    [SerializeField] private TextMeshProUGUI density;

    [SerializeField] private TextMeshProUGUI heatCapacity;

    [SerializeField] private DataCollector data;

    // Start is called before the first frame update
    void Start()
    {
        liquids.Add(new Vector3(1.2f, 713.0f, 0.00002f));
        liquids.Add(new Vector3(700.0f, 2000.0f, 0.1f));
        liquids.Add(new Vector3(995.0f, 4200.0f, 0.001f));
        data = FindObjectOfType<DataCollector>();
        dropdown.onValueChanged.AddListener((v) => {
            Debug.Log(v);
            density.text = liquids[v].x.ToString("0.00");
            heatCapacity.text = liquids[v].y.ToString("0.00");
            data.Collect();
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
