using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    [SerializeField]
    private List<Sprite> states;

    [SerializeField]
    private SpriteRenderer characterSprite;

    [SerializeField]
    private BoxCollider2D characterColl;

    [SerializeField]
    private float speed;

    [SerializeField]
    private List<Vector2> colliderSizeStates;
    [SerializeField]
    private List<Vector2> colliderOffsetStates;

    private Vector3 mover = Vector3.zero;
#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.W))
        {
            mover = Vector3.zero;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            mover = Vector3.zero;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            mover = Vector3.zero;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            mover = Vector3.zero;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            mover = Vector3.up;
            characterColl.size = colliderSizeStates[0];
            characterColl.offset = colliderOffsetStates[0];
            characterSprite.sprite = states[0];
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            mover = Vector3.down;
            characterColl.size = colliderSizeStates[2];
            characterColl.offset = colliderOffsetStates[2];
            characterSprite.sprite = states[2];
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            mover = Vector3.left;
            characterColl.size = colliderSizeStates[3];
            characterColl.offset = colliderOffsetStates[3];
            characterSprite.sprite = states[3];
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            mover = Vector3.right;
            characterColl.size = colliderSizeStates[1];
            characterColl.offset = colliderOffsetStates[1];
            characterSprite.sprite = states[1];
        }

        

    }


    private void FixedUpdate()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = mover * speed*Time.fixedDeltaTime;
    }
#endif
}
