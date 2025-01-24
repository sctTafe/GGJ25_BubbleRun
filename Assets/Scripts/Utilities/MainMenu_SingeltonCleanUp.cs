using Unity.Netcode;
using UnityEngine;

/// <summary>
/// Removes the games singletons on return to Main Menu
/// </summary>
public class MainMenu_SingeltonCleanUp : MonoBehaviour
{
    private void Awake()
    {
        if (NetworkManager.Singleton != null)
        {
            Destroy(NetworkManager.Singleton.gameObject);
        }
    }
}
