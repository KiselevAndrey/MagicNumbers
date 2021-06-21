using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputBorders : MonoBehaviour
{
    [SerializeField] Text infoText;
    [SerializeField] public Story story;
    [SerializeField] MagicNumber magicNumber;

    public int Number { get; private set; }
    public int Min { get; private set; }
    public int Max { get; private set; }
    public bool SlyMonkey { get; private set; }

    string _number;
    bool _inputMin, _telTask;

    // Start is called before the first frame update
    public void Start()
    {
        MyReset();
    }

    // Update is called once per frame
    void Update()
    {
        //если введена цифра
        if (HelpInputDigit.InputKey(PressMode.Up, out string digit))
        {
            _number += digit;

            string str = _inputMin ? "первое" : "второе";            
            str = "Твоё " + str + " число: " + _number + ".";

            PrintStory(str, true);
        }

        // если нажат Enter
        if (HelpInputSpecialSymbal.InputKey(PressMode.Up, NameSymbal.Enter))
        {
            string str;

            ///// если идет объяснение /////
            if (_telTask)
            {
                str = "Введи число.";

                _telTask = false;
                PrintStory(str, false);
                return;
            }

            // если ввод первого числа
            if (_inputMin)
            {
                if (!CheckInputNumber(ref _number, out int min)) return;

                Min = min;

                str = (Min > 9) ? "Кусок биоплазмы справился!" : "О, жалкое подобие разума! Одна цифра?";
                str += "\n\nТеперь вводи второе число.";
                
                PrintStory(str, false);
            }
            else
            {
                if (!CheckInputNumber(ref _number, out int max)) return;

                Max = max;

                if (Max < Min)
                {
                    str = "Скрытый тест провален. Даже приматы твоего мира подозревают что второе число должно быть больше.\n";
                    int temp = Min;
                    Min = Max;
                    Max = temp;
                }
                else if (Max - Min < 10 && Max - Min > 0)
                {
                    str = "Максимум попыток пройти тест: " + (Max - Min + 1) + ".\nДумаешь ты самый хитрый, ошибка генетики?\n";
                    SlyMonkey = true;
                }
                else if (Max - Min == 0)
                {
                    str = "Сборище белков, жиров и углеродов попыталось меня обмануть? На что ты надеялся подопытный № " + story.SubjectNumber + "? Вводи число заново.";

                    _number = "";
                    PrintStory(str, false);
                    return;
                }
                else
                    str = "Пока ты это делал я уже решил \"Главный вопрос жизни, вселенной и всего такого\". И да, это не 42.";

                PrintStory(str, true);
                SetActive(false);
            }


            _number = "";
            _inputMin = !_inputMin;
        }


        if (HelpInputSpecialSymbal.InputKey(PressMode.Up, NameSymbal.Space)) ResetGame();
    }
    
    public bool CheckInputNumber(ref string strNumber, out int number)
    {
        if (!int.TryParse(strNumber, out number))
        {
            string str;

            if (strNumber.Length == 0)
            {
                if (Random.Range(0, 10) > 5)
                    str = "Кого мне подсунули? Эта протоплазма умеет нажимать только одну кнопку!\nДаю тебе ещё одну возможность ввести число.";
                else
                    str = "Что за тупая органика!\nЧитай задание внимательнее вводи цифры, а не Enter.";
            }
            else
            {
                str = "Критическая ошибка Е" + Random.Range(0, 1001) + "_BIG_NUMBER_" + strNumber + ".\n\nЧисло обнулено.";
                strNumber = "";
            }
            PrintStory(str, false);

            return false;
        }

        strNumber = "";
        return true;
    }

    void PrintStory(string str, bool enterToContinue) => infoText.text = str + (enterToContinue ? "\n\nДля продолжения жми Enter." : "");

    void MyReset()
    {
        SlyMonkey = false;
        _inputMin = _telTask = true;
        _number = "";
    }

    public void ResetGame()
    {
        MyReset();
        story.enabled = true;
        story.ResetGame();
        SetActive(false);
    }

    void SetActive(bool value)
    {
        this.enabled = value;
        magicNumber.enabled = !value;
    }
}
