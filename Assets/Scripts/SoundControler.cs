using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControler : MonoBehaviour
{
    public AudioSource audio; // ���������� ��� �������� ���������� AudioSource
    public AudioClip clickSound; // ���������� ��� �������� ��������� ����� ��� �����

    void Start()
    {
        audio = GetComponent<AudioSource>(); // ��������� ���������� AudioSource ��� ������
    }

    public void onButtonClickAudio()
    {
        audio.PlayOneShot(clickSound); // ��������������� ��������� ����� ��� ������� ������
    }
}
