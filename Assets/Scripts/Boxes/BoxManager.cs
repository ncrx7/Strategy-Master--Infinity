using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using System;

public class BoxManager : MonoBehaviour
{
    [SerializeField] private BoxType _boxType;
    [SerializeField] private BoxStatStrategy[] _boxTypesData;
    #region STATS
    [SerializeField] private SpriteRenderer _statSpriteRenderer;
    private BoxStatStrategy _currentBoxStat;
    [SerializeField] private float _boxHealth;
    [SerializeField] TextMeshPro _healthText;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private GameObject statHolderObject;
    [SerializeField] private Animator _statHolderObjectAnimator;
    [SerializeField] private PlayerStatManager _playerStatManager;
    #endregion

    private void Start()
    {
        //_currentBoxStat = _boxStats[0];
        //_statSpriteRenderer.sprite = _currentBoxStat.statSprite;
        InititializeBoxObject();
        InititializeStatObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamage>(out IDamage bullet))
        {
            bullet.DealDamage(ref _boxHealth, (int)_playerStatManager.GetPlayerFixedStatValue(StatType.PF));
            _healthText.text = _boxHealth.ToString();

            if (CheckBoxHealth())
            {
                OnBoxHealtRunnedOut();
            }
        }
    }

/*     private void FixedUpdate()
    {
        HandleHealthTextLookPlayer();
    } */

    private void HandleHealthTextLookPlayer()
    {
        _healthText.transform.LookAt(_playerTransform);
        //_healthText.transform.Rotate(0, 180, 0);
/*         UnityEngine.Vector3 eulerAngles = new UnityEngine.Vector3(0, _healthText.transform.rotation.eulerAngles.y, 0);
        UnityEngine.Quaternion targetRotation = UnityEngine.Quaternion.Euler(eulerAngles);
        _healthText.transform.rotation = targetRotation; */
    }

    private bool CheckBoxHealth()
    {
        if (_boxHealth <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnBoxHealtRunnedOut()
    {
        statHolderObject.transform.parent = null;
        _statHolderObjectAnimator.CrossFade("StatUpDown", 0.5f);
        
        Destroy(gameObject);
        //PLAY DESTROY VFX
        //INSTANTIATE STAT OBJECT
    }

    private void InititializeBoxObject()
    {
        //_boxType = GetRandomEnumValue<BoxType>();

        switch (_boxType)
        {
            case BoxType.HP_BOX:
                _boxHealth = 200f;
                statHolderObject.AddComponent<HPStatObject>();
                break;
            case BoxType.AP_BOX:
                _boxHealth = 180f;
                statHolderObject.AddComponent<APStatObject>();
                break;
            case BoxType.MANA_BOX:
                _boxHealth = 150f;
                statHolderObject.AddComponent<ManaStatObject>();
                break;
            case BoxType.AD_BOX:
                _boxHealth = 200f;
                statHolderObject.AddComponent<ADStatObject>();
                break;
            case BoxType.DEX_BOX:
                _boxHealth = 150f;
                statHolderObject.AddComponent<DEXStatObject>();
                break;
            default:
                _boxHealth = 100;
                break;
        }

        _healthText.text = _boxHealth.ToString();
    }

    public void ReduceHealth(float damage)
    {
        _boxHealth -= damage;
    }

    private void InititializeStatObject()
    {
        SetBoxObjectAsType();
        _statSpriteRenderer.sprite = _currentBoxStat.statSprite;
        //ADD COLLECTABLE INTERFACE TO STAT OBJECT
    }

    private T GetRandomEnumValue<T>()
    {
        Array enumValues = Enum.GetValues(typeof(T));
        int randomIndex = UnityEngine.Random.Range(0, enumValues.Length);
        return (T)enumValues.GetValue(randomIndex);
    }

    private void SetBoxObjectAsType()
    {
        for (int i = 0; i < _boxTypesData.Length; i++)
        {
            if (_boxTypesData[i].boxType == _boxType)
            {
                _currentBoxStat = _boxTypesData[i];
            }

        }
    }
}

public enum BoxType
{
    HP_BOX,
    MANA_BOX,
    AD_BOX,
    AP_BOX,
    DEX_BOX
}
