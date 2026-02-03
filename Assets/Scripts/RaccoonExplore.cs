using NodeCanvas.Framework;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class RaccoonExplore : ActionTask 
		{
		public float exploreRadius;

		private NavMeshAgent navAgent;

		protected override string OnInit() 
		{
			navAgent = agent.GetComponent<NavMeshAgent>();
			return null;
		}

		protected override void OnExecute() {
			Vector3 randomPoint = Random.insideUnitSphere * exploreRadius + agent.transform.position;

			NavMeshHit navHit;
			if (!NavMesh.SamplePosition(randomPoint, out navHit, exploreRadius, NavMesh.AllAreas))
			{
				return;
			}

			navAgent.SetDestination(navHit.position);



		}


		protected override void OnUpdate() 
		{
			if(navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
			{
				EndAction(true);
			}
		}

		protected override void OnStop() {
			
		}

		protected override void OnPause() {
			
		}
	}
}