using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject thirdPersonPlayer;
    [SerializeField] private GameObject sanityMeter;
    [SerializeField] private GameObject firstPersonPlayer;
    [Header("AI")]
    [SerializeField] private GameObject wheelchairHunterFirst;
    [SerializeField] private GameObject finalHunter;
    [Header("VHS Objects")]
    [SerializeField] private GameObject vhsTape1;
    [SerializeField] private GameObject vhsTape2;
    [Header("Starting Room Objects")]
    [SerializeField] private GameObject startingWheelChair;
    [SerializeField] private GameObject startingBed;
    [SerializeField] private GameObject finalMan;

    //Checks for the Hunters
    private static string __HUNTERWHEELCHAIR__ = "WHEELCHAIR";
    private static string __HUNTERFINAL__ = "FINAL";

    private void Awake()
    {
        finalHunter.SetActive(false);
        wheelchairHunterFirst.SetActive(false);
        vhsTape1.SetActive(false);
        vhsTape2.SetActive(false);
        startingWheelChair.SetActive(false);
        startingBed.SetActive(false);
        finalMan.SetActive(false);
    }


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

    public void ActivateHunter(bool keyCard, string hunterType)
    {
        if(keyCard && hunterType == __HUNTERWHEELCHAIR__)
        {
            wheelchairHunterFirst.SetActive(true);
        }

        if(keyCard && hunterType == __HUNTERFINAL__)
        {
            finalHunter.SetActive(true);
        }
        
    }

    public void DisableHunter(string hunterType)
    {
        if(hunterType == __HUNTERWHEELCHAIR__)
        {
            wheelchairHunterFirst.SetActive(false);
        }
    }

    public void StartingActivateObjects(int objectNumber)
    {
        if(objectNumber == 0)
        {
            vhsTape1.SetActive(true);
            startingWheelChair.SetActive(true);
        }
        if(objectNumber == 1)
        {
            vhsTape2.SetActive(true);
            startingBed.SetActive(true);
        }
    }
}
