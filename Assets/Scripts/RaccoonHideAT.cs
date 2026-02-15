using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;


namespace NodeCanvas.Tasks.Actions {

	public class RaccoonHideAT : ActionTask {

		public Renderer bodyRenderer;
		public Color hideColor = Color.gray;

		public float hideDuration = 5f;

		private Color originalColor;
		private float hideTimer;
		private NavMeshAgent navAgent;
		private bool originalStopped;

		protected override string OnInit()
		{ 
			navAgent = agent.GetComponent<NavMeshAgent>();
			return null;
		}


		protected override void OnExecute() {
			hideTimer = 0f;
			
			if(navAgent != null)
			{
				originalStopped = navAgent.isStopped;
				navAgent.isStopped = true;
				navAgent.ResetPath();
			}

			if (bodyRenderer != null)
			{
				originalColor = bodyRenderer.material.color;
				bodyRenderer.material.color = hideColor;
			}
		}


		protected override void OnUpdate() {
			hideTimer += Time.deltaTime;

			if(hideTimer >= hideDuration)
			{
				EndAction(true);
			}
		}


		protected override void OnStop() {
			if (navAgent != null)
			{
				navAgent.isStopped = originalStopped;
			}

			if (bodyRenderer != null)
			{
				bodyRenderer.material.color = originalColor;
			}
		}


		protected override void OnPause() {
			
		}
	}
}