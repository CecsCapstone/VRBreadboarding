using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetSelectController : MonoBehaviour {

    private TargetController currentTarget;
    private TargetController FirstTarget;
	private TargetController hovering;
	private TargetController closestTarget;
    private ClosestObjectFinder finder;
    private GameObject controller;
    private bool waiting = false;
    public float waitingTime = 1f;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("HandController");
        finder = controller.GetComponent<ClosestObjectFinder>();
    }

    private void FixedUpdate()
    {
		closestTarget = finder.ClosestTarget();

		if(closestTarget != null && hovering == null)
		{
			SetHover(closestTarget);
		}

		else if(closestTarget == null && hovering != null)
		{
			RemoveHover();
			return;
		}

		else if(closestTarget != hovering)
		{
			if(hovering !=null)	
			{
				RemoveHover();
			}

			SetHover(closestTarget);
		}

        GameObject selected = null;
		if(GameObject.FindGameObjectWithTag("HandModel") != null && GameObject.FindGameObjectWithTag("HandModel").GetComponent<IsPinching>().Pinching(1) && closestTarget != null)
        {
            selected = finder.selected;
            currentTarget = closestTarget;
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
							SetFirstTarget(connectorController);
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
                                FirstTarget = null;
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

	private void SetHover(TargetController closestTarget)
	{
		//if(closestTarget.GetComponent<ConnectorController> ==
		if(closestTarget == FirstTarget)
			return;
		hovering = closestTarget;
		closestTarget.GetComponent<Light>().color = UnityEngine.Color.red;
		closestTarget.GetComponent<Light>().intensity = 2;
		Debug.Log(closestTarget.GetComponent<Light>().intensity);
	}

	private void RemoveHover()
	{
		if(closestTarget == FirstTarget)
			return;
		hovering.GetComponent<Light>().color = UnityEngine.Color.red;
		hovering.GetComponent<Light>().intensity = 0;
		hovering = null;
	}

	private void SetFirstTarget(ConnectorController connectorController)
	{
		connectorController.start = currentTarget;
		FirstTarget = currentTarget;
		currentTarget.GetComponent<Light>().color = UnityEngine.Color.green;
		currentTarget.GetComponent<Light>().intensity = 2;
	}
}
