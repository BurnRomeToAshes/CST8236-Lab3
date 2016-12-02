using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]

public class SphereControl : MonoBehaviour
{
    
    private float speed;
    private Vector3 pos;
    private float power;
    private float radius;
    private bool firstTime;
    private ParticleSystem[] explosions;
    private GameObject[] destroyableWalls;

    void Start()
    {
        firstTime = true;
        speed = 0.05f;
        radius = 1.0f;
        power = 200.0f;
        explosions = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem e in explosions)
        {
            e.Stop();
        }

        destroyableWalls = GameObject.FindGameObjectsWithTag("DestWalls");
        foreach (GameObject go in destroyableWalls)
        {
            go.SetActive(false);
        }

    }

    void Update()
    {
        pos = transform.position;
        if (Input.GetKey(KeyCode.W))
        {
            pos.z += speed;
            transform.position = Vector3.MoveTowards(transform.position, pos, 1f);
        }

        if (Input.GetKey(KeyCode.S))
        {
            pos.z -= speed;
            transform.position = Vector3.MoveTowards(transform.position, pos, 1f);
        }
        if (Input.GetKey(KeyCode.A))
        {
            pos.x -= speed;
            transform.position = Vector3.MoveTowards(transform.position, pos, 1f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            pos.x += speed;
            transform.position = Vector3.MoveTowards(transform.position, pos, 1f);
        }
    }

    void OnCollisionEnter(Collision col)
    {
      // for the first hit when spawn, don't do explosion
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null && !firstTime && hit.gameObject.name != "Roof" &&  hit.gameObject.name != "Sphere")
            {
                AudioSource audio = GetComponent<AudioSource>();
                switch (hit.gameObject.name)
                {
                    case "Wall1":
                        explosions[0].transform.position = hit.transform.position;
                        explosions[0].Play();
                        hit.gameObject.SetActive(false);
                        destroyableWalls[1].SetActive(true);
                        rb.AddExplosionForce(power, transform.position, radius, (float)ForceMode.Impulse);                  
                        audio.Play();
                        audio.Play(44100);
                        break;
                    case "Wall2":
                        explosions[1].transform.position = hit.transform.position;
                        explosions[1].Play();
                        hit.gameObject.SetActive(false);
                       destroyableWalls[0].SetActive(true);
                        rb.AddExplosionForce(power, transform.position, radius, (float)ForceMode.Impulse);
                        audio = GetComponent<AudioSource>();
                        audio.Play();
                        audio.Play(44100);
                        break;
                    case "Wall3":
                        explosions[2].transform.position = hit.transform.position;
                        explosions[2].Play();
                        hit.gameObject.SetActive(false);
                        destroyableWalls[3].SetActive(true);
                        rb.AddExplosionForce(power, transform.position, radius, (float)ForceMode.Impulse);
                        audio = GetComponent<AudioSource>();
                        audio.Play();
                        audio.Play(44100);
                        break;
                    case "Wall4":
                        explosions[3].transform.position = hit.transform.position;
                        explosions[3].Play();                       
                        hit.gameObject.SetActive(false);
                        destroyableWalls[2].SetActive(true);
                        rb.AddExplosionForce(power, transform.position, radius, (float)ForceMode.Impulse);
                        audio = GetComponent<AudioSource>();
                        audio.Play();
                        audio.Play(44100);
                        break;
                }
            }
        }
        firstTime = false;
    }
}
