// Decompiled with JetBrains decompiler
// Type: Intermech.FormDesigner.Navigator.FormDesignerEditorViewProvider
// Assembly: Intermech.FormDesigner, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D8D2DB66-E218-49D4-A1F2-016208B123B1
// Assembly location: D:\IPS\Client\Intermech.FormDesigner.dll
// XML documentation location: D:\IPS\Client\Intermech.FormDesigner.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.FormDesigner.Navigator;

/// <summary>
/// 
/// </summary>
internal class FormDesignerEditorViewProvider : IViewsProvider
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="items"></param>
  /// <param name="services"></param>
  /// <returns></returns>
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    ViewsInfo views = ViewsInfo.Empty;
    if (items.Count == 1)
    {
      views = new ViewsInfo();
      views.Add("FormDesignerEditorObjects", new ViewInfo(0, 1140, typeof (FormDesignerView)));
    }
    return views;
  }
}
