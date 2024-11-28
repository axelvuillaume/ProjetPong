using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private KeyCode startKey = KeyCode.Space;

    [SerializeField] private GameObject effectPrefab; 
    
    private Vector3 movementDirection;
    private bool isGameStarted = false;

    private void Update()
    {
        if (!isGameStarted && Input.GetKeyDown(startKey))
        {
            StartGame();
        }

        if (isGameStarted)
        {
            MoveBall();
        }
        if( Mathf.Abs(transform.position.x) > 11){
            isGameStarted = false;
            Vector3 movement =  new Vector3(0,0,5 );
            transform.position = movement;
            TriggerEffect();

        }
    }

private void TriggerEffect()
{
    // Vérifie si le prefab d'effet est assigné
    if (effectPrefab != null)
    {
        // Instancie l'effet à la position de la balle
        GameObject effectInstance = Instantiate(effectPrefab, transform.position, Quaternion.identity);
        
        // Détruit l'effet après 2 secondes
        Destroy(effectInstance, 2f);
    }
    else
    {
        Debug.LogWarning("Aucun effet visuel assigné !");
    }
}

    private void StartGame()
    {
        isGameStarted = true;

        float angle = Random.Range(-45f, 45f);
        
        float direction = Random.Range(0, 2) == 0 ? 1 : -1;
        
        float radians = angle * Mathf.Deg2Rad;
        movementDirection = new Vector3(Mathf.Cos(radians) * direction, Mathf.Sin(radians), 0).normalized;
    }

    private void MoveBall()
    {
        transform.position += movementDirection * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider != null)
        {
            Vector3 normal = collision.contacts[0].normal;
            
            movementDirection = Vector3.Reflect(movementDirection, normal).normalized;
            
        }
    }
}
