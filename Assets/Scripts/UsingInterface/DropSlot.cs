
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
public class DropSlot : MonoBehaviour, IDropHandler
{
    //actually its for UI component
    [SerializeField] RectTransform anchorpos;
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Debug.Log("sndjb");
           // eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =GetComponent<Transform>().position;
            string word = eventData.pointerDrag.GetComponent<TextMeshProUGUI>().text;
            Actions.answercheck?.Invoke(word);
        }
    }
}
