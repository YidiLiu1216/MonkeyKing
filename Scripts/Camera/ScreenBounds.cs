using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[RequireComponent(typeof(BoxCollider2D))]
public class ScreenBounds : MonoBehaviour
{
    public Camera MainCamera;
    BoxCollider2D boxCollider;


    public UnityEvent<Collider2D> ExitTirggerFired;

    [SerializeField] private float teleportOffset = 0.2f;
    [SerializeField] private float cornerOffset = 1.0f;

    private void Awake()
    {
        this.MainCamera.transform.localScale = Vector3.one;
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
    }
    void Start()
    {
        transform.position = Vector3.zero;
        UpdateBoundSize();
    }

    // Update is called once per frame

    public void UpdateBoundSize()
    {
        float ySize = MainCamera.orthographicSize * 2;
        Vector2 boxColliderSize = new Vector2(ySize * MainCamera.aspect, ySize);
        boxCollider.size = boxColliderSize;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        ExitTirggerFired?.Invoke(collision);
    }
    public bool IsOutOfBounds(Vector2 worldPosition)
    {
        return Mathf.Abs(worldPosition.x) > Mathf.Abs(boxCollider.bounds.min.x) ||
            Mathf.Abs(worldPosition.y) > Mathf.Abs(boxCollider.bounds.min.y);
    }
    public Vector2 CalculateWrappedPosition(Vector2 worldPosition)
    {
        bool xBoundResult = Mathf.Abs(worldPosition.x) > (Mathf.Abs(boxCollider.bounds.min.x) - cornerOffset);
        bool yBoundResult = Mathf.Abs(worldPosition.y) > (Mathf.Abs(boxCollider.bounds.min.y) - cornerOffset);
        Vector2 SignVector = new Vector2(Mathf.Sign(worldPosition.x), Mathf.Sign(worldPosition.y));

        if (xBoundResult)
        {
            return new Vector2(worldPosition.x * -1, worldPosition.y) + new Vector2(teleportOffset * SignVector.x, teleportOffset);
        }
        else
        {
            return worldPosition;
        }

    }
}

