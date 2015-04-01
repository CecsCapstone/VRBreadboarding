using UnityEngine;
using System.Collections;
using Hovercast.Core.Navigation;
using Hovercast.Core.Display;
using Leap;

public class LEDSelectListener : MenuBaseListener<NavItemRadio>
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
            LEDObject.GetComponent<SelectedObject>().Deselect();
            return;
        }
        finder.Select(LEDObject);
    }
}
