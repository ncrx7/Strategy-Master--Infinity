using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;

public class ArenaBaseManager : MonoBehaviour
{
    [SerializeField] private ArenaUIManager _arenaUIManager;
    [SerializeField] private int _baseHealthCoefficient;
    public BaseType baseType;

    public float CurrentBaseHealth { get; set; }
    public int CurrentBaseSP { get; set; }

    private float _maximumHealth;
    private int _maxSP;

    private void OnEnable()
    {
        if (baseType == BaseType.PLAYER_BASE)
        {
            EventSystem.SpReduce += HandleReducingSP;
            EventSystem.SpCheck += HandleSpCheck;
            EventSystem.SpRefund += HandleRefundingSP;
        }
    }

    private void OnDisable()
    {
        if (baseType == BaseType.PLAYER_BASE)
        {
            EventSystem.SpReduce -= HandleReducingSP;
            EventSystem.SpCheck -= HandleSpCheck;
            EventSystem.SpRefund -= HandleRefundingSP;
        }
    }

    private void Start()
    {
        SetInitialHealth();
        SetMaximumSP();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetMaximumSP();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("general collision");
        if (other.TryGetComponent<IUnitEquipmentDamage>(out IUnitEquipmentDamage damage))
        {
            //Debug.Log("daddsd : " + damage.ToString());
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

    #region HEALTH
    private async void SetInitialHealth()
    {
        await SetMaximumHealth();

        CurrentBaseHealth = _maximumHealth;
        EventSystem.SetSliderBarValue?.Invoke(BarType.HEALTH_BAR, CurrentBaseHealth, _maximumHealth, baseType);
    }

    private async UniTask SetMaximumHealth()
    {
        if (baseType == BaseType.PLAYER_BASE)
        {
            _maximumHealth = PlayerStatusManager.Instance.GetPlayerStatObjectReference().hp * _baseHealthCoefficient;
        }
        else if (baseType == BaseType.OPPOSING_BASE)
        {
            _maximumHealth = PlayerStatusManager.Instance.GetPlayerStatObjectReference().hp * _baseHealthCoefficient; //TODO: CHANGE HERE ACCORDING TO LEVEL ENEMY STATS
        }
        await UniTask.Delay(100);
    }

    public void SetNewHealth(float newHealth)
    {
        CurrentBaseHealth = newHealth;
        EventSystem.SetSliderBarValue?.Invoke(BarType.HEALTH_BAR, CurrentBaseHealth, _maximumHealth, baseType);
    }
    #endregion

    #region SP
    private void SetMaximumSP()
    {
        if (baseType == BaseType.OPPOSING_BASE)
            return;

        _maxSP = (int)PlayerStatusManager.Instance.GetPlayerStatObjectReference().mana;

        CurrentBaseSP = _maxSP;
        EventSystem.SetSliderBarValue?.Invoke(BarType.MANA_BAR, CurrentBaseSP, _maxSP, baseType);
    }

    private void HandleReducingSP(int spPrice)
    {
        CurrentBaseSP -= spPrice;

        if (CurrentBaseSP < 0)
        {
            CurrentBaseSP = 0;
        }

        EventSystem.SetSliderBarValue?.Invoke(BarType.MANA_BAR, CurrentBaseSP, _maxSP, baseType);
    }

    private void HandleRefundingSP(int spRefundAmount)
    {
        CurrentBaseSP += spRefundAmount;

        if (CurrentBaseSP > _maxSP)
        {
            CurrentBaseSP = _maxSP;
        }

        EventSystem.SetSliderBarValue?.Invoke(BarType.MANA_BAR, CurrentBaseSP, _maxSP, baseType);
    }

    private bool HandleSpCheck(int spPrice)
    {
        int newSp = CurrentBaseSP - spPrice;

        if (newSp < 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    #endregion

}

public enum BaseType
{
    PLAYER_BASE,
    OPPOSING_BASE
}
