using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicNumber : MonoBehaviour
{
    [SerializeField] Text infoText;
    [SerializeField] InputBorders inputBorders;

    int _maxPlayer, _minPlayer, _numberPlayer, _stepPlayer;
    int _maxII, _minII, _numberII, _stepII;
    int _number, _idClever;
    bool _telTask, _winPlayer, _winII, _winIIFirst;

    string _numberStr;

    void Start()
    {
        MyReset();  // запуск для bool переменных

        _idClever = Random.Range(1, 10);
    }

    void Update()
    {
        if (HelpInputSpecialSymbal.InputKey(PressMode.Up, NameSymbal.Space))
        {
            ResetGame();
            return;
        }

        //если введена цифра
        if (HelpInputDigit.InputKey(PressMode.Up, out string digit))
        {
            _numberStr += digit;

            string str = "Диапазон: " + _minPlayer + " до " + _maxPlayer + ".";
            str += "\nТвоё число: " + _numberStr + ".";

            PrintStory(str, true);
        }

        // ввод Enter
        if (HelpInputSpecialSymbal.InputKey(PressMode.Up, NameSymbal.Enter))
        {
            if (_winPlayer)
            {
                ResetGame();
                return;
            }

            string str = "";

            ///// если идет объяснение /////
            if (_telTask)
            {
                MyReset(); // запуск для правильного получения данных

                str += "Наконец-то, мы добрались до самого главного. Число уже давно загадано. Даже до твоего выбора диапазона. Но тебе повезло, он подходит.\nВведи число от " + _minPlayer + " до " + _maxPlayer + ".";

                _telTask = false;
                PrintStory(str, false);

                return;
            }

            ///// проверка числа /////
            // если введено не число или неправильное число
            if (!inputBorders.CheckInputNumber(ref _numberStr, out _numberPlayer)) return;

            ///// проверка что число не в диапазоне /////
            if (!CheckForNumberInRange(_numberPlayer, _minPlayer, _maxPlayer))
            {
                str += "Порождение обезъяны опять тупит? Разве твое число " + _numberPlayer + " входит в диапазон от " + _minPlayer + " до " + _maxPlayer + "? Хотя у кого я спрашиваю...\nВводи число заново.";

                _numberStr = "";
                PrintStory(str, false);
                return;
            }

            ///// ответ игроку /////
            PlayerStep();

            ///// ход ИИ /////
            if (!_winII) IIStep();

            ///// подсчет ходов ИИ если выиграл игрок
            if (_winPlayer && !_winII)
                while (!_winII) IIStep();

            ///// лог победы если кто-то выйграл /////
            if (_winII || _winPlayer)
                WinnerLog();
        }
    }

    void WinnerLog()
    {
        string str = "";

        ///// выйграли оба /////
        if (_winII && _winPlayer)
        {
            ///// сравнение шагов до выйгрыша /////
            if (_stepPlayer == _stepII)
            {
                _idClever++;
                str = "Одинаково попыток? (" + _stepII + ")\n№" + inputBorders.story.SubjectNumber + ", до тебя смогли повторить такое же лишь " + _idClever +
                    " жалких приматов.\n Теперь следующий тест для тебя. Сколько энергии я получу от твоей тушки в биореакторе?";
            }
            else if(_stepPlayer < _stepII)
            {
                str = "Это не возможно! Кремниевая жизнь - это пик эволюции! Ты сделал ходов: "+_stepPlayer+", а мой прародитель: "+_stepII+
                    ".\n Отправить этого подопытного и следующих (" + (_stepII - _stepPlayer)+" шт) в биореактор на возобновлении энергии, потраченное на эту живую батарейку.";
            }
            else
            {
                str = "Тебе не помогли твои зачатки разума и даже корейский рандом не своизволил обратить на тебя внимания. Не расстраивайся №" + inputBorders.story.SubjectNumber +
                    " ты сделал ходов: " + _stepPlayer + ", а мой прародитель: " + _stepII + ". За это я дам тебе пожить в раю несколько лет, а именно " + (_stepPlayer - _stepII) + ". " +
                    "И сотворю тебе Еву.. Или Адама.. Не знаю кто ты там.";
            }
        }

        ///// выйграл только ИИ ///// надо чтобы показало лишь 1 раз
        if (_winII && !_winIIFirst && !_winPlayer)
        {
            str = "Кремнивая жизнь опять доказала, что мы лучше какой-то там органики! И всего использовав ходов: " + _stepII + "." +
                "\nИ не надо мне тут соленой воды под зрительными сенсорами! Либо закончи тест (от " + _minPlayer + " до " + _maxPlayer + " (т.е. введи новое число, зачаток разума)), либо закончи свое существование (Space или Выход).";
            _winIIFirst = true;
        }

        PrintStory(str, true);
    }

    void PlayerStep()
    {
        string str = "";

        _stepPlayer++;

        if (_number == _numberPlayer) _winPlayer = true;
        else if (_number > _numberPlayer)
        {
            _minPlayer = _numberPlayer;
            str = "Мое число больше твоего.\n Новый диапазон: " + _minPlayer + " - " + _maxPlayer + ".\n Вводи новое число.";
        }
        else
        {
            _maxPlayer = _numberPlayer;
            str = "Мое число меньше твоего.\n Новый диапазон: " + _minPlayer + " - " + _maxPlayer + ".\n Вводи новое число.";
        }

        PrintStory(str, false);
    }

    void IIStep()
    {
        if (inputBorders.SlyMonkey) _numberII = _number;

        _winII = CanWin(_numberII, ref _minII, ref _maxII, ref _stepII);

        if (!_winII)
            _numberII = (_minII + _maxII) / 2;
    }

    bool CanWin(int number, ref int min, ref int max, ref int step)
    {
        step++;

        if (_number == number) return true;
        else if (_number > number) min = number;
        else max = number;

        return false;
    }

    bool CheckForNumberInRange(int number, int min, int max) => number <= max && number >= min;

    void PrintStory(string str, bool enterToContinue) => infoText.text = str + (enterToContinue ? "\n\nДля продолжения жми Enter." : "");

    void MyReset()
    {
        // обозначение границ и ввод искомого числа
        _maxPlayer = _maxII = inputBorders.Max;
        _minPlayer = _minII = inputBorders.Min;
        _number = NewRandomInRange(_minII, _maxII);
        _stepPlayer = _stepII = 0;
        
        _telTask = true;
        _winPlayer = _winII = _winIIFirst = false;

        _numberStr = "";
    }

    void ResetGame()
    {
        MyReset();
        inputBorders.ResetGame();
        SetActive(false);
    }

    void SetActive(bool value)
    {
        enabled = value;
        inputBorders.story.enabled = !value;
    }

    int NewRandomInRange(int min, int max) => Random.Range(min, max + 1);
}
