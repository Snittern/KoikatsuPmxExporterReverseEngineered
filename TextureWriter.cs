// Decompiled with JetBrains decompiler
// Type: PmxLib.TextureWriter
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System;
using System.IO;
using UnityEngine;

namespace PmxLib
{
  public class TextureWriter
  {
    public static Texture2D Render2Texture2D(RenderTexture renderTexture)
    {
      int width = ((Texture) renderTexture).width;
      int height = ((Texture) renderTexture).height;
      Texture2D texture2D = new Texture2D(width, height, (TextureFormat) 5, false);
      RenderTexture.active = renderTexture;
      texture2D.ReadPixels(new Rect(0.0f, 0.0f, (float) width, (float) height), 0, 0);
      texture2D.Apply();
      return texture2D;
    }

    public static void WriteTexture2D(string path, Texture tex)
    {
      Texture2D texture2D1 = !(tex is RenderTexture) ? tex as Texture2D : TextureWriter.Render2Texture2D(tex as RenderTexture);
      Texture2D texture2D2 = new Texture2D(((Texture) texture2D1).width, ((Texture) texture2D1).height, (TextureFormat) 5, false);
      try
      {
        Color[] pixels = texture2D1.GetPixels();
        texture2D2.SetPixels(pixels);
        byte[] png = texture2D2.EncodeToPNG();
        File.WriteAllBytes(path, png);
      }
      catch (Exception ex)
      {
        ((Texture) texture2D1).filterMode = (FilterMode) 0;
        RenderTexture temporary = RenderTexture.GetTemporary(((Texture) texture2D1).width, ((Texture) texture2D1).height);
        ((Texture) temporary).filterMode = (FilterMode) 0;
        RenderTexture.active = temporary;
        Graphics.Blit((Texture) texture2D1, temporary);
        Texture2D texture2D3 = new Texture2D(((Texture) texture2D1).width, ((Texture) texture2D1).height);
        texture2D3.ReadPixels(new Rect(0.0f, 0.0f, (float) ((Texture) texture2D1).width, (float) ((Texture) texture2D1).height), 0, 0);
        texture2D3.Apply();
        RenderTexture.active = (RenderTexture) null;
        Color[] pixels = texture2D3.GetPixels();
        texture2D2.SetPixels(pixels);
        byte[] png = texture2D2.EncodeToPNG();
        File.WriteAllBytes(path, png);
      }
    }
  }
}
