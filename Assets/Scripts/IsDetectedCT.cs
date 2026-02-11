using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class IsDetectedCT : ConditionTask {

		public BBParameter<Transform> targetTransform;
		protected override bool OnCheck() {
			return targetTransform.value != null;
		}
	}
}