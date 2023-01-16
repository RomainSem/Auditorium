using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class SetVolume : MonoBehaviour
{
    #region Exposed

    [SerializeField] AudioMixer _mixer;

    #endregion

    #region Unity Lifecycle

    private void Awake()
    {
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    #endregion

    #region Methods

    public void SetLevel(float sliderValue)
    {
        _mixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
    }

    #endregion

    #region Private & Protected

    

    #endregion
}
