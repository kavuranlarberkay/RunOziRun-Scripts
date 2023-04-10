using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int coin = 0;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI liveText;
    void Awake()
    {
        int numGamesSessions = FindObjectsOfType<GameSession>().Length;
        if (numGamesSessions >1)
        {
            Destroy(gameObject);
        }

        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

     void Start()
    {
        coinText.text = coin.ToString();
        liveText.text = playerLives.ToString();
    }

    public IEnumerator processPlayerDeath()
    {
        if (playerLives > 1)
        {
            yield return new WaitForSecondsRealtime(1);
            TakeLife();
        }
        else
        {
            yield return new WaitForSecondsRealtime(1);
            ResetGameSession();
        }
    }

    public void AddToScore(int pointsToAdd)
    {
        coin += pointsToAdd;
        coinText.text = coin.ToString();
    }

    void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    void TakeLife()
    {
        playerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        liveText.text = playerLives.ToString();
    }

}
