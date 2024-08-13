using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Threading;

[CreateAssetMenu(fileName = "FireSkillStrategy", menuName = "ScriptableObjects/Skills/FireSkillStrategy")]
public class FireSkillStrategy : SkillStrategy
{
    public override async void CastSkill(Transform origin, UnitCharacterManager unitCharacterManager)
    {
        Debug.Log("FIREEE");
        unitCharacterManager.GetUnityCharacterAnimationManager().HandlePlayAnimation("Unit_Rifle_Fire");
        //unitCharacterManager.GetUnitCharacterSkillManager().characterTargetPoints
        Transform firePoint = unitCharacterManager.GetUnitCharacterSkillManager().characterTargetPoints["barrel"];
        //Task.Delay(3000).Wait(); 

        await Task.Run(() =>
        {
            Thread.Sleep(500);
        });

        HandleBulletInit(unitCharacterManager, firePoint);

        EventSystem.PlaySoundClip?.Invoke(SoundType.RIFLE_BULLET); //GIVE ANOTHER BULLET THAT THE PLAYER USES
    }

    private void HandleBulletInit(UnitCharacterManager unitCharacterManager, Transform firePoint)
    {
        Bullet bullet = BulletPoolManager.Instance.GetBullet();

        UnitRifleBullet unitRifleBullet = bullet as UnitRifleBullet;
        unitRifleBullet.SetUnitCharacterManager(unitCharacterManager);

        bullet.transform.position = firePoint.position;
        float eulerAnglesX = bullet.transform.rotation.eulerAngles.x;
        Quaternion targetRotation = Quaternion.Euler(eulerAnglesX, firePoint.transform.rotation.eulerAngles.y, firePoint.transform.rotation.eulerAngles.z);
        bullet.transform.rotation = targetRotation;
    }
}
