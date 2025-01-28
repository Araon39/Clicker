using UnityEngine;
using UnityEngine.UI;
public class ClikerScripts : MonoBehaviour
{
    public int score; // Переменная для хранения текущего счета
    public Text click_Text; // UI элемент для отображения счета

    void Start()
    {
        score = 0; // Инициализация счета
        score = PlayerPrefs.GetInt("Score+", score); // Получение сохраненного значения счета из памяти
    }

    void Update()
    {
        click_Text.text = score.ToString(); // Обновление текста UI элемента для отображения текущего счета
    }

    public void ScroreAdd() // Этот метод вызывается каждый раз при клике на кнопку
    {
        score++; // Увеличение счета на 1
        PlayerPrefs.SetInt("Score+", score); // Сохранение текущего счета в память
    }

    public void Reset() // Этот метод вызывается при нажатии кнопки рестарт
    {
        PlayerPrefs.DeleteAll(); // Удаление всех сохраненных данных из памяти
    }
}
