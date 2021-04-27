using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    public GameObject virtualCamera;

    public string placeName;
    public GameObject textOfPlace;
    public Text placeText;
    
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCamera.SetActive(true);
            StartCoroutine(PlaceNameCo());
        }
    }

    public virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            StartCoroutine(DisableCamera());
        }
    }

    private IEnumerator DisableCamera()
    {
        yield return new WaitForSeconds(1f);
        virtualCamera.SetActive(false);
    }

    private IEnumerator PlaceNameCo()
    {
        textOfPlace.SetActive(true);
        placeText.text = placeName;
        yield return new WaitForSeconds(3f);
        textOfPlace.SetActive(false);

    }
}
