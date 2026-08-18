
// Type: Intermech.Client.Core.FormDesigner.Controls.FormDesignerUtils
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;


namespace Intermech.Client.Core.FormDesigner.Controls;

/// <summary>
/// 
/// </summary>
public static class FormDesignerUtils
{
  /// <summary>
  /// 
  /// </summary>
  public static Dictionary<string, Image> ButtonImages { get; private set; }

  /// <summary>
  /// 
  /// </summary>
  public static Dictionary<string, string> ButtonHints { get; private set; }

  /// <summary>
  /// 
  /// </summary>
  public static Size ButtonSize { get; private set; }

  /// <summary>Конструктор.</summary>
  static FormDesignerUtils()
  {
    FormDesignerUtils.ButtonSize = new Size(22, 22);
    FormDesignerUtils.LoadControlButtonImages();
    FormDesignerUtils.LoadControlButtonHints();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nil"></param>
  private static void LoadControlButtonImages()
  {
    FormDesignerUtils.ButtonImages = new Dictionary<string, Image>(16 /*0x10*/);
    Assembly assembly = typeof (FormDesignerUtils).Assembly;
    string path = "Intermech.Client.Core.FormDesigner.Resources";
    FormDesignerUtils.AddImageToList(assembly, path, "Dots");
    FormDesignerUtils.AddImageToList(assembly, path, "DotsDisabled");
    FormDesignerUtils.AddImageToList(assembly, path, "Calc");
    FormDesignerUtils.AddImageToList(assembly, path, "CalcDisabled");
    FormDesignerUtils.AddImageToList(assembly, path, "ReCalc");
    FormDesignerUtils.AddImageToList(assembly, path, "ReCalcDisabled");
    FormDesignerUtils.AddImageToList(assembly, path, "Add");
    FormDesignerUtils.AddImageToList(assembly, path, "AddDisabled");
    FormDesignerUtils.AddImageToList(assembly, path, "Del");
    FormDesignerUtils.AddImageToList(assembly, path, "DelDisabled");
    FormDesignerUtils.AddImageToList(assembly, path, "Edit");
    FormDesignerUtils.AddImageToList(assembly, path, "EditDisabled");
    FormDesignerUtils.AddImageToList(assembly, path, "Clean");
    FormDesignerUtils.AddImageToList(assembly, path, "CleanDisabled");
    FormDesignerUtils.AddImageToList(assembly, path, "Form");
    FormDesignerUtils.AddImageToList(assembly, path, "FormDisabled");
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="assembly"></param>
  /// <param name="path"></param>
  /// <param name="name"></param>
  private static void AddImageToList(Assembly assembly, string path, string name)
  {
    Bitmap resourceData = FormDesignerUtils.GetResourceData<Bitmap>(assembly, $"{path}.{name}.png");
    if (resourceData == null)
      return;
    resourceData.MakeTransparent();
    FormDesignerUtils.ButtonImages.Add(name, (Image) resourceData);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="assembly"></param>
  /// <param name="resStr"></param>
  /// <returns></returns>
  /// <remarks>В качестве типа Т предполагаются типы принимающие в конструкторе объект Stream, такие как Icon, Bitmap</remarks>
  private static T GetResourceData<T>(Assembly assembly, string resStr) where T : IDisposable
  {
    T resourceData = default (T);
    Stream manifestResourceStream = assembly.GetManifestResourceStream(resStr);
    try
    {
      object instance = Activator.CreateInstance(typeof (T), (object) manifestResourceStream);
      if (instance != null)
        resourceData = (T) instance;
    }
    finally
    {
      if (typeof (T) == typeof (Icon))
        manifestResourceStream.Close();
    }
    return resourceData;
  }

  /// <summary>
  /// 
  /// </summary>
  private static void LoadControlButtonHints()
  {
    FormDesignerUtils.ButtonHints = new Dictionary<string, string>(8);
    FormDesignerUtils.ButtonHints.Add("Dots", LocalizationHolder.rm.GetString("FormDesigner_SetValue"));
    FormDesignerUtils.ButtonHints.Add("Calc", LocalizationHolder.rm.GetString("FormDesigner_Calc"));
    FormDesignerUtils.ButtonHints.Add("ReCalc", LocalizationHolder.rm.GetString("FormDesigner_ReCalc"));
    FormDesignerUtils.ButtonHints.Add("Add", LocalizationHolder.rm.GetString("Client.Core_1125"));
    FormDesignerUtils.ButtonHints.Add("Del", LocalizationHolder.rm.GetString("Client.Core_1127"));
    FormDesignerUtils.ButtonHints.Add("Edit", LocalizationHolder.rm.GetString("Client.Core_1126"));
    FormDesignerUtils.ButtonHints.Add("Clean", LocalizationHolder.rm.GetString("Client.Core_1128"));
    FormDesignerUtils.ButtonHints.Add("Form", LocalizationHolder.rm.GetString("AttrTextBtn.Button.ObjectCard.ToolTip"));
  }
}
