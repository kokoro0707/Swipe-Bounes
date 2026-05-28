using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            Debug.Log("Ç∑ÇƒÅ[Ç∂Ç≠ÇËÇ†");
        }
    }
}
