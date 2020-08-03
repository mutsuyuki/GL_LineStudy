using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GL_Lines : MonoBehaviour {
    static Material lineMaterial;

    static int objectCount = 100;
    float[] val1 = new float[objectCount];
    float[] val2 = new float[objectCount];
    public float lineWidth = 100.0f;

    void Start() {
        for (int i = 0; i < objectCount; i++) {
            val1[i] = Random.Range(-100, objectCount);
            val2[i] = Random.Range(-200, objectCount);
        }
    }


    static void CreateLineMaterial() {
        if (!lineMaterial) {
            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            Shader shader = Shader.Find("Hidden/Internal-Colored");

            //Shader shader = Shader.Find ("Custom/Texture");
            lineMaterial = new Material(shader);
            lineMaterial.hideFlags = HideFlags.HideAndDontSave;

            // Turn on alpha blending
            lineMaterial.SetInt("_SrcBlend", (int) UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int) UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);

            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int) UnityEngine.Rendering.CullMode.Off);

            // Turn off depth writes
            lineMaterial.SetInt("_ZWrite", 0);
        }
    }


    public void OnRenderObject() {
        CreateLineMaterial();
        
        
        // Apply the line material
        lineMaterial.SetPass(0);
//
//        // Set transformation matrix for drawing to
//        // match our transform

        GL.PushMatrix();
        {
            GL.MultMatrix(transform.localToWorldMatrix);

            // Draw lines
            GL.Begin(GL.LINES);
            {
                GL.Color(new Color(0, 1, 0.25f, 0.8F));

                //星を描く
                for (int m = 0; m < objectCount; m++) {
                    GL.Vertex3(-0.71f + val1[m], 1.49f + val2[m], 5.0f);
                    GL.Vertex3(-1.72f + val1[m], -1.68f + val2[m], 5.0f);

                    GL.Vertex3(-1.72f + val1[m], -1.68f + val2[m], 5.0f);
                    GL.Vertex3(0.82f + val1[m], 0.15f + val2[m], 5.0f);

                    GL.Vertex3(0.82f + val1[m], 0.15f + val2[m], 5.0f);
                    GL.Vertex3(-2.3f + val1[m], 0.15f + val2[m], 5.0f);

                    GL.Vertex3(-2.3f + val1[m], 0.15f + val2[m], 5.0f);
                    GL.Vertex3(0.3f + val1[m], -1.68f + val2[m], 5.0f);

//            GL.Vertex3(0.3f + val1[m], -1.68f + val2[m], 5.0f); //ここで止めることで、次の星の間を線で結ぶ書き方にする
                }
            }
            GL.End();
        }
        GL.PopMatrix();
    }
}