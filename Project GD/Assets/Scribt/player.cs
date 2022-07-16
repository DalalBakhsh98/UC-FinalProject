using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    public Shoot pew;
    public float speed = 5.0f;
    private bool pewActive;
    public AudioSource bang;


    void Start()
    {
       bang = GetComponent<AudioSource>(); 
    }


    private void Update()
    {
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += Vector3.left * this.speed * Time.deltaTime;
            
        }else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            bang.Play();
            shoot();
        }
    }

    private void shoot()
    {
        

        if (!pewActive)
        {
            Shoot pam = Instantiate(this.pew, this.transform.position, Quaternion.identity);
            pam.destroyed += pewDestroyed; 
            pewActive = true;
        }
            
    }

    public void pewDestroyed()
    {
        pewActive =false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("invadr"))
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
