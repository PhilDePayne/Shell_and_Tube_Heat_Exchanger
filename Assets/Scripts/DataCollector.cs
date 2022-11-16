using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataCollector : MonoBehaviour
{
    private const int SEGMENTS = 100;

    private const float P = Mathf.PI * 0.1f;

    //Reynolds number
    private const int Re = 2000;
    public List<Vector3> liquids = new List<Vector3>(); //TODO: DRY
    List<float> tco = new List<float>();
    List<float> tho = new List<float>();
    public TextMeshProUGUI tci_t;
    private float _tci;
    public TextMeshProUGUI thi_t;
    private float _thi;
    public TextMeshProUGUI dc_t;
    private float _dc;
    public TextMeshProUGUI dh_t;
    private float _dh;
    public TextMeshProUGUI cc_t;
    private float _cc;
    public TextMeshProUGUI ch_t;
    private float _ch;
    public TextMeshProUGUI l_t;
    private float _length;
    public TextMeshProUGUI r_t;
    private float _radius;
    public TextMeshProUGUI U_t;
    private float _U;
    public Toggle cf_t;
    private bool _counterflow;

    // Start is called before the first frame update
    void Start()
    {
        liquids.Add(new Vector3(1200.00f, 2400.00f, 1.0f));
        liquids.Add(new Vector3(700.0f, 2000.0f, 0.1f));
        liquids.Add(new Vector3(995.0f, 4200.0f, 0.001f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Collect(){

        Debug.Log("Collecting data");

        _tci = float.Parse(tci_t.text);
        _thi = float.Parse(thi_t.text);
        _dc = float.Parse(dc_t.text);
        _dh = float.Parse(dh_t.text);
        _cc = float.Parse(cc_t.text);
        _ch = float.Parse(ch_t.text);
        _length = float.Parse(l_t.text);
        _radius = float.Parse(r_t.text);
        _U = float.Parse(U_t.text);
        _counterflow = cf_t.isOn;

    }

    public void Calculate() {

        Collect();

        tco.Clear();
        tho.Clear();

        //calculate speed based on Reynolds number (<2000 for laminar flow)
        float vc = (Re * liquids[get_liquid(_dc)].z) / (_dc * (_radius * 2));
        float vh = (Re * liquids[get_liquid(_dh)].z) / (_dh * (_radius * 2));

        //calculate cross-sectional area of the pipe
        float S = Mathf.PI * _radius * _radius;

        //calculate heat capacity rate of both fluids
        float Cc = _cc * _dc * vc * S;
        float Ch = _ch * _dh * vh * S;

        //calculate deltas
        float delta_c = P * _U / Cc;
        float delta_h = (P + 0.001f) * _U / Ch;

        if(!_counterflow) {

            float C1p = delta_c * (_tci - _thi);
            float C2p = (_tci*delta_h + _thi*delta_c)/(delta_c + delta_h);

            for(float i = 0; i < SEGMENTS; i++) {

                tco.Add(((C1p*Mathf.Exp(-(i/SEGMENTS * _length)*(delta_h + delta_c)))/(delta_c + delta_h)) + C2p);

                tho.Add((-C1p*Mathf.Exp(-(i/SEGMENTS * _length)*(delta_h + delta_c))*delta_h/(delta_c*(delta_c + delta_h))) + C2p);

            }

        } else {

            float C1c = (delta_c*(delta_c - delta_h)*(_thi - _tci))/(delta_h - delta_c*Mathf.Exp(-_length*(delta_h - delta_c)));
            float C2c = (_tci*delta_h - _thi*delta_c*Mathf.Exp(-_length*(delta_h - delta_c)))/(delta_h - delta_c*Mathf.Exp(-_length*(delta_h - delta_c)));

            Debug.Log(C1c);
            Debug.Log(C2c);

            for(float i = 0; i < SEGMENTS; i++) {

                tco.Add((C1c*Mathf.Exp(-(i/SEGMENTS * _length)*(delta_h - delta_c))/(delta_c - delta_h)) + C2c);

                tho.Add(((C1c*Mathf.Exp(-(i/SEGMENTS * _length)*(delta_h - delta_c))*delta_h)/(delta_c*(delta_c - delta_h))) + C2c);

            }

        }

        int j = 0;

        foreach(float temperature in tco) {
            Debug.Log(j.ToString() + " " + temperature.ToString());
            j++;
        }

    }

    private int get_liquid(float density) { //TODO: tmp, usunac ze zmiana listy 'liquids'

        for(int i = 0; i < 3; i++){
            if(liquids[i].x == density)
            return i;
        }

        return -1;

    }
}
