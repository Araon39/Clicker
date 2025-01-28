using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System; //для Serializable класса Save и для сохранения в JSON???
public class ClikerScripts1 : MonoBehaviour
{
    public int score; // Переменная для хранения текущего счета
    public Text click_Text; // UI элемент для отображения счета

    public GameObject shopPan; // Панель магазина
    public GameObject bonusPan; // Панель бонусов

    public int[] costInt; // Массив для хранения стоимости товаров
    public Text[] costText; // Массив для отображения стоимости товаров
    private int clickScore = 1; // Стоимость одного клика

    public int[] costBonus; // Массив для хранения стоимости бонусов

    private Save _sv = new Save(); // Создание нового экземпляра класса Save

    private int totalBonus; // Переменная для подсчета заработанных бонусов в офлайне

    // Загрузка сохранения на старте
    private void Awake()
    {
        if (PlayerPrefs.HasKey("SV")) // Проверка наличия сохранения
        {
            _sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("SV")); // Загрузка сохранения в экземпляр класса
            score = _sv.score;
            clickScore = _sv.clickScore;
            // Цикл для записи данных в текущие переменные "бонусы" из сохранения
            for (int i = 0; i < 1; i++)
            {
                costBonus[i] = _sv.costBonus[i];
                totalBonus += _sv.costBonus[i]; // Подсчет заработанных бонусов в офлайне
            }
            // Цикл для записи данных в текущие переменные "магазин" из сохранения
            for (int i = 0; i < 2; i++)
            {
                costInt[i] = _sv.costInt[i];
                costText[i].text = _sv.costInt[i] + "$"; // Отображение стоимости товаров
            }
        }
    }

    void Start()
    {
        StartCoroutine(BonusShop());

        // Создание переменной для времени и запись в нее последнего сохранения
        DateTime dt = new DateTime(_sv.date[0], _sv.date[1], _sv.date[2], _sv.date[3], _sv.date[4], _sv.date[5]);
        // Переменная для расчета разницы во времени между текущим и последним сохранением
        TimeSpan ts = DateTime.Now - dt;

        // Расчет очков за время отсутствия в игре (глобальное время в секундах умноженное на множитель очков)
        score += (int)ts.TotalSeconds * totalBonus;
        print("Вы заработали: " + (int)ts.TotalSeconds * totalBonus + " $");
    }

    void Update()
    {
        click_Text.text = score + "$"; // Обновление текста UI элемента для отображения текущего счета
    }

    // Метод для увеличения счета при клике
    public void ScroreAdd()
    {
        score += clickScore;
    }

    // Включение/выключение панели магазина
    public void SopPanActivated()
    {
        shopPan.SetActive(!shopPan.activeSelf);
    }

    // Включение/выключение панели бонусов
    public void BonusPanActivated()
    {
        bonusPan.SetActive(!bonusPan.activeSelf);
    }

    // Покупка увеличения стоимости за клик
    public void OnClickBuyLevel()
    {
        if (score >= costInt[0]) // Проверка наличия достаточного количества денег
        {
            score -= costInt[0]; // Вычитание денег (покупка)
            costInt[0] *= 2; // Увеличение стоимости в 2 раза
            clickScore *= 2; // Увеличение стоимости за клик в 2 раза
            costText[0].text = costInt[0] + "$"; // Обновление текста товара
        }
    }

    // Покупка бонуса автокликера
    public void OnClickBuyBonus()
    {
        if (score >= costInt[1]) // Проверка наличия достаточного количества денег
        {
            score -= costInt[1]; // Вычитание денег (покупка)
            costInt[1] *= 2; // Увеличение стоимости в 2 раза
            costBonus[0] += 2; // Увеличение бонуса автокликера на 2
            costText[1].text = costInt[1] + "$"; // Обновление текста товара
        }
    }

    // Автоматическое добавление денег после улучшения
    IEnumerator BonusShop()
    {
        while (true) // Бесконечный цикл
        {
            score += costBonus[0]; // Добавление бонуса к счету
            yield return new WaitForSeconds(1); // Ожидание 1 секунду
        }
    }

#if UNITY_ANDROID && !UNITY_EDITOR
        // Метод для сохранения данных на телефоне после билда
        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                SaveData();
            }
        }
#else
    // Метод для сохранения данных в редакторе при выходе из игры
    private void OnApplicationQuit()
    {
        SaveData();
    }
#endif

    // Метод для сохранения данных
    private void SaveData()
    {
        _sv.score = score;
        _sv.clickScore = clickScore;

        _sv.costBonus = new int[1]; // Создание массива размером в единицу для одного товара
        _sv.costInt = new int[2]; // Создание массива размером в два для двух товаров
                                  // Цикл для записи данных в бонусы
        for (int i = 0; i < 1; i++)
        {
            _sv.costBonus[i] = costBonus[i];
        }
        // Цикл для записи данных в магазин
        for (int i = 0; i < 2; i++)
        {
            _sv.costInt[i] = costInt[i];
        }

        _sv.date[0] = DateTime.Now.Year;
        _sv.date[1] = DateTime.Now.Month;
        _sv.date[2] = DateTime.Now.Day;
        _sv.date[3] = DateTime.Now.Hour;
        _sv.date[4] = DateTime.Now.Minute;
        _sv.date[5] = DateTime.Now.Second;

        // Запись всех данных в JSON
        PlayerPrefs.SetString("SV", JsonUtility.ToJson(_sv));
    }
}

// Класс для переменных, которые нужно сохранять
[Serializable]
public class Save
{
    public int score;
    public int clickScore;
    public int[] costInt;
    public int[] costBonus;

    // Массив времени с 6 ячейками для года, месяца, дня, часа, минуты и секунды
    public int[] date = new int[6];
}
