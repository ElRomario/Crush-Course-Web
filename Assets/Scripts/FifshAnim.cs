using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class FifshAnim : MonoBehaviour
{
    public Image introImage; // —сылка на Intro (первое изображение)
    public Image waitingImage; // —сылка на "Waiting for Start" (второе изображение)
    public void OnIntroAnimationFinished()
    {
        introImage.gameObject.SetActive(false);
        waitingImage.gameObject.SetActive(true);
    }
}
