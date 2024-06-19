using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    void DealDamage(BoxManager boxManager);
    void PlayParticleVfx(GameObject box);
}
