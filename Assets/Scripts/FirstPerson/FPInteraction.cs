using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FPInteraction : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactionDistance = 100f;
    [SerializeField] private gameInput gameInput;

    [Header("UI")]
    [SerializeField] private TMP_Text canvasText;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameLogic gameLogic;

    [Header("Music")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip audioClip;

    private Note notes;

    private bool hasVHS;

    private void Start()
    {
        gameInput.OnInteractionHandler += GameInput_OnInteractionHandler;

        TextAsset jsonFile = Resources.Load<TextAsset>("GameScript");
        notes = JsonUtility.FromJson<Note>(jsonFile.text);
    }

    private void OnEnable()
    {
        hasVHS = false;
        if(musicSource != null)
            PlayClipSounds.Instance.PlayAudio(musicSource, audioClip, true);
    }

    private void OnDestroy()
    {
        if (gameInput != null)
            gameInput.OnInteractionHandler -= GameInput_OnInteractionHandler;
    }

    private void GameInput_OnInteractionHandler(object sender, System.EventArgs e)
    {
        //Debug.Log("Interaction started");
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        RaycastHit rayHit;
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height /2f, 0f));

        Debug.DrawRay(ray.origin, ray.direction * interactionDistance, Color.red);

        if (Physics.Raycast(ray,
                            out rayHit,
                            interactionDistance))
        {
            // Notes
            NoteObject note = rayHit.collider.GetComponentInParent<NoteObject>();

            if (note != null)
            {
                ShowNote(note);
                return;
            }

            VHS vhs = rayHit.collider.GetComponentInParent<VHS>();

            if(vhs != null)
            {
                if(!hasVHS)
                {
                    vhs.HandleInteraction(canvas, canvasText); 
                    return;
                }
                gameLogic.ActivateThirdPersonCamera();
                return;
            }

            VHSObject vhsTape = rayHit.collider.GetComponent<VHSObject>();
            Debug.Log(vhsTape);
            if(vhsTape != null)
            {
                vhsTape.Interacted();
                HandleVHSInteraction();
                return;
            }
        }
    }


    private void HandleVHSInteraction()
    {
        hasVHS = true;
    }
    
    
    private void ShowNote(NoteObject note)
    {
        if (notes.Notes[note.GetNoteType()] != null)
        {
            if (canvas.activeInHierarchy)
            {
                if (note.GetNoteType() == 3)
                {
                    Time.timeScale = 1f;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
                
                canvas.SetActive(false);
                Time.timeScale = 1f;
                return;
            }
            
            canvas.SetActive(true);
            canvasText.text = 
            notes.Notes[note.GetNoteType()].ContainsLine1 + "\n" +
            notes.Notes[note.GetNoteType()].ContainsLine2 + "\n" +
            notes.Notes[note.GetNoteType()].ContainsLine3;
            
            Time.timeScale = 0f;
        }
    }
}