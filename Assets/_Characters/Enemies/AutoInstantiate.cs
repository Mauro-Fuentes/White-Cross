using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoInstantiate : MonoBehaviour
{
    [SerializeField] Transform particles1;

    public List<Transform> copies;

    void SpawnAttackParticle()
    {
        particles1 = GetComponent<Transform>();
        Transform playAttackParticle = Instantiate ( particles1, transform.position, particles1.transform.rotation);

        copies.Add(playAttackParticle);
        

    }

}
