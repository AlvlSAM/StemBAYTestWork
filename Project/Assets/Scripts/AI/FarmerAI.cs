using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FarmerAI : MonoBehaviour, IBaseAI, IBombInteractor
{

    [SerializeField]
    [Range(0f, 100f)]
    private float walkSpeed = 0;

    [SerializeField]
    [Range(0f, 100f)]
    private float runSpeed = 0;

    private float speed;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private Transform AI;

    [SerializeField]
    private float maxRandTime = 0;

    [SerializeField]
    private List<Sprite> states;

    [SerializeField]
    private List<Sprite> freezingStates;

    [SerializeField]
    private SpriteRenderer characterSprite;


    [SerializeField]
    private BoxCollider2D characterColl;

    [SerializeField]
    private float HP;

    [SerializeField]
    private float DestroyBombDamage;


    [SerializeField]
    private List<Vector2> colliderSizeStates;
    [SerializeField]
    private List<Vector2> colliderOffsetStates;

    [SerializeField]
    private float FreezingTime;
    private float nowFrezing;
    private bool freez;


    private float timeRand = 0;
    private Vector3 mover;
    private List<IBaseAI.Control> history = new List<IBaseAI.Control>();
    public void Move()
    {

    }

    public void Observation()
    {

    }

    void Start()
    {
        speed = walkSpeed;
    }


    private void FixedUpdate()
    {
        timeRand -= Time.fixedDeltaTime;
        Random.InitState((int)Time.realtimeSinceStartup);
        RaycastHit2D hitUp;
        RaycastHit2D hitDown;
        RaycastHit2D hitLeft;
        RaycastHit2D hitRight;

        hitUp = Physics2D.Raycast(AI.position, Vector2.up, 1.5f);
        hitDown = Physics2D.Raycast(AI.position, Vector2.down, 1.5f);
        hitLeft = Physics2D.Raycast(AI.position, Vector2.left, 1.5f);
        hitRight = Physics2D.Raycast(AI.position, Vector2.right, 1.5f);

        float rand = Random.Range(0, 20f);

        if (rand > timeRand)
        {
            List<IBaseAI.Control> controls = new List<IBaseAI.Control>();

            bool check = history.Count < 6;
            if (check)
            {
                if (hitUp.collider is null)
                {
                    controls.Add(IBaseAI.Control.Up);
                }
                if (hitDown.collider is null)
                {
                    controls.Add(IBaseAI.Control.Down);
                }
                if (hitLeft.collider is null)
                {
                    controls.Add(IBaseAI.Control.Left);
                }
                if (hitRight.collider is null)
                {
                    controls.Add(IBaseAI.Control.Right);
                }
            }
            else
            {
                List<bool> available = checkHistory();
                if (hitUp.collider is null && available[0])
                {
                    controls.Add(IBaseAI.Control.Up);
                }
                if (hitDown.collider is null && available[2])
                {
                    controls.Add(IBaseAI.Control.Down);
                }
                if (hitLeft.collider is null && available[3])
                {
                    controls.Add(IBaseAI.Control.Left);
                }
                if (hitRight.collider is null && available[1])
                {
                    controls.Add(IBaseAI.Control.Right);
                }
            }

            if (controls.Count == 0)
            {
                return;
            }

            int randInd = Random.Range(0, controls.Count - 1);
            if (check)
            {
                history.Add(controls[randInd]);
            }
            else
            {
                for(int i = 4; i > 0; i--)
                {
                    history[i] = history[i-1];
                }
                history[0] = controls[randInd];

            }
            switch (controls[randInd])
            {
                case IBaseAI.Control.Up:
                    mover = Vector3.up;
                    SetState(0);
                    break;
                case IBaseAI.Control.Right:
                    mover = Vector3.right;
                    SetState(1);
                    break;
                case IBaseAI.Control.Down:
                    mover = Vector3.down;
                    SetState(2);
                    break;
                case IBaseAI.Control.Left:
                    mover = Vector3.left;
                    SetState(3);
                    break;
            }
            timeRand = maxRandTime;
        }
        rb.velocity = mover * speed * Time.fixedDeltaTime;
    }

    private List<bool> checkHistory()
    {
        List<int> countHistory = new List<int> { 0, 0, 0, 0 };
        List<bool> available = new List<bool>();
        foreach (IBaseAI.Control c in history)
        {
            switch (c)
            {
                case IBaseAI.Control.Up:
                    countHistory[0] += 1;
                    break;
                case IBaseAI.Control.Right:
                    countHistory[1] += 1;
                    break;
                case IBaseAI.Control.Down:
                    countHistory[2] += 1;
                    break;
                case IBaseAI.Control.Left:
                    countHistory[3] += 1;
                    break;
            }
        }
        foreach (int i in countHistory)
        {
            available.Add(i < 2);
        }
        return available;
    }


    #region BombInteract

    IEnumerator Freez()
    {
        while (nowFrezing < FreezingTime)
        {
            nowFrezing += Time.deltaTime;
            yield return null;
        }
        speed = walkSpeed;
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
                    speed = 0;
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
                    GetDamage();
                    if(HP <= 0)
                    {
                        Destroy(gameObject);
                    }
                    else
                    {
                        walkSpeed *= HP / 100;
                        speed *= HP / 100;
                    }
                    break;
                }
        }
    }

    #endregion

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

    public void GetDamage()
    {
        HP -= DestroyBombDamage;
    }
}
