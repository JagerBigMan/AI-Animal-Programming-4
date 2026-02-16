using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class FleeAT : ActionTask {

		public BBParameter<Transform> targetTransform;
		public float fleeDistance = 12f;
		public float fleeFrequency = 0.15f;
		public float fleeSpeed = 6f;

		public float safeDistance = 7f;

		public Color fleeColor = Color.red;
		public Renderer bodyRenderer;

        public string soundName = "Flee";

        private float timeSinceLastHide;
		private NavMeshAgent navAgent;

		private float originalSpeed;
		private Color originalColor;

		protected override string OnInit() {
			navAgent = agent.GetComponent<NavMeshAgent>();
			return null;
		}

		protected override void OnExecute() {
            AudioManager.Instance.StopAll();
            AudioManager.Instance.PlaySound(soundName);
            
			if (targetTransform.value == null)
			{
				EndAction(false);
				return;
			}
			timeSinceLastHide = 0f;

			originalSpeed = navAgent.speed;
			navAgent.speed = fleeSpeed;

			if (bodyRenderer != null)
			{
				originalColor = bodyRenderer.material.color;
				bodyRenderer.material.color = fleeColor;
			}

			SetFleeDestination();
		}


		protected override void OnUpdate() {
			if (targetTransform.value == null)
			{
				EndAction(true);
				return;
			}

			timeSinceLastHide += Time.deltaTime;
			if (timeSinceLastHide > fleeFrequency)
			{
				SetFleeDestination();
				timeSinceLastHide = 0f;

				float distance = Vector3.Distance(agent.transform.position, targetTransform.value.position);
				if (distance >= safeDistance)
				{
					EndAction(true);
				}	
			}
		}

        private void SetFleeDestination()
		{
			if (targetTransform.value == null) return;

			Vector3 directionAwayFromTarget = agent.transform.position - targetTransform.value.position;
			Vector3 rawTargetPosition = directionAwayFromTarget.normalized * fleeDistance + agent.transform.position;

			NavMeshHit hit;
			if (NavMesh.SamplePosition(rawTargetPosition, out hit, fleeDistance, NavMesh.AllAreas))
			{
				navAgent.SetDestination(hit.position);
			}
            else
            {
                 navAgent.SetDestination(rawTargetPosition);
            }
        }
        protected override void OnStop() {
			if (navAgent != null)
			{
				navAgent.speed = originalSpeed;
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