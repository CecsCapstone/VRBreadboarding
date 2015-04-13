using UnityEngine;
using System.Collections;

public class SelectedObject : MonoBehaviour {
	public Light light;

	void Start () {
		light = GetComponent<Light> ();
		light.color = UnityEngine.Color.red;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TurnOnLight()
	{
		light.intensity = 2;
	}

	public void TurnOffLight()
	{
		light.intensity = 0;
	}

	public void Select()
	{
		light.color = UnityEngine.Color.green;
		TurnOnLight ();
	}

	public void Deselect()
	{
		light.color = UnityEngine.Color.red;
		TurnOffLight ();
	}
}
