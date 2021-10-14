using UnityEngine;

/// <summary>Each level needs to have a LevelManager, and only one LevelManager can exist in each level</summary>
public class LevelManager : MonoBehaviour
{
    public static LevelManager CurrentLevelManager { get; private set; }
    public Player Player;

    private void Start()
    {
        CurrentLevelManager = this;
    }

    
}
