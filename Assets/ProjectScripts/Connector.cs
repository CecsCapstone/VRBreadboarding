using UnityEngine;
using System.Collections;

public class Connector : MonoBehaviour {
    
    public TargetController start = null;
    public TargetController end = null;
    private Vector3 direction;
    private GameObject wire;
    private bool placed = false;
    const float wireScaleX = .05f;
    const float wireScaleY = .1f;
    const float wireScaleZ = .05f;
    
    public Connector()
    {
    }

	// Use this for initialization
	public void PlaceWire(GameObject prefab)
    {
        if (this.end != null && !placed)
        {
            direction = (end.transform.position - start.transform.position);
            wire = Instantiate(prefab, start.transform.position - new Vector3(direction.x != 0 ? wireScaleY : 0, 0, direction.z != 0 ? wireScaleY : 0), Quaternion.Euler(90, 90, direction.z != 0 ? 90 : 0)) as GameObject;
            wire.transform.localScale = new Vector3(wireScaleX, wireScaleY, wireScaleZ);

            placed = true;
        }
    }
}
