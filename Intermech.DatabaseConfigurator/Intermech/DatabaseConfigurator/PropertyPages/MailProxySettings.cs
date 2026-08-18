// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.PropertyPages.MailProxySettings
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace Intermech.DatabaseConfigurator.PropertyPages;

internal class MailProxySettings : 
  IPropertyPage,
  ISortedPropertyGrid,
  IPropertyPageSearchOptionEvents
{
  private System.IServiceProvider _provider;
  private ProxyServer _proxy;
  private ClassWrapperForPropertyGrid _object;

  public MailProxySettings(System.IServiceProvider provider)
  {
    this._provider = provider;
    this.Load();
    ((IPropertyPagesService) this._provider.GetService(typeof (IPropertyPagesService)))?.AddPage("Система\\Электронная почта\\Настройки прокси сервера", (IPropertyPage) this);
    this._object = new ClassWrapperForPropertyGrid((object) this._proxy);
  }

  private void Load()
  {
    this._proxy = ((IEmailService) (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IEmailService))).Proxy;
    if (this._proxy != null)
      return;
    this._proxy = new ProxyServer();
  }

  public event EventHandler Changed;

  public PropertyPageType Type => PropertyPageType.Object;

  public object Control => (object) this._object;

  public string PageName => "Настройки прокси сервера";

  public void Apply()
  {
    ((IEmailService) (ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IEmailService))).Proxy = this._proxy;
  }

  public void Cancel() => this.Load();

  public string HelpTopicID => "-1";

  public string HeaderText => this.PageName;

  public PropertySort Sort => PropertySort.Categorized;

  public List<string> GetOptionNames()
  {
    return !(this.Control is ClassWrapperForPropertyGrid control) ? new List<string>() : IPropertyPageHelper.GetOptionNames((ICustomTypeDescriptor) control);
  }
}
