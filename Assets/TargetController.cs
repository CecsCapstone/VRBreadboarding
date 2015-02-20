using UnityEngine;
using System.Collections;

public class TargetController : MonoBehaviour {
	public Light light;
	GameObject controller;
	GameObject instantiated;

	// Use this for initialization
	void Start () 
	{
		light = GetComponent<Light> ();
		controller = GameObject.FindGameObjectWithTag ("HandController");
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		Collider[] close_things = Physics.OverlapSphere (transform.position, .075f, -1);
		GameObject selected = null;
		for(int i = 0; i < close_things.Length; i++)
		{
			//Debug.Log(close_things[i].ToString());
			if(close_things[i].name == "palm")
			{
				selected = controller.GetComponent<ClosestObjectFinder>().selected;
				if(selected != null && instantiated == null)
				{
					light.intensity = 1;
					Instantiate(selected,transform.position,Quaternion.Euler(0,0,0));
					instantiated = selected;
				}
				else if (selected != null && selected.name != instantiated.name)
				{
					Destroy(instantiated);
					Instantiate(selected,transform.position,Quaternion.Euler(0,0,0));
					instantiated = selected;
				}
			}
		}
	}
}
