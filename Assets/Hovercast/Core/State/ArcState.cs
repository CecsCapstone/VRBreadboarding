﻿using System.Collections.Generic;
using System.Linq;
using Hovercast.Core.Custom;
using Hovercast.Core.Input;
using Hovercast.Core.Navigation;
using UnityEngine;

namespace Hovercast.Core.State {

	/*================================================================================================*/
	public class ArcState {

		public delegate void LevelChangeHandler(int pDirection);
		public event LevelChangeHandler OnLevelChange;

		public bool IsInputAvailable { get; private set; }
		public bool IsLeft { get; private set; }
		public Vector3 Center { get; private set; }
		public Quaternion Rotation { get; private set; }
		public float Size { get; private set; }
		public float DisplayStrength { get; private set; }
		public float NavBackStrength { get; private set; }
		public SegmentState NearestSegment { get; private set; }

		private readonly NavRoot vNavRoot;
		private readonly IList<SegmentState> vSegments;
		private readonly InteractionSettings vSettings;
		private bool vIsGrabbing;


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public ArcState(NavRoot pNavRoot, InteractionSettings pSettings) {
			vNavRoot = pNavRoot;
			vSegments = new List<SegmentState>();
			vSettings = pSettings;

			IsLeft = vSettings.IsMenuOnLeftSide;

			OnLevelChange += (d => {});

			vNavRoot.OnLevelChange += HandleLevelChange;
			HandleLevelChange(0);
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		public SegmentState[] GetSegments() {
			return vSegments.ToArray();
		}

		/*--------------------------------------------------------------------------------------------*/
		public NavItem GetLevelParentItem() {
			NavLevel parNavLevel = vNavRoot.GetParentLevel();
			return (parNavLevel == null ? null : parNavLevel.LastSelectedParentItem);
		}

		/*--------------------------------------------------------------------------------------------*/
		public string GetLevelTitle() {
			return vNavRoot.GetLevelTitle();
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		internal void UpdateAfterInput(IInputMenu pInputMenu) {
			IsInputAvailable = pInputMenu.IsAvailable;
			IsLeft = pInputMenu.IsLeft;
			Center = pInputMenu.Position;
			Rotation = pInputMenu.Rotation;
			Size = pInputMenu.Radius;
			DisplayStrength = pInputMenu.DisplayStrength;
			NavBackStrength = pInputMenu.NavigateBackStrength;

			CheckGrabGesture(pInputMenu);
		}

		/*--------------------------------------------------------------------------------------------*/
		internal void UpdateWithCursor(CursorState pCursor) {
			bool allowSelect = (pCursor.IsInputAvailable && DisplayStrength > 0);
			Vector3? cursorPos = (allowSelect ? pCursor.Position : (Vector3?)null);

			NearestSegment = null;

			foreach ( SegmentState seg in vSegments ) {
				seg.UpdateWithCursor(cursorPos);

				if ( !allowSelect ) {
					continue;
				}

				if ( NearestSegment == null ) {
					NearestSegment = seg;
					continue;
				}

				if ( seg.HighlightDistance < NearestSegment.HighlightDistance ) {
					NearestSegment = seg;
				}
			}

			foreach ( SegmentState seg in vSegments ) {
				if ( seg.SetAsNearestSegment(seg == NearestSegment) ) {
					break; //stop loop upon actual selection because the segment list can change
				}
			}
		}


		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		private void CheckGrabGesture(IInputMenu pInputMenu) {
			if ( pInputMenu == null ) {
				vIsGrabbing = false;
				return;
			}

			if ( vIsGrabbing && pInputMenu.NavigateBackStrength <= 0 ) {
				vIsGrabbing = false;
				return;
			}

			if ( !vIsGrabbing && pInputMenu.NavigateBackStrength >= 1 ) {
				vIsGrabbing = true;
				vNavRoot.Back();
				return;
			}
		}

		/*--------------------------------------------------------------------------------------------*/
		private void HandleLevelChange(int pDirection) {
			vSegments.Clear();

			NavItem[] items = vNavRoot.GetLevel().Items;

			foreach ( NavItem navItem in items ) {
				var seg = new SegmentState(navItem, vSettings);
				vSegments.Add(seg);
			}

			OnLevelChange(pDirection);
		}

	}

}
