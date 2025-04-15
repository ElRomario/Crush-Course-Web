using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnShit : MonoBehaviour
{
   
      public float disableTime = 5f; // Время до отключения
      public GameObject objectToEnable; // Объект, который нужно включить после отключения текущего

        void OnEnable()
        {
            Invoke("DisableAndActivate", disableTime);
        }

        void DisableAndActivate()
        {
            gameObject.SetActive(false); // Отключаем текущий объект

            if (objectToEnable != null)
            {
                objectToEnable.SetActive(true); // Включаем другой объект
            }
        }
    

}
