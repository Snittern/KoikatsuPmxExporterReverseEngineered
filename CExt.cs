﻿// Decompiled with JetBrains decompiler
// Type: PmxLib.CExt
// Assembly: ClassLibrary1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CCD5B2CC-FDBA-4BE9-8BB4-450852B11426
// Assembly location: C:\illusion\Koikatu\BepInEx\plugins\PmxExport\PmxExport.dll

using System.Collections.Generic;
using System.IO;

namespace PmxLib
{
  internal static class CExt
  {
    public const string Ext_X = ".x";
    public const string Ext_Pmd = ".pmd";
    public const string Ext_Pmx = ".pmx";
    public const string Ext_Vmd = ".vmd";
    public const string Ext_Vpd = ".vpd";
    public const string Ext_CSV = ".csv";
    public const string Ext_PSK = ".psk";
    public const string Ext_FX = ".fx";
    public const string Ext_CFX = ".cfx";
    public const string Ext_XML = ".xml";
    public const string Ext_TXT = ".txt";
    public const string DlgFilter_All = "すべてのファイル (*.*)|*.*";
    public const string DlgFilter_Pmd = "PMDファイル (*.pmd)|*.pmd|すべてのファイル (*.*)|*.*";
    public const string DlgFilter_Vmd = "MMDモーションファイル (*.vmd)|*.vmd|すべてのファイル (*.*)|*.*";
    public const string DlgFilter_Txt = "txtファイル (*.txt)|*.txt|すべてのファイル (*.*)|*.*";
    public const string DlgFilter_CSV = "csvファイル (*.csv)|*.csv|すべてのファイル (*.*)|*.*";
    public const string DlgFilter_X = "Xファイル (*.x)|*.x|すべてのファイル (*.*)|*.*";
    public const string DlgFilter_Pmx = "拡張モデルファイル (*.pmx)|*.pmx|すべてのファイル (*.*)|*.*";
    public const string DlgFilter_PmxPmd = "PMX／PMD (*.pmx;*.pmd)|*.pmx;*.pmd|すべてのファイル (*.*)|*.*";
    public const string DlgFilter_PmxPmdX = "PMX／PMD／X (*.pmx;*.pmd;*.x)|*.pmx;*.pmd;*.x|すべてのファイル (*.*)|*.*";
    public const string DlgFilter_PmxPmdXPsk = "PMX／PMD／X／PSK (*.pmx;*.pmd;*.x;*.psk)|*.pmx;*.pmd;*.x;*.psk|すべてのファイル (*.*)|*.*";
    public const string DlgFilter0_PmxPmdX = "PMX／PMD／X (*.pmx;*.pmd;*.x)|*.pmx;*.pmd;*.x";
    public const string DlgFilter0_PmxPmdXPsk = "PMX／PMD／X／PSK (*.pmx;*.pmd;*.x;*.psk)|*.pmx;*.pmd;*.x;*.psk";
    public const string DlgFilter_Vpd = "MMDポーズファイル (*.vpd)|*.vpd|すべてのファイル (*.*)|*.*";
    public const string DlgFilter_Psk = "PSKファイル (*.psk)|*.psk|すべてのファイル (*.*)|*.*";
    public const string DlgFilter_Fx = "FXファイル (*.fx)|*.fx|すべてのファイル (*.*)|*.*";
    public const string DlgFilter_Xml = "XMLファイル (*.xml)|*.xml|すべてのファイル (*.*)|*.*";
    private static Dictionary<string, int> m_scriptExtTable;

    public static Dictionary<string, int> ScriptExtTable
    {
      get
      {
        if (CExt.m_scriptExtTable == null)
        {
          CExt.m_scriptExtTable = new Dictionary<string, int>();
          CExt.m_scriptExtTable.Add(".exe", 0);
          CExt.m_scriptExtTable.Add(".bat", 1);
          CExt.m_scriptExtTable.Add(".asp", 2);
          CExt.m_scriptExtTable.Add(".js", 3);
          CExt.m_scriptExtTable.Add(".vbs", 4);
          CExt.m_scriptExtTable.Add(".wsf", 5);
          CExt.m_scriptExtTable.Add(".wsh", 6);
          CExt.m_scriptExtTable.Add(".cmd", 7);
          CExt.m_scriptExtTable.Add(".url", 8);
        }
        return CExt.m_scriptExtTable;
      }
    }

    public static string GetDlgFilter(string ext) => "ファイル (*." + ext.ToLower() + ")|*." + ext.ToLower() + "|すべてのファイル (*.*)|*.*";

    public static bool IsExtPath(string path, string ext) => !string.IsNullOrEmpty(path) && Path.GetExtension(path).ToLower() == ext;

    public static bool IsPmxPath(string path) => CExt.IsExtPath(path, ".pmx");

    public static bool IsPmdPath(string path) => CExt.IsExtPath(path, ".pmd");

    public static bool IsXPath(string path) => CExt.IsExtPath(path, ".x");

    public static bool IsCSVPath(string path) => CExt.IsExtPath(path, ".csv");
  }
}
