using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    [SerializeField] Color baseColor;
    [SerializeField] Color highlightedColor;

    private void Start()
    {
        
    }
    private void OnEnable()
    {
        Actions.onDrag += HighlightOnDrag;
    }
    private void OnDisable()
    {
        Actions.onDrag -= HighlightOnDrag;
    }
    void HighlightOnDrag(bool dropref)
    {
       
        if (dropref)
        {
           transform.localScale = new Vector3(1.2f, 1.2f, 1.2f); // Scale up
            GetComponent<SpriteRenderer>().color = highlightedColor;
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f); // Revert to original size
            GetComponent<SpriteRenderer>().color = baseColor;
        }
       
      
    }
}
