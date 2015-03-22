using UnityEngine;
using System.Collections;
using Hovercast.Core.Custom;
using Hovercast.Core.Navigation;
using System.Collections.Generic;

public abstract class ResistanceScaleSelectBaseListener<T> : HovercastNavItemListener<T> where T : NavItem 
{ 

	protected Resistor resistor { get; private set; }
	protected GameObject resistorObject { get; private set; }
	

	protected override void Setup() 
	{
		resistorObject = GameObject.FindGameObjectWithTag("ResistorObject");
		resistor = resistorObject.GetComponent<Resistor>();
	
	}


}
