using UnityEngine;

public class ScrollUI : MonoBehaviour
{
    public int MaxScroll;
    public int MinScroll = 1;
    public float ZoomSpeed;
    void Update()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollInput != 0)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();

            rectTransform.localScale = new Vector3(rectTransform.localScale.x - scrollInput * ZoomSpeed, rectTransform.localScale.y - scrollInput * ZoomSpeed, 1);
            
            if (rectTransform.localScale.x >= MaxScroll)
            {
                rectTransform.localScale = new Vector3(MaxScroll, MaxScroll,1);
            }
            if (rectTransform.localScale.x <= MinScroll)
            {
                rectTransform.localScale = new Vector3(MinScroll, MinScroll, 1);
            }


        }
    }
}
