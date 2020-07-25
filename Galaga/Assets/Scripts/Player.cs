using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //Variables velocidad
    [SerializeField]
    private float _speed = 5f;
    private float _minSpeed = 5f;
    private float _maxSpeed = 10f;

    //Poder triple shot
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private Vector3 _offset = new Vector3(0, .5f, 0);
    [SerializeField]
    private Vector3 _offsetTripleshot = new Vector3(0, .5f, 0);
    [SerializeField]
    private bool _isTripleshotActive = false;

    //Poder escudos
    [SerializeField]
    private GameObject _shield;
    private float _cantidadMaximaEscudos =3;
    private float _cantidadEscudos;
    private bool _escudoActivo;

    //Propiedades disparo
    private float _puedeDisparar = -1;
    [SerializeField]
    private float _fireRate = .5f;

    //Sistema de vidas
    [SerializeField]
    private float _vidas = 3;
    [SerializeField]
    private GameObject _spawn;

    //UI
    [SerializeField]
    private Text _TextoVidas;
    [SerializeField]
    private Text _TextoEscudos;
    [SerializeField]
    private GameObject[] _ImagenesVidas;
    [SerializeField]
    private GameObject[] _ImagenesEscudos;

    void Start()
    {
        transform.position = new Vector3(0,0,0);
    }

    void Update()
    {
        Movimiento();
        Limites();
        Disparo();
        ActivarEscudo();
        UserIterface();
    }

    void Disparo()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _puedeDisparar)
        {
            _puedeDisparar = Time.time + _fireRate;
            if (_isTripleshotActive == true)
            {
                Instantiate(_tripleShotPrefab, transform.position + _offsetTripleshot, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + _offset, Quaternion.identity);
            }
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
        if (_escudoActivo == true)
        {
            _shield.SetActive(false);
            _escudoActivo = false;
            return;
        }
        _vidas--;

        
        _ImagenesVidas[Mathf.RoundToInt(_vidas)].SetActive(false);

        Debug.Log(_vidas);

        if (_vidas <= 0)
        {
            Destroy(gameObject);
            _spawn.GetComponent<Spawn>().StopSpawn();
        }
    }

    public void ActivarTripleshot()
    {
        _isTripleshotActive = true;
        StartCoroutine(DesactivarTripleshot());
    }

    IEnumerator DesactivarTripleshot()
    {
        yield return new WaitForSeconds(3f);
        _isTripleshotActive = false;
    }

    public void AumentarSpeed()
    {
        if (_speed < _maxSpeed)
        {
            _speed++;
        }
        StartCoroutine(DisminuirSpeed());
    }

    IEnumerator DisminuirSpeed()
    {
        yield return new WaitForSeconds(10f);
        if (_speed > _minSpeed)
        {
            _speed --;
        }
    }

    public void AumentarEscudo()
    {
        Debug.Log(_cantidadEscudos);
        if (_cantidadEscudos < _cantidadMaximaEscudos)
        {
            _ImagenesEscudos[Mathf.RoundToInt(_cantidadEscudos)].SetActive(true);
            _cantidadEscudos++;
        }
    }

    private void ActivarEscudo()
    {
        if (_cantidadEscudos > 0 && Input.GetKeyDown(KeyCode.Q))
        {
            _escudoActivo = true;
            _shield.SetActive(true);
            _cantidadEscudos--;
            _ImagenesEscudos[Mathf.RoundToInt(_cantidadEscudos)].SetActive(false);
        }
    }

    void UserIterface()
    {
        _TextoVidas.text = "Vidas: " + _vidas;
        _TextoEscudos.text = "Escudos: " + _cantidadEscudos;
    }

}
