using UnityEngine;

[RequireComponent(typeof(HealthManager))]
public class AppearOnDeath : MonoBehaviour
{
    public GameObject Target;

    private void Start()
    {
        Target.SetActive(false);
        GetComponent<HealthManager>().Died += HandleAppear;
    }

    private void HandleAppear(HealthManager healthManager)
    {
        Target.SetActive(true);
    }
}