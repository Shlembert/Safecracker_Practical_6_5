using System.Collections.Generic;
using UnityEngine;

public class ColorPinController : MonoBehaviour
{
    [SerializeField] private List<SpriteRenderer> pinsColor;
    [SerializeField] private Color rightColor, origColor;

    public void SetStartColor()
    {
        foreach (var pin in pinsColor) pin.color = origColor;
    }

    public void SetColor(int index, bool right)
    {
        Color currentColor;

        if (right) currentColor = rightColor;
        else currentColor = origColor;

        pinsColor[index].color = currentColor;
    }
}
