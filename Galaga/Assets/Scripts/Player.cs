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
    private UIManager _uIManager;

    //Animations
    private Animator anim;

    //Wings
    [SerializeField]
    private GameObject _leftWing, _rightWing, _thrusters;

    //Audio
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _laser;
    [SerializeField]
    private AudioClip _explosion;
    [SerializeField]
    private AudioClip _powerUp;

    void Start()
    {
        anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        transform.position = new Vector3(0,0,0);
        _leftWing.SetActive(false);
        _rightWing.SetActive(false);
    }

    void Update()
    {
        Movimiento();
        Limites();
        Disparo();
        ActivarEscudo();
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
            _audioSource.clip = _laser;
            _audioSource.Play(0);
        }
    }

    void Movimiento()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");
        anim.SetInteger("Turn", Mathf.RoundToInt(inputHorizontal));
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


        _uIManager.ActualizarVidas(Mathf.RoundToInt(_vidas));

        Debug.Log(_vidas);

        if (_vidas == 2)
        {
            _leftWing.SetActive(true);
        }

        if (_vidas == 1)
        {
            _rightWing.SetActive(true);
        }

        if (_vidas <= 0)
        {
            Death();
        }
    }

    public void ActivarTripleshot()
    {
        _isTripleshotActive = true;
        _audioSource.clip = _powerUp;
        _audioSource.Play(0);
        StartCoroutine(DesactivarTripleshot());
    }

    IEnumerator DesactivarTripleshot()
    {
        yield return new WaitForSeconds(3f);
        _isTripleshotActive = false;
    }

    public void AumentarSpeed()
    {
        _audioSource.clip = _powerUp;
        _audioSource.Play(0);
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
        if (_cantidadEscudos < _cantidadMaximaEscudos)
        {
            _audioSource.clip = _powerUp;
            _audioSource.Play(0);
            _uIManager.AumentarEscudos(Mathf.RoundToInt(_cantidadEscudos));
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
            _uIManager.ReducirEscudos(Mathf.RoundToInt(_cantidadEscudos));
        }
    }

    private void Death()
    {
        _audioSource.clip = _explosion;
        _audioSource.Play(0);
        _thrusters.SetActive(false);
        _leftWing.SetActive(false);
        _rightWing.SetActive(false);
        CapsuleCollider2D coll = GetComponent<CapsuleCollider2D>();
        coll.enabled = false;
        _speed = 0;
        anim.SetTrigger("Explode");
        Destroy(gameObject, 2.5f);
        _spawn.GetComponent<Spawn>().StopSpawn();
    }

}
