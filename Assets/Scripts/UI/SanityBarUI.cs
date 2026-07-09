using UnityEngine;
using UnityEngine.UI;

public class SanityBarUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;

    private void Start()
    {
        fillImage.fillAmount = 1;
    }

    private void Update()
    {

        if(PlayerController.Instance == null) return;

        float sanityPercent = PlayerController.Instance.GetInsanityPercent();

        fillImage.fillAmount = sanityPercent;

        fillImage.color = Color.Lerp(Color.red, Color.green, sanityPercent);
    }
}
