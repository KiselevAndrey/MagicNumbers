using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTransfusion_255 : MonoBehaviour
{
    /// <summary>
    /// Плавное изменение цвета основная функция
    /// </summary>
    /// <param name="current">Текущий цвет</param>
    /// <param name="required">Требуемый цвет</param>
    /// <param name="percent">Процент требуемого цвета. Меняется от 0 до 1. С проверкой на вхождение в требуемый отрезок</param>
    /// <param name="percentChanger">Шаг изменения процента</param>
    /// <returns>Новый цвет от процентного смешивания двух цветов </returns>
    public static Color NextColorStep(ref Color current, ref Color required, ref float percent, float percentChanger = 0.01f)
    {
        if (percent > 1f || percent < 0f)
        {
            current = required;
            required = NewRandomColor();
            percent = 0f;
        }

        percent += percentChanger;
        return FadeToColor(current, required, percent);
    }

    /// <summary>
    /// Плавное изменение цвета основная функция
    /// </summary>
    /// <param name="current">Текущий цвет</param>
    /// <param name="required">Требуемый цвет</param>
    /// <param name="percent">Процент требуемого цвета. Меняется от 0 до 1. С проверкой на вхождение в требуемый отрезок</param>
    /// <param name="percentChanger">Шаг изменения процента</param>
    /// <returns>Новый цвет от процентного смешивания двух цветов </returns>
    public static Color NextColorStep(Color current, ref Color required, ref float percent, float percentChanger = 0.01f)
    {
        if (percent > 1f || percent < 0f)
        {
            required = NewRandomColor();
            percent = 0f;
        }

        percent += percentChanger;
        return FadeToColor(current, required, percent);
    }

    /// <summary>
    /// Новый рандомный цвет
    /// </summary>
    /// <returns>Возвращает новый рандомный цвет</returns>
    public static Color NewRandomColor()
    {
        return new Color(Random.Range(0, 256), Random.Range(0, 256), Random.Range(0, 256));
    }

    /// <summary>
    /// Процентное изменение цвета от текущего к требующемуся
    /// </summary>
    /// <param name="current">Текущий цвет</param>
    /// <param name="required">Требуемый цвет</param>
    /// <param name="perсent">Процент требуемого цвета. Меняется от 0 до 1. С проверкой на вхождение в требуемый отрезок</param>
    /// <returns>Новый цвет от процентного смешивания двух цветов </returns>
    static Color FadeToColor(Color current, Color required, float perсent)
    {
        if (perсent >= 1f || perсent <= 0f) perсent = 0f;

        int r = (int)(required.r * perсent + current.r * (1f - perсent));
        int g = (int)(required.g * perсent + current.g * (1f - perсent));
        int b = (int)(required.b * perсent + current.b * (1f - perсent));

        return new Color(r, g, b);
    }
}
