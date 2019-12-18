using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Level : MonoBehaviour
{
    [SerializeField] float delayInSecs = 1f;
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        FindObjectOfType<GameLogic>().ResetGame();
    }

    public void LoadGameOver()
    {
        StartCoroutine(DelayLoadGameOver());
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator DelayLoadGameOver()
    {
        yield return new WaitForSeconds(delayInSecs);
        SceneManager.LoadScene("Game Over");       
        
    }
}
