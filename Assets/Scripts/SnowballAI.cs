using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SnowballAI : MonoBehaviour
{
    private enum State {
        Roaming,
        Chase,
        RunAway,
    }
    private SizeChange sizeChange;
    private PlayerJoyStickMovement playerJoyStickMovement;
    public NavMeshAgent agent;
    public Transform player;
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
        float AIsize = sizeChange.size;
        float playerSize = playerJoyStickMovement.size;

        float distance = Vector3.Distance(player.position, transform.position);

        if(distance <= inRange && AIsize >= playerSize) state = State.Chase;
        else if(distance <= inRange && AIsize < playerSize) state = State.RunAway;
        else state = State.Roaming;

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
        
    }
    private void FixedUpdate()
    {
        agent.speed -= 0.0005f;
    }

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
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void RunFromPlayer()
    {
        agent.SetDestination(-player.position);
    }
}
