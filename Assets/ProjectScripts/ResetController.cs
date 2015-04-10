using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResetController : MonoBehaviour {

	public List<TargetController> targets;
	public Connector power;
	public Connector ground;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void reset()
	{
		foreach(var target in targets)
		{
			target.instantiated = null;
			if(target.connectors.Contains(power))
			{
				foreach(var connector in target.connectors)
				{
					if(connector.name != "Power")
						target.connectors.Remove(connector);
						Destroy(connector);
				}
			}
			else if(target.connectors.Contains(ground))
			{
				foreach(var connector in target.connectors)
				{
					if(connector.name != "Ground")
						target.connectors.Remove(connector);
						Destroy(connector);
				}
			}

			else
			{
				foreach(var connector in target.connectors)
				{
					Destroy(connector);
				}
					target.connectors.Clear();
			}
			target.connectors.Clear();
		}
	}
}
