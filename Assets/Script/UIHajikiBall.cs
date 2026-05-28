using UnityEngine;

using UnityEngine.InputSystem;



public class UIHajikiBall : MonoBehaviour

{

    [Header("ƒ{[ƒ‹")]

    public RectTransform ball;



    [Header("“®‚¯‚é”ÍˆÍ")]

    public RectTransform playArea;



    [Header("“G")]

    public RectTransform[] enemies;



    [Header("”­ŽËÝ’è")]

    public float shotPower = 5f;

    public float damping = 0.98f;

    public float stopSpeed = 20f;



    private Vector2 velocity;

    private Vector2 startPos;

    private Vector2 currentPos;



    private bool isDragging;

    private bool isMoving;

    private RectTransform lastHitEnemy;
    private float hitCooldown = 0.15f;
    private float hitTimer;



    void Update()

    {
        if (hitTimer>0)
        {
            hitTimer -= Time.deltaTime;
        }

        if (isMoving)

        {

            MoveBall();

            CheckEnemyHit();

            return;

        }



        if (Mouse.current.leftButton.wasPressedThisFrame)

        {

            startPos = Mouse.current.position.ReadValue();

            isDragging = true;

        }



        if (Mouse.current.leftButton.isPressed && isDragging)

        {

            currentPos = Mouse.current.position.ReadValue();

        }



        if (Mouse.current.leftButton.wasReleasedThisFrame && isDragging)

        {

            currentPos = Mouse.current.position.ReadValue();



            Vector2 drag = startPos - currentPos;

            velocity = drag * shotPower;



            isDragging = false;

            isMoving = true;

        }

    }



    void MoveBall()

    {

        ball.anchoredPosition += velocity * Time.deltaTime;

        velocity *= damping;



        Vector2 pos = ball.anchoredPosition;



        Vector2 halfArea = playArea.rect.size / 2f;

        Vector2 halfBall = ball.rect.size / 2f;



        if (pos.x < -halfArea.x + halfBall.x)

        {

            pos.x = -halfArea.x + halfBall.x;

            velocity.x *= -1;

        }



        if (pos.x > halfArea.x - halfBall.x)

        {

            pos.x = halfArea.x - halfBall.x;

            velocity.x *= -1;

        }



        if (pos.y < -halfArea.y + halfBall.y)

        {

            pos.y = -halfArea.y + halfBall.y;

            velocity.y *= -1;

        }



        if (pos.y > halfArea.y - halfBall.y)

        {

            pos.y = halfArea.y - halfBall.y;

            velocity.y *= -1;

        }



        ball.anchoredPosition = pos;



        if (velocity.magnitude < stopSpeed)

        {

            velocity = Vector2.zero;

            isMoving = false;

        }

    }




    void CheckEnemyHit()

    {

        if (hitTimer > 0) return;



        for (int i = 0; i < enemies.Length; i++)

        {

            if (enemies[i] == null) continue;



            float distance = Vector2.Distance(

            ball.position,

            enemies[i].position

            );



            float ballRadius = ball.rect.width * 0.5f * ball.lossyScale.x;

            float enemyRadius = enemies[i].rect.width * 0.5f * enemies[i].lossyScale.x;



            float hitDistance = ballRadius + enemyRadius;



            if (distance <= hitDistance)

            {

                UIEnemyHP enemyHP = enemies[i].GetComponent<UIEnemyHP>();



                if (enemyHP != null)

                {

                    enemyHP.Damage(1);

                }



                Vector2 reflectDir =

                ((Vector2)ball.position - (Vector2)enemies[i].position).normalized;



                velocity = reflectDir * velocity.magnitude;



                ball.position =

                (Vector2)enemies[i].position + reflectDir * hitDistance;



                hitTimer = hitCooldown;



                if (enemyHP != null && enemyHP.hp <= 0)

                {

                    enemies[i] = null;

                }



                return;

            }

        }

    }



}