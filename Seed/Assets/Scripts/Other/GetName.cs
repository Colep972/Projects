using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GetName : MonoBehaviour
{
    public TMP_Text pseudoText;

    private bool factoryNameSet = false;

    void Start()
    {
        if (!factoryNameSet)
        {
            InitDefaultName();
        }
    }

    public void InitDefaultName()
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
        if (pseudoText != null)
        {
            pseudoText.text = name;
            Debug.Log("Factory name set to: " + pseudoText.text);
            factoryNameSet = true;
        }
        else
        {
            Debug.LogError("pseudoText is null. Assigne-le dans l'inspecteur.");
        }
    }
}
