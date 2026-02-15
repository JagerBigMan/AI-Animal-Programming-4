using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class RaccoonExplore : ActionTask 
		{
		public float arriveDistance = 0.5f;

		public float minExploreTime = 5f;			//Random explore time
		public float maxExploreTime = 10f;

		public string soundName = "Explore";

		private float exploreTimer;
		private float exploreDuration;

		private NavMeshAgent navAgent;
		private Vector3 targetPosition;

		private Vector3 minBounds = new Vector3(-20f, 0f, -20f);			//I decided to hardcode the boundaries for the agent because it seems to keep moving out of bound
		private Vector3 maxBounds = new Vector3(20f, 0f, 20f);

		protected override string OnInit() 
		{
			navAgent = agent.GetComponent<NavMeshAgent>();
			return null;
		}

		protected override void OnExecute()
		{
			AudioManager.Instance.StopAll();
			AudioManager.Instance.PlaySound(soundName);
			exploreTimer = 0f;
			exploreDuration = Random.Range(minExploreTime, maxExploreTime);

			AssignNewTarget();
		}

		protected override void OnUpdate() 
		{
			exploreTimer += Time.deltaTime;

			if (exploreTimer >= exploreDuration)		//This is the state ending condition
			{
				EndAction(true);
				return;
			}
			if (navAgent.pathPending)       //It prevents the logic from running while the path is still being calculated, it will prevent rapid destination changes
            {                               // https://docs.unity3d.com/530/Documentation/ScriptReference/NavMeshAgent-pathPending.html
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

			targetPosition = new Vector3(x, agent.transform.position.y, z);		//It keeps the agent inside the boundaries I set
			navAgent.SetDestination(targetPosition);
		}

		protected override void OnStop() {
			if (navAgent != null)
			{
				navAgent.ResetPath();		//This clears the current path when explore ends, prevents any movement when switching states.
			}
		}

		protected override void OnPause() {
			
		}
	}
}