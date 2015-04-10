using UnityEngine;
using System.Collections;
using Hovercast.Core.Navigation;
using Hovercast.Core.Display;
using Leap;

public class ResetNoMenuItemListener:MenuBaseListener<NavItemSelector>
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
		return;
		//do nothing
	}
}