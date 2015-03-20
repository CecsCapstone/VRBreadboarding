using UnityEngine;
using System.Collections;
using Hovercast.Core.Navigation;
using Leap;

public class ResistorSelectListener : HovercastNavItemListener<NavItemRadio>
{
    protected override void Setup()
    {
        Item.OnValueChanged += HandleValueChanged;
    }
	
    protected override void BroadcastInitialValue()
    {
        HandleValueChanged(Item);
    }

    private void HandleValueChanged(NavItem<bool> pNavItem)
    {
        GameObject resistor = GameObject.FindGameObjectWithTag("ResistorObject");
        GameObject controller = GameObject.FindGameObjectWithTag("HandController");
        if (!pNavItem.Value)
        {
            resistor.GetComponent<SelectedObject>().Deselect();
            return;
        }
        controller.GetComponent<ClosestObjectFinder>().Select(resistor);
        //GameObject.FindObjectOfType<TargetSelectController>().enabled = true;
        //resistor.GetComponent<SelectedObject>().Select();
    }
}
