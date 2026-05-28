using UnityEngine;

public class LanePlayer : MonoBehaviour
{

    public Transform[] lanes;



    public float playerY = -4f; // ★追加：プレイヤーを出す高さ



    private int currentLane = 1;



    void Start()
    {

        MoveToLane(currentLane);

    }



    public void MoveToLane(int laneIndex)
    {

        if (laneIndex < 0 || laneIndex >= lanes.Length) return;



        currentLane = laneIndex;



        // ★変更：Xはレーン、YはplayerY固定

        transform.position = new Vector3(

         lanes[currentLane].position.x,

         playerY,

         transform.position.z

         );

    }

}