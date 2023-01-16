using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBoxCOntroller : MonoBehaviour
{
    #region Expose

    [SerializeField] float _volumeUpPerParticle;
    [SerializeField] float _volumeDecayPerSecond;
    [SerializeField] float _volumeDecayDelay;
    [SerializeField] AudioSource _audioSource;

    [Header("Changement de couleur des barres de son")]
    [SerializeField] SpriteRenderer[] _volumeBars;
    [SerializeField] Color _enabledColor;
    [SerializeField] Color _disabledColor;


    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {

    }

    void Start()
    {
    }

    void Update()
    {
        if (Time.timeSinceLevelLoad > startDecayTime)
        {
            _volume -= _volumeDecayPerSecond * Time.deltaTime;
            _volume = Mathf.Clamp01(_volume);
        }
        _audioSource.volume = _volume;
        UpdateRenderer();

    }

    #endregion

    #region Methods
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _volume += _volumeUpPerParticle;
        //_volume = Mathf.Min(_volume + _volumeUpPerParticle, 1);
        _volume = Mathf.Clamp01(_volume);


        startDecayTime = Time.timeSinceLevelLoad + _volumeDecayDelay;
    }

    //Methode pour changer la couleur des barres de son
    void UpdateRenderer()
    {
        int numberBarsToEnable = Mathf.FloorToInt(_volumeBars.Length * _volume); // FloorToInt =  Arrondi un float à l'entier supérieur
        for (int i = 0; i < _volumeBars.Length; i++)
        {
            if (i < numberBarsToEnable)
            {
                _volumeBars[i].color = _enabledColor;
            }
            else
            {
                _volumeBars[i].color = _disabledColor;
            }
        }
    }



    #endregion

    #region Private & Protected

    private float _volume;
    private float startDecayTime;

    #endregion
}

