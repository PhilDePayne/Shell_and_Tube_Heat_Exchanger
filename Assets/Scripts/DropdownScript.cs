using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

struct Pair<T, U>{

    public T x;
    public U y;

    public Pair(T x, U y){
        this.x  = x;
        this.y = y;
    }

}

public class DropdownScript : MonoBehaviour
{
    List<Pair<float, float>> liquids = new List<Pair<float, float>>();
    [SerializeField] private TMP_Dropdown dropdown;
 
    [SerializeField] private TextMeshProUGUI density;

    [SerializeField] private TextMeshProUGUI heatCapacity;

    [SerializeField] private DataCollector data;

    // Start is called before the first frame update
    void Start()
    {
        liquids.Add(new Pair<float, float>(1.2f, 713.0f));
        liquids.Add(new Pair<float, float>(700.0f, 2000.0f));
        liquids.Add(new Pair<float, float>(995.0f, 4200.0f));
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
