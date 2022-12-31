using UnityEngine;
using UnityEngine.InputSystem;
public class InputController : MonoBehaviour
{
    public static InputController Instance;

    public Vector2 inputAxis { get; private set; }
    //private Vector2 lastAxis;
    private Vector2 moveVector;

    private Player player;
    private Rigidbody2D rb;
    private Animator animator;

    public bool canMove;

    [SerializeField] private float movingSpeed=1f;
    public float speedCoof=1f;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        player = Player.Instance;
        rb = player.GetComponent<Rigidbody2D>();
        animator = player.GetComponent<Animator>();

        canMove = true;
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            moveVector = inputAxis * movingSpeed * speedCoof * Time.deltaTime;
            rb.velocity = moveVector;
        }
    }
    private void OnMove(InputValue value)
    {
        inputAxis = value.Get<Vector2>();
        if (inputAxis != Vector2.zero) animator.SetBool("IsMove", true);
        else animator.SetBool("IsMove", false);
    }
}
