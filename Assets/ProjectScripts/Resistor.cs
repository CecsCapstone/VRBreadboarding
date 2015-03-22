using UnityEngine;
using System.Collections;

public class Resistor : MonoBehaviour {

	private float resistance;
	GameObject resistorObject;
	GameObject controller;
	private EnumScript.ResistanceScale scale; 


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
			return resistance / 1000;
	}
}
