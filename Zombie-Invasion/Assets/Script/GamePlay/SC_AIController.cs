using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using InfimaGames.LowPolyShooterPack;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class SC_AIController : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public float startWaitTime = 4; 
    public float timeToRotate = 4;
    public float speedWalk = 6;
    public float speedRun = 9;
    public bool attack = true;
    
    private float viewRadius = 15;
    public float viewAngle = 90;
    public LayerMask playerMask;
    public LayerMask obstacleMask;
    public float meshResolution = 1f;
    public int edgeIterations = 4;
    public float edgeDistance = 0.5f;

    public Transform[] waypoints;
    private int m_CurrentWaypointIndex;

    private Vector3 playerLastPosition = Vector3.zero;
    private Vector3 m_PlayerPosition;

    private float m_WaitTime;
    private float m_TimeToRotate;
    private bool m_PlayerInRange;
    private bool m_PlayerNear;
    private bool m_IsPatrol;
    public bool m_CaughtPlayer;
    private bool m_IsMoving = true;

    public float startHealth;
    public float hitDamage;
    private float hp;
    public float attackTimer = 1f;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        hp = startHealth;
        
        m_PlayerPosition = Vector3.zero;
        m_IsPatrol = true;
        m_CaughtPlayer = false;
        m_PlayerInRange = false;
        m_WaitTime = startWaitTime;
        m_TimeToRotate = timeToRotate;
        m_IsMoving = true;

        m_CurrentWaypointIndex = 0;
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speedWalk;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp > 0)
        {
            EnvironmentView();
            attackTimer -= Time.deltaTime;
            if (m_IsMoving)
            {
                playAnimation("isMoving", true);
                if (!m_IsPatrol)
                {
                    Chasing();
                }
                else
                {
                    Patroling();
                }
            }
            else
            {
                Move(0);
            }
        }
    }

    void playAnimation(string animationString = "isMoving", bool value = false)
    {
        animator.SetBool(animationString, value);
    }

    private void Chasing()
    {
        m_PlayerNear = false;
        playerLastPosition = Vector3.zero;
        if (!m_CaughtPlayer )
        {
            Move(speedRun );
            navMeshAgent.SetDestination(m_PlayerPosition);
        }

        if (navMeshAgent.remainingDistance <= 2.5f )
        {
            if (!m_CaughtPlayer && Vector3.Distance(transform.position,
                    GameObject.FindGameObjectWithTag("Player").transform.position) >= 6f )
            {
                m_IsPatrol = true;
                m_CaughtPlayer = false;
                m_PlayerNear = false;
                Move(speedWalk );
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                
            }
            else
            {
                if (Vector3.Distance(transform.position,
                        GameObject.FindGameObjectWithTag("Player").transform.position) <= 2.5f && attackTimer <= 0f )
                {
                    CaughtPlayer();
                    attackTimer = 2f;
                }
                else
                {
                    m_CaughtPlayer = false;
                }
            }
        }
    }

    private void Patroling()
    {
        if (m_PlayerNear)
        {
            if (m_TimeToRotate <= 0)
            {
                Move(speedWalk);
                LookingPlayer(playerLastPosition);
            }
            else
            {
                Stop();
                NextPoint();
                m_TimeToRotate -= Time.deltaTime;
            }
        }
        else
        {
            m_PlayerNear = false;
            playerLastPosition = Vector3.zero;
            if(navMeshAgent.remainingDistance <= 1f) 
            {
                NextPoint();
                Move(speedWalk);
            }
        }
    }

    void Move(float speed)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
    }

    void Stop()
    {
        navMeshAgent.isStopped = true;
        navMeshAgent.speed = 0;
    }
    
    public void NextPoint()
    {
        m_CurrentWaypointIndex += 1;
        if(m_CurrentWaypointIndex >= waypoints.Length)
        {
            m_CurrentWaypointIndex = 0;
        }
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position); 
    }

    void CaughtPlayer()
    {
        Move(0);
        m_CaughtPlayer = true;
        playAnimation("isAttacking", true);
        StartCoroutine(damageDelay());
        GameObject.FindGameObjectWithTag("Player").GetComponent<Character>().TakeDamage(hitDamage);
        // playAnimation("isMoving", true);
    }

    void LookingPlayer(Vector3 player)
    {
        navMeshAgent.SetDestination(player);
        if (Vector3.Distance(transform.position, player) <= 0.3 )
        {
            if (m_WaitTime <= 0)
            {
                m_PlayerNear = false;
                Move(speedWalk);
                navMeshAgent .SetDestination(waypoints[m_CurrentWaypointIndex].position);
                m_WaitTime = startWaitTime;
                m_TimeToRotate = timeToRotate;
            }
            else
            {
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }
    }

    void EnvironmentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask);
        for (int i = 0; i < playerInRange.Length; i++)
        {
            Transform player = playerInRange[i].transform;
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position);
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask))
                {
                    m_PlayerInRange = true;
                    m_IsPatrol = false;
                }
                else
                {
                    m_PlayerInRange = false;
                    m_IsPatrol = true;
                }
            }
            if (Vector3.Distance(transform.position, player.position) > viewRadius)
            {
                m_PlayerInRange = false;
            }
            if(m_PlayerInRange)
            {
                m_PlayerPosition = player.transform.position;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            Move(0);
            playAnimation();
            playAnimation("isDead", true);
            StartCoroutine(deadDelay());
        }
    }

    IEnumerator deadDelay(){
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
    
    IEnumerator damageDelay(){
        yield return new WaitForSeconds(1f);
        playAnimation("isAttacking");
    }

    private void OnCollisionStay(Collision collision)
    {
        
        if (attack && collision.gameObject.tag == "Enemy")
        {
            playAnimation("isAttacking");
        }
        
            
    }
}
