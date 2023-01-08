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
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inputAxis = context.ReadValue<Vector2>();
            player.Move(inputAxis, speedCoof);
        }
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        if(context.performed) player.weapon.Attack();
    }
}
