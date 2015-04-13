using UnityEngine;
using System.Collections;
using Hovercast.Core.Navigation;

public class KOhmsSelectListener:MenuBaseListener<NavItemRadio>
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

		if(!pNavItem.Value)
		{
			return;
		}
		resistor.setResistanceScale(EnumScript.ResistanceScale.KOhms);

	}
}