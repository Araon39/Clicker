using UnityEngine;
using UnityEngine.UI;
public class ClikerScripts : MonoBehaviour
{
    public int score;
    public Text click_Text;
    
    void Start()
    {
        score = 0;
        score = PlayerPrefs.GetInt("Score+", score); //запрос из памяти
    }

    
    void Update()
    {
    click_Text.text = score.ToString();             // обновление текста кнопки
    }

    public void ScroreAdd() // этот метод вызывается каждый раз при клике на кнопку
    {
        score++;                                     // добавление счета
        PlayerPrefs.SetInt("Score+", score);        // создаем переменую памяти
    }

    public void Reset() // этот метод вызывается при нажатии кнопки рестарт
    {
        PlayerPrefs.DeleteAll();                 //обнулить всю память
    }
}
