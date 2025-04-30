/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonFocus_UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public Image[] itemsFocus;
    public Image[] imagesFocus;
    public TextMeshProUGUI[] txtFocuses;
    public Color hoverBehaviour_Color_Enter, hoverBehaviour_Color_Exit;
    

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        foreach (Image item in itemsFocus)
            item.gameObject.SetActive(true);
        foreach (Image image in imagesFocus)
            image.color = hoverBehaviour_Color_Enter;
        foreach (var image in txtFocuses)
            image.color = hoverBehaviour_Color_Enter;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        foreach (Image item in itemsFocus)
            item.gameObject.SetActive(false);
        foreach (Image image in imagesFocus)
            image.color = hoverBehaviour_Color_Exit;
        foreach (var image in txtFocuses)
            image.color = hoverBehaviour_Color_Exit;
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
