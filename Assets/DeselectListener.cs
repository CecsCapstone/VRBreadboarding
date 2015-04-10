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
        GameObject.FindGameObjectWithTag("ResistorMenuItem").GetComponent<HovercastNavItem>().GetItem().DeselectStickySelections();
        GameObject.FindGameObjectWithTag("ConnectorMenuItem").GetComponent<HovercastNavItem>().GetItem().DeselectStickySelections();
        GameObject.FindGameObjectWithTag("LEDMenuItem").GetComponent<HovercastNavItem>().GetItem().DeselectStickySelections();

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