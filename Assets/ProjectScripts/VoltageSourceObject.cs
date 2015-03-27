using UnityEngine;
using System.Collections;

public class VoltageSourceObject : MonoBehaviour {

    private float voltage;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void SetVoltage(float voltage)
    {
        this.voltage = voltage;
        //Debug.Log(voltage);
    }
}
