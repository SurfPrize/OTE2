using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateSlider : MonoBehaviour
{
    [SerializeField] private string valueToGet;
    private void Start()
    {
        if (PlayerPrefs.HasKey(valueToGet))
            GetComponent<Slider>().value = PlayerPrefs.GetFloat(valueToGet);
    }
}
