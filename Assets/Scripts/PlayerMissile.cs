using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissile : Missile
{
    public GameObject explosion;
    protected override void Start()
    {
        // Эти параметры можно редактировать в инспекторе для ракеты игрока
        dropTime = 0.05f; // Время падения ракеты игрока
        speed = 250f; // Скорость ракеты игрока
        turnSpeed = 7f; // Скорость поворота ракеты игрока

        base.Start();
    }

    protected override void OnHitTarget(GameObject target)
    {
        AudioManager.Instance.PlayExplosions();
        // Специфическое поведение при попадании для ракеты игрока
        Debug.Log("Player missile hit " + target.name);
        Instantiate(explosion, target.transform.position, Random.rotationUniform);
        base.OnHitTarget(target);
    }
}