using RPG.Control;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace RPG.ScneneManagement
{
    public class portal : MonoBehaviour
    {
       enum DestinationIdentifer
        {
            A,B,C,D,E,F,G
        }
        [SerializeField] int sceneLoadNum = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifer destination;
        [SerializeField] float fadeOutSceneTime = 3f;
        [SerializeField] float fadeInSceneTime = 3f;
        [SerializeField] float fadeWaitSceneTime = 3f;


        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(transition());
            }
            
        }
        private IEnumerator transition()
        {

            if (sceneLoadNum < 0 )
            {
                Debug.LogError("Scene to load not set");
                yield break;    
            }
            
            DontDestroyOnLoad(gameObject);
            Fader fader = FindAnyObjectByType<Fader>();
            yield return fader.fadeOut(fadeOutSceneTime);
            yield return SceneManager.LoadSceneAsync(sceneLoadNum);
            portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            yield return new WaitForSeconds(fadeWaitSceneTime);
            yield return fader.fadeIn(fadeInSceneTime);
            Destroy(gameObject);
          
        }

        private void UpdatePlayer(portal otherPortal)
        {
            
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation;

        }

        private portal GetOtherPortal()
        {
            foreach (portal portal in FindObjectsOfType<portal>())
            {
                if (portal == this)
                {
                    continue;
                }
                if (portal.destination != destination)
                {
                    continue;
                }
                return portal;
            }
            return null; 
        }
    }
}


