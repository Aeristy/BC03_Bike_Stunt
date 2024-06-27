using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BC.Parking
{
    public class BC_UIController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Button button;

        [SerializeField] private GameObject ImgPress;
        [SerializeField] private GameObject ImgRelease;

        [HideInInspector] public float input;
        [SerializeField] private float sensitivity = 5;
        [SerializeField] private float gravity = 5;
        public bool pressing;
        public bool buttonInertial = true;

        void Awake()
        {

            button = GetComponent<Button>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (button && !button.interactable) return;
            if (ImgPress) ImgPress.SetActive(true);
            if (ImgRelease) ImgRelease.SetActive(false);
            pressing = true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (button && !button.interactable) return;
            if (ImgPress) ImgPress.SetActive(true);
            if (ImgRelease) ImgRelease.SetActive(false);
            pressing = true;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            if (button && !button.interactable) return;
            if (ImgPress) ImgPress.SetActive(false);
            if (ImgRelease) ImgRelease.SetActive(true);
            pressing = false;
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (button && !button.interactable) return;
            if (ImgPress) ImgPress.SetActive(false);
            if (ImgRelease) ImgRelease.SetActive(true);
            pressing = false;
        }

        void OnPress(bool isPressed)
        {

            if (isPressed)
                pressing = true;
            else
                pressing = false;

        }

        void Update()
        {
            if (button && !button.interactable)
            {
                pressing = false;
                input = 0f;
                return;
            }


            if (pressing)
            {
                if (buttonInertial)
                    input += Time.deltaTime * sensitivity;
                else input = 1f;
            }
            else
            {
                if (buttonInertial)
                    input -= Time.deltaTime * gravity;
                else input = 0f;
            }

            if (input < 0f)
                input = 0f;

            if (input > 1f)
                input = 1f;

        }

        void OnDisable()
        {

            input = 0f;
            pressing = false;

        }
    }
}