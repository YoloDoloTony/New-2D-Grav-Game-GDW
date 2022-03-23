using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{

    public bool isToggled = false;
    bool canInteract = false;
    public GameObject doorObject;

    // Update is called once per frame
    void Update()
    {
        if (canInteract == true && isToggled == false && Input.GetKeyDown("e"))
        {
            Debug.Log("Lever toggled");
            isToggled = true;
        }
        if (isToggled == true)
        {
            doorObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D leverBox)
    {
        if (leverBox.CompareTag("Player"))
        {
            canInteract = true;
            Debug.Log("Can toggle lever");
        }
    }

    private void OnTriggerExit2D(Collider2D leverBox)
    {
        if (leverBox.CompareTag("Player"))
        {
            canInteract = false;
            Debug.Log("Cannot toggle lever");
        }
    }
}
