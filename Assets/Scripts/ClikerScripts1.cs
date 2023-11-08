using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System; //для Serializable класса Save и для сохранения в JSON???
public class ClikerScripts1 : MonoBehaviour
{
    public int score;
    public Text click_Text;

    public GameObject shopPan;
    public GameObject bonusPan;

    public int[] costInt;       //что бы назначить цену товара
    public Text[] costText;     //что бы видеть цену товара
    private int clickScore = 1; //цена за клик

    public int[] costBonus;
    
    private Save _sv = new Save(); //создаем новый экземпляр класса

    private int totalBonus; //переменая для подсчёта заработаных бонусов в офлайне

    //загрузка сохранения на старте
    private void Awake()
    {
        if (PlayerPrefs.HasKey("SV")) //проверяем есть ли такое сохранение
        {
            _sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("SV")); //в экземпляр класа загружаем сохранение
            score = _sv.score;
            clickScore = _sv.clickScore;
            //создаем цикл для записи данных в наши текущие переменные "бонусы" из сохранения
            for (int i = 0; i < 1; i++)
            {
                costBonus[i] = _sv.costBonus[i];
                totalBonus += _sv.costBonus[i]; //мы узнаем сколько заработали в отсутсвии в игре
            }
            //создаем цикл для записи данных в в наши текущие переменные "магазин" из сохранения
            for (int i = 0; i < 2; i++)
            {
                costInt[i] = _sv.costInt[i];
                costText[i].text = _sv.costInt[i] + "$"; //визуализируем это в текст
            }
        }
    }

    void Start()
    {
        //score = 0;
        StartCoroutine(BonusShop());
        
        //создаем переменую для времени и записываем в него последнее сохранение
        DateTime dt = new DateTime(_sv.date[0],_sv.date[1],_sv.date[2],_sv.date[3],_sv.date[4],_sv.date[5]);
        //переменная расчета разницы во времени от текущего с прошлым
        TimeSpan ts = DateTime.Now - dt;

        //расчет очков за время отсутствия в игре (глобальное время в секундах умноженое на множитель очков)
        score += (int) ts.TotalSeconds * totalBonus;
        print("Вы заработали: " + (int) ts.TotalSeconds * totalBonus + " $");
    }

    void Update()
    {
    click_Text.text = score + "$";    //обновление картинки денег на главном экране         
    }

    //Монетка для клика
    public void ScroreAdd()
    {
        score+= clickScore;                                    
    }
    
    //вкл/выкл панель Магазина
    public void SopPanActivated()
    {
        shopPan.SetActive(!shopPan.activeSelf);
    }
    
    //вкл/выкл панель Бонуса
    public void BonusPanActivated()
    {
        bonusPan.SetActive(!bonusPan.activeSelf);
    }

    //покупка увеличения цены за клик
    public void OnClickBuyLevel()
    {
        if (score >= costInt[0])    //проверяем хватает ли денег
        {
            score -= costInt[0];    // вычитаем деньги (покупка)
            costInt[0] *= 2;        //увеличиваем цену в 2 раза
            clickScore *= 2;        //цена за клик увеличена в 2 раза
            costText[0].text = costInt[0] + "$"; //обновляем текст товара
        }
    }
    
    //покупка бонуса автокликера
    public void OnClickBuyBonus()
    {
        if (score >= costInt[1])    //проверяем хватает ли денег
        {
            score -= costInt[1];    // вычитаем деньги (покупка)
            costInt[1] *= 2;        //увеличиваем цену в 2 раза
            costBonus[0] += 2;      //при покупке автокликер будет увеличивать на два
            costText[1].text = costInt[1] + "$"; //обновляем текст товара
        }
    }

    //автоматически прибавляем деньги после улучшения
    IEnumerator BonusShop() 
    {
        while (true) //бесконечно
        {
           score += costBonus[0];                  //прибавка цифры к счету
           yield return new WaitForSeconds(1);     //подождать 1 сек
        }
    }
#if UNITY_ANDROID && !UNITY_EDITOR
    //этот метод поможет нам сохраняться в телефоне после билда
    //так как OnApplicationQuit сохраняет в эдиторе
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            _sv.score = score;
            _sv.clickScore = clickScore;
        
            _sv.costBonus = new int[1]; //создаем массив размером в единицу так как для одного товара
            _sv.costInt = new int[2]; //тут два товара
            //создаем цикл для записи данных в бонусы
            for (int i = 0; i < 1; i++)
            {
                _sv.costBonus[i] = costBonus[i];
            }
            //создаем цикл для записи данных в магазин
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
        
            //записываем все в Json???
            PlayerPrefs.SetString("SV", JsonUtility.ToJson(_sv));  
        }
    }
#else
    //метод сохранения данных в эдиторе - тот что нужно сохранить при выходе из игры
    private void OnApplicationQuit()
    {
        _sv.score = score;
        _sv.clickScore = clickScore;
        
        _sv.costBonus = new int[1]; //создаем массив размером в единицу так как для одного товара
        _sv.costInt = new int[2]; //тут два товара
        //создаем цикл для записи данных в бонусы
        for (int i = 0; i < 1; i++)
        {
            _sv.costBonus[i] = costBonus[i];
        }
        //создаем цикл для записи данных в магазин
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
        
        //записываем все в Json???
        PlayerPrefs.SetString("SV", JsonUtility.ToJson(_sv));
    }
#endif
}

//создаем новый класс для переменых которые хотим сохранять
[Serializable]
public class Save 
{
    public int score;
    public int clickScore;
    public int[] costInt;
    public int[] costBonus;
    
    //создаем массив временисо старта в 6 пустых ячеек
    //для 1год 2месяц 3день 4час 5мин 6сек
    public int[] date = new int[6]; 
}