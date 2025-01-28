using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControler : MonoBehaviour
{
    public AudioSource audio; // Переменная для хранения компонента AudioSource
    public AudioClip clickSound; // Переменная для хранения звукового клипа для клика

    void Start()
    {
        audio = GetComponent<AudioSource>(); // Получение компонента AudioSource при старте
    }

    public void onButtonClickAudio()
    {
        audio.PlayOneShot(clickSound); // Воспроизведение звукового клипа при нажатии кнопки
    }
}
