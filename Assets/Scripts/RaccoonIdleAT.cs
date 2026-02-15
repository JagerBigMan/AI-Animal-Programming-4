using NodeCanvas.Framework;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions
{

    public class RaccoonIdleAT : ActionTask
    {
        public float minIdleTime = 1f;      //Random idle time
        public float maxIdleTime = 3f;

        public Color idleColor = Color.white;

        public string soundName = "Idle";

        private float idleTimer;
        private float idleDuration;

        private NavMeshAgent navAgent;

        public Renderer bodyRenderer;       //I'm referencing raccoon's Renderer so that I can change the color of it

        private float originalSpeed;        //Stores value so the state can restore them on exit, although I'm not sure if I've set it up correctly
        private Color originalColor;

        protected override string OnInit()
        {
            navAgent = agent.GetComponent<NavMeshAgent>();
            return null;
        }


        protected override void OnExecute()
        {                                   //Entering Idle
            AudioManager.Instance.StopAll();
            AudioManager.Instance.PlaySound(soundName);

            idleTimer = 0f;
            idleDuration = Random.Range(minIdleTime, maxIdleTime);

            if (navAgent != null)
            {
                originalSpeed = navAgent.speed;
                navAgent.speed = 0f;
                navAgent.ResetPath();                           //This will clear any existing path and fully stops the agent.The raccoon becomes visually idle (no movement)
                navAgent.isStopped = true;                      //https://docs.unity3d.com/540/Documentation/ScriptReference/NavMeshAgent.ResetPath.html
            }                                                   //https://docs.unity3d.com/6000.3/Documentation/ScriptReference/AI.NavMeshAgent-isStopped.html

            if (bodyRenderer != null)
            {
                originalColor = bodyRenderer.material.color;
                bodyRenderer.material.color = idleColor;
            }
        }

        protected override void OnUpdate()
        {
            idleTimer += Time.deltaTime;

            if (idleTimer > idleDuration)
            {
                EndAction(true);
            }
        }

        protected override void OnStop()
        {
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

        protected override void OnPause()
        {

        }
    }
}