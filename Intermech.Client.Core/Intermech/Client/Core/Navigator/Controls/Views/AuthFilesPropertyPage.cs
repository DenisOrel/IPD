
// Type: Intermech.Client.Core.Navigator.Controls.Views.AuthFilesPropertyPage
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


namespace Intermech.Client.Core.Navigator.Controls.Views;

public class AuthFilesPropertyPage : IPropertyPage, IPropertyPageSearchOptionEvents
{
  /// <summary>Контейнер сервисов</summary>
  private IServiceProvider _provider;
  private AuthFilesProperties _props;
  private ClassWrapperForPropertyGrid _object;

  public AuthFilesPropertyPage(IServiceProvider provider)
  {
    if (!(ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service) || !service.IsAdmin)
      return;
    this._provider = provider;
    ((IPropertyPagesService) this._provider.GetService(typeof (IPropertyPagesService)))?.AddPage(LocalizationHolder.rm.GetString("AuthFilesSettings"), (IPropertyPage) this);
  }

  public event EventHandler Changed;

  public PropertyPageType Type => PropertyPageType.Object;

  public object Control
  {
    get
    {
      if (this._object == null)
      {
        this._props = new AuthFilesProperties();
        this._object = new ClassWrapperForPropertyGrid((object) this._props);
      }
      return (object) this._object;
    }
  }

  public string PageName => LocalizationHolder.rm.GetString("AuthFilesPageName");

  public void Apply()
  {
    if (this._props == null)
      return;
    this._props.ApplyUpdates();
    this._object.ResetOldValues();
    if (this.Changed == null)
      return;
    this.Changed((object) this, new EventArgs());
  }

  public void Cancel()
  {
    if (this._props == null)
      return;
    this._props._inited = false;
  }

  public string HelpTopicID => string.Empty;

  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  /// <summary>
  /// Возвращает список имен настроек, содержащихся в контроле
  /// </summary>
  public List<string> GetOptionNames()
  {
    return !(this.Control is ClassWrapperForPropertyGrid control) ? new List<string>() : IPropertyPageHelper.GetOptionNames((ICustomTypeDescriptor) control);
  }
}
