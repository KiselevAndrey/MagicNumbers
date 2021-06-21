using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandImageColor : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] int slowestIndex;
    [SerializeField] float percentChanger = 0.01f;

    Color _required, _current;
    float _percent;
    int _i;
    bool _colorIsRGB255;

    // Start is called before the first frame update
    void Start()
    {
        slowestIndex = Mathf.Abs(slowestIndex);
        
        _percent = 0f;
        _i = 0;
        _colorIsRGB255 = image.color.maxColorComponent > 1;

        _current = image.color;
        _required = _colorIsRGB255 ? ColorTransfusion_255.NewRandomColor() : ColorTransfusion_01.NewRandomColor();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_i > slowestIndex)
        {
            // _current = image.color;
            if (_colorIsRGB255) image.color = ColorTransfusion_255.NextColorStep(ref _current, ref _required, ref _percent, percentChanger);
            else image.color = ColorTransfusion_01.NextColorStep(ref _current, ref _required, ref _percent, percentChanger);

            _i = 0;
        }

        _i++;
    }

    void PrintColors()
    {
        print(_current + " _current");
        print(_required + " _required");
        print(image.color + " image.color");
        print(_percent + " percent");
    }
}
