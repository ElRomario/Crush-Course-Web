using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissileShooter : MissileShooter
{
    public float firingRange = 20f;// Дальность стрельбы
    public float disarmRange = 100f;
    private GameObject target; // Цель врага (например, игрок)


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }
    protected override List<GameObject> GetTargets()
    {
        // Проверяем, находится ли игрок в зоне поражения
        if (target != null && Vector3.Distance(transform.position, target.transform.position) <= firingRange)
        {
            return new List<GameObject> { target.gameObject };
        }

        return new List<GameObject>();
    }

    void Update()
    {
        
        FireMissiles();
    }
}
