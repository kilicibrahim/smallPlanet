using UnityEngine;
using UnityEngine.AI;
public class SnowballAI : MonoBehaviour
{
    private enum State {
        Roaming,
        Chase,
        RunAway,
    }
    public NavMeshAgent agent;
    public Transform player;

    // public LayerMask whatIsGround, whatIsPlayer;
    // public Vector3 walkPoint;
    // bool walkPointSet;
    // public float walkPointRange; 
    public float Range;
    public bool playerInRange;
    private State state;

    [SerializeField] float inRange = 25f;
    [Range(1,500f)] public float walkRadius;
    private void Awake(){
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        state = State.Roaming;
    }
    private void Start(){

    }
    
    private void Update(){

        float distance = Vector3.Distance(player.position, transform.position);
        if(distance <= inRange) state = State.Chase;
        
        switch(state){
            case State.Roaming:
                Roaming();
            break;
            case State.Chase:
                ChasePlayer();
            break;
            case State.RunAway:
                RunFromPlayer();
            break;
        }
        
        
        //playerInRange = Physics.CheckSphere(transform.position, Range, whatIsPlayer);
        //if(Vector3.Distance(transform.position, roamPos) < inRange)
        //{
        //    roamPos = GetRoamingPosition();
        //}
        
    }
    // private Vector3 GetRoamingPosition(){
    //     return startPos + new Vector3(UnityEngine.Random.Range(-1f,1f), UnityEngine.Random.Range(-1f,1f)).normalized * Random.Range(10f, 70f);
    // }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, inRange);
    }
    private Vector3 RandomRoamingPosition(){
        Vector3 finalPosition = Vector3.zero;
        Vector3 randomPosition = Random.insideUnitSphere * walkRadius; 
        randomPosition += transform.position;
        if(NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, walkRadius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
    private void Roaming()
    {
        agent.SetDestination(RandomRoamingPosition());
        //if(!walkPointSet) SearchWalkPoint();

        //if(walkPointSet)
        //{
       //     agent.SetDestination(walkPoint);
       // }

        //Vector3 distanceToWalkPoint = transform.position - walkPoint;
       // if(distanceToWalkPoint.magnitude <1f) walkPointSet = false;
    }
    // private void SearchWalkPoint()
    // {
    //     float randomZ = Random.Range(-walkPointRange, walkPointRange);
    //     float randomX = Random.Range(-walkPointRange, walkPointRange);

    //     walkPoint = new Vector3(transform.position.x + randomX, transform.position.y,  transform.position.z + randomZ);

    //     if(Physics.Raycast(walkPoint, - transform.up, 2f, whatIsGround))
    //     {
    //         walkPointSet = true;
    //     }
    // }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void RunFromPlayer()
    {
        agent.SetDestination(-player.position);
    }
}
