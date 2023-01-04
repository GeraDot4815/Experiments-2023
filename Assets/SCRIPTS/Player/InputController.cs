using UnityEngine;
using UnityEngine.InputSystem;
public class InputController : MonoBehaviour
{
    public static InputController Instance;

    public Vector2 inputAxis { get; private set; }

    private Player player;

    public float speedCoof=1f;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        player = Player.Instance;
    }
    private void OnMove(InputValue value)
    {
        inputAxis = value.Get<Vector2>();
        player.Move(inputAxis, speedCoof);
    }
}
