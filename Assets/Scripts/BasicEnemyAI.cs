using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;    
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer, whatIsEnemy;
    [Space]
    [Header("Patroling")]
    [SerializeField] private Vector3 walkPoint; // Y�r�y�� noktas�
    public bool walkPointSet;// Y�r�� noktas� varm� yokmu
    [SerializeField] private float walkPointRange;
    [Space]
    [Header("States")]
    [SerializeField] private float sightRange, enemySightRange; // Görüş ve atak menzil aralığı
    [SerializeField] private bool playerInSightRange, enemyInSightRange;  // Görüş menzilinde mi yoksa atak menzilinde mi
    [Space]
    [Header("Enemy")]
    [SerializeField] private float maxDistance;
    private float currentHitDistance;
    private Vector3 origin;
    private Vector3 direction;
    [SerializeField] private GameObject currentHitObject;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        
        if (!GameManager.gamemanagerInstance.isFinish && GameManager.gamemanagerInstance.isStartGame)
        {
            // Oyuncu atak m� yoksa g�r�� alan�nda m� kontrol edilir
            EnemySphere();
            playerInSightRange = Physics.CheckSphere(this.transform.position, sightRange, whatIsPlayer);    // Eğer görüş menzilindeyse True döner

            if (!playerInSightRange && !enemyInSightRange) Patroling();   // Sahnde de Random gezer
            if (playerInSightRange && !enemyInSightRange) ChasePlayer();  // Oyuncuyu takip eder
            if (enemyInSightRange && !playerInSightRange) ChaseEnemy();  // Oyuncuyu takip eder
            if (enemyInSightRange && playerInSightRange) Chase();  // Oyuncuyu takip eder
        }        
    }
    private void EnemySphere()
    {
        origin = this.transform.position;
        direction = this.transform.forward;
        RaycastHit hit;
        if(Physics.SphereCast(origin,enemySightRange,direction,out hit, maxDistance, whatIsEnemy, QueryTriggerInteraction.UseGlobal))
        {
            this.currentHitObject = hit.transform.gameObject;
            currentHitDistance = maxDistance;
            enemyInSightRange = true;            
        }
        else
        {
            currentHitDistance = maxDistance;
            currentHitObject = null;
            enemyInSightRange = false;
        }
    }
    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();   // Eðer yürüyüş alanında degilse yeni yürüþ noktasý oluþtur
        if (walkPointSet)
            agent.SetDestination(walkPoint);    // Eðer yürüyüş alanında ise o noktaya hareket et

        Vector3 distanceToWalkPoint = this.transform.position - walkPoint;
        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }
    private void Chase()
    {
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, sightRange))
        {            
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                Debug.DrawLine(this.transform.position, hit.point, Color.red);
                float distancePlayer = Vector3.Distance(this.transform.position, player.position);
                float distanceEnemy = Vector3.Distance(this.transform.position, hit.collider.gameObject.transform.position);

                if (distancePlayer < distanceEnemy)
                {
                    // Oyuncuyu takip et
                    this.agent.SetDestination(player.position);
                }
                else if(distancePlayer > distanceEnemy)
                {
                    // Enemy takip et
                    this.agent.SetDestination(hit.collider.gameObject.transform.position);
                }
                else if(distancePlayer == distanceEnemy)
                {
                    // Oyunucyu takip et
                    this.agent.SetDestination(player.position);
                }                
            }
            else
            {
                Debug.DrawLine(this.transform.position, hit.point, Color.green);
            }
        }
    }
    private void ChasePlayer()
    {
        this.agent.SetDestination(player.position);
    }
    private void ChaseEnemy()
    {
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, sightRange))
        {
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                Debug.DrawLine(this.transform.position, hit.point, Color.red);
                this.agent.SetDestination(hit.collider.gameObject.transform.position);                
            }
            else
            {
                Debug.DrawLine(this.transform.position, hit.point, Color.green);
            }
        }
    }
    private void SearchWalkPoint()
    {
        // X ve Z eksenlerinde walkPointRange aral���nda random nokta belirle
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(this.transform.position.x + randomX, this.transform.position.y, this.transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -this.transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }

    private void OnDrawGizmosSelected()
    {        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, sightRange);
        Gizmos.color = Color.red;
        Debug.DrawLine(this.origin, this.origin + this.direction * currentHitDistance);
        Gizmos.DrawWireSphere(this.origin + this.direction * currentHitDistance, enemySightRange);
    }    
}