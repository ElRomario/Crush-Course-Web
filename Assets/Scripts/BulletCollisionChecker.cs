using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletCollisionChecker : MonoBehaviour
{

    public  GameObject explosion;
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            AudioManager.Instance.PlayExplosions();
            Destroy(other.gameObject);
            Instantiate(explosion, other.transform.position, Random.rotationUniform);
            
        }

        if(other.gameObject.CompareTag("Turret"))
        {
            Destroy(other.transform.parent.gameObject);
            Instantiate(explosion, other.transform.position, Random.rotationUniform);
            //Destroy(other.gameObject);
        }
    }
}
