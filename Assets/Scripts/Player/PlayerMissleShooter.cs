using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMissileShooter : MissileShooter
{

    public float targetRange = 50f;
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = transform;

    }


    protected override List<GameObject> GetTargets()
    {
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        GameObject[] flyingDudes = GameObject.FindGameObjectsWithTag("Enemy");

        GameObject[] enemies = flyingDudes.Concat(turrets).ToArray();
        List<GameObject> validTargets = new List<GameObject>();

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(playerTransform.position, enemy.transform.position);
            if (distance <= targetRange)
            {
                validTargets.Add(enemy);
            }
        }

        return validTargets;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire2") && MissileUIManager.Instance.MissileCount > 0)
        {
            FireMissiles();     
        }
    }


}

