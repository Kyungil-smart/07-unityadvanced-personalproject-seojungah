using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    
    public enum BgmType
    {
        StartScene,
        GameScene 
    }
    
    public enum SfxType
    {
        Attack,
        Item,
        Walk,
        Click
    }

    [System.Serializable]
    public struct BgmInfo
    {
        public BgmType type;
        public AudioClip clip;
    }

    [System.Serializable]
    public struct SfxInfo
    {
        public SfxType type;
        public AudioClip clip;
    }

    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        [Header("AudioSource")]
        [SerializeField]private AudioSource bgmSource;
        [SerializeField] private AudioSource sfxSource;
        
        [Header("Resources")]
        [SerializeField] private List<BgmInfo> bgmList = new List<BgmInfo>();
        private Dictionary<BgmType, AudioClip> _bgmDict = new Dictionary<BgmType, AudioClip>();
        [SerializeField] private List<SfxInfo> sfxList = new List<SfxInfo>();
        private Dictionary<SfxType, AudioClip> _sfxDict = new Dictionary<SfxType, AudioClip>();

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                
                //기본 볼륨 줄이기
                if (bgmSource != null) bgmSource.volume = 0.5f;
                if (sfxSource != null) sfxSource.volume = 0.5f;
                
                foreach (var bgm in bgmList)
                {
                    if (!_bgmDict.ContainsKey(bgm.type))  _bgmDict.Add(bgm.type, bgm.clip);
                }

                foreach (var sfx in sfxList)
                {
                    if (!_sfxDict.ContainsKey(sfx.type)) _sfxDict.Add(sfx.type, sfx.clip);
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            switch (scene.name)
            {
                case "StartScene":
                    PlayBgm(BgmType.StartScene);
                    break;
                case "GameScene":
                    PlayBgm(BgmType.GameScene);
                    break;
            }
        }

        /// <summary>
        /// 배경음 재생
        /// </summary>
        public void PlayBgm(BgmType type)
        {
            if (_bgmDict.TryGetValue(type, out AudioClip clip))
            {
                if (bgmSource.clip == clip && bgmSource.isPlaying) return;

                bgmSource.clip = clip;
                bgmSource.loop = true;
                bgmSource.Play();
            }
        }

        /// <summary>
        /// 효과음 재생
        /// </summary>
        public void PlaySfx(SfxType type)
        {
            sfxSource.mute = false;
            if (_sfxDict.TryGetValue(type, out AudioClip clip))
            {
                sfxSource.PlayOneShot(clip);
            }
        }
        
        /// <summary>
        /// 효과음 정지
        /// </summary>
        public void MuteSfx()
        {
            sfxSource.mute = true;
        }
        
        /// <summary>
        /// 배경음 볼륨 조절
        /// </summary>
        public void SetBgmVolume(float volume)
        {
            if (bgmSource != null)
            {
                bgmSource.volume = volume;
            }
        }

        /// <summary>
        /// 효과음 볼륨 조절
        /// </summary>
        public void SetSfxVolume(float volume)
        {
            if (sfxSource != null)
            {
                sfxSource.volume = volume;
            }
        }
    }
}