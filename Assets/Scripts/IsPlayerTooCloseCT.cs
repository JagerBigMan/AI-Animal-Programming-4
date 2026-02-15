using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class IsPlayerTooCloseCT : ConditionTask {

		public BBParameter<Transform> targetTransform;
		public float triggerDistance = 5f;
		protected override string OnInit(){
			return null;
		}

		//Called whenever the condition gets enabled.
		protected override void OnEnable() {
			
		}

		//Called whenever the condition gets disabled.
		protected override void OnDisable() {
			
		}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck() {
			if (targetTransform.value == null)
			{
				return false;
			}

			float distance = Vector3.Distance(agent.transform.position, targetTransform.value.position);

			return distance <= triggerDistance;
		}
	}
}