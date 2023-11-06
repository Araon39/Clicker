using UnityEngine;
using UnityEngine.UI;
public class ClikerScripts1 : MonoBehaviour
{
    public int score;
    public Text click_Text;
    
    void Start()
    {
        score = 0;
    }

    
    void Update()
    {
    click_Text.text = score + "$";             
    }

    public void ScroreAdd()
    {
        score++;                                    
    }
}
