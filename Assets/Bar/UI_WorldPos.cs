using UnityEngine;
using System.Collections;

public class UI_WorldPos : MonoBehaviour {
    public Vector2 WorldPositionOffset;
    public Transform worldPosAnchor;

    public void SetWorldPosition(Vector3 pos)
    {
        RectTransform m_rect = GetComponent<RectTransform>();
        m_rect.localPosition = WorldToCanvasPosition(transform.parent.GetComponent<RectTransform>(), Camera.main, pos, WorldPositionOffset);
    }

    public void SetWorldPosObject(Transform tr)
    {
        worldPosAnchor = tr;
        UpdatePos();
    }

    public void UpdatePos()
    {
        SetWorldPosition(worldPosAnchor.position);
    }
    private Vector2 WorldToCanvasPosition(RectTransform canvas, Camera camera, Vector3 position, Vector2 pixelOffset)
    {
        //Vector position (percentage from 0 to 1) considering camera size.
        //For example (0,0) is lower left, middle is (0.5,0.5)
        Vector2 temp = camera.WorldToViewportPoint(position);

        //Calculate position considering our percentage, using our canvas size
        //So if canvas size is (1100,500), and percentage is (0.5,0.5), current value will be (550,250)
        temp.x *= canvas.sizeDelta.x;
        temp.y *= canvas.sizeDelta.y;

        //The result is ready, but, this result is correct if canvas recttransform pivot is 0,0 - left lower corner.
        //But in reality its middle (0.5,0.5) by default, so we remove the amount considering cavnas rectransform pivot.
        //We could multiply with constant 0.5, but we will actually read the value, so if custom rect transform is passed(with custom pivot) , 
        //returned value will still be correct.

        temp.x -= canvas.sizeDelta.x * canvas.pivot.x;
        temp.y -= canvas.sizeDelta.y * canvas.pivot.y;

        temp.x += pixelOffset.x;
        temp.y += pixelOffset.y;

        return temp;
    }

}
