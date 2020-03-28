// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategory(ActionCategory.Logic)]
	[Tooltip("Finds an FSM with the Template provided. Sends True/False events")]
	public class HasFSMTemplate : FsmStateAction
	{
		[RequiredField]
		[UIHint(UIHint.Variable)]
		public FsmOwnerDefault owner;

		[Tooltip("Does the gameobject contain this FSM template?")]
		public FsmTemplate template;

		[Tooltip("Event to send if object contains FSM with Template.")]
		public FsmEvent isTrue;

		[Tooltip("Event to send if object does NOT contain FSM with Template.")]
		public FsmEvent isFalse;

		public override void Reset()
		{
			owner = null;
			template = null;
			isTrue = null;
			isFalse = null;
		}

		public override void OnEnter()
		{
			CheckFSMTemplates();
			Finish();
		}

		void CheckFSMTemplates()
		{
			var fsms = owner.GameObject.Value.GetComponents<PlayMakerFSM>();

			for (int i = 0; i < fsms.Length; i++)
			{
				var comp = fsms[i];
				if (comp.UsesTemplate)
				{
					if (comp.FsmTemplate == template)
					{
						Fsm.Event(isTrue);
						return;
					}
				}
			}

			Fsm.Event(isFalse);
		}
	}
}