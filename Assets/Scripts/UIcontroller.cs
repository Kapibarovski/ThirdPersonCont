using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour
{
    [SerializeField] private Scrollbar _scrollbar;

    private void Start()
    {
        _scrollbar.value = PlayerPrefs.GetFloat("Sensitivety");
    }
    public void Update()
    {
        PlayerPrefs.SetFloat("Sensitivety", _scrollbar.value);
    }


}
