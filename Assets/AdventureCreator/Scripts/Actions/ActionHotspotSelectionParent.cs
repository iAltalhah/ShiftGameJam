/*
 *
 *	Adventure Creator
 *	by Chris Burton, 2013-2026
 *	
 *	"ActionHotspotSelectionParent.cs"
 * 
 *	If set, only Hotspots that are its children will be selectable.
 * 
 */

using UnityEngine;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AC
{

	[System.Serializable]
	public class ActionHotspotSelectionParent : Action
	{

		public int parameterID = -1;
		public int constantID = 0;
		public Transform parent;
		protected Transform runtimeParent;

		
		public override ActionCategory Category { get { return ActionCategory.Hotspot; }}
		public override string Title { get { return "Selection parent"; }}
		public override string Description { get { return "If set, only Hotspots that are its children will be selectable."; }}


		public override void AssignValues (List<ActionParameter> parameters)
		{
			runtimeParent = AssignFile (parameters, parameterID, constantID, parent);
		}

		
		public override float Run ()
		{
			KickStarter.playerInteraction.selectionParent = runtimeParent;
			return 0f;
		}

		
		#if UNITY_EDITOR
		
		public override void ShowGUI (List<ActionParameter> parameters)
		{
			ComponentField ("Selection parent:", ref parent, ref constantID, parameters, ref parameterID);
		}


		public override void AssignConstantIDs (bool saveScriptsToo, bool fromAssetFile)
		{
			constantID = AssignConstantID (parent, constantID, parameterID);
		}
		
		
		public override string SetLabel ()
		{
			if (parent != null)
			{
				return parent.name;
			}
			return string.Empty;
		}


		public override bool ReferencesObjectOrID (GameObject _gameObject, int id)
		{
			if (parameterID < 0)
			{
				if (constantID == id) return true;
			}
			return base.ReferencesObjectOrID (_gameObject, id);
		}

		#endif


		/**
		 * <summary>Creates a new instance of the 'Hotspot: Selection parent' Action</summary>
		 * <param name = "parent">The selection parent to assign</param>
		 * <returns>The generated Action</returns>
		 */
		public static ActionHotspotSelectionParent CreateNew (Transform parent)
		{
			ActionHotspotSelectionParent newAction = CreateNew<ActionHotspotSelectionParent> ();
			newAction.parent = parent;
			newAction.TryAssignConstantID (newAction.parent, ref newAction.constantID);
			return newAction;
		}
		
	}

}