using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class ClickAndSwipe : MonoBehaviour
{   
    private GameManager gameManager;
    private Camera cam;
    private Vector3 mousePos;
    private TrailRenderer trail;
    private BoxCollider col;
    private bool isSwiping = false;

    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
        trail = GetComponent<TrailRenderer>();
        col = GetComponent<BoxCollider>();
        trail.enabled = false;
        col.enabled = false;

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.isGameActive)
        {
            if(Input.GetMouseButtonDown(0))
            {
                isSwiping = true;
                UpdateComponents();
            }
            else if(Input.GetMouseButtonUp(0))
            {
                isSwiping = false;
                UpdateComponents();
            }

            if(isSwiping)
            {
                UpdateMousePosition();
            }
        }
    }

    void UpdateMousePosition()
    {
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
        transform.position = mousePos;
    }

    void UpdateComponents()
    {
        trail.enabled = isSwiping;
        col.enabled = isSwiping;
    }

    // private void OnCollisionEnter(Collision collision)
    // {   Debug.Log("Collision");
    //     if(collision.gameObject.GetComponent<Targets>())
    //     {
    //         //Destroy the target
    //         collision.gameObject.GetComponent<Targets>().DestroyTarget();
    //     }
    // }

}
