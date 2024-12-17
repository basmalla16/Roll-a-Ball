using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerController : MonoBehaviour
{
    public float speed = 0; 
    private Rigidbody rb; 
    private int count;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;



    private float movementX;
    private float movementY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent <Rigidbody>();
        count = 0;  
        SetCountText();
        winTextObject.SetActive(false);



    }
    void OnMove (InputValue movementValue)
   {
        Vector2 movementVector = movementValue.Get<Vector2>(); 
        movementX = movementVector.x; 
        movementY = movementVector.y; 
   }
      void FixedUpdate() 
   {
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed); 
   }
      void OnTriggerEnter(Collider other) 
   {
     if (other.gameObject.CompareTag("PickUp")) 
     {
          other.gameObject.SetActive(false);
          count = count + 1;

          SetCountText();
     }
   }

    void SetCountText() 
   {
       countText.text =  "Count: " + count.ToString();
       if (count >= 12)
       {
          winTextObject.SetActive(true);
          Destroy(GameObject.FindGameObjectWithTag("Enemy"));

       }
   }

   private void OnCollisionEnter(Collision collision)
   {
     if (collision.gameObject.CompareTag("Enemy"))
     {
       // Destroy the current object
       Destroy(gameObject); 
       // Update the winText to display "You Lose!"
       winTextObject.gameObject.SetActive(true);
       winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
     }
   } 
}
