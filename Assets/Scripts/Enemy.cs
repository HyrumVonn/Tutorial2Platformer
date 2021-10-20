using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int damage = 1;
    [SerializeField] bool destroyOnContact = true;

    public Transform[] moveToLocation;
    public float movementSpeed = 20f;

    Transform currentMoveToTarget;

    void Start()
    {
        currentMoveToTarget = moveToLocation[0];
        FindMoveToTarget();
    }

    void FindMoveToTarget()
    {
        for (int i = 0; i < moveToLocation.Length; i++)
        {
            if ((transform.position - moveToLocation[i].position).magnitude <= .01)
            {
                if (i < (moveToLocation.Length - 1))
                {
                    currentMoveToTarget = moveToLocation[i + 1];
                }
                else
                {
                    currentMoveToTarget = moveToLocation[0];
                }

                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.gameObject.GetComponent<PlayerController>())
        {
            PlayerController other = collision.collider.gameObject.GetComponent<PlayerController>();

            other.TakeDamage(damage);
            if(destroyOnContact)
            {
                gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        FindMoveToTarget();


        transform.position = Vector3.MoveTowards(transform.position, currentMoveToTarget.position, movementSpeed * Time.deltaTime);

    }
}

