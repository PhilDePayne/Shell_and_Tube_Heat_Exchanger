using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using System.Text;

public class DataCollector : MonoBehaviour
{
    FileStream fs;

    private List<GameObject> hotCylinders = new List<GameObject>();
    private List<GameObject> coldCylinders = new List<GameObject>();
    private const int SEGMENTS = 100;

    private const float P = Mathf.PI * 0.1f;

    //Reynolds number
    private const int Re = 2000;
    List<float> tco = new List<float>();
    List<float> tho = new List<float>();

    //=== UI DATA ===//
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
    public TMP_Dropdown hotFluid_d;
    private int _hotFluid;
    public TMP_Dropdown coldFluid_d;
    private int _coldFluid;

    // Start is called before the first frame update
    void Start()
    {
        SpawnCylinders();

        fs = File.Open("data.txt", FileMode.Create, FileAccess.Write);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Collect(){

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
        _hotFluid = hotFluid_d.value;
        _coldFluid = coldFluid_d.value;

    }

    public void Calculate() {

        Collect();

        tco.Clear();
        tho.Clear();

        //calculate speed based on Reynolds number (<2000 for laminar flow)
        float vc = (Re * Constants.Instance.liquids[_coldFluid].z) / (_dc * (0.1f * 2));
        float vh = (Re * Constants.Instance.liquids[_hotFluid].z) / (_dh * (0.1f * 2));

        //calculate cross-sectional area of the pipe
        float S = Mathf.PI * _radius * _radius;

        //calculate heat capacity rate of both fluids
        float Cc = _cc * _dc * vc * S;
        float Ch = _ch * _dh * vh * S;

        //calculate deltas
        float delta_c = P * _U / Cc;
        float delta_h = (P + 0.1f) * _U / Ch;

        if(!_counterflow) {

            float C1p = delta_c * (_tci - _thi);
            float C2p = (_tci*delta_h + _thi*delta_c)/(delta_c + delta_h);

            for(float i = 0; i < SEGMENTS; i++) {

                tco.Add(((C1p*Mathf.Exp(-(i/SEGMENTS * _length/2)*(delta_h + delta_c)))/(delta_c + delta_h)) + C2p);

                tho.Add((-C1p*Mathf.Exp(-(i/SEGMENTS * _length/2)*(delta_h + delta_c))*delta_h/(delta_c*(delta_c + delta_h))) + C2p);

            }

        } else {

            float C1c = (delta_c*(delta_c - delta_h)*(_thi - _tci))/(delta_h - delta_c*Mathf.Exp(-_length*(delta_h - delta_c)));
            float C2c = (_tci*delta_h - _thi*delta_c*Mathf.Exp(-_length*(delta_h - delta_c)))/(delta_h - delta_c*Mathf.Exp(-_length*(delta_h - delta_c)));

            for(float i = 0; i < SEGMENTS; i++) {

                tco.Add((C1c*Mathf.Exp(-(i/SEGMENTS * _length/2)*(delta_h - delta_c))/(delta_c - delta_h)) + C2c);

                tho.Add(((C1c*Mathf.Exp(-(i/SEGMENTS * _length/2)*(delta_h - delta_c))*delta_h)/(delta_c*(delta_c - delta_h))) + C2c);

            }

        }

        int j = 0;

        foreach(float temperature in tho) {
            byte[] info = new UTF8Encoding(true).GetBytes(temperature.ToString() + '\n');
            fs.Write(info, 0, info.Length);
            j++;
        }

        byte[] infoa = new UTF8Encoding(true).GetBytes(" " + '\n');
            fs.Write(infoa, 0, infoa.Length);

        foreach(float temperature in tco) {
            byte[] info = new UTF8Encoding(true).GetBytes(temperature.ToString() + '\n');
            fs.Write(info, 0, info.Length);
            j++;
        }

        StartCoroutine(ColorPipes());
    }

    private IEnumerator ColorPipes() {

        int i = 0;

        while(i < 100) {

            Color customColor = new Color(2 * (tho[i]/100), 2 * (1 - (tho[i]/100)), 0.0f, 1.0f);
            hotCylinders[i].GetComponent<Renderer>().material.SetColor("_Color", customColor);

            customColor = new Color(2 * (tco[i]/100), 2 * (1 - (tco[i]/100)), 0.0f, 1.0f);
            coldCylinders[i].GetComponent<Renderer>().material.SetColor("_Color", customColor);

            i++;

            yield return new WaitForSeconds(0.005f);

        }

    }

    private void SpawnCylinders() {

        for(int i = 0; i < 100; i++) {

            GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            cylinder.transform.localScale = new Vector3(21.0f, 1.05f, 21.0f);
            cylinder.transform.position = new Vector3(280.0f + (float)i * 2, 170.0f, -27.0f);
            cylinder.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
            hotCylinders.Add(cylinder);

            cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            cylinder.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.black);
            cylinder.transform.localScale = new Vector3(74.0f, 1.05f, 100.0f);
            cylinder.transform.position = new Vector3(280.0f + (float)i * 2, 170.0f, 17.0f);
            cylinder.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 90.0f);
            coldCylinders.Add(cylinder);

        }

    }

    void OnApplicationQuit()
    {
        fs.Close();
    }
}
