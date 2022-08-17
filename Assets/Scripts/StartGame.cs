using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public GameObject market, button, moveToPlay, leader;
    public GameObject[] enemies;
    public GameObject treAgain;
    public void startGame()
    {
        market.SetActive(false);
        button.SetActive(false);
        moveToPlay.SetActive(false);
        leader.SetActive(true);
        foreach (var item in enemies)
        {
            item.gameObject.GetComponent<NavMeshAgent>().enabled = true;
        }        
    }

    public void gameOver()
    {
        treAgain.gameObject.SetActive(true);
    }

    public void tryAgain()
    {
        SceneManager.LoadScene(0);
    }

}
