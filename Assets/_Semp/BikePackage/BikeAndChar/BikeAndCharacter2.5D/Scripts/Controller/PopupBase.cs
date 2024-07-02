using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.Events;

public class PopupBase : MonoBehaviour
{
    protected UnityEvent onHideCompleted;

    protected RectTransform rectTransform;

    protected CanvasGroup canvasGroup;

    protected Action actionOnHideCompleted;

    [Header("POPUP BASE"), Space]
    public PopupAnim animationIn = PopupAnim.SlideIn;

    public Ease easeIn = Ease.OutCubic;

    public MoveDirection positionStart = MoveDirection.BottomScreenEdge;

    [Range(0f, 10f)]
    public float timeAnimationIn = 0.25f;

    [Range(0f, 10f)]
    public float timeDelayIn;

    [SerializeField]
    protected UnityEvent onShowStart;

    [SerializeField]
    protected UnityEvent onShowCompleted;

    [Space(10f)]
    public PopupAnim animationOut = PopupAnim.SlideOut;

    public Ease easeOut = Ease.InCubic;

    [SerializeField]
    public MoveDirection positionOut = MoveDirection.BottomScreenEdge;

    [Range(0f, 10f)]
    public float timeAnimationOut = 0.175f;

    [Range(0f, 10f)]
    public float timeDelayOut;

    public PlayerModel _player;

    protected virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        if ((animationIn == PopupAnim.FadeIn || animationOut == PopupAnim.FadeOut) && !TryGetComponent(out canvasGroup))
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
    }

    protected virtual void OnEnable()
    {
        if (GameManager.Instance)
        {
            _player = GameManager.Instance._player;
        }
            
    }

    protected virtual void OnDisable()
    {
    }
    protected virtual void ShowStart() { }

    protected virtual void ShowCompleted() { }

    protected virtual void HideStart() { }

    public void Show(bool isAnimation = true, Action actionOnShowStart = null, Action actionOnShowCompleted = null, Action actionOnHideCompleted = null)
    {
        if (!UIManager.PopupList.Contains(this))
        {
            UIManager.PopupList.Add(this);
        }

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
        }

        gameObject.SetActive(true);
        
        rectTransform.DOKill(complete: true);
        rectTransform.anchoredPosition = UIManager.GetPosBy(rectTransform, positionStart);

        if (!isAnimation)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            gameObject.transform.localPosition = new Vector3(0, 0, 0);
            return;
        }

        if (canvasGroup != null)
        {
            canvasGroup.DOKill(complete: true);
            DOTween.To(() => canvasGroup.alpha, delegate (float x)
            {
                canvasGroup.alpha = x;
            }, 1f, timeAnimationIn).SetUpdate(UpdateType.Normal, isIndependentUpdate: true).SetDelay(timeDelayIn)
                .SetEase(easeIn)
                .OnStart(delegate
                {
                    actionOnShowStart?.Invoke();
                    onShowStart?.Invoke();
                    ShowStart();
                })
                .OnComplete(delegate
                {
                    actionOnShowCompleted?.Invoke();
                    onShowCompleted?.Invoke();
                    ShowCompleted();
                })
                .SetTarget(canvasGroup);
        }
        else
        {
            DOTween.To(() => rectTransform.anchoredPosition, delegate (Vector2 x)
            {
                rectTransform.anchoredPosition = x;
            }, Vector2.zero, timeAnimationIn).SetDelay(timeDelayIn).SetEase(easeIn)
                .SetUpdate(UpdateType.Normal, isIndependentUpdate: true)
                .OnStart(delegate
                {
                    actionOnShowStart?.Invoke();
                    onShowStart?.Invoke();
                    ShowStart();
                })
                .OnComplete(delegate
                {
                    actionOnShowCompleted?.Invoke();
                    onShowCompleted?.Invoke();
                    ShowCompleted();
                })
                .SetTarget(rectTransform);
        }

        this.actionOnHideCompleted = actionOnHideCompleted;
    }

    public void Hide(bool animation = true, Action onCompleted = null)
    {
        if (!rectTransform) return;
        if (!animation)
        {
            gameObject.SetActive(false);
            return;
        }

        if (canvasGroup != null)
        {
            canvasGroup.DOKill(complete: true);
            DOTween.To(() => canvasGroup.alpha, delegate (float x)
            {
                canvasGroup.alpha = x;
            }, 0f, timeAnimationOut).SetUpdate(UpdateType.Normal, isIndependentUpdate: true).SetDelay(timeDelayOut)
                .SetEase(easeOut)
                .OnComplete(delegate
                {
                    actionOnHideCompleted?.Invoke();
                    actionOnHideCompleted = null;
                    onHideCompleted?.Invoke();
                    onCompleted?.Invoke();
                    gameObject.SetActive(false);
                })
                .OnStart(HideStart)
                .SetTarget(canvasGroup);
            return;
        }

        Vector2 posBy = UIManager.GetPosBy(rectTransform, positionOut);
        rectTransform.DOKill(complete: true);
        DOTween.To(() => rectTransform.anchoredPosition, delegate (Vector2 x)
        {
            rectTransform.anchoredPosition = x;
        }, posBy, timeAnimationOut).SetDelay(timeDelayOut).SetEase(easeOut)
            .SetUpdate(UpdateType.Normal, isIndependentUpdate: true)
            .OnComplete(delegate
            {
                actionOnHideCompleted?.Invoke();
                actionOnHideCompleted = null;
                onHideCompleted?.Invoke();
                onCompleted?.Invoke();
                gameObject.SetActive(false);
            })
            .OnStart(HideStart)
            .SetTarget(rectTransform);
    }
    

    
}

public enum PopupAnim
{
    None,
    SlideIn,
    SlideOut,
    FadeIn,
    FadeOut
}

public enum MoveDirection
{
    ParentPosition,
    TopScreenEdge,
    RightScreenEdge,
    BottomScreenEdge,
    LeftScreenEdge
}
