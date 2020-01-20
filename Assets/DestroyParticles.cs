using UnityEngine;
using System.Collections;

public class DestroyParticles : MonoBehaviour
{

    private void Start()
    {
        Destroy(gameObject, GetComponent<ParticleSystem>().main.duration + GetComponent<ParticleSystem>().main.startLifetime.constant);
    }

}