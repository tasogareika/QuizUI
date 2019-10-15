using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

//ref: http://www.feelouttheform.net/unity3d-links-textmeshpro/

public class LinkOpener : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        TextMeshProUGUI pTextMeshPro = GetComponent<TextMeshProUGUI>();
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(pTextMeshPro, Input.mousePosition, null);  // If you are not in a Canvas using Screen Overlay, put your camera instead of null
            Debug.Log(linkIndex);
            if (linkIndex > -1)
            { // was a link clicked?
                TMP_LinkInfo linkInfo = pTextMeshPro.textInfo.linkInfo[linkIndex];

            }
        }
    }
}