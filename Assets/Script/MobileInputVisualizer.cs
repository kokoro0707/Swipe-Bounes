using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class MobileInputVisualizer : MonoBehaviour
{
    [Header("発射するボール")]
    [SerializeField] private Rigidbody2D playerRb;

    [Header("ショット設定")]
    [SerializeField] private float shotPower = 0.08f;
    [SerializeField] private float minDragDistance = 30f;
    [SerializeField] private float touchRadius = 1.0f;

    [Header("減速・停止設定")]
    [SerializeField] private float moveDamping = 1.5f;
    [SerializeField] private float stopSpeed = 0.15f;

    private Vector2 startScreenPos;
    private Vector2 currentScreenPos;

    private bool isDragging;
    private bool isMoving;

    private string currentState = "Ready";

    void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    void Update()
    {
        CheckStop();

        if (isMoving) return;

#if UNITY_EDITOR
        UpdateMouseInput();
#else
        UpdateTouchInput();
#endif
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            playerRb.linearVelocity *= 1f - moveDamping * Time.fixedDeltaTime;
        }
    }

    void CheckStop()
    {
        if (!isMoving) return;

        if (playerRb.linearVelocity.magnitude <= stopSpeed)
        {
            playerRb.linearVelocity = Vector2.zero;
            playerRb.angularVelocity = 0f;

            isMoving = false;
            currentState = "Ready";
        }
    }

    void UpdateMouseInput()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 screenPos = Mouse.current.position.ReadValue();
            TryStartDrag(screenPos);
        }

        if (Mouse.current.leftButton.isPressed && isDragging)
        {
            currentScreenPos = Mouse.current.position.ReadValue();
            currentState = "Dragging";
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame && isDragging)
        {
            currentScreenPos = Mouse.current.position.ReadValue();
            ReleaseShot();
            isDragging = false;
        }
    }
    void TryStartDrag(Vector2 screenPos)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        float distance = Vector2.Distance(worldPos, playerRb.position);

        if (distance <= touchRadius)
        {
            startScreenPos = screenPos;
            currentScreenPos = screenPos;
            isDragging = true;
            currentState = "Touch Ball";
        }
    }

    void ReleaseShot()
    {
        Vector2 drag = startScreenPos - currentScreenPos;

        if (drag.magnitude < minDragDistance)
        {
            currentState = "Too Short";
            return;
        }

        currentState = "Moving";

        playerRb.linearVelocity = Vector2.zero;
        playerRb.angularVelocity = 0f;

        playerRb.AddForce(drag * shotPower, ForceMode2D.Impulse);

        isMoving = true;
    }

    void OnGUI()
    {
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.fontSize = 32;
        style.normal.textColor = Color.white;

        GUI.Label(new Rect(20, 20, 500, 50), "State : " + currentState, style);
    }
}

