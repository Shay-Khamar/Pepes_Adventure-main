using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;


public class ClickButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _default, _pressdown;
    [SerializeField] private AudioClip _compressedClip, _uncompressedClip;
    [SerializeField] private AudioSource _source;

    public static ClickButton Instance;

    public enum Scene
    {
        Level01,
        MainMenu
    }

    private void Awake()
    {
        Instance = this;

    }

    public void OnPointerDown(PointerEventData eventData) //On mouse Down
    {
        _img.sprite = _pressdown;
        _source.PlayOneShot(_compressedClip);
    }

    public void OnPointerUp(PointerEventData eventData) //On mouse up
    {
        _img.sprite = _default;
        _source.PlayOneShot(_uncompressedClip);
    }

    public void LoadNewGame()
    {
        SceneManager.LoadScene(Scene.Level01.ToString());
    }
}
    /*
    public void WasClicked(string sceneName)
    {
        GameManager.Instance.LoadScene(sceneName);
    }

}


public class LoadingBar : MonoBehaviour
{
    public GameObject menu;
    public GameObject loadingInterface;
    public Image Loadingbar;
    
    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    // Start is called before the first frame update
    void PlayGame() //Click play and hide menu, show loading bar and load the scenes simultaneously
    {
        HideMenu();
        ShowLoading();
        scenesToLoad.Add(SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Additive)); //load these scenes from the main menu
        //scenesToLoad.Add(SceneManager.LoadSceneAsync("Level1", LoadSceneMode.Additive));
        StartCoroutine(LoadingScreen());

    }

    public void HideMenu()
    {
        menu.SetActive(false);
    }

    public void ShowLoading()
    {
        menu.SetActive(false);
    }

    IEnumerator LoadingScreen()
    {
        float totalProgress=0;
        for(int i=0; i<scenesToLoad.Count; ++i)
        {
        while(!scenesToLoad[i].isDone)
        {
            totalProgress += scenesToLoad[i].progress; //whilst the scenes havent loaded, add the scene loading progress to the total progress
            loadingProgressBar.fillAmount = totalProgress/scenesToLoad.Count; //creates value between 0 and 1 to fill loading bar
            yield return null;
        }
        }
    }
    
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _default, _pressdown;
    [SerializeField] private AudioClip _compressedClip, _uncompressedClip;
    [SerializeField] private AudioSource _source;


    public void OnPointerDown(PointerEventData eventData) //On mouse Down
    {
        _img.sprite = _pressdown;
        _source.PlayOneShot(_compressedClip);
    }

    public void OnPointerUp(PointerEventData eventData) //On mouse up
    {
        _img.sprite = _default;
        _source.PlayOneShot(_uncompressedClip);
    }

    public void WasClicked()
    {
        Debug.Log("Clicked");
    }

}


public class LoadingBar : MonoBehaviour
{
    public GameObject menu;
    public GameObject loadingInterface;
    public Image Loadingbar;

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();

    // Start is called before the first frame update
    void PlayGame() //Click play and hide menu, show loading bar and load the scenes simultaneously
    {
        HideMenu();
        ShowLoading();
        scenesToLoad.Add(SceneManager.LoadSceneAsync("SampleScene")); //load these scenes from the main menu
        //scenesToLoad.Add(SceneManager.LoadSceneAsync("Level1", LoadSceneMode.Additive));
        StartCoroutine(LoadingScreen());

    }

    public void HideMenu()
    {
        menu.SetActive(false);
    }

    public void ShowLoading()
    {
        menu.SetActive(false);
    }

    IEnumerator LoadingScreen()
    {
        float totalProgress=0;
        for(int i=0; i<scenesToLoad.Count; ++i)
        {
            while(!scenesToLoad[i].isDone)
            {
                totalProgress += scenesToLoad[i].progress; //whilst the scenes havent loaded, add the scene loading progress to the total progress
                loadingProgressBar.fillAmount = totalProgress/scenesToLoad.Count; //creates value between 0 and 1 to fill loading bar
                yield return null;
            }
        }
    }
    
    
}
*/