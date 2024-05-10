using UnityEngine;
using UnityEngine.UI;

public class InteractRaycaster : MonoBehaviour
{
    [SerializeField][Range(0.1f, 30f)] private float interactDistance;
    [SerializeField] private Text interactText;
    private Interactable _interactable;
    private GameObject _currentHit;
    private RaycastHit _hit;
    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out _hit, interactDistance))
        {
            _currentHit = _hit.collider.gameObject;
            _interactable = _currentHit.GetComponent<Interactable>();

            if (_interactable != null)
            {
                if (_interactable.IsLastInteracted)
                {
                    _interactable = null;
                }
                else
                {
                    _interactable.OnFocus();

                    interactText.text = _interactable.InteractText;

                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        _interactable.OnInteract();
                    }
                }
            }
            else
            {
                _interactable?.OnLoseFocus();
                interactText.text = "";
                _interactable = null;
            }
        }
        else
        {
            interactText.text = "";
            _interactable?.OnLoseFocus();
            _interactable = null;
        }
    }
}