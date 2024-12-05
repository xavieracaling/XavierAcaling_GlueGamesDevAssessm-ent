using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro ;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public EnemySpawner EnemySpawner;
    public TextMeshProUGUI KillCountResult1UI;
    public TextMeshProUGUI KillCountResult2UI;
    public TextMeshProUGUI ResultUI;
    public GameObject PanelResult;
    public GameObject BulletPrefab;
    public Transform BulletSpawnPoint;
    public Transform PoolingContainer;

    public int KillCount;
    public bool EndGame;
    public void RestartGame () => SceneManager.LoadScene(0);
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        InitializeBullets(10);
    }
    public void Result(bool win)
    {
        EndGame = true;
        PanelResult.SetActive(true);
        if(win)
        {
            ResultUI.color = Color.green;
            ResultUI.text = "YOU WIN";
        }
        else
        {
            ResultUI.color = Color.red;
            ResultUI.text = "YOU LOSE";
        }
    }
    public void UpdateKillCount()
    {
        KillCount++;
        KillCountResult1UI.text  = $"KILL COUNT : {KillCount}";
        KillCountResult2UI.text  = $"KILL COUNT : {KillCount}";
        if(KillCount >= 10)
            Result(true);
    }
    public void InitializeBullets(int count)
    {
        for (int i = 0; i < count; i++)
            Instantiate(BulletPrefab, BulletSpawnPoint.position, Quaternion.identity,PoolingContainer).SetActive(false);
    }
    public void GetAvailableBullet(Vector2 direction)
    {
        for (int i = 0; i < PoolingContainer.childCount; i++)
        {
            Bullet bullet = PoolingContainer.GetChild(i).GetComponent<Bullet>();
            if(!bullet.gameObject.activeSelf)
            {
                bullet.transform.position = BulletSpawnPoint.position;
                bullet.gameObject.SetActive(true);
                bullet.Initialize(direction);
                break;
            }
        }
    }
}
