using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    public float speed = 5f;
    public Projectile laserPrefab;
    private Projectile laser;

    // Touch control settings
    [Tooltip("Enable automatic touch controls on mobile (also works when manually enabled).")]
    public bool enableTouchControls = true;

    [Range(0.1f, 0.6f)]
    [Tooltip("Fraction of the lower screen used for movement touches (0..1).")]
    public float movementZoneHeightRatio = 0.4f;

    private void Update()
    {
        Vector3 position = transform.position;

        // Aggregate horizontal input from keyboard / arrows
        float horizontal = 0f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            horizontal -= 1f;
        } else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            horizontal += 1f;
        }

        // Touch controls (mobile). Enabled automatically on mobile platforms if enableTouchControls is true.
        if (enableTouchControls && Application.isMobilePlatform) {
            HandleTouchInput(ref horizontal);
        }

        // Apply movement
        position.x += horizontal * speed * Time.deltaTime;

        // Clamp the position of the character so they do not go out of bounds
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        position.x = Mathf.Clamp(position.x, leftEdge.x, rightEdge.x);

        // Set the new position
        transform.position = position;

        // Firing: keep existing keyboard/mouse fire behaviour
        if (laser == null && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))) {
            laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        }
    }

    private void HandleTouchInput(ref float horizontal)
    {
        // Determine the vertical split between movement zone (bottom) and fire zone (top)
        float movementZoneHeight = Screen.height * movementZoneHeightRatio;

        for (int i = 0; i < Input.touchCount; i++) {
            Touch t = Input.GetTouch(i);

            // Movement: touches in the bottom movement zone (held) control left/right
            if (t.position.y <= movementZoneHeight && (t.phase == TouchPhase.Began || t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary)) {
                if (t.position.x < Screen.width * 0.5f) {
                    horizontal -= 1f; // left
                } else {
                    horizontal += 1f; // right
                }
            }

            // Fire: touches that begin in the top area trigger a shot
            if (t.phase == TouchPhase.Began && t.position.y > movementZoneHeight) {
                if (laser == null) {
                    laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Missile") ||
            other.gameObject.layer == LayerMask.NameToLayer("Invader")) {
            GameManager.Instance.OnPlayerKilled(this);
        }
    }

}
