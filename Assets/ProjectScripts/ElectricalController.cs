using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElectricalController : MonoBehaviour 
{

	public AudioController audioController;

    VoltageSourceObject voltageSource;
    List<GameObject> componentsPath;

    TargetController currentTarget;
    TargetController previousTarget;
    TargetController voltageTarget;
    List<Connector> currentTargetConnectors;
    Connector power;
    Connector ground;
    bool isTurnedOn; 

	// Use this for initialization
	void Start () 
    {
        voltageSource = GameObject.FindGameObjectWithTag("VoltageSource").GetComponent<VoltageSourceObject>();
        voltageTarget = GameObject.FindGameObjectWithTag("VoltageTarget").GetComponent<TargetController>();
        currentTargetConnectors = voltageTarget.connectors;
        power = currentTargetConnectors[0];
        ground = currentTargetConnectors[1];

        isTurnedOn = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {

	}

    public void TurnOn()
    {
        isTurnedOn = true;
        Analyze();
    }

    public void TurnOff()
    {
        isTurnedOn = false;
    }

    public bool IsTurnedOn()
    {
        return isTurnedOn;
    }

    private void Analyze()
    {
		int iterations = 0;
        componentsPath = new List<GameObject>();
        //currentTargetConnectors = power.end.connectors;
        previousTarget = power.end;
		if(!FirstTargetHasTwoConnectors())
		{
			return;
		}
        if(previousTarget.instantiated != null)
        {
            componentsPath.Add(previousTarget.instantiated);
        }
        if( previousTarget.connectors[1].end != previousTarget)
        {
            currentTarget = previousTarget.connectors[1].end;
        }
        else 
        {
            currentTarget = previousTarget.connectors[1].start;
        }
        while (currentTarget != voltageTarget && iterations < 20)
        {
            foreach (var connector in currentTarget.connectors)
            {
                if (connector.start != previousTarget && connector.end != previousTarget)
                {
                    if (currentTarget.instantiated != null)
                    {
                        componentsPath.Add(currentTarget.instantiated);
                    }
                    if (connector.start == currentTarget)
                    {
                        previousTarget = currentTarget;
                        currentTarget = connector.end;
                    }
                    else
                    {
                        previousTarget = currentTarget;
                        currentTarget = connector.start;
                    }
                   
                }
            }
            iterations++;
        }

		if(iterations == 20)
		{
			audioController.playClip(EnumScript.CustomAudioClips.failHorn);
			return;
		}

		float totalResistance = 0;
		List<LED> LEDInCircuit = new List<LED>();
		float totalCurrent = 0;

        foreach(var comp in componentsPath)
        {
			if(comp.GetComponent<LED>() != null)
			{
				LEDInCircuit.Add(comp.GetComponent<LED>());
			}
			else
			{
				//it's probably dangerous to assume it's a resistor if it isn't an LED but that's all we got :)
				Resistor currentResistor = comp.GetComponent<Resistor>();
				totalResistance = currentResistor.GetResistanceInOhms();
			}
			Debug.Log(comp.name); 
		}

		totalCurrent = voltageSource.GetVoltage() / totalResistance;
		
		if(totalCurrent > LEDInCircuit[0].LEDExplode)
		{
			//explode LED here
		}
		else if(totalCurrent > LEDInCircuit[0].threshold)
		{
			turnOnLEDs(LEDInCircuit, totalCurrent);
			audioController.playClip(EnumScript.CustomAudioClips.dingDing);
		}
        Debug.Log(componentsPath);
        iterations = 0;
        
    }

	private bool FirstTargetHasTwoConnectors()
	{
		return previousTarget.connectors.Count == 2;
	}

	private void turnOnLEDs(List<LED> LEDsInCircuit, float totalCurrent)
	{
		foreach(var LED in LEDsInCircuit)
		{
			Light LEDLight = LED.GetComponent<Light>();
			LEDLight.color = Color.red;
			LEDLight.intensity = totalCurrent * .5f; //arbitrary intensity

		}
	}
}
