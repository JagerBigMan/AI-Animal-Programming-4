using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class DetectAT : ActionTask {

		public LayerMask detectionLayerMask;
		public float detectionRadius;
		public BBParameter<Transform> targetTransform;
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			targetTransform.value = null;
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			Collider[] detectedColliders = Physics.OverlapSphere(agent.transform.position,detectionRadius, detectionLayerMask);

			if (detectedColliders.Length > 0 )
			{
				targetTransform.value = detectedColliders[0].transform;
			}
			else
			{
				targetTransform.value = null;
			}
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}