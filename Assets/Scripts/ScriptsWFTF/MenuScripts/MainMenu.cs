using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject loadingScreen;
    public Animator animatorLoading;
    [SerializeField] private Button ContunueGameBT;
    private void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
        //if (!DataPersistenceManager.Instance.hasData())
        //{
        //    ContunueGameBT.interactable = false;
        //}
    }
    public void Play()
    {
        StartCoroutine(waitForAnimation());
        //DataPersistenceManager.Instance.NewGame();
    }
    public void Load()
    {
        StartCoroutine(waitForAnimation());
        //DataPersistenceManager.Instance.LoadGame();
    }
    private IEnumerator waitForAnimation()
    {
        loadingScreen.SetActive(true);
        animatorLoading.Play("FadeIn", 0, 0.0f);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync(1);


    }
    public void Quit()
    {
        Application.Quit();
    }
}
