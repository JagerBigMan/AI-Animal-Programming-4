using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class RaccoonExplore : ActionTask 
		{
		public float exploreRadius = 5f;
		public float arriveDistance = 0.5f;

		public float minExploreTime = 5f;
		public float maxExploreTime = 10f;

		private float exploreTimer;
		private float exploreDuration;

		private NavMeshAgent navAgent;
		private Vector3 targetPosition;

		private Vector3 minBounds = new Vector3(-20f, 0f, -20f);
		private Vector3 maxBounds = new Vector3(20f, 0f, 20f);

		protected override string OnInit() 
		{
			navAgent = agent.GetComponent<NavMeshAgent>();
			return null;
		}

		protected override void OnExecute()
		{
			exploreTimer = 0f;
			exploreDuration = Random.Range(minExploreTime, maxExploreTime);

			AssignNewTarget();
		}

		protected override void OnUpdate() 
		{
			if (exploreTimer >= exploreDuration)
			{
				exploreTimer = 0f;
				exploreDuration = Random.Range(minExploreTime, maxExploreTime);
				AssignNewTarget();
			}
			if (navAgent.pathPending)
			{
				return;
			}
			if (navAgent.remainingDistance <= arriveDistance)
			{
				AssignNewTarget();
			}
		}
		private void AssignNewTarget()
		{
			float x = Random.Range(minBounds.x, maxBounds.x);
			float z = Random.Range(minBounds.z, maxBounds.z);

			targetPosition = new Vector3(x, agent.transform.position.y, z);
			navAgent.SetDestination(targetPosition);
		}

		protected override void OnStop() {
			
		}

		protected override void OnPause() {
			
		}
	}
}