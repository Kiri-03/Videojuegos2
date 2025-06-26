
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkStarter : MonoBehaviour
{
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));

        // Mostrar botones solo si aún no se ha conectado
        if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            if (GUILayout.Button("Host (Servidor + Jugador)"))
            {
                NetworkManager.Singleton.StartHost();
            }

            if (GUILayout.Button("Client (Solo Jugador)"))
            {
                NetworkManager.Singleton.StartClient();
            }
        }
        else
        {
            // Mostrar el estado de red actual
            string modo = NetworkManager.Singleton.IsHost ? "Host" :
                          NetworkManager.Singleton.IsServer ? "Server" : "Client";

            GUILayout.Label($"Modo de red activo: {modo}");

            // Botón para cerrar la conexión
            if (GUILayout.Button("Desconectar y Reiniciar"))
            {
                NetworkManager.Singleton.Shutdown();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        GUILayout.EndArea();
    }
}

