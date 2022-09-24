using System.Collections;
using System.Collections.Generic;
using Project.Runner;
using UnityEngine;
using Objects;

namespace Objects {
    public class SpawnTrigger : MonoBehaviour {
        public GameObject StackController;
        public GameObject spawnObject;
        public int maxSpawn = 9;
        private bool canSpawn = true;
        public Transform Spawner;
    
        private GameObject[] _spawnedObjects;
        void Spawn() {
            Vector3 center = Spawner.position;
            for (int i = 0; i < maxSpawn; i++){
                Vector3 pos = RandomCircle(center, 1.0f);
                Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center-pos);
                var Spawned = Instantiate(spawnObject, pos, rot);
                Spawned.transform.parent = Spawner.transform;
                Spawned.GetComponent<Rigidbody>().AddForce(Spawned.transform.forward * 250);
            }
        }
        Vector3 RandomCircle ( Vector3 center ,   float radius  ){
            float ang = Random.value * 360;
            Vector3 pos;
            pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
            pos.y = 1f;
            pos.z = center.z + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
            return pos;
        }
        void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent<PlayerMainModule>(out PlayerMainModule player))
            {
                
                Spawn();
                canSpawn = false;
                StackController.SetActive(true);
            }
        }
    }
}

