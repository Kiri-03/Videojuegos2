using Unity.Netcode;
using UnityEngine;

public class SpawnManager : NetworkBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    public MouseLook mouseLookScript; 

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += SpawnPlayer;
            //SpawnPlayer(NetworkManager.Singleton.LocalClientId);
        }
    }

    private void SpawnPlayer(ulong clientId)
    {
        if (NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject != null)
            return; // Ya tiene jugador, no crear otro

        Vector3 spawnPosition = new Vector3(
            Random.Range(50f, 100f),
            101f,
            Random.Range(5f, 50f)
        );
        
    
        GameObject player = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        player.GetComponent<NetworkObject>().SpawnAsPlayerObject(clientId);
        mouseLookScript.SetPlayerBody(player.transform);
    }

}
