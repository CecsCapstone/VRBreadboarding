using UnityEngine;
using System.Collections;

public class TargetSelectController : MonoBehaviour {

    private TargetController currentTarget;
    private ClosestObjectFinder finder;
    private GameObject controller;
    private GameObject closestObject;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("HandController");
        finder = controller.GetComponent<ClosestObjectFinder>();
    }

    private void FixedUpdate()
    {
        closestObject = finder.ClosestTarget();
        GameObject selected = null;
        //Debug.Log(closestObject.GetComponent<TargetController>());
        if (GameObject.FindGameObjectWithTag("HandModel")!= null && GameObject.FindGameObjectWithTag("HandModel").GetComponent<IsPinching>().Pinching(2) && closestObject != null && closestObject.GetComponent<TargetController>() != null)
        {
            selected = finder.selected;
            currentTarget = closestObject.GetComponent<TargetController>();
            if (selected != null)
            {
                ConnectorController connectorController = selected.GetComponent<ConnectorController>();
                if (connectorController == null)
                {
                    if (selected != null && currentTarget.instantiated == null)
                    {
                        //light.intensity = 1;
                        currentTarget.instantiated = currentTarget.PlaceObject(selected);
                    }
                    else if (selected != null && selected.name != currentTarget.instantiated.name)
                    {
                        Destroy(currentTarget.instantiated);
                        currentTarget.instantiated = currentTarget.PlaceObject(selected);
                    }
                }
                else
                {
                    if (connectorController.start == null)
                    {
                        connectorController.start = currentTarget;
                    }
                    else if (connectorController.end == null && currentTarget != connectorController.start)
                    {
                        if ((connectorController.start.transform.position - currentTarget.transform.position).x != 0 && (connectorController.start.transform.position - currentTarget.transform.position).z != 0)
                        {
                            return;
                        }

                        connectorController.end = currentTarget;
                        Connector newConnector = connectorController.PlaceWire();
                        currentTarget.connectors.Add(newConnector);
                        newConnector.start.connectors.Add(newConnector);
                        currentTarget.connectorIndex++;
                        newConnector.start.connectorIndex++;
                    }
                }
            }
        }
    }
}
