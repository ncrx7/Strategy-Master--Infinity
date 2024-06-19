// Assets/Scripts/BulletPoolManager.cs
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager Instance;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int initialPoolSize = 20;

    private ObjectPoolManager<Bullet> bulletPool;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        bulletPool = new ObjectPoolManager<Bullet>(bulletPrefab.GetComponent<Bullet>(), initialPoolSize, transform);
    }

    public Bullet GetBullet()
    {
        return bulletPool.GetObject();
    }

    public void ReturnBullet(EquilibriumBullet bullet)
    {
        bulletPool.ReturnObject(bullet);
    }
}
