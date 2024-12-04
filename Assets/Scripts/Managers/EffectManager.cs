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

    public void PlayBulletEffect(Vector3 position) => instantiateEffect(Impact,position);
    
    void instantiateEffect(GameObject effecx,Vector3 position)
    {
        GameObject fx = Instantiate(effecx);
        fx.transform.position = position;
    }
}