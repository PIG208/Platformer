using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>Only a single LevelManager exists throughout the game</summary>
public class LevelManager : MonoBehaviour
{
    public static LevelManager CurrentLevelManager { get; private set; }
    public Player Player;
    public List<Enemy> Enemies;
    public Camera MainCamera;

    private void Awake()
    {
        CurrentLevelManager = this;
        Player = FindObjectOfType<Player>();
        Transform startingTransform = GameObject.FindGameObjectWithTag("Start").transform;

        if (Player == null)
        {
            Player = Instantiate(Resources.Load<GameObject>(Constants.PlayerPrefab), startingTransform.position, Quaternion.identity).GetComponent<Player>();
        }
        else
        {
            Player.gameObject.transform.position = startingTransform.position;
        }
        DontDestroyOnLoad(Player);

        Enemies = new List<Enemy>();

        MainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        FindObjectOfType<Cinemachine.CinemachineVirtualCamera>().Follow = Player.transform;
    }

    private void Start()
    {
        Player.Health.HealthBar = GameObject.FindGameObjectWithTag("HealthBar").GetComponent<Slider>();
        Player.Health.UpdateHealthBar();
    }

    public void LoadLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public IEnumerable<Entity> FindSurroundingTargets(Entity entity, float MaxDistance = 10f)
    {
        if (entity.IsPlayer)
        {
            return Enemies.FindAll(enemy => Vector2.Distance(enemy.transform.position, entity.transform.position) < MaxDistance);
        }
        else
        {
            return (Vector2.Distance(entity.transform.position, Player.transform.position) < MaxDistance)
                    ? new Player[] { Player } : new Entity[] { };
        }
    }
}
