using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private GameManager gameManager;

    public RectTransform menuPanel;
    public GameObject warningPanel;

    private Button menuButton;
    private InputField widthInputField;
    private InputField heightInputField;

    [SerializeField]
    private bool isUp = false;
    [SerializeField]
    private bool isCheck = true;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();

        menuButton = menuPanel.GetComponentInChildren<Button>();
        widthInputField = menuPanel.Find("InputWidth").GetComponent<InputField>();
        heightInputField = menuPanel.Find("InputHeight").GetComponent<InputField>();
    }

    private void Start()
    {
        menuButton.onClick.AddListener(() =>
        {
            if (isCheck && !isUp)
            {
                isCheck = false;
                StartCoroutine(ShowMenu());
            }
            else
            {
                if (widthInputField.text == string.Empty && heightInputField.text == string.Empty)
                {
                    WarningPopup();
                }
                else
                {
                    isCheck = false;
                    StartCoroutine(CloseMenu());
                    gameManager.DestroyCells();
                    gameManager.SetScale(int.Parse(widthInputField.text), int.Parse(heightInputField.text));
                }
            }
        });

        Button closeButton = warningPanel.GetComponentInChildren<Button>();
        closeButton.onClick.AddListener(() => warningPanel.SetActive(false));
    }

    IEnumerator ShowMenu()
    {
        float posY = 0;

        while (!isCheck)
        {
            posY = Mathf.Lerp(posY, 275, Time.deltaTime * 20f);
            menuPanel.anchoredPosition = new Vector2(0, posY);
            if (posY >= 270)
            {
                isCheck = true;
                isUp = true;
            }
            yield return null;
        }
    }

    IEnumerator CloseMenu()
    {
        float posY = 270;
        while (!isCheck)
        {
            posY = Mathf.Lerp(posY, -5, Time.deltaTime * 20f);
            menuPanel.anchoredPosition = new Vector2(0, posY);
            if (posY <= 0)
            {
                isCheck = true;
                isUp = false;
            }
            yield return null;
        }
    }

    private void WarningPopup()
    {
        warningPanel.SetActive(true);
    }
}
