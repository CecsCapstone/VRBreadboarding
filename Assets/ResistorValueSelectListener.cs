using UnityEngine;
using System.Collections;
using Hovercast.Core.Navigation;
using Leap;

public class ResistorValueSelectListener: HovercastNavItemListener<NavItemSlider> 
{
	private GameObject resistorObject;
	private Resistor resistor;
	private GameObject controller;
    protected override void Setup()
    {
        Item.OnValueChanged += HandleValueChanged;
		resistorObject = GameObject.FindGameObjectWithTag("ResistorObject");
		resistor = resistorObject.GetComponent<Resistor>();
		controller = GameObject.FindGameObjectWithTag("HandController");
		Debug.Log("starting slider");

    }
	
    protected override void BroadcastInitialValue()
    {
        HandleValueChanged(Item);
    }

	private void HandleValueChanged(NavItem<float> pNavItem)
    {
		resistor.setResistance((int)(pNavItem.Value * 100));
    }
}
