using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.ScneneManagement
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject PersistentGameobjectPrefab;
        static bool hasSpawned = false;
        private void Awake()
        {
            if (hasSpawned) { return; }
           
            SpawnPersistentObject();
            hasSpawned = true;
        }

        private void SpawnPersistentObject()
        {
            GameObject persistentObject =  Instantiate(PersistentGameobjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}

