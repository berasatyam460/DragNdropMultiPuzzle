
using UnityEngine;
using TMPro;

public class drag : MonoBehaviour
{
   
    private bool isDragging = false;
    private Vector2 MousePos;
    private Vector2 offset;
    private Vector2 originalPosition;

    private bool isIndropArea;
    private void Awake()
    {
        //save the initial position
        originalPosition = transform.position;
        
    }
    private void OnMouseDown()
    {
        //
        MousePos = GetMousePos();
        offset = (Vector2)transform.position - MousePos;
        isDragging = true;
    }
    private void OnMouseDrag()
    {
        if (isDragging)
        {

            transform.position = GetMousePos() + offset;
        }
    }
    private void OnMouseUp()
    {
        if (isIndropArea)
        {
            //get the text component
            string word = this.GetComponentInChildren<TextMeshProUGUI>().text;
            //invoke the action for checking the answer
            Actions.answercheck?.Invoke(word);
            isDragging = false;
        }
       
           // Snap back to original position if not in drop area
            transform.position = originalPosition;
            isDragging = false;
        
        
    }

    //for geting the mouse pos
    private Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject.tag == "DropArea")
            {
               isIndropArea = true;
               //invoke the action for scaling the answer container area 
               Actions.onDrag?.Invoke(isIndropArea);
              
            }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DropArea")
        {
            isIndropArea = false;
           //invoke the action for descaling the answer container area 
            Actions.onDrag?.Invoke(isIndropArea);
        }
    }


}
