// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.ExpFormViewProvider
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>View Provider for expert system formulae</summary>
public class ExpFormViewProvider : IViewsProvider
{
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items.Count != 1)
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    views.Add(LocalizationHolder.rm.GetString("Expert.Editor_201"), new ViewInfo(0, 1315, typeof (ExpFormView)));
    return views;
  }
}
