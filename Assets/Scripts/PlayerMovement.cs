using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sp;
    private float moveH, moveV;
    [SerializeField] private float moveSpeed;
     
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();

        sp.sprite = GameManager.instance.playerSprite;

        Debug.Log("Player Health: " + GameManager.instance.playerHealth);
        Debug.Log("Player Attack: " + GameManager.instance.playerAttack);
        Debug.Log("Player Exp: " + GameManager.instance.playerExp);
    }

    private void Update()
    {
        moveH = Input.GetAxis("Horizontal") * moveSpeed;
        moveV = Input.GetAxis("Vertical") * moveSpeed;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveH, moveV);
    }
}
