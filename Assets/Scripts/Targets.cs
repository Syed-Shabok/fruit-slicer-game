using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Targets : MonoBehaviour
{   
    public int pointValue;
    public float xRange = 4.5f;
    public float ySpawnPos = -2.0f;
    public float minSpeed = 12.0f;
    public float maxSpeed = 16.0f;
    public float maxTorque = 10.0f;
    private Rigidbody targetRb;
    private GameManager gameManager;
    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        transform.position = RandomSpawnPos();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        OnTouchEnter();
    }
    
    private void OnTouchEnter()
    {
        if (Input.touchCount > 0 && gameManager.isGameActive && Time.timeScale != 0.0f)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10f));

            // Define a larger touch zone radius
            float touchZoneRadius = 0.7f; // Adjust as needed
            Collider[] hitColliders = Physics.OverlapSphere(touchPosition, touchZoneRadius);

            foreach (Collider hitCollider in hitColliders)
            {
                if (hitCollider.gameObject == gameObject)
                {
                    Destroy(gameObject);
                    Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
                    gameManager.UpdateScore(pointValue);
                    break;
                }
            }
        }
    }


    
    // private void OnMouseEnter()
    // {
    //     if(Input.GetMouseButton(0) && gameManager.isGameActive && Time.timeScale != 0.0f)
    //     {
    //         Destroy(gameObject);
    //         Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
    //         gameManager.UpdateScore(pointValue);
    //     }

    // }

    private void OnTriggerEnter(Collider collider)
    {   Debug.Log("Trigger");
        if (gameManager.isGameActive)
        {   
            Destroy(gameObject);
            if (!gameObject.CompareTag("Bad"))
            {
                gameManager.UpdateLives(1);
            }
        }
    }

    // public void DestroyTarget()
    // {
    //     if(gameManager.isGameActive)
    //     {
    //         Destroy(gameObject);
    //         Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
    //         gameManager.UpdateScore(pointValue);
    //     }
    // }

    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    private Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos, 0);
    }

}
