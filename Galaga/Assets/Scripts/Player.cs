using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private Vector3 _offset = new Vector3(0, .5f, 0);

    private float _puedeDisparar = -1;
    [SerializeField]
    private float _fireRate = .5f;

    [SerializeField]
    private float _vidas = 3;
    [SerializeField]
    private GameObject _spawn;

    void Start()
    {
        transform.position = new Vector3(0,0,0);
    }

    void Update()
    {
        Movimiento();
        Limites();
        Disparo();
    }

    void Disparo()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _puedeDisparar)
        {
            _puedeDisparar = Time.time + _fireRate;
            Instantiate(_laserPrefab, transform.position + _offset, Quaternion.identity);
        }
    }

    void Movimiento()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        Vector3 direccion = new Vector3(inputHorizontal, inputVertical, 0);

        transform.Translate(direccion * _speed * Time.deltaTime);
    }

    void Limites()
    {
        if (transform.position.x > 9)
        {
            transform.position = new Vector3(-9, transform.position.y, 0);
        }
        else if (transform.position.x < -9)
        {
            transform.position = new Vector3(9, transform.position.y, 0);
        }

        if (transform.position.y >= 5.5f)
        {
            transform.position = new Vector3(transform.position.x, 5.5f, 0);
        }
        else if (transform.position.y <= -3.4f)
        {
            transform.position = new Vector3(transform.position.x, -3.4f, 0);
        }
    }

    public void SistemaVidas()
    {
        _vidas--;
        Debug.Log(_vidas);

        if (_vidas <= 0)
        {
            Destroy(gameObject);
            _spawn.GetComponent<Spawn>().StopSpawn();
        }
    }
}
