using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform playerTF;
    public Player Player;
    private float lastTime;
    private FP_FootSteps footSteps;
    
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private float attackDelay = 1;
    [SerializeField] private float health = 2;
    [SerializeField] private Transform CubeTF;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip attackSound;

    public delegate void Method(Enemy enemy);
    public static Method onEnemyDeath;

    private void Start()
    {
        lastTime = Time.time + attackDelay;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, playerTF.position);
        if (distance > 4.5f)
        {
            Move();
        }
        else if (distance < 4.5f && !navMeshAgent.isStopped)
        {
            navMeshAgent.isStopped = true;
        }
        else
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (Time.time > lastTime)
        {
            audioSource.clip = attackSound;
            audioSource.Play();
            Player.Damage(5);
            lastTime = Time.time + attackDelay;
        }
    }

    private void Move()
    {
        navMeshAgent.SetDestination(playerTF.position);
        navMeshAgent.isStopped = false;
    }

    public void Damage()
    {
        health --;
        if (health == 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Vector3 cubePosition = new Vector3(transform.position.x, CubeTF.position.y, transform.position.z);
        Instantiate(CubeTF, cubePosition, Quaternion.identity);
        onEnemyDeath?.Invoke(this);
        Destroy(gameObject);
    }
}
