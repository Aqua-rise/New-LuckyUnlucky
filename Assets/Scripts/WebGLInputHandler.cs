using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System.Runtime.InteropServices;

public class WebGLInputHandler : MonoBehaviour, IPointerClickHandler
{
    public string fieldId;

    // Only declared ONCE at the class level
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void showMobileKeyboard(string unityObject, string callbackMethod, string fieldId);
#endif

    public void OnPointerClick(PointerEventData eventData)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        showMobileKeyboard(gameObject.name, "OnInputReceived", fieldId);
#endif
    }

    public void OnInputReceived(string data)
    {
        var parts = data.Split('|');
        if (parts.Length == 2 && parts[0] == fieldId)
        {
            GetComponent<TMP_InputField>().text = parts[1];
        }
    }
}