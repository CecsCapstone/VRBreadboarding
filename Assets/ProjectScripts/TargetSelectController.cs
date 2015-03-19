using UnityEngine;
using System.Collections;

public class TargetSelectController : MonoBehaviour {

    private TargetController currentTarget;
    private TargetController previousTarget;
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

        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i] != closestObject && targets[i] != previousTarget)
            {
                targets[i].GetComponent<Light>().color = UnityEngine.Color.red;
                targets[i].GetComponent<Light>().intensity = 0;
            }
        }
        
        if (closestObject != null && closestObject.GetComponent<TargetController>() != null)
        {
            closestObject.GetComponent<Light>().intensity = 2;
        }
        GameObject selected = null;
        if (GameObject.FindGameObjectWithTag("HandModel") != null && GameObject.FindGameObjectWithTag("HandModel").GetComponent<IsPinching>().Pinching(1) && closestObject != null && closestObject.GetComponent<TargetController>() != null)
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
                    if (!waiting)
                    {
                        if (connectorController.start == null)
                        {
                            connectorController.start = currentTarget;
                            previousTarget = currentTarget;
                            currentTarget.GetComponent<Light>().color = UnityEngine.Color.green;
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
                                previousTarget = null;
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
