using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PlayerStatictics : MonoBehaviour
{
    [SerializeField] private Canvas _playerCanvas;
    [Header("Base parameters")]
    [Range(1, 200)] public float baseHP;
    #region items parameters
    public static float bonusHP = 0;
    public float currentHp;
    public static int baseDamage=5;
    public static int bonusDamage=0;
    public static int baseAmmo=4;
    public static int bonusAmmo=0;
    public  static float baseSpeed=7;
    public static float speedBonus=0;
    #endregion
    public Weapons currentWeapon;
    [Space]
    public Image hpBar;
    public Text extraHpText;
    #region items
    [HideInInspector]
    public HashSet<BaseItemBehaviour> PlayerItems;

    [Header("Item prefabs")]
    public List<GameObject> ItemPrefab;

    [Header("Canvas show item prefabs")]
    public GameObject itemPrefab; // Prefab для создания элементов в Canvas

    private List<GameObject> itemUIElements = new List<GameObject>();
    private Vector2 itemOffset = new Vector2(150f, 0f); // Смещение для нового элемента
    #endregion
    [Space]
    public Reg reg;
    public void Awake()
    {
        foreach (var item in ItemPrefab)
        {
            BaseEnemy.AddPrefabToList(item);
        }

        PlayerItems = new HashSet<BaseItemBehaviour>();
        currentHp = baseHP + bonusHP;

        hpBar.fillAmount = 1f;
    }
    public void Start()
    {
        DestroyClone();
    }
    public void Update()
    {
        hpBar.fillAmount = currentHp / baseHP + bonusHP;
        extraHpText.text = $"{currentHp}/{baseHP + bonusHP}";
        if (!Input.GetKey(KeyCode.Tab))
        {
            return;
        }
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        hpBar.fillAmount = currentHp / baseHP + bonusHP;
        if (currentHp <= 0)
        {
            DestroyClone();
            
            SceneManager.LoadScene(0);
        }
    }
    public void UpdateItemsOutput()
    {
        // Удаляем предыдущие элементы
        foreach (var itemUI in itemUIElements)
        {
            Destroy(itemUI);
        }
        itemUIElements.Clear();

        // Начальная позиция для новых элементов
        Vector2 startPosition = Vector2.zero;

        foreach (var item in PlayerItems)
        {
            GameObject newItemUI = Instantiate(itemPrefab, _playerCanvas.transform);

            // Устанавливаем позицию нового элемента с учетом смещения
            RectTransform rectTransform = newItemUI.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = startPosition;
            }

            Image image = newItemUI.GetComponent<Image>();
            Text text = newItemUI.GetComponentInChildren<Text>();

            //Установка значений для префаба
            image.sprite = item.ItemSprite;
            System.Type itemType = item.GetType();
            BaseItemBehaviour _temp = GetComponent(itemType) as BaseItemBehaviour;//cнимаем компонент с игрока такого же типа
            text.text = _temp.Stack.ToString();

            newItemUI.GetComponent<ShowItemDescription>().Item = _temp;//для вывода описания

            // Добавляем созданный UI элемент в список для последующего удаления
            itemUIElements.Add(newItemUI);

            // Обновляем начальную позицию для следующего элемента
            startPosition += itemOffset;
        }
    }

    public void DestroyClone()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // Проверяем, содержит ли имя объекта "(Clone)" в конце
            if (obj.name.EndsWith("(Clone)"))
            {
                // Уничтожаем объект
                Destroy(obj);
            }
        }
        reg.SaveProgress();
    }
}
