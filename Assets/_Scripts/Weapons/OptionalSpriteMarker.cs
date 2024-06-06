using UnityEngine;

namespace Whiskey.Weapons
{
    public class OptionalSpriteMarker : MonoBehaviour
    {
        public SpriteRenderer SpriteRenderer => gameObject.GetComponent<SpriteRenderer>();
    }
}