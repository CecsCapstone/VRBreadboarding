using UnityEngine;
using System.Collections;

public class TargetSelectController : MonoBehaviour {

    private TargetController currentTarget;
    private ClosestObjectFinder finder;
    private GameObject controller;
    private GameObject closestObject;
    private bool waiting = false;
    public float waitingTime = 1f;

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
                    if (connectorController.start != null && GameObject.FindGameObjectWithTag("HandModel") != null && GameObject.FindGameObjectWithTag("HandModel").GetComponent<IsPinching>().Pinching(3))
                    {
                        Debug.Log("Dongs are fun");
                        connectorController.Reset();
                    }
                    else if (!waiting)
                    {
                        if (connectorController.start == null)
                        {
                            connectorController.start = currentTarget;
                            currentTarget.GetComponent<Light>().intensity = 2;
                        }
                        else if (connectorController.end == null && currentTarget != connectorController.start)
                        {
                            if ((connectorController.start.transform.position - currentTarget.transform.position).x != 0 && (connectorController.start.transform.position - currentTarget.transform.position).z != 0)
                            {
                                return;
                            }

                            bool duplicate = false;
                            foreach (Connector conn in currentTarget.connectors)
                            {
                                if (conn.start == currentTarget)
                                {
                                    if (conn.end == connectorController.start)
                                    {
                                        duplicate = true;
                                    }
                                }
                                else if (conn.end == currentTarget)
                                {
                                    if (conn.start == connectorController.start)
                                    {
                                        duplicate = true;
                                    }
                                }
                            }

                            if (!duplicate)
                            {
                                connectorController.end = currentTarget;
                                Connector newConnector = connectorController.PlaceWire();
                                currentTarget.connectors.Add(newConnector);
                                newConnector.start.connectors.Add(newConnector);
                                currentTarget.connectorIndex++;
                                newConnector.start.connectorIndex++;
                                newConnector.start.GetComponent<Light>().intensity = 0;
                                newConnector.end.GetComponent<Light>().intensity = 0;
                                waiting = true;
                            }
                        }
                    }
                    else
                    {
                        if(waitingTime <= 0)
                        {
                            waiting = false;
                            waitingTime = 1f;
                        }
                        else
                        {
                            waitingTime -= Time.deltaTime;
                        }
                    }
                }
            }
        }
    }
}
