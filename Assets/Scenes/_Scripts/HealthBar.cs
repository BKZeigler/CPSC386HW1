using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour // https://www.youtube.com/watch?v=_lREXfAMUcE used in creation
{
    [SerializeField] private Slider slider;

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        slider.value = currentHealth / maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
