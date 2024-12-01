using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Image healthBarImage; // Reference to the health bar's Image component
    public BossScript bossScript; // Reference to the boss script
    public Transform bossTransform; // Reference to the boss for positioning
    public Color normalColor = Color.green;
    public Color damageColor = Color.red;

    private int totalHitsToKill;
    private int currentHitsTaken;

    void Start()
    {
        if (bossScript != null)
        {
            totalHitsToKill = bossScript.fireHitsToKill;
        }
        currentHitsTaken = 0;

        if (healthBarImage != null)
        {
            healthBarImage.fillAmount = 1f;
            healthBarImage.color = normalColor;
        }
    }

    void Update()
    {
        if (bossTransform != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(bossTransform.position + Vector3.up * 2f);
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBarImage != null && totalHitsToKill > 0)
        {
            float fillValue = (float)(totalHitsToKill - currentHitsTaken) / totalHitsToKill;
            StartCoroutine(SmoothHealthBar(Mathf.Clamp01(fillValue)));
        }
    }

    private IEnumerator SmoothHealthBar(float targetFillAmount)
    {
        while (!Mathf.Approximately(healthBarImage.fillAmount, targetFillAmount))
        {
            healthBarImage.fillAmount = Mathf.Lerp(healthBarImage.fillAmount, targetFillAmount, Time.deltaTime * 5f);
            yield return null;
        }
    }

    private IEnumerator FlashHealthBar()
    {
        healthBarImage.color = damageColor;
        yield return new WaitForSeconds(0.2f);
        healthBarImage.color = normalColor;
    }

}
