
//using UnityEngine;



//public class PlayerHit : MonoBehaviour

//{

//    public GameObject gameOverPanel;



//    public TMPro.TextMeshProUGUI gameOverScoreText;



//    private bool gameOver = false;



//    private RectTransform playerRect;



//    void Start()

//    {

//        playerRect = GetComponent<RectTransform>();



//        gameOverPanel.SetActive(false);

//    }



//    void Update()

//    {

//        if (gameOver) return;



//        Enemy[] enemies = FindObjectsOfType<Enemy>();



//        foreach (Enemy enemy in enemies)
//        {

//            RectTransform enemyRect =

//            enemy.GetComponent<RectTransform>();



//            float distance =

//            Vector2.Distance(

//            playerRect.anchoredPosition,

//            enemyRect.anchoredPosition

//            );



//            // “–‚½‚è”»’è‹——£

//            if (distance < 100f)

//            {

//                GameOver();

//                break;

//            }

//        }

//    }



//    void GameOver()
//    {

//        gameOver = true;



//        gameOverScoreText.text =

//        "Score : " + ScoreManager.Instance.GetScore();



//        gameOverPanel.SetActive(true);

//        Enemy.isStopped = true;

//        EnemySpawner spawner=FindObjectOfType<EnemySpawner>();
//        if (spawner != null)
//        {
//            spawner.isStopped = true;
//        }



//    }

//}

