using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _idPowerUp;
    private float _speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -1, 0) * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Player player = collision.gameObject.GetComponent<Player>();
            switch (_idPowerUp)
            {
                case 0:
                    player.ActivarTripleshot();
                    break;
                case 1:
                    player.AumentarSpeed();
                    break;
                case 2:
                    player.AumentarEscudo();
                    break;
                default:
                    Debug.Log("CASO DEFAULT");
                    break;
            }
            Destroy(gameObject);
        }
    }
}
