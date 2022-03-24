using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBomb : MonoBehaviour
{
    public event BombDetonate Detonate;

    [SerializeField]
    [Range(0f, 10f)]
    private float timeToExplosion;

    internal BombType bombType;

    internal virtual void SetType()
    {
        bombType = BombType.Freeze;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IBombInteractor interactor;
        if (collision.gameObject.TryGetComponent(out interactor))
        {
            Detonate += interactor.Interact;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IBombInteractor interactor;
        if (collision.gameObject.TryGetComponent(out interactor))
        {
            Detonate -= interactor.Interact;
        }
    }

    internal virtual void Start()
    {
        SetType();
        StartCoroutine(timer());
    }

    internal virtual void Explosion()
    {
        if (!(Detonate is null))
        {
            Detonate(bombType, Detonate);

        }
        Destroy(gameObject);
    }

    internal virtual IEnumerator timer()
    {
        while (timeToExplosion > 0)
        {
            timeToExplosion -= Time.deltaTime;
            yield return null;
        }
        Explosion();
        yield return null;
    }
}
