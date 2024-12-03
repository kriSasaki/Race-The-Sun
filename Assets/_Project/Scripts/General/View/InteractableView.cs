using Project.Utils.Extensions;
using UnityEngine;

public abstract class InteractableView : MonoBehaviour
{
    private const float GizmoLineLenght = 20f;
    
    [SerializeField] private float _verticalOffset = 0.5f;
    // твин на rotation
    // твин на распад?
    
    private Vector3 _originLocalPosition;

    private void Awake()
    {
        transform.localPosition = transform.localPosition.AddY(-_verticalOffset);
        _originLocalPosition = transform.localPosition;
    }

    public abstract void Disappear();
    
    
}
