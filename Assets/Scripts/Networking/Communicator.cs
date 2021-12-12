using UnityEngine;
using Mirror;

public class Communicator : NetworkBehaviour
{
    // Active local instance
    public static Communicator active;
    public string permission = "FALSE";

    // Nameplate object
    public GameObject nameplate;
    public bool enableLocalNameplate;

    // Connect to syncer
    public void Start()
    {
        if (hasAuthority)
        {
            permission = "TRUE";
            active = this;
            nameplate.SetActive(enableLocalNameplate);
        }
    }

    // Disconnect method
    public void SyncClientDisconnect()
    {
        if (hasAuthority)
            Events.active.ClientDisconnect();
    }

    // Internal call
    public void SyncBuildingDestroyed(int runtimeID)
    {
        // Sync building destroyed with all clients
        if (hasAuthority)
            CmdSyncBuildingDestroyed(runtimeID);
    }

    // Update collector grab for all players
    [Command]
    public void CmdSyncBuildingDestroyed(int runtimeID)
    {
        // Sync building destroyed with all clients
        if (!Server.entities.ContainsKey(runtimeID))
            Debug.Log("[SERVER] Received a destroy request for runtime ID " + runtimeID + ", but that ID does " +
                "not exist on the server. Will still attempt to remove ID on all clients...");

        // Attempt to sync destroy with other clients that contain the ID
        RpcSyncBuildingDestroyed(runtimeID);
    }

    // Rpc metadata on all clients
    [ClientRpc]
    public void RpcSyncBuildingDestroyed(int runtimeID)
    {
        // Sync building destroyed with all clients
        if (Server.entities.ContainsKey(runtimeID))
        {
            if (Server.entities[runtimeID] != null)
                Server.entities[runtimeID].DestroyEntity();
            else Server.entities.Remove(runtimeID);
        }
        else Debug.Log("[SERVER] Desync detected. Client received a destroy request for ID " + runtimeID + ", " +
            "but this client does not have a reference to that ID. Recommend reconnecting to avoid further issues!");
    }

    // Internal call
    public void SyncBuildPriority(int prio)
    {
        // Send build priority request to server
        if (hasAuthority)
            CmdSyncBuildPriority(prio);
    }

    // Update collector grab for all players
    [Command]
    public void CmdSyncBuildPriority(int prio)
    {
        // Sync build priority with clients
        RpcSyncBuildPriority(prio);
    }

    // Rpc metadata on all clients
    [ClientRpc]
    public void RpcSyncBuildPriority(int prio)
    {
        // Attempt to switch drone priority
        DroneManager.active.SyncPriority(prio);
    }

    // Internal call
    public void SyncGuardianBattle()
    {
        if (hasAuthority)
            CmdSyncGuardianBattle();
    }

    // Update collector grab for all players
    [Command]
    public void CmdSyncGuardianBattle()
    {
        RpcSyncGuardianBattle();
    }

    // Rpc metadata on all clients
    [ClientRpc]
    public void RpcSyncGuardianBattle()
    {
        // Attempt to start battle for all clients
        GuardianHandler.active.StartGuardianBattle();
    }

    // Internal call
    public void SyncMetadata(int id, int data)
    {
        if (hasAuthority)
            CmdSyncMetadata(id, data);
    }
    
    // Update collector grab for all players
    [Command]
    public void CmdSyncMetadata(int id, int data)
    {
        RpcSyncMetadata(id, data);
    }

    // Rpc metadata on all clients
    [ClientRpc]
    public void RpcSyncMetadata(int entity_id, int metadata)
    {
        // Attempt to destroy an active entity. If no entity found, attempt override on position
        if (Server.entities.ContainsKey(entity_id))
            Server.entities[entity_id].ApplyMetadata(metadata);
        else Debug.Log("[SERVER] Desync detected. An entity with ID " + entity_id + " received a " +
            "metadata change of " + metadata + " from the server, but the entities runtime ID does not exist on this client!");
    }

    // Internal call
    public void SyncCollector(int id, int amount)
    {
        if (hasAuthority)
            CmdCollector(id, amount);
    }

    // Update collector grab for all players
    [Command]
    public void CmdCollector(int id, int amount)
    {
        RpcCollect(id, amount);
    }

    // Rpc collector on all clients
    [ClientRpc]
    public void RpcCollect(int id, int amount)
    {
        // Reset the tile 
        if (Server.entities.ContainsKey(id))
            Server.entities[id].SyncEntity(amount);
        else Debug.Log("[SERVER] Client received a collector with a runtime ID that doesn't exist. " +
            "This will cause major issues with desyncing!");
    }
}
