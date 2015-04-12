using UnityEngine;
using System.Collections;

public class VoltageSourceObject : MonoBehaviour {

    private float voltage;
    public TextMesh text;
	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
	
	}
    public void SetVoltage(float voltage)
    {
        this.voltage = voltage;
        text.text = voltage.ToString("0.##V");
        //Debug.Log(voltage);
    }
	public float GetVoltage()
	{
		return voltage;
	}
}
