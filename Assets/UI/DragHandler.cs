using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static GameObject itemBeingDragged;
	Vector3 startPos;
	Transform startParent;

    #region IBeginDragHandler implementation

	public void OnBeginDrag(PointerEventData eventdata) {
		itemBeingDragged = gameObject;
		startPos = transform.position;
		startParent = transform.parent;
		GetComponent<CanvasGroup>().blocksRaycasts = false;
	}
    #endregion

    #region IDragHandler implementation

	public void OnDrag(PointerEventData eventdata) {
		transform.position = Input.mousePosition;
	}
    #endregion

    #region IEndDragHandler implementation

    public void OnEndDrag(PointerEventData eventdata) {
        itemBeingDragged = null;
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		if(transform.parent == startParent) {
            transform.position = startPos;
		}
        
    }
    #endregion

}
