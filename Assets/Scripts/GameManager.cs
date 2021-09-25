using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public AudioSource sound;
    public AudioClip[] musics;
    public AudioClip[] FX;
    public bool isWin;
    public float TotalScore;
    public PlayerData data;

    public enum Musics
        {
            level1,
            level2,
            level3,
            level4,
            level5,
            level6
        }
    public enum fx
    {
        jump,
        shoot,
        die,
        hold,
        level5,
        level6
    }


    private void Awake()
    {
        sound = GetComponent<AudioSource>();
        if (instance != null)
            Destroy(this);
        else
        { instance = this; DontDestroyOnLoad(this); }
        SaveManager.LoadData();
        if (data.Music)
        {
            string name = SceneManager.GetActiveScene().name;
            switch (name)
            {
                default: sound.clip = musics[6]; break;
                case "Level 1": sound.clip = musics[(int)Musics.level1]; break;
                case "Level 2": sound.clip = musics[(int)Musics.level2]; break;
                case "Level 3": sound.clip = musics[(int)Musics.level3]; break;
                case "Level 4": sound.clip = musics[(int)Musics.level4]; break;
                case "Final Level": sound.clip = musics[(int)Musics.level5]; break;
                case "Infinite ": sound.clip = musics[(int)Musics.level6]; break;
            }
            print(name);

            sound.Play();
        }
        else
            sound.Stop();

    }
    private void OnLevelWasLoaded(int level)
    {
        SaveManager.LoadData();
        if (data.Music)
        {
            string name = SceneManager.GetActiveScene().name;
            switch (name)
            {
                default: sound.clip = musics[6]; break;
                case "Level 1": sound.clip = musics[(int)Musics.level1]; break;
                case "Level 2": sound.clip = musics[(int)Musics.level2]; break;
                case "Level 3": sound.clip = musics[(int)Musics.level3]; break;
                case "Level 4": sound.clip = musics[(int)Musics.level4]; break;
                case "Final Level": sound.clip = musics[(int)Musics.level5]; break;
                case "Infinite ": sound.clip = musics[(int)Musics.level6]; break;
            }
            print(name);

            sound.Play();
        }
        else
            sound.Stop();
    }

    private void OnEnable()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            ReloadLevel();
    }

    public void PlayAnimation(Animator animator, string animationId,ref string CurrentState)
    {
        if (CurrentState == animationId)
            return;
        
        
        animator.Play(animationId);
        CurrentState = animationId;
    }
    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void LoadLevel(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
    public void PlaySound(int index)
    {
        if(data.SoundEffects)
            sound.PlayOneShot(FX[index]);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public PlayerData NewPlayer()
    {
        print(1);
        data = new PlayerData();
        print(2);
        data.CurrentLevel = 1;
        print(3);
        data.Music = true;
        print(4);
        data.SoundEffects = true;
        print(5);
        return data;
    }


}
