using UnityEngine;
using System.Collections;

public class Resistor : MonoBehaviour {

	private int resistance;
	GameObject resistorObject;
	GameObject controller;

	void start ()
	{
		resistorObject = GameObject.FindGameObjectWithTag("ResistorObject");
        controller = GameObject.FindGameObjectWithTag("HandController");
	}

	public void setResistance(int resistance)
	{

	}
}
