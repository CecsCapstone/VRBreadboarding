using UnityEngine;
using System.Collections;
using Hovercast.Core.Navigation;
using Leap;

public class ConnectorSelectListener : HovercastNavItemListener<NavItemRadio>
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
        GameObject connector = GameObject.FindGameObjectWithTag("ConnectorObject");
        GameObject controller = GameObject.FindGameObjectWithTag("HandController");
        if (!pNavItem.Value)
        {
            connector.GetComponent<SelectedObject>().Deselect();
            return;
        }
        controller.GetComponent<ClosestObjectFinder>().Select(connector);
        //GameObject.FindObjectOfType<TargetSelectController>().enabled = true;
        //connector.GetComponent<SelectedObject>().Select();
    }
}
