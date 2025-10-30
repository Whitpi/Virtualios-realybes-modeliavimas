using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; set; } //Singleton, visada yra scenoje

    [Header("UI elementai")]
    public GameObject DialogueBox;
    public GameObject InteractionText;
    //public GameObject Crosshair;
    public TextMeshProUGUI NpcName;
    public TextMeshProUGUI dialogue;


    [Header("Kodai kurie isjungiami")]
    private PlayerMovement playerMovement;
    private MouseLook mouseLook;
    private PlayerInteraction interaction;


    [Header("Kintamieji")]
    [SerializeField] private float typingSpeed = 0.45f;
    [SerializeField] private float punctuationPause = 0.12f;
    public bool isSpeaking = false;

    [Header("Kiekvienam veikejui priskiriami kintamieji")]
    private AudioClip voice;
    private Color TextColor;

    [Header("Dialogo eilutes")]
    private Queue<DialogueLine> lines = new Queue<DialogueLine>();

    private bool isTyping = false;

    private DialogueLine currentDialogueLine;

    private UnityEvent heldEvent;

    private void Start()
    {
        //Surandame kodus scenoje
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        mouseLook = FindAnyObjectByType<MouseLook>();
        interaction = FindAnyObjectByType<PlayerInteraction>();
    }

    private void Update()
    {
        if ((Input.GetKeyUp(KeyCode.Space)|| Input.GetKeyUp(KeyCode.Mouse0)) && isSpeaking)
        {
            if(isTyping)
            {
                StopAllCoroutines();
                dialogue.text = currentDialogueLine.text;
                isTyping = false;
            }
            else
            {
                DisplayNextDialogueLine();
            }

        }
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

    }

    //Dialogo pradzia
    public void DialogueStart(Dialogue dialogue)
    {
        //Veikejas kalba, dialogo langas yra aktyvus, paslepiame pelyte.
        isSpeaking = true;
        DialogueBox.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
     
        //Isjungiame judejima, peles valdyma ir UI elementus.
        HudManager.instance.DisableMovement();
        mouseLook.enabled = false;
        interaction.enabled = false;
        InteractionText.SetActive(false);
       // Crosshair.SetActive(false);

        //Isvalome dialogo eilutes eileje.
        lines.Clear();

        if (dialogue.afterDialogueEvent != null)
        {
            heldEvent = dialogue.afterDialogueEvent;
        }

        foreach (DialogueLine dialogueLine in dialogue.defaultDialogueLines)
        {
                lines.Enqueue(dialogueLine);
        }
            //Pradedame rodyti dialogo eilute.
            DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if(lines.Count ==0) //Jei nebera dialogo eiluciu, baigiame dialoga.
        {
            DialogueStop();
            return;
        }
        DialogueLine currentLine = lines.Dequeue(); //Is eiles ismetame dabar rodoma dialogo eilute.

        currentDialogueLine = currentLine;

        //Priskiriame balsa, spalva, varda.
        NpcName.text = currentLine.character.npcName;
        voice = currentLine.character.npcVoice;
        TextColor = currentLine.character.npcColor;
        NpcName.color = TextColor;

        //Animuotas teksto atsiradimas
        StopAllCoroutines();
        StartCoroutine(TypeText(currentLine));

    }
    //Dialogo sustabydmas, pasibaigimas
    private void DialogueStop()
    {
        //Aktyvuojame pries tai isjungtus kodus
        HudManager.instance.EnableMovement();
        mouseLook.enabled = true;
        interaction.enabled = true;
        InteractionText.SetActive(true);

        //Dialogo deze isjungiama, veikejas nebekalba
        DialogueBox.SetActive(false);
        isSpeaking=false;

        Cursor.lockState = CursorLockMode.Locked;


        if (heldEvent != null)
        {
            heldEvent.Invoke(); //Ivykdomas eventas, jei toks yra
            heldEvent = null; //Isvalome eventa, kad neivyktų veliau
        }

    }

    //Teksto atsiradimas animuotas
    private IEnumerator TypeText(DialogueLine dialogueLine)
    {
        dialogue.text = ""; //Isvalomas tekstas
        dialogue.color = TextColor; //Nustatoma spalva
        foreach (char letter in dialogueLine.text)
        {
            isTyping = true;

            //Garso efekto paleidimas
            //SoundFXManager.instance.PlaySoundFXClip(voice, transform, 0.2f);


            dialogue.text += letter;
            //Jeigu naudojami simboliai, truputeli uzlaikome
            if (letter == '.' || letter == ',' || letter == '!' || letter == '?')
            {
                yield return new WaitForSeconds(punctuationPause);
            }
            else
            {
                yield return new WaitForSeconds(typingSpeed);
            }
        }
        isTyping = false;
    }
}