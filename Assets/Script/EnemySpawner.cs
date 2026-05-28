using UnityEngine;



public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyPrefab;



    public RectTransform[] lanes;



    public RectTransform enemyParent;



    public float spawnY = 400f;

    public float spawnInterval = 1.2f;

    public bool isStopped = false;
    private float timer;



    void Update()
    {
        if(isStopped) return;
        timer += Time.deltaTime;



        if (timer >= spawnInterval)

        {

            timer = 0f;

            SpawnEnemy();

        }

    }



    void SpawnEnemy()
    {

        int laneIndex = Random.Range(0, lanes.Length);



        GameObject enemy = Instantiate(enemyPrefab, enemyParent);



        RectTransform enemyRect = enemy.GetComponent<RectTransform>();



        enemyRect.anchoredPosition = new Vector2(

        lanes[laneIndex].anchoredPosition.x,

        spawnY

        );

    }

}