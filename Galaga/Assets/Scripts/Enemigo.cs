using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    void Update()
    {
        transform.Translate(new Vector3(0,-1,0) * _speed * Time.deltaTime);

        if (transform.position.y < -4.5f)
        {
            transform.position = new Vector3(Random.Range(-8f, 8f), 6.5f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Si choca con el jugador
        if (other.tag == "Player")
        {
            Player pl = other.GetComponent<Player>();
            if (pl != null)
            {
                pl.SistemaVidas();
                Destroy(gameObject);
            }
        }

        //Si choca con el Laser
        if (other.tag == "Proyectil")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
