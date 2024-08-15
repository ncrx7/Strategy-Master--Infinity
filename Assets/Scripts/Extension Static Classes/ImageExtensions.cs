using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ImageExtensions
{
/*     public static void SetSliderBarValue(float newHealth, float maxHealth, Image image)
    {
        float healthRate = newHealth / maxHealth;
        image.fillAmount = healthRate;
    } */

    public static void SetSliderBarValue(this Image healthBarImage, float newHealth, float maxHealth)
    {
        float healthRate = newHealth / maxHealth;
        healthBarImage.fillAmount = healthRate;
    }
}
