using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class padsMovement : MonoBehaviour
{
    [SerializeField] private KeyCode up = KeyCode.W;
    [SerializeField] private KeyCode down = KeyCode.S  ;
    [SerializeField] private GameObject pad;
    [SerializeField] private GameObject effectPrefab; 
    [SerializeField] private float maxY = 3.5f;
    [SerializeField] private float minY = -3.5f;
    [SerializeField] private float speed = 5f;


    private GameObject CreatePad(){
        return Instantiate(pad);
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

    private void MovePad(){

        Vector3 movement = Vector3.zero;

        if (Input.GetKey(up) && transform.position.y < maxY )
        {
            movement = Vector3.up * speed * Time.deltaTime;
        }
        else if (Input.GetKey(down) && transform.position.y > minY )
        {
            movement = Vector3.down * speed * Time.deltaTime;
        }

        transform.position += movement;
    }


    private void OnCollisionEnter(Collision collision)
    {
        TriggerEffect();
    }


    private void Update()
    {
        MovePad();
    }

}
