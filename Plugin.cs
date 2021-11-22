// Decompiled with JetBrains decompiler
// Type: PmxExpoter
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using BepInEx;
using System.Threading;
using UnityEngine;

namespace PMXRedux2
{
    [BepInPlugin("com.bepis.bepinex.pmxexporter", "PmxExporter", "3.0")]
    public class PmxExpoter : BaseUnityPlugin
    {
        private PmxBuilder builder;
        private string msg = "";
        private bool destroyed;

        private void OnGUI()
        {
            if (!GUI.Button(new Rect(0.0f, 0.0f, 50f, 30f), "Export") || this.builder != null)
                return;
            this.builder = new PmxBuilder();
            this.msg += this.builder.BuildStart();
            Thread.Sleep(1000);
            this.builder = (PmxBuilder)null;
        }

        private void Update()
        {
            if (this.destroyed)
                return;
            Animator[] objectsOfType = Object.FindObjectsOfType<Animator>();
            if ((uint)objectsOfType.Length > 0U)
                this.destroyed = true;
            foreach (Object @object in objectsOfType)
                Object.Destroy(@object);
        }
    }

}
