using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// Timelineをループする際微調整を加えるためのメソッド
/// </summary>
public class TimelineOffset : MonoBehaviour
{
    [SerializeField]
    private PlayableDirector playableDirector;

    [SerializeField]
    private double loopStartTime = 0.0; // ループする起点となる時間

    [SerializeField]
    private float InitialTime = 25.0f; // ループ時に再生を開始する時間

    private bool isLooping = false;

    [SerializeField]
    private Camera cutOutCamera;



    private void Start()
    {
        if (playableDirector == null)
        {
            Debug.LogError("PlayableDirectorがアタッチされていません。");
            return;
        }
    }

    private void Update()
    {
        if (!isLooping)
        {
            if (playableDirector.time >= loopStartTime)
            {
                // ループの開始時間に到達したらループを開始
                playableDirector.time = InitialTime;
                playableDirector.Play();
                isLooping = true;
            }
        }
    }

    /// <summary>
    /// ループ再生時にMainCameraを非アクティブにして、俯瞰カメラに切り替える
    /// </summary>
    public void CutOutCamera()
    {
        cutOutCamera.gameObject.SetActive(false);

    }
}

