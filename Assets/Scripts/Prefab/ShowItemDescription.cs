using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShowItemDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BaseItemBehaviour Item;
    public GameObject itemDescriptionPrefab;
    private GameObject itemDescriptionInstance;

    public Vector3 offset;

    private Vector3 summ;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Создаем экземпляр префаба описания предмета
        itemDescriptionInstance = Instantiate(itemDescriptionPrefab);

        // Получаем компонент Text дочернего объекта префаба
        TMP_Text descriptionText = itemDescriptionInstance.GetComponentInChildren<TMP_Text>();

        // Устанавливаем текст описания предмета
        descriptionText.text = Item.itemDescription;

        // Устанавливаем позицию описания предмета рядом с курсором
        RectTransform descriptionRect = itemDescriptionInstance.GetComponent<RectTransform>();
        descriptionRect.position = summ;

        // Делаем описание предмета дочерним объектом канваса
        itemDescriptionInstance.transform.SetParent(GameObject.Find("PlayerInterface").transform, false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Удаляем экземпляр описания предмета при выходе курсора из объекта
        Destroy(itemDescriptionInstance);
    }

    private void Update()
    {
        summ = Input.mousePosition + offset;
    }
}
