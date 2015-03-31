using UnityEngine;
using System.Collections;

public class ConnectorController : MonoBehaviour {
    
    public TargetController start = null;
    public TargetController end = null;
    public GameObject wirePrefab;
    private Vector3 direction;
    const float wireScaleX = .1f;
    const float wireScale = 0.1631785f;

	// Use this for initialization
	public Connector PlaceWire()
    {
        Connector wire;
        GameObject connector;
        direction = (end.transform.position - start.transform.position);
        Vector3 scale = new Vector3(wireScaleX, wireScale, wireScale);
        connector = Instantiate(wirePrefab, (direction.x > 0 || direction.z > 0 ? end.transform.position : start.transform.position) + new Vector3(direction.x != 0 ? -.04f : 0, 0, direction.z != 0 ? -.04f : 0), Quaternion.Euler(0, direction.z == 0 ? 90 : 0, 90)) as GameObject;
        connector.AddComponent<Connector>();
        wire = connector.GetComponent<Connector>();
        wire.transform.localScale = scale;
        wire.GetComponent<SelectedObject>().enabled = false;
        wire.start = this.start;
        wire.end = this.end;

        Reset();

        return wire;
    }

    public void Reset()
    {
        this.start.GetComponent<Light>().intensity = 0;
        this.start = null;

        if (this.end != null)
        {
            this.end.GetComponent<Light>().intensity = 0;
            this.end = null;
        }
    }
}