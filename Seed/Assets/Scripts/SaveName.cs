using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SaveName : MonoBehaviour
{
    public TMP_InputField inputField;

    public void Save()
    {
        StaticClass._name = inputField.text;
    }
}