using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamage
{
    void DealDamage(ref float healthVariable, int playerPF);
    void PlayParticleVfx(GameObject box);
}
