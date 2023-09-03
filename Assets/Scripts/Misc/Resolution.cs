using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Resolution
{
    public int horizontal, vertical;

    public Resolution(int horizontal_, int vertical_)
    {
        horizontal = horizontal_;
        vertical = vertical_;
    }
}
