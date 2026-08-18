// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Document.Client.Configs.Visual.Dialog.SelectFieldContents.SelectFieldContents
// Assembly: Intermech.TechCard.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 92A871D8-0A89-4621-8C49-8F2DEC6669D9
// Assembly location: D:\IPS\Client\Intermech.TechCard.Document.Client.dll

using Intermech.Diagnostics;
using Intermech.TechCard.Document.Interfaces.Configs.Attributes;
using Intermech.TechCard.Document.Interfaces.Configs.Structure.FieldContents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Intermech.TechCard.Document.Client.Configs.Visual.Dialog.SelectFieldContents;

public static class SelectFieldContents
{
  private static Dictionary<FieldContentsType, UITypeEditor> _registeredEditors = new Dictionary<FieldContentsType, UITypeEditor>();

  private static void InitEditors()
  {
    ((IEnumerable<Type>) Assembly.GetExecutingAssembly().GetTypes()).ToList<Type>().ForEach((Action<Type>) (type =>
    {
      if (!type.IsSubclassOf(typeof (UITypeEditor)))
        return;
      IEnumerable<FieldContentsTypeEditorAttribute> customAttributes = type.GetCustomAttributes<FieldContentsTypeEditorAttribute>();
      if (!customAttributes.Any<FieldContentsTypeEditorAttribute>())
        return;
      foreach (FieldContentsTypeEditorAttribute typeEditorAttribute in customAttributes)
        Intermech.TechCard.Document.Client.Configs.Visual.Dialog.SelectFieldContents.SelectFieldContents._registeredEditors[typeEditorAttribute.ContentsType] = Activator.CreateInstance(type) as UITypeEditor;
    }));
  }

  static SelectFieldContents() => Intermech.TechCard.Document.Client.Configs.Visual.Dialog.SelectFieldContents.SelectFieldContents.InitEditors();

  public static bool Select([NotNull] ref IFieldContents fieldContent)
  {
    UITypeEditor uiTypeEditor;
    if (Intermech.TechCard.Document.Client.Configs.Visual.Dialog.SelectFieldContents.SelectFieldContents._registeredEditors.TryGetValue(fieldContent.ContentsType, out uiTypeEditor) && uiTypeEditor.GetEditStyle() == UITypeEditorEditStyle.Modal)
    {
      IFieldContents fieldContents = uiTypeEditor.EditValue((IServiceProvider) null, (object) fieldContent) as IFieldContents;
      if (fieldContents == fieldContent)
        return false;
      fieldContent = fieldContents;
      return true;
    }
    if (!(TypeDescriptor.GetEditor((object) fieldContent, typeof (UITypeEditor)) is UITypeEditor editor) || editor.GetEditStyle() != UITypeEditorEditStyle.Modal)
      return false;
    IFieldContents fieldContents1 = editor.EditValue((IServiceProvider) null, (object) fieldContent) as IFieldContents;
    if (fieldContents1 == fieldContent)
      return false;
    fieldContent = fieldContents1;
    return true;
  }
}
