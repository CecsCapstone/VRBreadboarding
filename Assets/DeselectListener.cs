using UnityEngine;
using System.Collections;
using Hovercast.Core.Navigation;
using Leap;

public class DeselectListener : MenuBaseListener<NavItemSelector>
{
    protected override void Setup()
    {
        Item.OnSelected += HandleSelected;
        base.Setup();
    }

    protected override void BroadcastInitialValue()
    {
    }

    private void HandleSelected(NavItem pNavItem)
    {
        
       
        if (finder.selected != null)
        {
            finder.selected.GetComponent<SelectedObject>().Deselect();
            controller.GetComponent<TargetSelectController>().enabled = false;
            if (finder.selected.GetComponent<ConnectorController>() != null && finder.selected.GetComponent<ConnectorController>().start != null)
            {
                finder.selected.GetComponent<ConnectorController>().Reset();
            }
            finder.selected = null;
        }
    }
}