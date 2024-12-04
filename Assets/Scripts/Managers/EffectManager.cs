using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance;
    public GameObject Impact;
    public GameObject Impact3;
    public GameObject BloodDead;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void PlayBulletEffect(Vector3 position) => instantiateEffect(Impact,position);
    public void PlayDeadEffect(Vector3 position) => instantiateEffect(BloodDead,position);
    public void PlayPlayerHitEffect(Vector3 position) => instantiateEffect(Impact3,position);
    
    
    void instantiateEffect(GameObject effecx,Vector3 position)
    {
        GameObject fx = Instantiate(effecx);
        fx.transform.position = position;
    }
}