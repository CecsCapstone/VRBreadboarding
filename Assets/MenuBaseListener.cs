using UnityEngine;
using System.Collections;
using Hovercast.Core.Custom;
using Hovercast.Core.Navigation;
using System.Collections.Generic;

public abstract class MenuBaseListener<T> : HovercastNavItemListener<T> where T : NavItem 
{ 

	protected Resistor resistor { get; private set; }
	protected GameObject resistorObject { get; private set; }
    protected GameObject controller { get; private set; }
    protected ClosestObjectFinder finder { get; private set; }
    protected GameObject connector {get; private set;}  
	

	protected override void Setup() 
	{
		resistorObject = GameObject.FindGameObjectWithTag("ResistorObject");
		resistor = resistorObject.GetComponent<Resistor>();
		resistor.setResistance(1f);
        controller = GameObject.FindGameObjectWithTag("HandController");
        finder = controller.GetComponent<ClosestObjectFinder>();
	    connector = GameObject.FindGameObjectWithTag("ConnectorObject");
	}


}
