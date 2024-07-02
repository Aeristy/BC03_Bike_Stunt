
using System;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public UI_Controller TouchZone;
    public static Transform RootTransform = null;
    public static List<PopupBase> PopupList = new List<PopupBase>();
    public static RectTransform RootRectTransform = null;
    private static Vector2 startAnchoredPosition2D = Vector2.zero;

    public int bikeid = 1;
    public UIGarage Garage;
    public UIIngame Ingame;
    public UISelectLevel SelectLevel;
    private void Awake()
    {
        Instance = this;
        foreach(PopupBase popup in transform.GetComponentsInChildren<PopupBase>())
        {
            PopupList.Add(popup);
            popup.gameObject.SetActive(false);
        }
        PopupList.Add(Ingame);
        Ingame.gameObject.SetActive(false);
    }
    public static Vector2 GetPosBy(RectTransform rectTransform, MoveDirection position)
    {
        try
        {
            Vector3 vector = Vector3.zero;
            Vector3 zero = Vector3.zero;
            _ = Vector3.zero;
            if (Instance)
            {
                if (RootTransform == null)
                {
                    RootTransform = Instance?.GetComponent<Transform>();
                }

                if (RootRectTransform == null)
                {
                    RootRectTransform = Instance?.GetComponent<RectTransform>();
                }
            }


            float num = RootRectTransform.rect.width / 2f + rectTransform.rect.width * rectTransform.pivot.x;
            float num2 = RootRectTransform.rect.height / 2f + rectTransform.rect.height * rectTransform.pivot.y;
            switch (position)
            {
                case MoveDirection.ParentPosition:
                    if (RootRectTransform == null)
                    {
                        return vector;
                    }

                    vector = new Vector2(RootRectTransform.anchoredPosition.x + zero.x, RootRectTransform.anchoredPosition.y + zero.y);
                    break;
                case MoveDirection.TopScreenEdge:
                    vector = new Vector2(zero.x + startAnchoredPosition2D.x, zero.y + num2);
                    break;
                case MoveDirection.RightScreenEdge:
                    vector = new Vector2(zero.x + num, zero.y + startAnchoredPosition2D.y);
                    break;
                case MoveDirection.BottomScreenEdge:
                    vector = new Vector2(zero.x + startAnchoredPosition2D.x, zero.y - num2);
                    break;
                case MoveDirection.LeftScreenEdge:
                    vector = new Vector2(zero.x - num, zero.y + startAnchoredPosition2D.y);
                    break;
                default:
                    Debug.LogWarning("[UIManager] This should not happen! DoMoveIn in UIAnimator went to the default setting!");
                    break;
            }

            return vector;
        }
        catch (Exception ex)
        {
            Debug.LogError("[UIManager] GetPosition: " + rectTransform.name + " " + ex.Message + "\n" + ex.StackTrace);
            return default(Vector3);
        }
    }
    


}
