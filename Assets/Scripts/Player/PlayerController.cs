using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour, IDoorInteractionParent
{
    public static PlayerController Instance { get; private set; }

    //stays here for now, I will change this from a door object to a keycard player object
    //Baiscally a SO that allows me to 
    [SerializeField] private DoorObjectSO doorObjectSO;
    [SerializeField] private KeyCardSO keycardObjectSO;
  
    //private DoorInteractions doorInteraction;
    //private bool hasKeycard = true;
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private GameInput gameInput;
    //[SerializeField] private Transform testMovement;
    private bool isWalking = false;
    private Vector3 lastInteractionDir;
    
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

        float interactionDistance = 2f;

        if (Physics.Raycast(transform.position, lastInteractionDir, out RaycastHit raycastHit, interactionDistance))
        {
            //These 2 if else statements are just teporary while I setup proper scripts, basically I will setup scriptable objects for both
            //The door and the keycard and the 'Transform' that we have here will be changed from 'Transform' to the scriptable object
            if (raycastHit.transform.TryGetComponent(out DoorObject doorObject))
            {
                doorObject = raycastHit.transform.GetComponent<DoorObject>();
                Transform movePlayerPoint = doorObject.GetNewPlayerPosition();
                HandleDoorInteraction(movePlayerPoint);
            }
            else if(raycastHit.transform.TryGetComponent(out KeycardObject keycardObject))
            {
                KeycardObject keyCardObject = raycastHit.transform.GetComponent<KeycardObject>();
                HandleKeycardInteraction(keyCardObject);
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

    public bool IsDoorInteractable()
    {
        if(PlayerHasKeyCard())
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

    public bool PlayerHasKeyCard()
    {
        return doorObjectSO.canOpen;   
    }
    public bool PlayerCollectKeyCard()
    {
        return !doorObjectSO.canOpen;
    }

    private void HandleDoorInteraction(Transform doorMoveLocation)
    {
        //Debug.Log(doorMoveLocation);
        if(IsDoorInteractable())
        {
            MovePlayerToNewPosition(doorMoveLocation);
        }
        else
        {
            Debug.Log("You have not collected the required keycard for this area");
        }
    }

    private void HandleKeycardInteraction(KeycardObject keycardObject)
    {
        keycardObject.HasKeycard();
    }
}