using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Kamgam.BikeAndCharacter25D
{
    public static class MaterialShaderFixer
    {
        public enum RenderPiplelineType
        {
            URP, HDRP, Standard
        }

        static Dictionary<string, Color> Materials = new Dictionary<string, Color> {
            { "Assets/BikeAndCharacter2.5D/Materials/Bike3DBody.mat", Color.white },
            { "Assets/BikeAndCharacter2.5D/Materials/Bike3DParts.mat", Color.white },
            { "Assets/BikeAndCharacter2.5D/Materials/Bike3DPlate.mat", Color.white },
            { "Assets/BikeAndCharacter2.5D/Materials/Bike3DWheel.mat", Color.white },
            { "Assets/BikeAndCharacter2.5D/Materials/Character3D.mat", Color.white },
            { "Assets/BikeAndCharacter2.5D/Materials/DirtParticles.mat", Color.white },
            { "Assets/BikeAndCharacter2.5D/Materials/ExhaustParticles.mat", Color.white },
            { "Assets/BikeAndCharacter2.5D/Materials/Helmet3D.mat", Color.white },

            { "Assets/BikeAndCharacter2.5D/Examples/Materials/Sky.mat", Color.white },
            { "Assets/BikeAndCharacter2.5D/Examples/Materials/TreeLeavesGreen.mat", new Color(0.15f, 0.5f, 0.15f) },
            { "Assets/BikeAndCharacter2.5D/Examples/Materials/TreeLeavesYellow.mat", new Color(0.9f, 0.8f, 0.0f) },
            { "Assets/BikeAndCharacter2.5D/Examples/Materials/TreeTrunk.mat", new Color(0.55f, 0.25f, 0.0f) },
            { "Assets/BikeAndCharacter2.5D/Examples/Materials/BridgePartTextured.mat", Color.white },
            { "Assets/BikeAndCharacter2.5D/Materials/BridgeEdgePart.mat", Color.white },
            { "Assets/BikeAndCharacter2.5D/Materials/BridgePart.mat", Color.white },
            { "Assets/BikeAndCharacter2.5D/Materials/BridgeRope.mat", new Color(0.6f, 0.6f, 0.6f) }
        };

        public static void FixMaterials(RenderPiplelineType createdForRenderPipleline)
        {
            var currentRenderPipline = GetCurrentRenderPiplelineType();

            if (currentRenderPipline != createdForRenderPipleline)
            {
                EditorUtility.DisplayDialog(
                    "Render pipeline mismatch detected.",
                    "The materials in this asset have been created with the Universal Render Pipeline (URP). You are using a different renderer, thus some of the materials may be broken (especially particle materials).\n\nThe tool will attempt to auto update materials now. In case some are still broken afterwards please fix those manually.",
                    "Understood"
                    );

                Shader shader = GetDefaultShader();
                foreach (var kv in Materials)
                {
                    Material material = AssetDatabase.LoadAssetAtPath<Material>(kv.Key);
                    if (material != null)
                    {
                        material.shader = shader;
                        material.color = kv.Value;
                        EditorUtility.SetDirty(material);
                    }
                }
            }

            AssetDatabase.SaveAssets();
        }

        public static RenderPiplelineType GetCurrentRenderPiplelineType()
        {
            // Assume URP as default
            var renderPipeline = RenderPiplelineType.URP;

            // check if Standard or HDRP
            if (getUsedRenderPipeline() == null)
                renderPipeline = RenderPiplelineType.Standard; // Standard
            else if (!getUsedRenderPipeline().GetType().Name.Contains("Universal"))
                renderPipeline = RenderPiplelineType.HDRP; // HDRP

            return renderPipeline;
        }

        public static Shader GetDefaultShader()
        {
            if (getUsedRenderPipeline() == null)
                return Shader.Find("Standard");
            else
                return getUsedRenderPipeline().defaultShader;
        }

        /// <summary>
        /// Returns the current pipline. Returns NULL if it's the standard render pipeline.
        /// </summary>
        /// <returns></returns>
        static UnityEngine.Rendering.RenderPipelineAsset getUsedRenderPipeline()
        {
            if (UnityEngine.Rendering.GraphicsSettings.currentRenderPipeline != null)
                return UnityEngine.Rendering.GraphicsSettings.currentRenderPipeline;
            else
                return UnityEngine.Rendering.GraphicsSettings.defaultRenderPipeline;
        }

    }
}