
// Type: Intermech.Navigator.Views.ViewsCollector
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using System;
using System.Windows.Forms;


namespace Intermech.Navigator.Views;

/// <summary>Класс позволяет собрать закладки Навигатора</summary>
internal class ViewsCollector
{
  /// <summary>Требуется ли показать сообщение об ошибке</summary>
  private static bool _showErrorMsg = true;
  /// <summary>Коллекция выделенных элементов</summary>
  private ISelectedItems _items;
  /// <summary>Контейнер сервисов</summary>
  private System.IServiceProvider _services;

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="items">Коллекция выделенных элементов</param>
  /// <param name="services">Контейнер сервисов</param>
  public ViewsCollector(ISelectedItems items, System.IServiceProvider services)
  {
    this._items = items;
    this._services = services;
  }

  /// <summary>Отобразить сообщение об ошибке</summary>
  /// <param name="e">Исключение</param>
  private void ShowError(Exception e)
  {
    if (e == null)
      return;
    if (ServicesManager.GetService(typeof (IOutputView)) is IOutputView service)
    {
      string text = string.Format(LocalizationHolder.rm.GetString("Client.Core_1370"), (object) e.Message);
      service.WriteString(LocalizationHolder.rm.GetString("IMClient_51"), text);
      service.WriteString(LocalizationHolder.rm.GetString("IMClient_51"), e.StackTrace);
    }
    if (!ViewsCollector._showErrorMsg)
      return;
    ViewsCollector._showErrorMsg = false;
    switch (IMMessageBox.Show(LocalizationHolder.rm.GetString("Client.Core_1534"), LocalizationHolder.rm.GetString("Client.Core_1535") + LocalizationHolder.rm.GetString("Client.Core_1536"), new IMMessageBoxButton[3]
    {
      new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_1374"), DialogResult.No),
      new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_1375"), DialogResult.Yes),
      new IMMessageBoxButton(LocalizationHolder.rm.GetString("Client.Core_1376"), DialogResult.Abort)
    }, IMMessageBoxImage.Information))
    {
      case DialogResult.Abort:
        ExceptionHelper.ExceptionService.ShowException(e);
        break;
      case DialogResult.Yes:
        service?.ShowView();
        break;
    }
  }

  /// <summary>Выполнить сбор закладок</summary>
  /// <returns>Таблица сведений об отображаемых закладках</returns>
  public ViewsTable Execute()
  {
    ViewsTableBuilder viewsTableBuilder = new ViewsTableBuilder(this._services);
    if (this._items != null && this._items.Count > 0)
    {
      bool flag1 = false;
      bool flag2 = false;
      INodeID nodeId = this._items.GetItemID(0);
      for (int index = 1; index < this._items.Count; ++index)
      {
        INodeID itemId = this._items.GetItemID(index);
        flag1 |= itemId.TypeID != nodeId.TypeID;
        flag2 |= itemId.CategoryID != nodeId.CategoryID;
        if (!(flag1 & flag2))
          nodeId = itemId;
        else
          break;
      }
      if (!flag2)
      {
        if (!flag1)
        {
          IViewsProvider[] viewsProviders = Holder.Factory.GetViewsProviders(nodeId.CategoryID, nodeId.TypeID);
          if (viewsProviders != null)
          {
            for (int index = 0; index < viewsProviders.Length; ++index)
            {
              try
              {
                ViewsInfo views = viewsProviders[index].GetViews(this._items, this._services);
                viewsTableBuilder.Append(1, views);
              }
              catch (Exception ex)
              {
                this.ShowError(ex);
              }
            }
          }
        }
        IViewsProvider[] viewsProviders1 = Holder.Factory.GetViewsProviders(nodeId.CategoryID);
        if (viewsProviders1 != null)
        {
          for (int index = 0; index < viewsProviders1.Length; ++index)
          {
            try
            {
              ViewsInfo views = viewsProviders1[index].GetViews(this._items, this._services);
              viewsTableBuilder.Append(2, views);
            }
            catch (Exception ex)
            {
              this.ShowError(ex);
            }
          }
        }
      }
      IViewsProvider[] viewsProviders2 = Holder.Factory.GetViewsProviders();
      if (viewsProviders2 != null)
      {
        for (int index = 0; index < viewsProviders2.Length; ++index)
        {
          try
          {
            ViewsInfo views = viewsProviders2[index].GetViews(this._items, this._services);
            viewsTableBuilder.Append(3, views);
          }
          catch (Exception ex)
          {
            this.ShowError(ex);
          }
        }
      }
    }
    return viewsTableBuilder.ToViewsTable();
  }
}
