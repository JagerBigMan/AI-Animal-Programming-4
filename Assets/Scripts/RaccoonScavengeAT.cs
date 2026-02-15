using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Actions
{
    public class RaccoonScavengeAT : ActionTask
    {
        public BBParameter<Transform> trashCan;
        public BBParameter<float> trashCooldown;
        public float cooldownDuration = 15f;
        public float approachDistance = 1.2f;
        public float scavengeTime = 3f;
        public float leaveDistance = 5f;

        public string soundName = "Scavenge";

        private NavMeshAgent navAgent;
        private float timer;
        private bool arrived;
        private bool leaving;

        protected override string OnInit()
        {
            navAgent = agent.GetComponent<NavMeshAgent>();
            if (navAgent == null) return "Missing NavMeshAgent on agent.";
            return null;
        }

        protected override void OnExecute()
        {
            if (trashCan.value == null)
            {
                EndAction(false);
                return;
            }

            timer = 0f;
            arrived = false;
            leaving = false;

            navAgent.isStopped = false;
            navAgent.ResetPath();

            NavMeshHit hit;
            if (NavMesh.SamplePosition(trashCan.value.position, out hit, 2f, NavMesh.AllAreas))
                navAgent.SetDestination(hit.position);
            else
                navAgent.SetDestination(trashCan.value.position);
        }

        protected override void OnUpdate()
        {
            if (trashCan.value == null)
            {
                EndAction(false);
                return;
            }

            if (!arrived)
            {
                if (navAgent.pathPending) return;

                if (navAgent.remainingDistance <= approachDistance)
                {
                    arrived = true;

                    navAgent.isStopped = true;
                    navAgent.ResetPath();

                    AudioManager.Instance.StopAll();
                    AudioManager.Instance.PlaySound(soundName);

                    timer = 0f;
                }

                return;
            }

            if (!leaving)
            {
                timer += Time.deltaTime;

                if (timer >= scavengeTime)
                {
                    leaving = true;

                    navAgent.isStopped = false;
                    navAgent.ResetPath();

                    SetLeaveDestination();
                }

                return;
            }

            // 3) After leaving, finish when reaching leave point
            if (navAgent.pathPending) return;

            if (navAgent.remainingDistance <= approachDistance)
            {
                trashCooldown.value = cooldownDuration;
                EndAction(true);
            }
        }

        private void SetLeaveDestination()
        {
            Vector3 away = agent.transform.position - trashCan.value.position;
            away.y = 0f;

            if (away.sqrMagnitude < 0.001f)
                away = agent.transform.forward;

            away.Normalize();

            Vector3 target = agent.transform.position + away * leaveDistance;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(target, out hit, leaveDistance, NavMesh.AllAreas))
                navAgent.SetDestination(hit.position);
            else
                navAgent.SetDestination(target);
        }

        protected override void OnStop()
        {
            if (navAgent != null)
                navAgent.isStopped = false;
        }

        protected override void OnPause() { }
    }
}
