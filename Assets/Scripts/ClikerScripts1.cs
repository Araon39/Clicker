using System.Collections;
using UnityEngine;
using UnityEngine.UI;
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
    void Start()
    {
        score = 0;
        StartCoroutine(BonusShop());
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
}
