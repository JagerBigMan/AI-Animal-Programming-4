using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class IsNearTrashCanCT : ConditionTask {
		public BBParameter<Transform> trashCan;
		public BBParameter<float> trashCooldown;
		public float approachDistance = 1.2f;

		protected override string OnInit(){
			return null;
		}


		protected override void OnEnable() {
			
		}

		protected override void OnDisable() {
			
		}

		protected override bool OnCheck() {
			if (trashCooldown.value > 0f)
			{
                return false;
            }

            if (trashCan.value == null)
            {
                return false;
            }

            float distance = Vector3.Distance(agent.transform.position, trashCan.value.position);

			return distance <= approachDistance;
        }
	}
}