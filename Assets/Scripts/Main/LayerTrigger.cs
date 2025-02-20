using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    //when object exit the trigger, put it to the assigned layer and sorting layers
    //used in the stair objects for player to travel between layers
    public class LayerTrigger : MonoBehaviour
    {
        public string layer;
        public string sortingLayer;

        private void OnTriggerExit2D(Collider2D other)
        {
            other.gameObject.layer = LayerMask.NameToLayer(layer);

            // Check and update SpriteRenderers
            SpriteRenderer[] srs = other.gameObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (SpriteRenderer sr in srs)
            {
                sr.sortingLayerName = sortingLayer;
            }

            // Check and update SortingGroups
            UnityEngine.Rendering.SortingGroup[] sortingGroups = other.gameObject.GetComponentsInChildren<UnityEngine.Rendering.SortingGroup>();
            foreach (UnityEngine.Rendering.SortingGroup sg in sortingGroups)
            {
                sg.sortingLayerName = sortingLayer;
            }
        }
    }
}