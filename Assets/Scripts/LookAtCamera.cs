using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private void Update()
    {
        transform.rotation = LevelManager.CurrentLevelManager.MainCamera.transform.rotation;
    }
}