using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;
    public GameObject Impact;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void PlayBulletEffect(Vector3 position)
    {
        GameObject fx = Instantiate(Impact);
        fx.transform.position = position;
    }
}