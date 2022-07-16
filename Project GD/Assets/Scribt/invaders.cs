using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class invaders : MonoBehaviour
{
    public invader[] Prefab;

    public int rows = 5;

    public int cols = 15;

    Vector3 _direction = Vector2.right;
    public AnimationCurve speed;

    public int amountKilled { get; private set; }
    public int totalInvaders => this.rows * this.cols;
    public float percentkilled => (float) this.amountKilled /(float) this.totalInvaders;


    private void Awake()
    {
        for(int row = 0; row < this.rows; row++)
        {
            float width = 2.0f * (this.cols - 1);
            float height = 2.0f * (this.rows -1);
            Vector2 centering = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 2.0f), 0.0f);
            for(int col = 0; col < this.cols; col++)
            {
                invader inv = Instantiate(this.Prefab[row], this.transform);
                inv.killed += InvadeKilled;
                Vector3 position = rowPosition;
                position.x += col * 2.0f;
                inv.transform.localPosition = position;

            }

                
        }

    
    }

    private void Update()
    {
        this.transform.position += _direction * this.speed.Evaluate(this.percentkilled) * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach(Transform inv in this.transform)
        {
            if(!inv.gameObject.activeInHierarchy)
            {
                continue;
            }
            if(_direction == Vector3.right && inv.position.x >= (rightEdge.x -1.0f))
            {
                AdvanceRow();

            }else if (_direction == Vector3.left && inv.position.x <= (leftEdge.x + 1.0f))
                    {
                AdvanceRow();

            }
        }

    }

    private void AdvanceRow()
    {
        _direction.x *= -1.0f;

        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }

    public void InvadeKilled()
    {
        this.amountKilled++;

        if(this.amountKilled >= this.totalInvaders)
        {
            SceneManager.LoadScene("Level2");
        }

    }
}

