using UnityEngine;

public class GameStart : MonoBehaviour
{

    [SerializeField] private GameObject thirdPersonPlayer;
    [SerializeField] private GameObject sanityMeter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        thirdPersonPlayer.SetActive(false);
        sanityMeter.SetActive(false);
    }
}
