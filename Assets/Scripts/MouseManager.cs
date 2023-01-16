using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    #region Exposed

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] Texture2D _mouseTextureMove;
    [SerializeField] Texture2D _mouseTextureResize;
    [SerializeField] float _forceRatio;

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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, _layerMask);
        if (hit.collider != null && !_isPressedDownResize && !_isPressedDownMove)
        {
            if (hit.collider.CompareTag("Move"))
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.green);
                Vector2 hotSpot = new Vector2(16, 16);
                Cursor.SetCursor(_mouseTextureMove, hotSpot, CursorMode.Auto);
                if (Input.GetMouseButtonDown(0))
                {
                    _isPressedDownMove = true;
                    _activeEffector = hit.collider.transform.parent;
                }
            }


            else if (hit.collider.CompareTag("Resize"))
            {
                Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue);
                Vector2 hotSpot = new Vector2(16, 16);
                Cursor.SetCursor(_mouseTextureResize, hotSpot, CursorMode.Auto);
                if (Input.GetMouseButtonDown(0))
                {
                    _isPressedDownResize = true;
                    _activeEffector = hit.collider.transform;
                }
                
            }

        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);


        }

        if (_isPressedDownResize == true && _activeEffector != null)
        {

            float radius = Vector2.Distance(_activeEffector.position, hit.point);


            _activeEffector.GetComponent<CircleShape>().Radius = Mathf.Clamp(radius, 1, 2.5f);

            _activeEffector.GetComponent<AreaEffector2D>().forceMagnitude = radius * _forceRatio;
        }

        if (_isPressedDownMove == true && _activeEffector != null)
        {
            _activeEffector.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isPressedDownResize = false;
            _isPressedDownMove = false;
            _activeEffector = null;
        }
    }






    #endregion

    #region Methods




    #endregion

    #region Private & Protected

    private Transform _activeEffector;
    private bool _isPressedDownResize;
    private bool _isPressedDownMove;

    #endregion
}



