using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions
{
    [Category("Raccoon/Audio")]         //experimenting with the category, this will make it go under raccoon -> audio in the task menu when assigning task to state
                                        //I'm thinking it might be useful for organizing because I feel like there's going to be a lot of tasks in the future
    public class StartExploreAudio : ActionTask
    {
        public AudioClip exploreLoop;       //The audio signifier for the explore state
        public float volume = 0.4f;

        private AudioSource raccoonAudio;   //reference to the raccon's AudioSource

        protected override string OnInit()
        {
            raccoonAudio = agent.GetComponent<AudioSource>();       //It gets the AudioSource from the FSM agent, also show warning if it's missing
            if (raccoonAudio == null)
                return "Missing AudioSource on agent.";

            return null;
        }

        protected override void OnExecute()
        {
            if (exploreLoop == null)        //Safety check
            {
                EndAction(false);
                return;
            }

            raccoonAudio.clip = exploreLoop;        //This is where I assigned the explore sound, looping is enabled so that the sound persists throughout the explore states
            raccoonAudio.loop = true;
            raccoonAudio.volume = volume;

            if (!raccoonAudio.isPlaying)        //Starts playback only if not already playing, prevents restart spam
                raccoonAudio.Play();
        }

        protected override void OnUpdate()
        {

        }

        protected override void OnStop()
        {
            if (raccoonAudio != null && raccoonAudio.loop)      //It will stop the loop cleanly and reset the AudioSource
            {
                raccoonAudio.Stop();
                raccoonAudio.loop = false;
                raccoonAudio.clip = null;
            }
        }

        protected override void OnPause()
        {
        }
    }
}
