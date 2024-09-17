using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBaseManager : MonoBehaviour
{
    [SerializeField] private ArenaUIManager _arenaUIManager;
    public BaseType baseType;

    public float CurrentBaseHealth { get; set; }
    private float _maximumHealth;

    private void Start()
    {
        SetMaximumHealth();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("general collision");
        if (other.TryGetComponent<IUnitEquipmentDamage>(out IUnitEquipmentDamage damage))
        {
            Debug.Log("daddsd : " + damage.ToString());
            damage.DealDamageToBaseBuilding(this);
            //damage.PlayParticleVfx();

            Debug.Log("base collision worked : ");
        }

/*         if (_unitCharacterManager.GetUnitCharacterStatManager().CheckHealth())
        {
            _unitCharacterManager.ChangeState(new UnitCharacterDeadState());
            //RETURN OBJECT AFTER 2 SECONDS
            //UnitCharacterPoolManager.Instance.ReturnUnitCharacter(_unitCharacterManager);
        } */
    }

    private void SetMaximumHealth()
    {
        _maximumHealth = 1500 * PlayerStatusManager.Instance.GetPlayerStatObjectReference().level;

        CurrentBaseHealth = _maximumHealth;
        EventSystem.SetSliderBarValue?.Invoke(BarType.HEALTH_BAR, CurrentBaseHealth, _maximumHealth, baseType);
    }

    public void SetNewHealth(float newHealth)
    {
        EventSystem.SetSliderBarValue?.Invoke(BarType.HEALTH_BAR, newHealth, _maximumHealth, baseType);
    }

}

public enum BaseType
{
    PLAYER_BASE,
    OPPOSING_BASE
}
