using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    private float moveSpeed = 20.0f;
    [SerializeField] private gameInput gameInput;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameLogic gameLogic;

    [Header("Insanity")]
    [SerializeField] private float maxInsanity = 100f;
    private float currentInsanity;

    private bool isWalking = false;
    private bool isTeleporting = false;

    private float interactionCooldown = 0f;

    private bool hasRedKeycard = false;
    private bool hasBlueKeycard = false;
    private bool hasYellowKeycard = false;

    private static string __CHECK_RED_KEYCARD = "RedKeyCard";
    private static string __CHECK_BLUE_KEYCARD = "BlueKeyCard";
    private static string __CHECK_YELLOW_KEYCARD = "YellowKeyCard";

    /// <summary>
    /// Canvas time
    /// </summary>
    
    [SerializeField] private TMP_Text canvasText;
    [SerializeField] private GameObject canvas;

    /// <summary>
    /// Checks for the hunter scripts
    /// </summary>
    private static string __HUNTERWHEELCHAIR__ = "WHEELCHAIR";
    private static string __HUNTERFINAL__ = "FINAL";
    //-----------------------------------------------------------------------------------------------------------------------\\
    private Note notes;
    //this is for loading objects into the starting scene
    private int objectNumber = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There is more than one player instance");
        }

        Instance = this;
        currentInsanity = maxInsanity;
    }

    private void Start()
    {
        currentInsanity = maxInsanity;
        gameInput.OnInteractionHandler += GameInput_OnInteractionHandler;

        //GameScript Call
        TextAsset jsonFile = Resources.Load<TextAsset>("GameScript");

        notes = JsonUtility.FromJson<Note>(jsonFile.text);

        //Debug.Log(notes.Notes[0].ContainsLine1);

    }

    private void GameInput_OnInteractionHandler(object sender, System.EventArgs e)
    {
        HandleInteractions();
    }

    private void FixedUpdate()
    {
        if (interactionCooldown > 0)
        {
            interactionCooldown -= Time.deltaTime;
        }

        HandleMovement();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleMovement()
    {
        if (isTeleporting)
            return;

        if (interactionCooldown > 0)
            return;

        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDirection = new Vector3(
            inputVector.x,
            0,
            inputVector.y
        );

        float moveDistance = moveSpeed * Time.deltaTime;

        float playerRadius = 0.7f;
        float playerHeight = 2f;

        bool canMove = !Physics.CapsuleCast(
            transform.position,
            transform.position + Vector3.up * playerHeight,
            playerRadius,
            moveDirection,
            moveDistance
        );

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDirection.x, 0, 0).normalized;

            bool canMoveX =
                moveDirection.x != 0 &&
                !Physics.CapsuleCast(
                    transform.position,
                    transform.position + Vector3.up * playerHeight,
                    playerRadius,
                    moveDirX,
                    moveDistance
                );

            if (canMoveX)
            {
                moveDirection = moveDirX;
            }
            else
            {
                Vector3 moveDirZ = new Vector3(0, 0, moveDirection.z).normalized;

                bool canMoveZ =
                    moveDirection.z != 0 &&
                    !Physics.CapsuleCast(
                        transform.position,
                        transform.position + Vector3.up * playerHeight,
                        playerRadius,
                        moveDirZ,
                        moveDistance
                    );

                if (canMoveZ)
                {
                    moveDirection = moveDirZ;
                }
                else
                {
                    moveDirection = Vector3.zero;
                }
            }
        }

        if (moveDirection != Vector3.zero)
        {
            transform.position += moveDirection * moveDistance;
        }

        isWalking = moveDirection != Vector3.zero;

        if (moveDirection != Vector3.zero)
        {
            float rotationSpeed = 10f;

            transform.forward = Vector3.Slerp(
                transform.forward,
                moveDirection,
                Time.deltaTime * rotationSpeed
            );
        }
    }

    private void HandleInteractions()
    {
        float interactionDistance = 5f;

        Vector3 rayOrigin = this != null
            ? this.transform.position
            : transform.position;

        Vector3 rayDirection = this != null
            ? this.transform.forward
            : transform.forward;

        Debug.DrawRay(
            rayOrigin,
            rayDirection * interactionDistance,
            Color.red,
            5f
        );

        //Debug.Log(rayDirection);

        if (Physics.Raycast(
            rayOrigin,
            rayDirection,
            out RaycastHit raycastHit,
            interactionDistance))
        {
            Debug.Log("Ray hit: " + raycastHit.collider.name);

            DoorObject doorObject =
                raycastHit.collider.GetComponent<DoorObject>();

            if (doorObject != null)
            {
                //Debug.Log("called");
                interactionCooldown = 0.25f;
                doorObject.Interaction(this);
                return;
            }

            KeycardObject keycardObject =
                raycastHit.transform.GetComponentInParent<KeycardObject>();

            if (keycardObject != null)
            {
                HandleKeycardInteraction(keycardObject);
                return;
            }

            
            NoteObject noteObject = 
                raycastHit.transform.GetComponentInParent<NoteObject>();

            if(noteObject != null)
            {
                HandleNoteInteractions(noteObject);
                return;
            }
            
        }
    }

    public void MovePlayerToNewPosition(Transform movePlayer)
    {
        StartCoroutine(TeleportPlayer(movePlayer));
    }

    private IEnumerator TeleportPlayer(Transform movePlayer)
    {
        isTeleporting = true;

        transform.position = movePlayer.position;
        transform.rotation = movePlayer.rotation;

        Physics.SyncTransforms();

        yield return null;
        yield return null;

        isTeleporting = false;
    }

    public bool IsDoorInteractable(DoorObject doorObjectCheck)
    {
        return PlayerHasKeyCard(doorObjectCheck);
    }

    public bool PlayerHasKeyCard(DoorObject doorObjectCheck)
    {
        if (doorObjectCheck.GetRequiredKeycard() == __CHECK_RED_KEYCARD)
            return hasRedKeycard;

        if (doorObjectCheck.GetRequiredKeycard() == __CHECK_BLUE_KEYCARD)
            return hasBlueKeycard;

        if (doorObjectCheck.GetRequiredKeycard() == __CHECK_YELLOW_KEYCARD)
            return hasYellowKeycard;

        if (doorObjectCheck.GetRequiredKeycard() == "None")
            return true;

        return false;
    }

    private void HandleKeycardInteraction(KeycardObject keycardObject)
    {
        if (keycardObject.GetKeycardType() == __CHECK_RED_KEYCARD)
        {
            hasRedKeycard = true;
            gameLogic.ActivateFirstPersonCamera();
            gameLogic.ActivateHunter(hasRedKeycard, __HUNTERWHEELCHAIR__);
            gameLogic.StartingActivateObjects(objectNumber);
            objectNumber++;
        }

        if (keycardObject.GetKeycardType() == __CHECK_BLUE_KEYCARD)
        {
            hasBlueKeycard = true;
            gameLogic.ActivateFirstPersonCamera();
            gameLogic.ActivateHunter(hasRedKeycard, __HUNTERFINAL__);
            gameLogic.StartingActivateObjects(objectNumber);
        }

        if (keycardObject.GetKeycardType() == __CHECK_YELLOW_KEYCARD)
            hasYellowKeycard = true;
    }

    private void HandleNoteInteractions(NoteObject noteObject)
    {
        if(notes.Notes[noteObject.GetNoteType()] != null)
        {
            if(canvas.activeInHierarchy)
            {
                if (noteObject.GetNoteType() == 3)
                {
                    Time.timeScale = 1f;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                canvas.SetActive(false);
                Time.timeScale = 1f;
                return;
            }
            canvas.SetActive(true);
            canvasText.text = notes.Notes[noteObject.GetNoteType()].ContainsLine1 + "\n" +  notes.Notes[noteObject.GetNoteType()].ContainsLine2 + "\n" +  notes.Notes[noteObject.GetNoteType()].ContainsLine3;
            Time.timeScale = 0f;
        }
    }

    public void TakeDamage(float damage)
    {
        currentInsanity -= damage;
        currentInsanity = Mathf.Clamp(currentInsanity, 0f, maxInsanity);

        //Debug.Log("Insanity: " + currentInsanity);

        if (currentInsanity <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //Debug.Log("Player Died");

        // Disable movement
        enabled = false;

        GameOverUI.Instance.GameOver();
    }

    public float GetInsanity()
    {
        return currentInsanity;
    }

    public float GetInsanityPercent()
    {
        return currentInsanity / maxInsanity;
    }

    public void SetMoveSpeed(float newMoveSpeed)
    {
        moveSpeed = newMoveSpeed;
    }
}