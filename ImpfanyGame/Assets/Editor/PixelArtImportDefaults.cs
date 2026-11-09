using UnityEditor;
using UnityEngine;

// Sets pixel-art defaults (Point filter, no compression) on textures the first
// time they are imported. Later manual changes in the Inspector are kept.
public class PixelArtImportDefaults : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        if (!assetImporter.importSettingsMissing)
            return;

        var importer = (TextureImporter)assetImporter;
        importer.filterMode = FilterMode.Point;
        importer.textureCompression = TextureImporterCompression.Uncompressed;
    }
}
