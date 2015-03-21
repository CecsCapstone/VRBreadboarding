using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetSelectController : MonoBehaviour {

    private TargetController closestTarget;
    private TargetController FirstTarget;
	private TargetController hovering;
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
		
		//if we aren't close to anything remove the hovering and return
		if(closestTarget == null)
		{
			RemoveHover();
			return;
		}
		
		//connector select mode
		else if (FirstTarget !=null) 
		{
			if(closestTarget == FirstTarget)
			{
				RemoveHover();
			}
			if(closestTarget != FirstTarget && closestTarget != hovering)
			{
				RemoveHover();
				SetHover(closestTarget);
			}
		}
		else 
		{
			if(hovering == null)
			{
				SetHover(closestTarget);
			}

			else if(hovering != null && closestTarget != hovering)
			{
				RemoveHover();
				SetHover(closestTarget);
			}
		}
		

        GameObject selected = null;
		if(GameObject.FindGameObjectWithTag("HandModel") != null && GameObject.FindGameObjectWithTag("HandModel").GetComponent<IsPinching>().Pinching(1))
        {
            selected = finder.selected;
            if (selected != null)
            {
                ConnectorController connectorController = selected.GetComponent<ConnectorController>();
                if (connectorController == null)
                {
                    if (closestTarget.instantiated == null)
                    {
                        closestTarget.instantiated = closestTarget.PlaceObject(selected);
                    }
                    else if (selected.name != closestTarget.instantiated.name) //there is an object on the target, but you are trying to place something else
                    {
                        Destroy(closestTarget.instantiated);
                        closestTarget.instantiated = closestTarget.PlaceObject(selected);
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
                        else if (connectorController.end == null && closestTarget != connectorController.start)
                        {
							if(AttemptingDiagonal(connectorController, closestTarget))
                            {
                                return;
                            }
							bool duplicate = CheckDuplicates(connectorController,closestTarget);
                            if (!duplicate)
                            {
								PlaceWire(connectorController, closestTarget );
                                
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
		hovering = closestTarget;
		TurnOnLights(Color.red, hovering);
	}

	private void RemoveHover()
	{	
		if(hovering == null)
		{
			return;
		}
		
		TurnOffLights(hovering);
		hovering = null;
	}

	private void SetFirstTarget(ConnectorController connectorController)
	{
		connectorController.start = closestTarget;
		FirstTarget = closestTarget;
		RemoveHover();
		TurnOnLights(Color.green, FirstTarget);
		closestTarget = null;
	}

	private void TurnOnLights(Color color,TargetController target)
	{
		target.GetComponent<Light>().color = color;
		target.GetComponent<Light>().intensity = 2;
	}

	private void TurnOffLights(TargetController target)
	{
		target.GetComponent<Light>().intensity = 0;
	}

	private bool AttemptingDiagonal(ConnectorController CC, TargetController closestTarget)
	{
		return ((CC.start.transform.position - closestTarget.transform.position).x != 0 && (CC.start.transform.position - closestTarget.transform.position).z != 0);
	}

	private bool CheckDuplicates(ConnectorController connectorController, TargetController closestTarget)
	{
		foreach(Connector conn in closestTarget.connectors)
		{
			if((conn.start == closestTarget && conn.end == connectorController.start) || (conn.end == closestTarget && conn.start == connectorController.start))
			{
				return true;
			}
		}

		return false;
	}

	private void PlaceWire(ConnectorController connectorController, TargetController closestTarget)
	{
		connectorController.end = closestTarget;
		Connector newConnector = connectorController.PlaceWire();
		closestTarget.connectors.Add(newConnector);
		newConnector.start.connectors.Add(newConnector);
		closestTarget.connectorIndex++;
		newConnector.start.connectorIndex++;
		newConnector.start.GetComponent<Light>().intensity = 0;
		newConnector.end.GetComponent<Light>().intensity = 0;
		FirstTarget = null;
		waiting = true;
	}

}
