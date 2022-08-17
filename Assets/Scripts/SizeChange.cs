using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeChange : MonoBehaviour
{
    public Vector3 sizeChange = new Vector3 (0.001f, 0.001f, 0.001f);
    public float size = 3;
    public float sizeChanger = 0.001f;
    float AIsize;
    float playerSize; 
    int eatScore;
    private float displaySize = 3000;
    public static bool isEaten = false;

    private PlayerJoyStickMovement playerJoyStickMovement;
    private void Start(){

        playerJoyStickMovement = GameObject.Find("Player").GetComponent<PlayerJoyStickMovement>();
    }
    private void FixedUpdate() {
    transform.localScale  += sizeChange;
    size += sizeChanger;
    }
    private void Update(){
        //AIsize = sizeChange.size;
        playerSize = playerJoyStickMovement.size;
        displaySize = size *1000;
    }

     private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag.Equals("Player"))
        {
            if(AIsize >  playerSize){
                Destroy(collision.gameObject);       
        }
            else if(playerSize > AIsize){
                Destroy(gameObject);
                eatScore +=1;
                isEaten = true;
                playerSize += AIsize;
            }
        }
        // else if(collision.gameObject.tag.Equals("Enemy"))
        // {
        //     if(size > AIsize) Destroy(collision.gameObject);
        //     else if(AIsize > size) Destroy(gameObject);
        // }
        
    }
}
