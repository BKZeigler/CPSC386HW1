using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour // https://www.youtube.com/watch?v=_lREXfAMUcE used in creation
{
    [SerializeField] private Slider slider; // the health bar slider

    public void UpdateHealthBar(float currentHealth, float maxHealth) // function call to update visual health bar
    {
        slider.value = currentHealth / maxHealth; // current/max is the ratio of the slider/health bar to fill
    }

}
