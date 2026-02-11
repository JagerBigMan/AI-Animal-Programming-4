using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class RaccoonObserveAT : ActionTask {
		public BBParameter<Transform> targetTransform;

		public Color observeColor = Color.gray;
		public Renderer bodyRenderer;

		private NavMeshAgent navAgent;
		private float originalSpeed;
		private Color originalColor;
		protected override string OnInit() {
			navAgent = agent.GetComponent<NavMeshAgent>();
			return null;
		}
		protected override void OnExecute() {
			if (navAgent != null)
			{
				originalSpeed = navAgent.speed;
				navAgent.speed = 0f;
				navAgent.ResetPath();
				navAgent.isStopped = true;
			}

			if (bodyRenderer != null)
			{
				originalColor = bodyRenderer.material.color;
				bodyRenderer.material.color = observeColor;
			}
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			if (targetTransform.value == null)
			{
				EndAction(true);
			}
				
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			if (navAgent != null)
			{
				navAgent.isStopped = false;
				navAgent.speed = originalSpeed;
			}

			if (bodyRenderer != null)
			{
				bodyRenderer.material.color = originalColor;
			}
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}