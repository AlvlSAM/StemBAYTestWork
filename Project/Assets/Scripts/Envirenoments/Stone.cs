using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour, IBombInteractor, IDestroyable
{
    [SerializeField]
    [Range(0,1000)]
    private float HP;

    [SerializeField] [Range(0, 1000)]
    private float DestroyBombDamage;
    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void DoDamage(float damage)
    {
        HP -= damage;
    }

    public void Interact(BombType bombType, BombDetonate bombDetonate)
    {
        switch (bombType)
        {
            case BombType.Destroyer:
            {
                    DoDamage(DestroyBombDamage);
                    if(HP <= 0)
                    {
                        bombDetonate -= this.Interact;
                        Destroy();
                    }
                break;
            }
        }
    }
}
