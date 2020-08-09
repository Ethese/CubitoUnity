using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    private Animator _anim;
    private CapsuleCollider2D _collider2D;

    //Audio
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _explosion;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _anim = gameObject.GetComponent<Animator>();
        _collider2D = gameObject.GetComponent<CapsuleCollider2D>();
    }

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
                _speed = 0;
                _collider2D.enabled = false;
                _audioSource.clip = _explosion;
                _audioSource.Play(0);
                _anim.SetTrigger("Explode");
                Destroy(gameObject, 4f);
            }
        }

        //Si choca con el Laser
        if (other.tag == "Proyectil")
        {
            Destroy(other.gameObject);
            _speed = 0;
            _collider2D.enabled = false;
            _audioSource.clip = _explosion;
            _audioSource.Play(0);
            _anim.SetTrigger("Explode");
            Destroy(gameObject, 4f);
        }
    }
}
