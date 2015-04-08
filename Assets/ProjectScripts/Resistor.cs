using UnityEngine;
using System.Collections;

public class Resistor : MonoBehaviour {

	public float resistance;
    public EnumScript.ResistanceScale scale; 
	GameObject resistorObject;
	GameObject controller;


	void start ()
	{
		resistorObject = GameObject.FindGameObjectWithTag("ResistorObject");
        controller = GameObject.FindGameObjectWithTag("HandController");
		scale = EnumScript.ResistanceScale.Ohms;
	}

	public void setResistance(float resistanceValue)
	{
		resistance = resistanceValue;
	}

	public void setResistanceScale(EnumScript.ResistanceScale localScale)
	{
		scale = localScale;
	}

	public float GetResistanceInOhms()
	{
		if(scale == EnumScript.ResistanceScale.Ohms)
			return resistance;
		else
			return resistance * 1000;
	}
}
