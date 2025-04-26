using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AutoFocusInput : MonoBehaviour
{
    public InputField inputField;

    void Start()
    {
        inputField.Select();
        inputField.ActivateInputField();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            FocusInput();
        }
    }

    void FocusInput()
    {
        StartCoroutine(DelayFocus());
    }

    IEnumerator DelayFocus()
    {
        yield return null;
        inputField.Select();
        inputField.ActivateInputField();
    }
}
