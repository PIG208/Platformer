using UnityEngine;

/// <summary>This will be used as the generic manager for AIs.
/// Enable AI for any movables by attaching this component to the prefab.</summary>
[RequireComponent(typeof(IMovable))]
public class AIManager : MonoBehaviour
{
    private IMovable _target;

    private void Start()
    {
        this._target = GetComponent<IMovable>();
    }
}
