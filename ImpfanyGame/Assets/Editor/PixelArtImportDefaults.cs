using UnityEditor;
using UnityEngine;

// Sets pixel-art defaults (Point filter, no compression, 32 PPU, Full Rect mesh)
// on textures the first time they are imported. Later manual changes in the Inspector are kept.
public class PixelArtImportDefaults : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        if (!assetImporter.importSettingsMissing)
            return;

        var importer = (TextureImporter)assetImporter;
        importer.filterMode = FilterMode.Point;
        importer.textureCompression = TextureImporterCompression.Uncompressed;
        importer.spritePixelsPerUnit = 32;

        // Mesh type is only reachable through the settings object.
        var settings = new TextureImporterSettings();
        importer.ReadTextureSettings(settings);
        settings.spriteMeshType = SpriteMeshType.FullRect;
        importer.SetTextureSettings(settings);
    }
}
