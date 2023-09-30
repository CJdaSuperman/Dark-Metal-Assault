using UnityEngine;
using UnityEngine.UI;

namespace UI.Buttons
{
    /// <summary>
    /// The base class for menu buttons
    /// </summary>
    public class MenuButton : MonoBehaviour
    {
        [SerializeField]
        protected Button m_button;

        protected void Awake()
        {
            if (!m_button)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its button.");
        }
    }
}
