using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovementController : MonoBehaviour
{
    [SerializeField]
    private Transform objectDirection;
    [SerializeField, Range(0f, 100f)]
    float speedForce = 10f;
    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();

        GameController.Instance.GameStop.AddListener(() => gameObject.SetActive(false));
    }

    private void FixedUpdate()
    {
        if (!GameController.Instance.GameStarted) return;

        Vector2 playerInput;
        playerInput.x = Input.GetAxis("Horizontal");
        playerInput.y = Input.GetAxis("Vertical");
        playerInput = Vector2.ClampMagnitude(playerInput, 1f);

        Vector3 velocity = new Vector3(playerInput.x, 0f, playerInput.y) * speedForce;
        velocity = Quaternion.AngleAxis(objectDirection.rotation.eulerAngles.y, Vector3.up) * velocity;

        rigidBody.AddForce(velocity);
    }
}
