using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Missile : MonoBehaviour
{
    [SerializeField] protected float dropTime; // Время, пока ракета будет "падать"
    [SerializeField] protected float speed; // Скорость ракеты
    [SerializeField] protected float turnSpeed; // Скорость поворота к цели

    protected GameObject target; // Цель ракеты
    private bool isTracking = false; // Начала ли ракета преследовать цель
    private float dropTimer;
    public TrailRenderer trailRenderer;

    protected AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    protected virtual void Start()
    {
        dropTimer = dropTime;
    }

    protected virtual void Update()
    {
        if (dropTimer > 0)
        {
            
            // Эффект "падения" ракеты
            dropTimer -= Time.deltaTime;
            trailRenderer.enabled = false;
            transform.Translate(Vector3.down * speed * Time.deltaTime);
        }
        else
        {
            isTracking = true;
        }

        if (isTracking && target != null)
        {
            TrackTarget();
        }
        else if (isTracking)
        {
            MoveForward();
        }
    }

    protected virtual void TrackTarget()
    {
        // Направление к цели
        Vector3 direction = (target.transform.position - transform.position).normalized;
        // Поворот к цели
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        // Движение вперёд
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        trailRenderer.enabled = true;
    }

    protected virtual void MoveForward()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void SetTarget(GameObject newTarget)
    {
        target = newTarget;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Если ракета попадает в цель
        if (other.gameObject == target && other.gameObject.tag != "Player")
        {
            OnHitTarget(other.gameObject);
        }

    }

    protected virtual void OnHitTarget(GameObject target)
    {

        if (target.CompareTag("Turret"))
        {
            Destroy(target.transform.parent.gameObject);
        }
        else
        { 
            Destroy(target);
            Destroy(gameObject);
        }
    }
}