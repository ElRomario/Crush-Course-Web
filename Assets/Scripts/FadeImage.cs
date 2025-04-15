using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    
        
        public VideoAndImageSwitch manager;
        public Image targetImage; 
        public float fadeDuration = 1f; 
        public float visibleTime = 2f; 

        private void Start()
        {
            if (targetImage != null)
            {
                StartCoroutine(FadeRoutine());
              
        }
        }

        private IEnumerator FadeRoutine()
        {
            yield return StartCoroutine(FadeIn());
            yield return new WaitForSeconds(visibleTime);
            yield return StartCoroutine(FadeOut());
            manager.enabled = true;

    }

        private IEnumerator FadeIn()
        {
            float elapsed = 0f;
            Color color = targetImage.color;
            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                color.a = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
                targetImage.color = color;
                yield return null;
            }
            color.a = 1f;
            targetImage.color = color;
        }

        private IEnumerator FadeOut()
        {
            float elapsed = 0f;
            Color color = targetImage.color;
            while (elapsed < fadeDuration)
            {
                elapsed += Time.deltaTime;
                color.a = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
                targetImage.color = color;
            yield return null;
            }
            color.a = 0f;
            targetImage.color = color;
            



        }
    }

