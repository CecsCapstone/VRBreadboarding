using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ElectricalController : MonoBehaviour 
{

	public AudioController audioController;
	public TVSkinController tvSkinController;
    VoltageSourceObject voltageSource;
    TargetController voltageTarget;
    Connector power;
    bool isTurnedOn; 

	// Use this for initialization
	void Start () 
    {
        voltageSource = GameObject.FindGameObjectWithTag("VoltageSource").GetComponent<VoltageSourceObject>();
        voltageTarget = GameObject.FindGameObjectWithTag("VoltageTarget").GetComponent<TargetController>();
        power = voltageTarget.connectors[0];

        isTurnedOn = false;
	}
	
    public void TurnOn()
    {
        isTurnedOn = true;
        Analyze();
    }

    public void TurnOff()
    {
        isTurnedOn = false;
		tvSkinController.MakeTvCollin();
        turnOffLEDs();
    }

    public bool IsTurnedOn()
    {
        return isTurnedOn;
    }

    private void Analyze()
    {
		int iterations = 0;
        List<GameObject> componentsPath = new List<GameObject>();
        List<TargetController> targetsWithLEDs = new List<TargetController>();
        TargetController currentTarget;
        TargetController previousTarget;
        previousTarget = power.end;
		if(previousTarget.connectors.Count < 2)
		{
            audioController.playClip(EnumScript.CustomAudioClips.failHorn);
			tvSkinController.MakeTvCollin();
			return;
		}
        if(previousTarget.instantiated != null)
        {
            componentsPath.Add(previousTarget.instantiated);
            if (previousTarget.instantiated.GetComponent<LED>() != null)
            {
                targetsWithLEDs.Add(previousTarget);
            }
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
                        if (currentTarget.instantiated.GetComponent<LED>() != null)
                        {
                            targetsWithLEDs.Add(currentTarget);
                        }
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
			tvSkinController.MakeTvCollin();
			return;
		}

		float totalResistance = 0;
		List<GameObject> LEDInCircuit = new List<GameObject>();
		float totalCurrent = 0;

        foreach(var comp in componentsPath)
        {
			if(comp.GetComponent<LED>() != null)
			{
				LEDInCircuit.Add(comp);
			}
			else
			{
				//it's probably dangerous to assume it's a resistor if it isn't an LED but that's all we got :)
				Resistor currentResistor = comp.GetComponent<Resistor>();
				totalResistance += currentResistor.GetResistanceInOhms();
			}
			Debug.Log(comp.name); 
		}


        totalCurrent = totalResistance == 0 ? Mathf.Infinity : voltageSource.GetVoltage() / totalResistance;
        Debug.Log(totalResistance);
        Debug.Log(voltageSource.GetVoltage());
        Debug.Log(totalCurrent);

        if (LEDInCircuit.Count != 0)
        {
            if (totalCurrent > LEDInCircuit[0].GetComponent<LED>().LEDExplode)
            {
                //explode LED here
                //LEDInCircuit[0].GetComponentInChildren<ParticleEmitter>().Simulate(-2);
                //LEDInCircuit[0].GetComponentInChildren<ParticleEmitter>().Emit();
                foreach (var target in targetsWithLEDs)
                {
                    target.instantiated.GetComponent<Animator>().Play("ExplodeLED");
                    audioController.playClip(EnumScript.CustomAudioClips.explosion);
                    target.instantiated = null;
                }
            }
            else if (totalCurrent > LEDInCircuit[0].GetComponent<LED>().threshold)
            {
                turnOnLEDs(LEDInCircuit, totalCurrent);
                audioController.playClip(EnumScript.CustomAudioClips.dingDing);
				tvSkinController.UpdateTV(voltageSource.GetVoltage(),totalResistance,totalCurrent);
            }
            iterations = 0;
        }
        else
        {
            audioController.playClip(EnumScript.CustomAudioClips.failHorn);
			tvSkinController.MakeTvCollin();
        }
        
        
    }

	private void turnOnLEDs(List<GameObject> LEDsInCircuit, float totalCurrent)
	{
		foreach(var LED in LEDsInCircuit)
		{
			Light LEDLight = LED.GetComponent<LED>().pointLight;
			LEDLight.color = Color.red;
			LEDLight.intensity = 8; //arbitrary intensity
		}
	}

    private void turnOffLEDs()
    {
        List<GameObject> LEDLights = new List<GameObject> (GameObject.FindGameObjectsWithTag("LEDLight"));
        foreach (var LEDLight in LEDLights)
        {
            LEDLight.GetComponent<Light>().intensity = 0;
        }
    }
}
