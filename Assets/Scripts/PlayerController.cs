using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public float jumpForce =2f;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;


    private Rigidbody rb;
    private bool jumpPermission = true;
    private float movementX;
    private float movementY;
    
    private int count;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winTextObject.gameObject.SetActive(false);
    }

    void OnJump()
    {
        if (jumpPermission)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpPermission = false;
        }
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 19)
        {
            // Set the text value of your 'winText'
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("Respawn"))
        {
            transform.position = new Vector3(0f, 0.5f, 0f);
            rb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            jumpPermission = true;
        }
    }
}
