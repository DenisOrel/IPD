
// Type: Intermech.Client.Core.BarsDockingProperty
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;


namespace Intermech.Client.Core;

/// <summary>Summary description for BarsDockingProperty.</summary>
public class BarsDockingProperty : IPropertyPage, IPropertyPageSearchOptionEvents
{
  private IServiceProvider _provider;
  private ClassWrapperForPropertyGrid _object;
  private DockBarsSettings _settings;

  public BarsDockingProperty(IServiceProvider provider)
  {
    this._provider = provider;
    this._settings = new DockBarsSettings(provider);
    this._object = new ClassWrapperForPropertyGrid((object) this._settings);
    ((IPropertyPagesService) this._provider.GetService(typeof (IPropertyPagesService)))?.AddPage(LocalizationHolder.rm.GetString("Client.Core_884"), (IPropertyPage) this);
  }

  /// <summary>вернуть id раздела в хелпе для данной страницы</summary>
  public string HelpTopicID => "661";

  public void Cancel() => this._settings.LoadConfiguration();

  public object Control => (object) this._object;

  public void Apply()
  {
    this._settings.ApplyChanges();
    this._object.ResetOldValues();
  }

  public PropertyPageType Type => PropertyPageType.Object;

  public string PageName => LocalizationHolder.rm.GetString("Client.Core_885");

  /// <summary>
  /// Текст заголовка (пустое значение - заголовок не отображается)
  /// </summary>
  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  public event EventHandler Changed;

  /// <summary>
  /// Возвращает список имен настроек, содержащихся в контроле
  /// </summary>
  public List<string> GetOptionNames()
  {
    return !(this.Control is ClassWrapperForPropertyGrid control) ? new List<string>() : IPropertyPageHelper.GetOptionNames((ICustomTypeDescriptor) control);
  }
}
