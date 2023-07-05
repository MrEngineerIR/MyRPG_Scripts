using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        private GameObject player;
        private void Start()
        {
            GetComponent<PlayableDirector>().played += disabelControl;
            GetComponent<PlayableDirector>().stopped += enableControl;
            player = GameObject.FindWithTag("Player");
        }
        void disabelControl(PlayableDirector pd)
        {
            
            player.GetComponent<ActionScheduler>().cancelCurrentAction();
            player.GetComponent<playerController>().enabled = false;
        }
        void enableControl(PlayableDirector pd)
        {
            player.GetComponent<playerController>().enabled = true;
        }
    }
}


