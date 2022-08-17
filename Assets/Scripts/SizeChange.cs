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
    public static bool itEats = false;

    private PlayerJoyStickMovement playerJoyStickMovement;
    private void Start(){

        playerJoyStickMovement = GameObject.Find("Player").GetComponent<PlayerJoyStickMovement>();
    }
    private void FixedUpdate() {
    transform.localScale  += sizeChange;
    size += sizeChanger;

    if(SizeChange.itEats == true){
            size += playerSize;
            transform.localScale = new Vector3(size, size, size);
            SizeChange.itEats = false;
        }
    }
    private void Update(){
        //AIsize = sizeChange.size;
        playerSize = playerJoyStickMovement.size;
        displaySize = size *1000;

        
    }

     private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag.Equals("Player"))
        {
            if(size >  playerSize){
                Destroy(collision.gameObject);
                SizeChange.itEats = true;       
        }
            else if(playerSize > size){
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
