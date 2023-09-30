using UnityEngine;
using UnityEngine.UI;

namespace UI.Texts
{
    /// <summary>
    /// The base class for updating menu texts
    /// </summary>
    public class MenuText : MonoBehaviour
    {
        [SerializeField]
        protected Text m_text;

        protected void Awake()
        {
            if (!m_text)
                Debug.LogError($"{gameObject.name} doesn't have a reference to its text.");
        }

        protected virtual void UpdateText()
        {
            Debug.LogError($"{gameObject.name} didn't define how to update its text.");
        }
    }
}
