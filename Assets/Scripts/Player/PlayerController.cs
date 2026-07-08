using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    //stays here for now, I will change this from a door object to a keycard player object
    //Baiscally a SO that allows me to 
    [SerializeField] private DoorObjectSO doorObjectSO;
  
    //private DoorInteractions doorInteraction;
    //private bool hasKeycard = true;
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private GameInput gameInput;
    //[SerializeField] private Transform testMovement;
    private bool isWalking = false;
    private Vector3 lastInteractionDir;

    private bool hasRedKeycard = false;
    private bool hasBlueKeycard = false;
    private bool hasYellowKeycard = false;

    //******************************************************************************************************************\\
    //Setting these so that I don't have to worry about failed spelling
    private static string __CHECK_RED_KEYCARD = "RedKeyCard";
    private static string __CHECK_BLUE_KEYCARD = "BlueKeyCard";
    private static string __CHECK_YELLOW_KEYCARD = "YellowKeyCard";
    //******************************************************************************************************************\\

    
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("There is more then one player instance");
        }
        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractionHandler += GameInput_OnInteractionHandler;
    }

    private void GameInput_OnInteractionHandler(object sender, System.EventArgs e)
    {
        HandleInteractions();
    }

    private void Update()
    {
        HandleMovement();
        //HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleForwardMovement()
    {
        transform.position += Vector3.forward.normalized;
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

        if(moveDirection != Vector3.zero)
        {
            lastInteractionDir = moveDirection;
        }

        float interactionDistance = 5f;

        if (Physics.Raycast(transform.position, lastInteractionDir, out RaycastHit raycastHit, interactionDistance))
        {
            Debug.DrawRay(transform.position, lastInteractionDir);
            //These 2 if else statements are just teporary while I setup proper scripts, basically I will setup scriptable objects for both
            //The door and the keycard and the 'Transform' that we have here will be changed from 'Transform' to the scriptable object
            if (raycastHit.transform.TryGetComponent(out DoorObject doorObject))
            {
                doorObject = raycastHit.transform.GetComponent<DoorObject>();
                HandleDoorInteraction(doorObject);
            }
            else if(raycastHit.transform.TryGetComponent(out KeycardObject keycardObject))
            {
                KeycardObject keyCardObject = raycastHit.transform.GetComponent<KeycardObject>();
                HandleKeycardInteraction(keyCardObject);
                Debug.Log(hasRedKeycard);
                //Destroy(keycardObject);
            }
            else
            {
                Debug.Log("Unable to interact with object, please try again");
            }
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y); 
        
        //new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2f;

        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

        if(!canMove)
        {
            //attempt to move on the X movement
            Vector3 moveDirX = Vector3.forward.normalized;
            //Range captured for a distance
            canMove = (moveDirection.x < -0.5f || moveDirection.x > +0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);

            if(canMove)
            {
                //Can move on the X
                moveDirection = moveDirX;
            }
            
            else
            {
                //Cannot move only on the X

                //attempt a Z move only
                Vector3 moveDirZ = new Vector3(0, 0, moveDirection.z).normalized;

                //This range caputres for distance
                canMove = (moveDirection.z < 0.5f || moveDirection.z > +0.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if(canMove)
                {
                    moveDirection = moveDirZ;
                }
                
            }
        }

        if(canMove)
        {
            transform.position += moveDirection * moveDistance;
        }

        isWalking = moveDirection != Vector3.zero;

        float rotationSpeed = 10.0f;

        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
    }

    public bool IsDoorInteractable(DoorObject doorObject)
    {
        if(PlayerHasKeyCard(doorObject))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void MovePlayerToNewPosition(Transform movePlayer)
    {
        //Debug.Log(movePlayer.position);
        transform.position = movePlayer.position;
    }

    public bool PlayerHasKeyCard(DoorObject doorObjectCheck)
    {
        bool returnValue = false;
        if(doorObjectCheck.GetRequiredKeycard() == __CHECK_RED_KEYCARD)
        {
            Debug.Log("In this for loop");
            returnValue = CheckHasRedKeycard();
        }
        if(doorObjectCheck.GetRequiredKeycard() == __CHECK_YELLOW_KEYCARD)
        {
            returnValue = CheckHasYellowKeycard();
        }
        if(doorObjectCheck.GetRequiredKeycard() == __CHECK_BLUE_KEYCARD)
        {
            returnValue = CheckHasBlueKeycard();
        }
        if(doorObjectCheck.GetRequiredKeycard() == "None")
        {
            returnValue = true;
        }

        return returnValue;   
    }
    public bool PlayerCollectKeyCard()
    {
        return !doorObjectSO.canOpen;
    }

    private void HandleDoorInteraction(DoorObject doorMoveLocation)
    {
        //Debug.Log(doorMoveLocation);
        if(IsDoorInteractable(doorMoveLocation))
        {
            MovePlayerToNewPosition(doorMoveLocation.DoorOpenLocation());
        }
        else
        {
            Debug.Log("You have not collected the required keycard for this area");
        }
    }

    //******************************************************************************************************************\\
    // *** Keycard code

    private void HandleKeycardInteraction(KeycardObject keycardObject)
    {
        if(keycardObject.GetKeycardType() == __CHECK_RED_KEYCARD)
        {
            hasRedKeycard = !hasRedKeycard;
        }
        if(keycardObject.GetKeycardType() == __CHECK_BLUE_KEYCARD)
        {
            hasBlueKeycard = !hasBlueKeycard;
        }
        if(keycardObject.GetKeycardType() == __CHECK_YELLOW_KEYCARD)
        {
            hasYellowKeycard = !hasYellowKeycard;
        }
    }

    private bool CheckHasRedKeycard()
    {
        return hasRedKeycard;
    }
    private bool CheckHasBlueKeycard()
    {
        return hasBlueKeycard;
    }
    private bool CheckHasYellowKeycard()
    {
        return hasYellowKeycard;
    }

    //******************************************************************************************************************\\

    private void PrintDoorLockedMessage()
    {
        Debug.Log("Door Cannot Open");
    }
}