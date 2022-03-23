using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerAI : MonoBehaviour, IBaseAI
{

    [SerializeField]
    [Range(0f, 100f)]
    private float walkSpeed = 0;

    [SerializeField]
    [Range(0f, 100f)]
    private float runSpeed = 0;

    public void Move()
    {

    }

    public void Observation()
    {

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
