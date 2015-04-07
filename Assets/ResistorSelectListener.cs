using UnityEngine;
using System.Collections;
using Hovercast.Core.Navigation;
using Hovercast.Core.Display; 
using Leap;

public class ResistorSelectListener : MenuBaseListener<NavItemSelector>
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
        }
        finder.Select(resistorObject);
    }
}
