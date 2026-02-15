using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions {

	public class TrashCooldownAT : ActionTask {
		public BBParameter<float> trashCooldown;
		protected override void OnUpdate() {
			if(trashCooldown.value > 0f)
			{
				trashCooldown.value -= Time.deltaTime;
			}
		}
	}
}