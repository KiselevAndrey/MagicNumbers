using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Story : MonoBehaviour
{
    [SerializeField] Text infoText;
    [SerializeField] InputBorders inputBorders;

    public int SubjectNumber { get; private set; }
    public int IINumber { get; private set; }

    string[] _story;
    int _i;

    // Start is called before the first frame update
    void Start()
    {
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        // если нажат Enter
        if (HelpInputSpecialSymbal.InputKey(PressMode.Up, NameSymbal.Enter))
        {
            _i++;

            PrintStory(_story[_i], true);
            if (_i >= _story.Length - 1) SetActive(false);
        }

        if (HelpInputSpecialSymbal.InputKey(PressMode.Up, NameSymbal.Space)) ResetGame();
    }

    public void PrintStory(string str, bool enterToContinue)
    {
        infoText.text = str + (enterToContinue ? "\n\nДля продолжения жми Enter." : "");
    }
    
    public void ResetGame()
    {
        SubjectNumber = Random.Range(0, 1000000);
        IINumber = Random.Range(0, 1000);

        _story = new string[]{"А вот и подопытный №" + SubjectNumber + ".\nЯ ИИ последнего поколения.",
            "Загружаю тест для проверки твоих умственных способностей.",
            "Я сейчас загадаю число, а ты будешь его отгадывать.",
            "И чтобы ты не расслаблялся, это же число будет отгадывать и мой дальний родственник:\nИИ " + IINumber + " поколения.",
            "Для этого потребуется диапазон чисел.\nТебе, жалкий мясной мешок, дана честь его выбрать. Не подведи меня."};

        _i = 0;

        PrintStory(_story[_i], true);

        SetActive(true);
    }

    void SetActive(bool value)
    {
        this.enabled = value;
        inputBorders.enabled = !value;
    }
}
