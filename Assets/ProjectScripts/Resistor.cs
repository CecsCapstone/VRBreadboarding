using UnityEngine;
using System.Collections;

public class Resistor : MonoBehaviour {

	public float resistance;
    public EnumScript.ResistanceScale scale; 


	void start ()
	{
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
