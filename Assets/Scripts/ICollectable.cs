using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable 
{
    void Collect(PlayerStatManager playerStatManager);
    void PlaySoundEffect();
}
