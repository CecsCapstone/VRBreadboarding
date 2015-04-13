using UnityEngine;
using System.Collections;

public class TVSkinController : MonoBehaviour {

	public Material blackTv;
	public Material collinTv;
	public TextMesh voltageText;
	public TextMesh resistanceText;
	public TextMesh currentText;

	public void UpdateTV(float voltage,float resistance,float current)
	{
		MakeTvBlack();
		voltageText.text = "Voltage: " + voltage.ToString("0.##") + "V";
		resistanceText.text = "Res: " + resistance.ToString("0.##") + "Ohms";
		voltageText.text = "Current: " + current.ToString("0.##") + "Amps";
	}

	private void MakeTvBlack()
	{
		if(renderer.material != blackTv)
		renderer.material = blackTv;
	}

	public void MakeTvCollin()
	{
		if(renderer.material !=collinTv)
		renderer.material = collinTv;

		voltageText.text = "";
		resistanceText.text = "";
		voltageText.text = "";
	}
	
	
}
