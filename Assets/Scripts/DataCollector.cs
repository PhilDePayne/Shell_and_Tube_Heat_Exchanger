using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataCollector : MonoBehaviour
{
    public TextMeshProUGUI Tci_t;
    private float Tci;
    public TextMeshProUGUI Thi_t;
    private float Thi;
    public TextMeshProUGUI dc_t;
    private float dc;
    public TextMeshProUGUI dh_t;
    private float dh;
    public TextMeshProUGUI cc_t;
    private float cc;
    public TextMeshProUGUI ch_t;
    private float ch;
    public TextMeshProUGUI l_t;
    private float length;
    public TextMeshProUGUI r_t;
    private float radius;
    public TextMeshProUGUI U_t;
    private float U;
    public Toggle cf_t;
    private bool counterflow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Collect(){

        Debug.Log("Collecting data");

        Tci = float.Parse(Tci_t.text);
        Thi = float.Parse(Thi_t.text);
        dc = float.Parse(dc_t.text);
        dh = float.Parse(dh_t.text);
        cc = float.Parse(cc_t.text);
        ch = float.Parse(ch_t.text);
        length = float.Parse(l_t.text);
        radius = float.Parse(r_t.text);
        U = float.Parse(U_t.text);
        counterflow = cf_t.isOn;

    }
}
