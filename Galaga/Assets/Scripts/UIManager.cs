using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //Vidas y escudo
    [SerializeField]
    private Image _imagenVidas;
    [SerializeField]
    private Sprite[] _vidas;
    [SerializeField]
    private GameObject[] _ImagenesEscudos;

    //Game over
    [SerializeField]
    private Text _GameOver;

    [SerializeField]
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActualizarVidas(int vidasActuales)
    {
        _imagenVidas.sprite = _vidas[vidasActuales];

        if (vidasActuales == 0)
        {
            _GameOver.gameObject.SetActive(true);
            _gameManager.GameOver();
            StartCoroutine(GameOverParpadeando());
        }
    }

    public void AumentarEscudos(int escudosActuales)
    {
        Debug.Log("Escudo aumentado");
        _ImagenesEscudos[escudosActuales].SetActive(true);
    }
    public void ReducirEscudos(int escudosActuales)
    {
        _ImagenesEscudos[escudosActuales].SetActive(false);
    }

    IEnumerator GameOverParpadeando()
    {
        while (true)
        {
            _GameOver.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _GameOver.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
