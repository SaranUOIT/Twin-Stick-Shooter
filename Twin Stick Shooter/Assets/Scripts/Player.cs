using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // InputMaster
    InputMaster inputMaster;

    // Move
    Vector2 moveInput;
    // Fire Direction
    Vector2 lookPostion;

    public Camera mainCamera;
    public float speed = 10f;
    Rigidbody rb;

    public Gun currentGun;

    public float health;

    private void Awake()
    {
        inputMaster = new InputMaster();
        inputMaster.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputMaster.Player.FireDirection.performed += ctx => lookPostion = ctx.ReadValue<Vector2>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        // Move the player
        float h = moveInput.x;
        float v = moveInput.y;

        Vector3 movement = new Vector3(h, 0, v) * speed * Time.deltaTime;

        rb.MovePosition(transform.position + movement);
    }

    void Rotate()
    {
        // Rotate Player
        Vector2 lookInput = lookPostion;

        Vector3 lookDir = (Vector3.right * lookInput.x + Vector3.forward * lookInput.y);
        // Look Direction
        if (lookDir.sqrMagnitude > 0.0f)
        {
            rb.MoveRotation(Quaternion.LookRotation(lookDir));
            Shoot(true);
        }   
        else
        {
            Shoot(false);
        }
    }

    void Shoot(bool temp)
    {
        currentGun.isShooting = temp;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Enemy e = collision.gameObject.GetComponent<Enemy>();
        if (e)
        {
            health -= e.damage;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnEnable()
    {
        inputMaster.Enable();
    }

    private void OnDisable()
    {
        inputMaster.Disable();
    }
}
