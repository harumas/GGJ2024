using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LifeView : MonoBehaviour
    {
        [SerializeField] private Image[] lifeImages;
        [SerializeField] private Sprite damageSprite;

        public void UpdateDamage(int damage)
        {
            for (int i = 0; i < damage; i++)
            {
                lifeImages[i].sprite = damageSprite;
            }
        }
    }
}