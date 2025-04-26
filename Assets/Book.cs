using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    public Animator animator;
    public GameObject page;
    // Start is called before the first frame update
   
    public void CloseOpenBook()
    {
        print("Check");
        if (animator.GetBool("Open") == true)
        {
            animator.SetBool("Open", false);
            page.SetActive(false);
        }
        else
        {
            animator.SetBool("Open",true);
            page.SetActive(true);
        }
    }
}
