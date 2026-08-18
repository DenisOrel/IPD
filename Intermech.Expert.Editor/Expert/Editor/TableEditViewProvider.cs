// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.TableEditViewProvider
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.Expert.Editor;

/// <summary>Класс провайдер для вьюшки</summary>
public class TableEditViewProvider : IViewsProvider
{
  /// <summary>Получить список вьшек</summary>
  /// <param name="items"></param>
  /// <param name="services"></param>
  /// <returns></returns>
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (!items.Count.Equals(1))
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    views.Add("TableEdit", new ViewInfo(0, 1326, typeof (TableEditView)));
    return views;
  }
}
