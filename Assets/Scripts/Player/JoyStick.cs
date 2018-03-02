using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/* JoyStick Stuff */
public class JoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    /* Pad of JoyStick */
    [SerializeField]
    private Image m_JoyStick_Back;
    /* JoyStick Itself */
    [SerializeField]
    private Image m_JoyStick;

    /* This is used in Player to move Player */
    private Vector2 InputDirection;
    public Vector2 Direction
    {
        set
        {
            InputDirection = value;
        }
        get
        {
            return InputDirection;
        }
    }

    // Use this for initialization
    private void Start ()
    {
        InputDirection = Vector2.zero;
	}

    /* When Drag, JoyStick will follow drag */
    public void OnDrag(PointerEventData eventDataDrag)
    {
        Vector2 pos = Vector2.zero;

        /* If the Tap in within the JoyStick Back size */
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle
            (m_JoyStick_Back.rectTransform, 
             eventDataDrag.position, // point on screen
             eventDataDrag.pressEventCamera, // pov camera 
             out pos)) // out pos is like getting info and storing in pos
        {
            /* Get Position of the Tap on the Screen on the JoyStick_Back */
            pos.x = (pos.x / m_JoyStick_Back.rectTransform.sizeDelta.x);
            pos.y = (pos.y / m_JoyStick_Back.rectTransform.sizeDelta.y);

            /* Multiply in Direction of where Pivot point is */
            float pivotX, pivotY;
            if (m_JoyStick_Back.rectTransform.pivot.x == 1) // right 
                pivotX = pos.x * 2.0f + 1.0f;
            else
                pivotX = pos.x * 2.0f - 1.0f; // left

            if (m_JoyStick_Back.rectTransform.pivot.y == 1) // up 
                pivotY = pos.y * 2.0f + 1.0f;
            else
                pivotY = pos.y * 2.0f - 1.0f; // down

            /* Store values into a Vector2 */
            InputDirection = new Vector2(pivotX, pivotY);

            /* Normalise the Vector to clamp */
            InputDirection = (InputDirection.magnitude > 1) ? 
                InputDirection.normalized : InputDirection;

            /* Move the JoyStick on top of the JoyStick Back ( not more then 1/3 of image size )*/
            m_JoyStick.rectTransform.anchoredPosition =
                new Vector2(InputDirection.x * (m_JoyStick_Back.rectTransform.sizeDelta.x / 3),
                InputDirection.y * (m_JoyStick_Back.rectTransform.sizeDelta.y / 3));
        }

    }

    /* When not Touching, reset the values */
    public void OnPointerUp(PointerEventData eventData)
    {
        ///* Reset the Direction used to move Player */
        InputDirection = Vector2.zero;
        /* Reset the location of the JoyStick */
        m_JoyStick.rectTransform.anchoredPosition = Vector2.zero;
    }

    /* When Touching Down, Drag the JOyStick to that Position */
    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }
}
