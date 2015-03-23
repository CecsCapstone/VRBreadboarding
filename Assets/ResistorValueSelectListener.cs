using UnityEngine;
using System.Collections;
using Hovercast.Core.Navigation;
using Leap;
using System;

public class ResistorValueSelectListener:ResistanceScaleSelectBaseListener<NavItemSlider>
{
	
	private GameObject controller;
    protected override void Setup()
    {
		base.Setup();
        Item.OnValueChanged += HandleValueChanged;
		controller = GameObject.FindGameObjectWithTag("HandController");
		Item.ValueToLabel = (s => Component.Label + ": " + Math.Round(s.RangeValue));

    }
	
    protected override void BroadcastInitialValue()
    {
        //HandleValueChanged(Item);
    }

	private void HandleValueChanged(NavItem<float> pNavItem)
    {
		resistor.setResistance(Item.RangeValue);
    }
}
