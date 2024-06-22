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
    [SerializeField] private BoxStatStrategy[] _boxStats;
    #region STATS
    [SerializeField] private SpriteRenderer _statSpriteRenderer;
    private BoxStatStrategy _currentBoxStat;
    [SerializeField] private float _boxHealth;
    [SerializeField] TextMeshProUGUI _healthText;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private GameObject statHolderObject;
    [SerializeField] private Animator _statHolderObjectAnimator;
    #endregion

    private void Start()
    {
        //_currentBoxStat = _boxStats[0];
        //_statSpriteRenderer.sprite = _currentBoxStat.statSprite;
        SetBoxHealth();
        InititializeStatObject();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ontrigger enter");
        if (other.TryGetComponent<IDamage>(out IDamage bullet))
        {
            Debug.Log("ontrigger enter inside IDAMAGE");
            bullet.DealDamage(this);
            _healthText.text = _boxHealth.ToString();

            if (CheckBoxHealth())
            {
                OnBoxHealtRunnedOut();
            }
        }
    }

    private void FixedUpdate()
    {
        HandleHealthTextLookPlayer();
    }

    private void HandleHealthTextLookPlayer()
    {
        _healthText.transform.LookAt(_playerTransform);
        _healthText.transform.Rotate(0, 180, 0);
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

    private void SetBoxHealth()
    {
        switch (_boxType)
        {
            case BoxType.HP_BOX:
                _boxHealth = 200f;
                break;
            case BoxType.AP_BOX:
                _boxHealth = 180f;
                break;
            case BoxType.MANA_BOX:
                _boxHealth = 150f;
                break;
            case BoxType.AD_BOX:
                _boxHealth = 200f;
                break;
            case BoxType.DEX_BOX:
                _boxHealth = 150f;
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
        _boxType = GetRandomEnumValue<BoxType>();
        SetStatData();
        _statSpriteRenderer.sprite = _currentBoxStat.statSprite;
        //ADD COLLECTABLE INTERFACE TO STAT OBJECT
        Debug.Log("stat data : " + _currentBoxStat.statSprite);
    }

    private T GetRandomEnumValue<T>()
    {
        Array enumValues = Enum.GetValues(typeof(T));
        int randomIndex = UnityEngine.Random.Range(0, enumValues.Length);
        return (T)enumValues.GetValue(randomIndex);
    }

    private void SetStatData()
    {
        for (int i = 0; i < _boxStats.Length; i++)
        {
            if (_boxStats[i].boxType == _boxType)
            {
                _currentBoxStat = _boxStats[i];
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
