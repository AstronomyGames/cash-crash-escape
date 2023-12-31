using UnityEngine.UI.Procedural;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace UnityEditor.UI
{
    /// <summary>
    /// This class adds a Menu Item "GameObject/UI/Procedural Image"
    /// Bahviour of this command is the same as with regular Images
    /// </summary>
    public class ProceduralImageEditorTool
    {
        [MenuItem("GameObject/UI/Image Modifier")]
        public static void AddProceduralImage()
        {
            GameObject o = new GameObject();
            o.AddComponent<ImageModifier>();
            o.layer = LayerMask.NameToLayer("UI");
            o.name = "Image Modifier";
            if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponentInParent<Canvas>() != null)
            {
                o.transform.SetParent(Selection.activeGameObject.transform, false);
                Selection.activeGameObject = o;
            }
            else
            {
                if (GameObject.FindObjectOfType<Canvas>() == null)
                {
                    EditorApplication.ExecuteMenuItem("GameObject/UI/Canvas");
                }
                Canvas c = GameObject.FindObjectOfType<Canvas>();

                //Set Texcoord shader channels for canvas
                c.additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord1 | AdditionalCanvasShaderChannels.TexCoord2 | AdditionalCanvasShaderChannels.TexCoord3;

                o.transform.SetParent(c.transform, false);
                Selection.activeGameObject = o;
            }
        }

        /// <summary>
        /// Replaces an Image Component with a Procedural Image Component.
        /// </summary>
        [MenuItem("CONTEXT/Image/Replace with Procedural Image")]
        public static void ReplaceWithProceduralImage(MenuCommand command)
        {
            Image image = (Image)command.context;
            GameObject obj = image.gameObject;
            GameObject.DestroyImmediate(image);
            obj.AddComponent<ImageModifier>();
        }
    }
}
