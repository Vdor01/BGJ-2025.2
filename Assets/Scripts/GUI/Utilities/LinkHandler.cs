using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BGJ_2025_2.GUI
{
    /// <summary>
    /// A <see cref="GUIManager">GUI</see>-n található labelekbõl nyeri ki a kattintás helyén <c>&lt;link=URL&gt;</c> formátumban <br/>
    /// megadott URL-t, amit aztán a játékos alapértelmezett böngészõjében nyit meg.
    /// </summary>
    public class LinkHandler : MonoBehaviour, IPointerClickHandler
    {
        // Fields
        [SerializeField] private TextMeshProUGUI _label;


        // Methods

        /// <summary>
        /// A megadott labelre kattintáskor megnézi, hogy a kattintás helyén van-e kinyerhetõ URL és ha van, megnyitja.
        /// </summary>
        /// <param name="pointerEventData">A kattintással kapcsolatos információk.</param>
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