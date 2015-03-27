using UnityEngine;
using System.Collections;
using Hovercast.Core.Navigation;
using Leap;

public class ConnectorSelectListener : MenuBaseListener<NavItemRadio>
{
    protected override void Setup()
    {
        base.Setup();
        Item.OnValueChanged += HandleValueChanged;
    }

    protected override void BroadcastInitialValue()
    {
        HandleValueChanged(Item);
    }

    private void HandleValueChanged(NavItem<bool> pNavItem)
    {
        if (!pNavItem.Value)
        {
            connector.GetComponent<SelectedObject>().Deselect();
            return;
        }
        controller.GetComponent<ClosestObjectFinder>().Select(connector);
    }
}
