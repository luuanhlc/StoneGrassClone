/*using UnityEngine;
using UnityEngine.EventSystems;

namespace Unicorn
{
    public class BtnItemIsLand : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        public int number;

        public UiTop _data;

        private Transform visualReferenceRect;
        /// <summary>
        /// Visual reference of the building
        /// </summary>
        private GameObject visualReference;

        public Vector3 CalculateGridPosition(Vector3 eventPosition)
        {
            Vector3 gridPos = GameInput.CalculatePositionInGame(eventPosition);

            return grid.GetNearestTilePosition(gridPos);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            visualReference = Instantiate(_data.building[number]);
            visualReferenceRect = visualReference.transform;

            visualReferenceRect.position = CalculateGridPosition(eventData.position);
        }
        /// <summary>
        /// Implementation of the drag event, set the position of the item to the 
        /// nearest tile position on the grid
        /// </summary>
        /// <param name="eventData">Relevant data of the event</param>
        public void OnDrag(PointerEventData eventData)
        {
            // calculate if trying to build a building or move the catalog if move the catalog change to drag of the owner
            visualReferenceRect.position = owner.CalculateGridPosition(eventData.position);
        }

        /// <summary>
        /// Implementation of the End drag event, creates the building and reset the item
        /// </summary>
        /// <param name="eventData"></param>
        public void OnEndDrag(PointerEventData eventData)
        {
            //catalogue build building
            owner.CreateBuilding(this, eventData.position);
            Debug.Log("CreateBuild end drag");
            Reset();
        }
        /// <summary>
        /// Implementation of the pointer click event, does nothing at the moment
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Reset to the default values of the item
        /// </summary>
        public void Reset()
        {
            Destroy(visualReference);
            visualReferenceRect = null;
        }
    }
}
*/