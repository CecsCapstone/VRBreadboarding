using UnityEngine;
using System.Collections;

public class ConnectorController : MonoBehaviour {
    
    public TargetController start = null;
    public TargetController end = null;
    private Vector3 direction;
    //private Connector wire;
    const float wireScaleX = .05f;
    const float wireScaleY = .1f;
    const float wireScaleZ = .05f;
    
    public ConnectorController()
    {
    }

	// Use this for initialization
	public Connector PlaceWire(GameObject prefab)
    {
        Connector wire;
        direction = (end.transform.position - start.transform.position);
        wire = Instantiate(prefab, start.transform.position - new Vector3(direction.x != 0 ? wireScaleY : 0, 0, direction.z != 0 ? wireScaleY : 0), Quaternion.Euler(90, 90, direction.z != 0 ? 90 : 0)) as Connector;
        wire.transform.localScale = new Vector3(wireScaleX, wireScaleY, wireScaleZ);
        wire.start = this.start;
        wire.end = this.end;

        this.start = null;
        this.end = null;

        return wire;
    }
}
