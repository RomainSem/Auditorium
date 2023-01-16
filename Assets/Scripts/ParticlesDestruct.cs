using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesDestruct : MonoBehaviour
{
    #region Expose

    [SerializeField] private float _minimumSpeed;

    #endregion

    #region Unity Lyfecycle

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(_rigidbody.velocity.magnitude < _minimumSpeed)
        {
            gameObject.SetActive(false);
        }
    }
    #endregion

    #region Methods

    #endregion

    #region Private & Protected

    private Rigidbody2D _rigidbody;

    #endregion
}
