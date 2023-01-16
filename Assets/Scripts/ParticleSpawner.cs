using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ParticleSpawner : MonoBehaviour
{
    #region Exposed

    [Header("Données des particules")]
    [SerializeField] ParticlePool _pool;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _particleSpeed;
    [SerializeField] private float _spawnerRadius;
    [SerializeField] private float _nextSpawnTime;

    [SerializeField]
    [Range(0.5f, 2)]
    private float _particleDrag;

    [Header("Valeurs pour dessiner le gizmo")]
    [SerializeField] private Color _gizmoColor;
    [SerializeField] private bool _drawGizmo;

    #endregion

    #region Unity Lyfecycle
    void Start()
    {

    }

    void Update()
    {
        if (Time.timeSinceLevelLoad > _nextSpawnTime)
        {
            //Spawn des particules
            GameObject newParticle = SpawnParticle();

            //Launch des particules
            LaunchParticle(newParticle);

            //Debug.Log("Spawn particle");
            _nextSpawnTime = Time.timeSinceLevelLoad + _spawnDelay;
        }
    }
    #endregion

    #region Methods

    private void OnDrawGizmos()
    {
        if (_drawGizmo)
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawWireSphere(transform.position, _spawnerRadius);
            Gizmos.DrawRay(transform.position, transform.right * _particleSpeed);
        }
    }

    private GameObject SpawnParticle()
    {
        Vector2 position = Random.insideUnitCircle * _spawnerRadius + (Vector2)transform.position;
        GameObject particle = _pool.GetParticle();
        if (particle != null)
        {
            particle.GetComponent<TrailRenderer>().Clear();
            particle.SetActive(true);
            particle.transform.position = position;
        }
        return particle;
    }

    private void LaunchParticle(GameObject particle)
    {
        if (particle != null)
        {
            Rigidbody2D rigidbody = particle.GetComponent<Rigidbody2D>();
            rigidbody.drag = _particleDrag;
            rigidbody.velocity = transform.right * _particleSpeed;
        }
    }

    #endregion

    #region Private & Protected

    #endregion
}
