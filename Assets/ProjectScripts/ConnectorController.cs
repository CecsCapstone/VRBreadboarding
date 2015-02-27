using UnityEngine;
using System.Collections;

public class ConnectorController : MonoBehaviour {
    
    public GameObject prefab;
    public TargetController start = null;
    public TargetController end = null;
    private Vector3 direction;
    private GameObject wire;
    private bool placed = false;
    const float wireScaleX = .05f;
    const float wireScaleY = .1f;
    const float wireScaleZ = .05f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (this.end != null && !placed)
        {
            direction = (end.transform.position - start.transform.position);
            wire = Instantiate(prefab, start.transform.position - new Vector3(direction.x != 0 ? wireScaleY : 0, 0, direction.z != 0 ? wireScaleY : 0), Quaternion.Euler(90, 90, direction.z != 0 ? 90 : 0)) as GameObject;
            wire.transform.localScale = new Vector3(wireScaleX, wireScaleY, wireScaleZ);
            
            placed = true;
        }
	}
}
