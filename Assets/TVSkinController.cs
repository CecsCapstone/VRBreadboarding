using UnityEngine;
using System.Collections;

public class TVSkinController : MonoBehaviour {

	// Use this for initialization

	public Material blackTv;
	public Material collinTv;
	public TextMesh voltageText;
	public TextMesh resistanceText;
	public TextMesh currentText;
	void Start () {
	
	}

	public void UpdateTV(float voltage,float resistance,float current)
	{
		MakeTvBlack();
		voltageText.text = "Voltage: " + voltage.ToString("0.##") + "V";
		resistanceText.text = "Res: " + resistance.ToString("0.##") + "Ohms";
		voltageText.text = "Current: " + current.ToString("0.##") + "Amps";
	}

	public void MakeTvBlack()
	{
		renderer.material = blackTv;
	}

	public void MakeTvCollin()
	{
		renderer.material = collinTv;
	}
	
	
}
