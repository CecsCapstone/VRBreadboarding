using UnityEngine;
using System.Collections;

public class LED : MonoBehaviour {

    public float threshold;
    public float LEDExplode;
    public Light pointLight;
    public GameObject LEDObj;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DestroyLED()
    {
        Destroy(LEDObj);
    }
}
