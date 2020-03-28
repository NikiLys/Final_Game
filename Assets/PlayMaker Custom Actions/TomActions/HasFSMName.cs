// (c) Copyright HutongGames, LLC 2010-2013. All rights reserved.

using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Logic)]
    [Tooltip("Finds an FSM with the Name provided. Sends True/False events")]
    public class HasFSMName : FsmStateAction
    {
        [RequiredField]
        [UIHint(UIHint.Variable)]
        public FsmOwnerDefault owner;

        [Tooltip("Does the gameobject contain an FSM with this Name?")]
        public string fsmName;

        [Tooltip("Event to send if Fsm Name matches.")]
        public FsmEvent isTrue;

        [Tooltip("Event to send if Fsm Name does NOT match.")]
        public FsmEvent isFalse;

        public override void Reset()
        {
            owner = null;
            fsmName = null;
            isTrue = null;
            isFalse = null;
        }

        public override void OnEnter()
        {
            CheckFSMNames();
            Finish();
        }

        void CheckFSMNames()
        {
            var fsms = owner.GameObject.Value.GetComponents<PlayMakerFSM>();

            for (int i = 0; i < fsms.Length; i++)
            {
                var comp = fsms[i];
                if (comp.FsmName == fsmName)
                {
                    Fsm.Event(isTrue);
                    return;
                }
            }

            Fsm.Event(isFalse);
        }
    }
}