using UnityEngine;
using System.Collections;

public class ConnectorController : MonoBehaviour {
    
    public TargetController start = null;
    public TargetController end = null;
    public GameObject wirePrefab;
    private Vector3 direction;
    const float wireScaleX = .05f;
    const float wireScaleY = .1f;
    const float wireScaleZ = .05f;

	// Use this for initialization
	public Connector PlaceWire()
    {
        Connector wire;
        GameObject connector;
        Vector3 scale = new Vector3(wireScaleX, wireScaleY, wireScaleZ);
        direction = (end.transform.position - start.transform.position);
        connector = Instantiate(wirePrefab, (direction.x > 0 || direction.z > 0 ? end.transform.position : start.transform.position) - new Vector3(direction.x != 0 ? wireScaleY : 0, 0, direction.z != 0 ? wireScaleY : 0), Quaternion.Euler(90, 90, direction.z != 0 ? 90 : 0)) as GameObject;
        connector.AddComponent<Connector>();
        wire = connector.GetComponent<Connector>();
        wire.transform.localScale = scale;
        wire.start = this.start;
        wire.end = this.end;

        this.start = null;
        this.end = null;

        return wire;
    }
}