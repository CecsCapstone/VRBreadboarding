﻿using System;
using System.Linq;
using Hovercast.Core.Display;
using UnityEngine;

namespace Hovercast.Core.Custom {

	/*================================================================================================*/
	public abstract class HovercastCustomCursor : MonoBehaviour {


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public Type GetRenderer() {
			Type type = GetRendererInner();

			if ( type == null ) {
				throw new Exception(GetErrorPrefix()+"cannot be null.");
			}

			if ( !type.GetInterfaces().Contains(typeof(IUiCursorRenderer)) ) {
				throw new Exception(GetErrorPrefix()+"must implement the "+
					typeof(IUiCursorRenderer).Name+" interface.");
			}

			return type;
		}

		/*--------------------------------------------------------------------------------------------*/
		protected abstract Type GetRendererInner();

		/*--------------------------------------------------------------------------------------------*/
		public abstract CursorSettings GetSettings();


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		private static string GetErrorPrefix() {
			return "Hovercast | The 'Cursor' Renderer ";
		}

	}

}
