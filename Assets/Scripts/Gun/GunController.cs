// Assets/Scripts/GunController.cs
using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireInterval = 1f;
    bool _fireStarted = false;

    void Update()
    {
        FireWithInterval();
    }

    void FireWithInterval()
    {
        if(!_fireStarted)
        {
            StartCoroutine(FireWithIntervalCoroutine());
        }
    }

    IEnumerator FireWithIntervalCoroutine()
    {
        _fireStarted = true;
        while (true)
        {
            Fire();
            //Debug.Log("fire interval");
            yield return new WaitForSeconds(_fireInterval);
        }
    }

    void Fire()
    {
        Bullet bullet = BulletPoolManager.Instance.GetBullet();
        bullet.transform.position = _firePoint.position;
        float eulerAnglesX = bullet.transform.rotation.eulerAngles.x;
        Quaternion targetRotation = Quaternion.Euler(eulerAnglesX, _firePoint.transform.rotation.eulerAngles.y, _firePoint.transform.rotation.eulerAngles.z);
        bullet.transform.rotation = targetRotation;
        EventSystem.PlaySoundClip?.Invoke(SoundType.EQUILIBRIUMBULLET); //GIVE ANOTHER BULLET THAT THE PLAYER USES
    }
}
