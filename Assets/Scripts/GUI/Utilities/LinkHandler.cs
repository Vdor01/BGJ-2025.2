using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BGJ_2025_2.GUI
{
    /// <summary>
    /// A <see cref="GUIManager">GUI</see>-n tal�lhat� labelekb�l nyeri ki a kattint�s hely�n <c>&lt;link=URL&gt;</c> form�tumban <br/>
    /// megadott URL-t, amit azt�n a j�t�kos alap�rtelmezett b�ng�sz�j�ben nyit meg.
    /// </summary>
    public class LinkHandler : MonoBehaviour, IPointerClickHandler
    {
        // Fields
        [SerializeField] private TextMeshProUGUI _label;


        // Methods

        /// <summary>
        /// A megadott labelre kattint�skor megn�zi, hogy a kattint�s hely�n van-e kinyerhet� URL �s ha van, megnyitja.
        /// </summary>
        /// <param name="pointerEventData">A kattint�ssal kapcsolatos inform�ci�k.</param>
        public void OnPointerClick(PointerEventData pointerEventData)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(_label, pointerEventData.position, null);

            if (linkIndex != -1)
            {
                TMP_LinkInfo linkInfo = _label.textInfo.linkInfo[linkIndex];
                Application.OpenURL(linkInfo.GetLinkID());
            }
        }
    }
}