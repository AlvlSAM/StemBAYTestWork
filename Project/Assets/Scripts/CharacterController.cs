using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour, IBombInteractor
{

    [SerializeField]
    private List<Sprite> states;

    [SerializeField]
    private List<Sprite> freezingStates;

    [SerializeField]
    private SpriteRenderer characterSprite;

    [SerializeField]
    private BoxCollider2D characterColl;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float Maxspeed = 0;
    private float speed;

    [SerializeField]
    private float FreezingTime;
    private float nowFrezing;
    private bool freez;

    [SerializeField]
    private List<Vector2> colliderSizeStates;
    [SerializeField]
    private List<Vector2> colliderOffsetStates;

    [SerializeField]
    private List<GameObject> bombs;

    [SerializeField]
    private List<GameObject> bombsCount;

    private Vector3 mover = Vector3.zero;

    private int selectedBomb = 0;
    #region BombInteract

    IEnumerator Freez()
    {
        while (nowFrezing < FreezingTime)
        {
            nowFrezing += Time.deltaTime;
            yield return null;
        }
        speed = Maxspeed;
        freez = false;

        int ind = -1;
        ind = freezingStates.IndexOf(characterSprite.sprite);
        if (ind != -1)
        {
            SetState(ind);
        }
        yield return null;
    }
    public void Interact(BombType bombType, BombDetonate bombDetonate)
    {
        switch (bombType)
        {
            case BombType.Freeze:
                {
                    nowFrezing = 0;
                    speed /= 2;
                    freez = true;
                    int ind = -1;
                    ind = states.IndexOf(characterSprite.sprite);
                    if (ind != -1)
                    {
                        SetState(ind);
                    }
                    StartCoroutine(Freez());
                    break;
                }
            case BombType.Jumper:
                {
                    rb.AddForce(mover * 20);
                    break;
                }

            case BombType.Destroyer:
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    break;
                }
        }
    }

    #endregion

    #region ONLY_UNITY
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
            SetState(0);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            mover = Vector3.down;
            SetState(2);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            mover = Vector3.left;
            SetState(3);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            mover = Vector3.right;
            SetState(1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bombs[2], gameObject.transform.position, gameObject.transform.rotation);
        }

    }
#endif
    #endregion

    #region Android Movement
    public void MoveUp()
    {
        mover = Vector3.up;
        SetState(0);
    }
    public void MoveRight()
    {
        mover = Vector3.right;
        SetState(1);
    }
    public void MoveDown()
    {
        mover = Vector3.down;
        SetState(2);
    }
    public void MoveLeft()
    {
        mover = Vector3.left;
        SetState(3);
    }

    public void StopMove()
    {
        mover = Vector3.zero;
    }
    public void ChangeBomb(int ind)
    {
        selectedBomb = ind;
    }
    public bool PlantBomb()
    {
        if(GameObject.FindObjectOfType<UIController>().nowBombs[selectedBomb] > 0)
        {
            Instantiate(bombs[selectedBomb], gameObject.transform.position, gameObject.transform.rotation);
            return true;
        }
        return false;
    }
    private void Start()
    {
        speed = Maxspeed;
    }

    #endregion
    private void FixedUpdate()
    {
        rb.velocity = mover * speed * Time.fixedDeltaTime;
    }

    private void SetState(int state)
    {
        if (freez)
        {
            if (state == 0)
            {
                characterColl.size = colliderSizeStates[0];
                characterColl.offset = colliderOffsetStates[0];
                characterSprite.sprite = freezingStates[0];
            }
            else if (state == 2)
            {
                characterColl.size = colliderSizeStates[2];
                characterColl.offset = colliderOffsetStates[2];
                characterSprite.sprite = freezingStates[2];
            }
            else if (state == 3)
            {
                characterColl.size = colliderSizeStates[3];
                characterColl.offset = colliderOffsetStates[3];
                characterSprite.sprite = freezingStates[3];
            }
            else if (state == 1)
            {
                characterColl.size = colliderSizeStates[1];
                characterColl.offset = colliderOffsetStates[1];
                characterSprite.sprite = freezingStates[1];
            }
        }
        else
        {
            if (state == 0)
            {
                characterColl.size = colliderSizeStates[0];
                characterColl.offset = colliderOffsetStates[0];
                characterSprite.sprite = states[0];
            }
            else if (state == 2)
            {
                characterColl.size = colliderSizeStates[2];
                characterColl.offset = colliderOffsetStates[2];
                characterSprite.sprite = states[2];
            }
            else if (state == 3)
            {
                characterColl.size = colliderSizeStates[3];
                characterColl.offset = colliderOffsetStates[3];
                characterSprite.sprite = states[3];
            }
            else if (state == 1)
            {
                characterColl.size = colliderSizeStates[1];
                characterColl.offset = colliderOffsetStates[1];
                characterSprite.sprite = states[1];
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IBaseAI baseAI;
        if(collision.gameObject.TryGetComponent(out baseAI))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
