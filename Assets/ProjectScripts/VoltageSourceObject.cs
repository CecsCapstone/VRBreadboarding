using UnityEngine;
using System.Collections;

public class VoltageSourceObject : MonoBehaviour {

    private float voltage;
    private TextMesh text;
	// Use this for initialization
	void Start () {
        text = GameObject.FindGameObjectWithTag("VoltageText").GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void SetVoltage(float voltage)
    {
        this.voltage = voltage;
        text.text = voltage.ToString("0.##V");
        //Debug.Log(voltage);
    }
}
