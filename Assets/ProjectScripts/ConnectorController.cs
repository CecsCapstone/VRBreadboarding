using UnityEngine;
using System.Collections;

public class ConnectorController : MonoBehaviour {
    
    public GameObject prefab;
    public TargetController start = null;
    public TargetController end = null;
    private Vector3 direction;
    private GameObject wire;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        if (this.end != null)
        {
            direction = (end.transform.position - start.transform.position);
            wire = Instantiate(prefab, start.transform.position, Quaternion.Euler(90, direction.z != 0 ? 0: 90, 0)) as GameObject;
            wire.transform.localScale = direction;
        }
	}
}
