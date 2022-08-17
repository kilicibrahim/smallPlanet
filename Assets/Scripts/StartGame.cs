using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject button, moveToPlay;
    public GameObject[] enemies; 
    public void startGame()
    {
        button.SetActive(false);
        moveToPlay.SetActive(false);
        foreach (var item in enemies)
        {
            item.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }        
    }

}
