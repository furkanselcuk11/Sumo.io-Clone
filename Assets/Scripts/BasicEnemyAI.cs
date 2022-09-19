using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;    
    [SerializeField] private Transform player;
    [SerializeField] private LayerMask whatIsGround, whatIsPlayer, whatIsEnemy; // Layer isimleri
    [Space]
    [Header("Patroling")]
    [SerializeField] private Vector3 walkPoint; // yürüyüş noktalrı
    public bool walkPointSet;// Yrüyüş noktası var mıyokmu
    [SerializeField] private float walkPointRange;  // Yürüyeceği alanı belirler
    [Space]
    [Header("States")]
    [SerializeField] private float sightRange, enemySightRange; // Player Görüş ve AI görüş menzil aralığı
    [SerializeField] private bool playerInSightRange, enemyInSightRange;  // Görüş menzilinde ve AI görü menzilinde mi
    [Space]
    [Header("Enemy")]
    [SerializeField] private float maxDistance; // Maksimum 
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
            // AI , Player Görüş veya AI görüş alanında mı kontrol edilir
            EnemySphere();
            playerInSightRange = Physics.CheckSphere(this.transform.position, sightRange, whatIsPlayer);    // Eğer Player görüş menzilindeyse playerInSightRange True döner

            if (!playerInSightRange && !enemyInSightRange) Patroling();   // Sahnde de Random gezer - ( Eğer Player veya AI karakter görmediyse)
            if (playerInSightRange && !enemyInSightRange) ChasePlayer();  // Oyuncuyu takip eder - ( Eğer Player görmüş ama AI karakter görmediyse)
            if (enemyInSightRange && !playerInSightRange) ChaseEnemy();  // AI, diğer AI'yı takip eder - ( Eğer Player görmediyse ve AI karakter gördüyse)
            if (enemyInSightRange && playerInSightRange) Chase();  // Oyuncuyu veya AI'yı takip eder - ( Eğer Player ve AI karakter gördüyse)
        }        
    }
    private void EnemySphere()
    {
        // AI görüş alanında Her hangi bir AI var mı RaycastHit ile kontrol edilir ve varsa enemyInSightRange true döner
        origin = this.transform.position;   
        direction = this.transform.forward;
        RaycastHit hit;
        if(Physics.SphereCast(origin,enemySightRange,direction,out hit, maxDistance, whatIsEnemy, QueryTriggerInteraction.UseGlobal))
        {
            this.currentHitObject = hit.transform.gameObject;
            currentHitDistance = maxDistance;
            enemyInSightRange = true;  // AI görüş alanında true olur          
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
        // Oyuncuyu veya AI'yı takip eder - ( Eğer Player ve AI karakter gördüyse) En yakın olanı takipr eder
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, sightRange))
        {            
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                // Eğer görüş alanındna AI varsa
                Debug.DrawLine(this.transform.position, hit.point, Color.red);
                float distancePlayer = Vector3.Distance(this.transform.position, player.position);  // Karakterin Player ile arasındaki mesafe
                float distanceEnemy = Vector3.Distance(this.transform.position, hit.collider.gameObject.transform.position); // Karakterin AI ile arasındaki mesafe

                if (distancePlayer < distanceEnemy)
                {
                    // Player daha yakınsa - Oyuncuyu takip et
                    this.agent.SetDestination(player.position);
                }
                else if(distancePlayer > distanceEnemy)
                {
                    // AI daha yakınsa - Enemy takip et
                    this.agent.SetDestination(hit.collider.gameObject.transform.position);
                }
                else if(distancePlayer == distanceEnemy)
                {
                    // Eğer eşitse - Oyunucyu takip et
                    this.agent.SetDestination(player.position);
                }                
            }
        }
    }
    private void ChasePlayer()
    {
        // Eğer görüş alanında player varsa player takip eder
        this.agent.SetDestination(player.position); // AI SetDestination özelliği ile takip eder
    }
    private void ChaseEnemy()
    {
        // Eğer görüş alanında AI varsa AI'yı takip eder
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
        // X ve Z eksenlerinde walkPointRange aralığında random nokta belirle
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        
        walkPoint = new Vector3(randomX, this.transform.position.y,randomZ); // Random nokta oluşturu ve oraya hareket eder

        if (Physics.Raycast(walkPoint, -this.transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;    // Yürüş noktası aktif hala gelir
        }
    }

    private void OnDrawGizmosSelected()
    {        
        // Gizmoz ekranda nasıl gösterleceği ve kapladığı alanları gösterir
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, sightRange);
        Gizmos.color = Color.red;
        Debug.DrawLine(this.origin, this.origin + this.direction * currentHitDistance);
        Gizmos.DrawWireSphere(this.origin + this.direction * currentHitDistance, enemySightRange);
    }    
}