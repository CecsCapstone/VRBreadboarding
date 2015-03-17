﻿using Hovercast.Core.Custom;
using Hovercast.Core.Input;
using Hovercast.Core.Navigation;
using UnityEngine;

namespace Hovercast.Core.State {

	/*================================================================================================*/
	public interface IHovercastState {

		HovercastNavProvider NavigationProvider { get; }
		HovercastCustomizationProvider CustomizationProvider { get; }
		HovercastInputProvider InputProvider { get; }

		bool IsMenuInputAvailable { get; }
		bool IsCursorInputAvailable { get; }
		bool IsMenuVisible { get; }
		float MenuDisplayStrength { get; }
		float NavigateBackStrength { get; }
		HovercastSideName MenuSide { get; }
		HovercastSideName CursorSide { get; }

		IHovercastItemState[] CurrentItems { get; }
		IHovercastItemState NearestItem { get; }

		Transform MenuTransform { get; }
		Transform CursorTransform { get; }
		Transform CameraTransform { get; }

	}

}
