using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int scores = 0;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoresText;
    void Start()
    {
        livesText.text = playerLives.ToString();
        scoresText.text = scores.ToString();
    }
    public void CollectCoin(int addToScore)
    {
        scores += addToScore;
        scoresText.text = scores.ToString();
    }

    // Hàm awake được gọi trước khi start
    // Vậy nên mỗi khi chúng ta load lại scene thì nó sẽ kiểm tra số lượng game session
    // Nếu số lượng game session lớn hơn 1 thì nó sẽ destroy game session hiện tại
    // Nếu số lượng game session bằng 1 thì nó sẽ không destroy game session hiện tại
    // Và cũng không destroy game session khi chúng ta load lại scene
    // Điều này giúp chúng ta không mất đi thông tin của game session khi chúng ta load lại scene
    void Awake() 
    {
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            StartCoroutine(ResetGameSession());
        }
    }

    IEnumerator ResetGameSession()
    {
        yield return new WaitForSecondsRealtime(3);
        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }

    void TakeLife()
    {
        playerLives--;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadSceneAfterDelay(currentSceneIndex));
        livesText.text = playerLives.ToString();
    }
    IEnumerator LoadSceneAfterDelay(int sceneIndex)
    {
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene(sceneIndex);
    }
}
