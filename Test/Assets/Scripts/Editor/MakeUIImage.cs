using UnityEngine;
using UnityEditor;

class MakeUIImage : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        // Automatically convert any texture file with "GUIImages" in its file name into an uncompressed
        //      unchanged GUI Image.
        if (assetPath.Contains("UI_Images") || assetPath.Contains("SpriteFonts") || assetPath.Contains("SpriteAtlases"))
        {
            // Replaced by OnPostprocessTexture Debug.Log()
            //Debug.Log("Importing new GUI Image!");
            TextureImporter myTextureImporter = (TextureImporter)assetImporter;
            myTextureImporter.textureType = TextureImporterType.GUI;
            
            myTextureImporter.textureCompression = TextureImporterCompression.Uncompressed;
            TextureImporterPlatformSettings platformSettings = new TextureImporterPlatformSettings
            {
                name = "Standalone",
                overridden = true,
                maxTextureSize = 2048,
                format = TextureImporterFormat.RGBA32
            };
            myTextureImporter.SetPlatformTextureSettings(platformSettings) ;
            //myTextureImporter.textureFormat = TextureImporterFormat.ARGB32;
            //myTextureImporter.maxTextureSize = 2048;
            myTextureImporter.convertToNormalmap = false;
            myTextureImporter.alphaSource = TextureImporterAlphaSource.FromInput;
            //myTextureImporter.generateCubemap = TextureImporterGenerateCubemap.None;
            myTextureImporter.textureShape = TextureImporterShape.Texture2D;
            myTextureImporter.npotScale = TextureImporterNPOTScale.None;
            myTextureImporter.isReadable = true;
            myTextureImporter.mipmapEnabled = false;
            myTextureImporter.mipmapFilter = TextureImporterMipFilter.BoxFilter;
            myTextureImporter.fadeout = false;
            myTextureImporter.convertToNormalmap = false;
            //myTextureImporter.lightmap = false;
            //myTextureImporter.ClearPlatformTextureSettings("Web");
            myTextureImporter.ClearPlatformTextureSettings("Standalone");
            //myTextureImporter.ClearPlatformTextureSettings("iPhone");
        }
    }

    void OnPostprocessTexture(Texture2D texture)
    {
        Debug.Log($"GUI Image [{assetPath}] imported");
    }
}
