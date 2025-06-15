using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public ScoreCalculator scoreCalculator;
    private Rigidbody rb;
    private Vector3 lastMousePosition;

    public float rollSpeed = 10f;
    public float maxForwardSpeed = 15f;

    public float dragSensitivity = 0.02f;
    public float horizontalLimit = 4.5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        GameObject findGameObj = GameObject.Find("ScoreCalculator");
        if (findGameObj != null) scoreCalculator = findGameObj.GetComponent<ScoreCalculator>();
    }

    private void FixedUpdate()
    {
        ApplyForwardForce();
        LimitForwardSpeed();
    }

    private void Update()
    {
        HandleHorizontalDrag();
    }

    private void ApplyForwardForce()
    {
        Vector3 forwardForce = Vector3.forward * rollSpeed;
        rb.AddForce(forwardForce, ForceMode.Force);
    }

    private void LimitForwardSpeed()
    {
        Vector3 velocity = rb.velocity;
        velocity.z = Mathf.Clamp(velocity.z, -maxForwardSpeed, maxForwardSpeed);
        rb.velocity = velocity;
    }

    private void HandleHorizontalDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 currentMousePosition = Input.mousePosition;
            float deltaX = currentMousePosition.x - lastMousePosition.x;

            Vector3 pos = transform.position;
            pos.x += deltaX * dragSensitivity;
            pos.x = Mathf.Clamp(pos.x, -horizontalLimit, horizontalLimit);
            transform.position = pos;

            lastMousePosition = currentMousePosition;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            scoreCalculator.isLevelFailed = true;
            Destroy(gameObject);
            Debug.Log("Game over");
        } 
        else if (other.CompareTag("Coin"))
        {
            if (scoreCalculator != null) scoreCalculator.coins++;
            Destroy(other.gameObject);
            Debug.Log("+1 coin");
        }
        else if (other.CompareTag("FinishGate"))
        {
            scoreCalculator.isLevelComplete = true;
            Destroy(gameObject);
            Debug.Log("Game Finished");
        }
    }
}
