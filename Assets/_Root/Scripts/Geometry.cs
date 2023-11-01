using System.Collections;
using SteamAudio;
using UnityEngine;

public class Geometry : MonoBehaviour
{
    public MaterialName materialName = MaterialName.Brick;

    private void Awake()
    {
        var steamAudioGeometry = gameObject.AddComponent<SteamAudioGeometry>();
        steamAudioGeometry.material = Resources.Load<SteamAudioMaterial>($"Materials/{materialName}");

        var steamAudioDynamicObject = gameObject.AddComponent<SteamAudioDynamicObject>();
        steamAudioDynamicObject.asset = ScriptableObject.CreateInstance<SerializedData>();

        SteamAudioManager.ExportDynamicObject(steamAudioDynamicObject, false);
    }
}

public enum MaterialName
{
    Brick,
    Carpet,
    Ceramic,
    Concrete,
    Glass,
    Gravel,
    Metal,
    Plaster,
    Rock,
    Wood,
    Default
}
