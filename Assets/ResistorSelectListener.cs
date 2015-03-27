using UnityEngine;
using System.Collections;
using Hovercast.Core.Navigation;
using Hovercast.Core.Display; 
using Leap;

public class ResistorSelectListener : MenuBaseListener<NavItemRadio>
{
    protected override void Setup()
    {
        base.Setup();
        Item.OnValueChanged += HandleValueChanged;
    }
	
    protected override void BroadcastInitialValue()
    {
    }

    private void HandleValueChanged(NavItem<bool> pNavItem)
    {
        if (!pNavItem.Value)
        {
            resistor.GetComponent<SelectedObject>().Deselect();
            return;
        }
        finder.Select(resistorObject);
    }
}
