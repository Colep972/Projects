using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetName : MonoBehaviour
{
    public TMP_Text pseudoText;

    void Start()
    {
        if (StaticClass._name != "")
        {
            pseudoText.text = StaticClass._name + " Factory";
        }
        else
        {
            pseudoText.text = "Guest Factory";
        }
    }

    public string getFactoryName()
    {
        return pseudoText.text;
    }

    public void setFactoryName(string name)
    {
        pseudoText.text = name;
    }
}
