using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRelayJoinCode : MonoBehaviour
{
    [SerializeField] private string _relayCode = "RELAYCODE";
    [SerializeField] private TextMeshProUGUI _relayCodeTxt;
    [SerializeField] private Button _relayCopyCodeButton;


    private void Awake()
    {
        if (_relayCopyCodeButton != null)
        {
            _relayCopyCodeButton.onClick.AddListener(CopyCodeToClipboard);
        }
    }

    private void Start()
    {
        _relayCode = RelayManager.Instance.RelayJoinCode; //NOTE: may need to update this to be based on an update event call
        _relayCodeTxt.text = _relayCode;
    }

    private void CopyCodeToClipboard()
    {
        // Sets the 'System CopyBuffer clipboard' to the join code
        GUIUtility.systemCopyBuffer = _relayCode;
        Debug.Log($"Relay Join code '{_relayCode}' copied to clipboard!");
    }
}
