using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ShowItemDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BaseItemBehaviour Item;
    public GameObject ItemDescriptionPrefab;
    private GameObject _itemDescriptionInstance;
    public Vector3 Offset;
    private Vector3 _totalVec;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Создаем экземпляр префаба описания предмета
        _itemDescriptionInstance = Instantiate(ItemDescriptionPrefab);

        // Получаем компонент Text дочернего объекта префаба
        TMP_Text descriptionText = _itemDescriptionInstance.GetComponentInChildren<TMP_Text>();

        // Устанавливаем текст описания предмета
        descriptionText.text = Item.ItemDescription;

        // Устанавливаем позицию описания предмета рядом с курсором
        RectTransform descriptionRect = _itemDescriptionInstance.GetComponent<RectTransform>();
        descriptionRect.position = _totalVec;

        // Делаем описание предмета дочерним объектом канваса
        _itemDescriptionInstance.transform.SetParent(GameObject.Find("PlayerInterface").transform, false);
    }

    public void OnPointerStay(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Удаляем экземпляр описания предмета при выходе курсора из объекта
        Destroy(_itemDescriptionInstance);
    }

    private void Update()
    {
        _totalVec = Input.mousePosition + Offset;
    }
}
