
// Type: Intermech.Navigator.Views.ViewsTableBuilder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Navigator.Views;

internal class ViewsTableBuilder
{
  private ViewsTable _table;
  private IServiceProvider _services;

  public ViewsTableBuilder() => this._table = new ViewsTable();

  public ViewsTableBuilder(IServiceProvider services)
    : this()
  {
    this._services = services;
  }

  public void Append(int level, ViewsInfo info)
  {
    if (info.ViewNames == null)
      return;
    for (int index = 0; index < info.ViewNames.Length; ++index)
    {
      string viewName = info.ViewNames[index];
      ViewInfo info1 = info.GetInfo(viewName);
      ViewsTableEntry viewsTableEntry = this._table[viewName];
      if (viewsTableEntry != null)
      {
        if (viewsTableEntry.Level == level && viewsTableEntry.ViewInfo.Priority < info1.Priority)
          viewsTableEntry.ViewInfo = info1;
      }
      else
        this._table.Add(viewName, new ViewsTableEntry(level, info1));
    }
  }

  /// <summary>Проверить, реализует ли закладка указанный интерфейс</summary>
  /// <param name="viewType">Тип закладки</param>
  /// <param name="intfType">Тип интерфейса</param>
  /// <returns>true - закладка реализует указанный интерфейс</returns>
  private bool CheckForImplements(Type viewType, Type intfType)
  {
    return viewType != (Type) null && intfType != (Type) null && viewType.IsClass && !viewType.IsAbstract && intfType.IsAssignableFrom(viewType);
  }

  public ViewsTable ToViewsTable()
  {
    NavigatorViewOptions service = this._services != null ? this._services.GetService(typeof (NavigatorViewOptions)) as NavigatorViewOptions : (NavigatorViewOptions) null;
    NavigatorViewContext navigatorViewContext = service != null ? service.Context : NavigatorViewContext.MainViews;
    Type intfType = typeof (INavigatorView);
    string[] viewNames = this._table.ViewNames;
    if (viewNames != null)
    {
      for (int index = 0; index < viewNames.Length; ++index)
      {
        ViewsTableEntry viewsTableEntry = this._table[viewNames[index]];
        if (viewsTableEntry.ViewInfo.CreatorCallback == null)
          this._table.Remove(viewNames[index]);
        if (navigatorViewContext == NavigatorViewContext.TreeViews && !this.CheckForImplements(viewsTableEntry.ViewInfo.ControlType, intfType))
          this._table.Remove(viewNames[index]);
      }
    }
    return this._table;
  }
}
