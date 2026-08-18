// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.PropertyPages.DocTypesPage
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.DatabaseConfigurator.PropertyPages;

public class DocTypesPage : IPropertyPage, IPropertyPageSearchOptionEvents
{
  private IServiceProvider _provider;
  private DocTypesPage.DocTypesProperties _doctypeProps;
  private ClassWrapperForPropertyGrid _object;

  public DocTypesPage(IServiceProvider provider)
  {
    if (!(ServicesManager.GetService(typeof (ICurrentUserAndRole)) is ICurrentUserAndRole service) || !service.IsAdmin)
      return;
    this._provider = provider;
    ((IPropertyPagesService) this._provider.GetService(typeof (IPropertyPagesService)))?.AddPage(LocalizationHolder.rm.GetString("DatabaseConfigurator_216"), (IPropertyPage) this);
  }

  public string HelpTopicID => "1626";

  public event EventHandler Changed;

  public PropertyPageType Type => PropertyPageType.Object;

  public object Control
  {
    get
    {
      if (this._object == null)
      {
        this._doctypeProps = new DocTypesPage.DocTypesProperties();
        this._object = new ClassWrapperForPropertyGrid((object) this._doctypeProps);
      }
      return (object) this._object;
    }
  }

  public string PageName => LocalizationHolder.rm.GetString("DatabaseConfigurator_216");

  public string HeaderText
  {
    [DebuggerStepThrough] get => this.PageName;
  }

  public void Apply()
  {
    if (this._doctypeProps == null)
      return;
    this._doctypeProps.ApplyUpdates();
    this._object.ResetOldValues();
    if (this.Changed == null)
      return;
    this.Changed((object) this, new EventArgs());
  }

  public void Cancel()
  {
    if (this._doctypeProps == null)
      return;
    this._doctypeProps._inited = false;
  }

  public List<string> GetOptionNames()
  {
    return !(this.Control is ClassWrapperForPropertyGrid control) ? new List<string>() : IPropertyPageHelper.GetOptionNames((ICustomTypeDescriptor) control);
  }

  private class DocTypesProperties
  {
    private string _separator = string.Empty;
    internal bool _inited;

    internal void ApplyUpdates()
    {
      (ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations).WriteString("KERNEL", "DOC_TYPES", "SEPARATOR_DESIGNATION", this._separator, 0L);
    }

    internal void LoadCurrentValues()
    {
      this._separator = (ServicesManager.GetService(typeof (IDBConfigurations)) as IDBConfigurations).ReadString("KERNEL", "DOC_TYPES", "SEPARATOR_DESIGNATION", Consts.DefaultSeparatorInDesignation, DBConfigMode.GlobalOnly);
    }

    private void CheckInited()
    {
      if (this._inited)
        return;
      this.LoadCurrentValues();
      this._inited = true;
    }

    [CustomDescription("Attribute.DatabaseConfigurator_22")]
    [CustomDisplayName("Attribute.DatabaseConfigurator_23")]
    public string Separator
    {
      get
      {
        this.CheckInited();
        return this._separator;
      }
      set => this._separator = value;
    }
  }
}
