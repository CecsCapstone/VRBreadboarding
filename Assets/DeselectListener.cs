using UnityEngine;
using System.Collections;
using Hovercast.Core.Navigation;
using Leap;

public class DeselectListener : HovercastNavItemListener<NavItemRadio>
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
        GameObject controller = GameObject.FindGameObjectWithTag("HandController");
        ClosestObjectFinder finder = controller.GetComponent<ClosestObjectFinder>();
        if (!pNavItem.Value)
        {
            return;
        }

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

        pNavItem.Value = !pNavItem.Value;
    }
}