using Opsive.UltimateCharacterController.Traits;
using UnityEngine;
using UnityEngine.Events;

public class BossEvents : MonoBehaviour
{
    [SerializeField] private AttributeManager attributeManager;

    public float currentHealthPercentage = 100f;
    public UnityEvent onBelow60Percent;
    public UnityEvent onBelow30Percent;

    private bool hasTriggered60 = false;
    private bool hasTriggered30 = false;
    private float maxHealth;
    private Attribute healthAttribute;

    private void Start()
    {
        if (attributeManager != null)
        {
            healthAttribute = attributeManager.GetAttribute("Health");
            if (healthAttribute != null)
            {
                maxHealth = healthAttribute.MaxValue;
            }
        }
    }

    private void Update()
    {
        currentHealthPercentage = CalculateHealthPercentage();

        if (!hasTriggered60 && currentHealthPercentage <= 60f)
        {
            hasTriggered60 = true;
            onBelow60Percent.Invoke();
        }

        if (!hasTriggered30 && currentHealthPercentage <= 30f)
        {
            hasTriggered30 = true;
            onBelow30Percent.Invoke();
        }
    }

    private float CalculateHealthPercentage()
    {
        if (healthAttribute == null || maxHealth <= 0f)
        {
            return 100f;
        }

        float currentHealth = healthAttribute.Value;
        float percentage = (currentHealth / maxHealth) * 100f;
        return percentage;
    }
}
