using UnityEngine;

public class GameLogic : MonoBehaviour
{
    
    [SerializeField] private GameObject thirdPersonPlayer;
    [SerializeField] private GameObject sanityMeter;
    [SerializeField] private GameObject firstPersonPlayer;


    public void ActivateThirdPersonCamera()
    {
        if(firstPersonPlayer.activeSelf)
        {
            thirdPersonPlayer.SetActive(true);
            sanityMeter.SetActive(true);
            firstPersonPlayer.SetActive(false);
        }
    }
    
    public void ActivateFirstPersonCamera()
    {
        if(thirdPersonPlayer.activeSelf && sanityMeter.activeSelf)
        {
            thirdPersonPlayer.SetActive(false);
            sanityMeter.SetActive(false);
            firstPersonPlayer.SetActive(true);   
        }
    }
}
