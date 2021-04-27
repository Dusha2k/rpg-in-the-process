using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : Interactable
{
    public string dialog;
    public GameObject dialogBox;
    public Text dialogText;
    
   void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && _playerInRange)
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
            }
        }
        
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            dialogBox.SetActive(false);
            base.OnTriggerExit2D(other);
        }
    }
}
