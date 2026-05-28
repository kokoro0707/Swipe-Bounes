using UnityEngine;

public class UIEnemyHP : MonoBehaviour
{
    public int hp = 3;

    public void Damage(int damage)
    {
        hp = -damage;

        Debug.Log(gameObject.name + "HP" + hp);

        if(hp<=0)
        {
            Destroy(gameObject);
            Debug.Log("Ž€‚ñ‚¾");
        }
    }
}
