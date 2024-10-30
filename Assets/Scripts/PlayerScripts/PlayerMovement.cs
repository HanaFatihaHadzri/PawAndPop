using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private DynamicJoystick Joystick;
    //Joystick reference for assign in inspector

    public float moveSpeed = 7f;
    public Rigidbody2D rb;
    public Animator _animator;
    
    public float delta = 0;
    public Vector2 movement;

    //player to message
    public GameObject playerMsg;

    private void Start()
    {
        if (Joystick == null)
        {
            Joystick = FindAnyObjectByType<DynamicJoystick>();
            //Joystick.OnUpCallback = () => 
            //{
            //    movement.x = 0;
            //    movement.y = 0;
            //};
            Debug.Log("joystick is null");
            return;
        }

        playerMsg.SetActive(true);
    }

   
    void Update()
    {
        if (Joystick == null)
        {
            Joystick = FindAnyObjectByType<DynamicJoystick>();
            Debug.Log("joystick is null");
            return;
        }

        delta += Time.deltaTime * 3;
        delta %= Mathf.PI * 2;
        movement.x = Joystick.Horizontal;
        movement.y = Joystick.Vertical;

        _animator.SetFloat("Horizontal", movement.x);
        _animator.SetFloat("Speed", movement.sqrMagnitude); 
        
        if(movement.x == 0 && movement.y == 0)
        {
            playerMsg.SetActive(true);
        }
        else
        {
            //playerMsg.SetActive(false);
        }

    }

    private void FixedUpdate()
    {        
        Vector2 normalizedMovement = movement;
        normalizedMovement.Normalize();
        rb.MovePosition(rb.position + normalizedMovement * moveSpeed * Time.fixedDeltaTime);
    }
}
