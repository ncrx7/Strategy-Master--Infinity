// Assets/Scripts/GunController.cs
using System.Collections;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _fireInterval = 1f;
    //[SerializeField] bool _fireStarted = false;
    Bullet bullet;
    Coroutine _fireCoroutine;

    void Start()
    {
        FireWithInterval();
    }

    private void OnEnable()
    {
        EventSystem.OnPlayerDied += HandleStopTheFire;
        EventSystem.OnTimeOutForEvolutionPhase += HandleStopTheFire;
    }

    private void OnDisable()
    {
        EventSystem.OnPlayerDied -= HandleStopTheFire;
        EventSystem.OnTimeOutForEvolutionPhase -= HandleStopTheFire;
    }

    void FireWithInterval()
    {
        //TODO: MERMİ SAYISI EKLENEBİLİR
        _fireCoroutine = StartCoroutine(FireWithIntervalCoroutine());
    }

    IEnumerator FireWithIntervalCoroutine()
    {
        yield return new WaitForSeconds(0.3f);

        while (true)
        {
            Fire();
            //Debug.Log("fire interval");
            yield return new WaitForSeconds(_fireInterval);
        }
    }

    void Fire()
    {
        bullet = BulletPoolManager.Instance.GetBullet();
        bullet.transform.position = _firePoint.position;
        float eulerAnglesX = bullet.transform.rotation.eulerAngles.x;
        Quaternion targetRotation = Quaternion.Euler(eulerAnglesX, _firePoint.transform.rotation.eulerAngles.y, _firePoint.transform.rotation.eulerAngles.z);
        bullet.transform.rotation = targetRotation;
        Debug.Log("fire working");
        EventSystem.PlaySoundClip?.Invoke(SoundType.EQUILIBRIUMBULLET); //GIVE ANOTHER BULLET THAT THE PLAYER USES
    }

    private void HandleStopTheFire()
    {
        if(_fireCoroutine == null)
            return;

        StartCoroutine(HandleStopTheFireCoroutine());
    }

    IEnumerator HandleStopTheFireCoroutine()
    {
        yield return new WaitForSeconds(2.5f);
        if(_fireCoroutine != null)
            StopCoroutine(_fireCoroutine);
        _fireCoroutine = null;
    }
}
