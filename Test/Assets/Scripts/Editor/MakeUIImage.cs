using UnityEngine;
using UnityEditor;

class MakeUIImage : AssetPostprocessor
{
    void OnPreprocessTexture()
    {
        // Automatically convert any texture file (put in folders when Editor run) with "UI_Image"
        // in its file name  into an uncompressed unchanged GUI Image.
        // All setting taked from example from Lesson

        // If file contain name corresponding foler
        if (assetPath.Contains("UI_Images"))
        {
            //Debug.Log("Importing new GUI Image!");        Replaced by OnPostprocessTexture Debug.Log()
            TextureImporter myTextureImporter = (TextureImporter)assetImporter;
            myTextureImporter.textureType = TextureImporterType.GUI;
            myTextureImporter.textureCompression = TextureImporterCompression.Uncompressed;

            // Some parameters for TextureImporte set through TextureImporterPlatformSettings now
            TextureImporterPlatformSettings platformSettings = new TextureImporterPlatformSettings
            {
                // Set special setting for PC, Mac & Unix
                name = "Standalone",
                overridden = true,
                maxTextureSize = 2048,
                format = TextureImporterFormat.RGBA32
            };
            myTextureImporter.SetPlatformTextureSettings(platformSettings) ;
            //myTextureImporter.textureFormat = TextureImporterFormat.ARGB32;       obsolete
            //myTextureImporter.maxTextureSize = 2048;      move to platformSettings
            myTextureImporter.convertToNormalmap = false;
            myTextureImporter.alphaSource = TextureImporterAlphaSource.FromInput;
            //myTextureImporter.generateCubemap = TextureImporterGenerateCubemap.None;      obsolete
            myTextureImporter.textureShape = TextureImporterShape.Texture2D;
            myTextureImporter.npotScale = TextureImporterNPOTScale.None;
            myTextureImporter.isReadable = true;
            myTextureImporter.mipmapEnabled = false;
            myTextureImporter.mipmapFilter = TextureImporterMipFilter.BoxFilter;
            myTextureImporter.fadeout = false;
            myTextureImporter.convertToNormalmap = false;
            //myTextureImporter.lightmap = false;       obsolete
            //myTextureImporter.ClearPlatformTextureSettings("Web");        doesn't touched in this scrpit
            
            // Return to default setting for PC, Mac & Unix
            myTextureImporter.ClearPlatformTextureSettings("Standalone");
            //myTextureImporter.ClearPlatformTextureSettings("iPhone");     doesn't touched in this scrpit
        }
    }

    void OnPostprocessTexture(Texture2D texture)
    {
        // Called after finishin Import
        Debug.Log($"MakeUIImage: GUI Image [{assetPath}] imported");
    }
}
