// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.ImDocumentClientConfig
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Document.DBCore;
using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace Intermech.Document.Client;

public class ImDocumentClientConfig : 
  ImDocumentConfigBase,
  IPropertyPage,
  IPropertyPageSearchOptionEvents
{
  private static ImDocumentClientConfig instance;

  public static ImDocumentClientConfig Instance
  {
    get
    {
      if (ImDocumentClientConfig.instance == null)
        ImDocumentClientConfig.instance = new ImDocumentClientConfig();
      return ImDocumentClientConfig.instance;
    }
  }

  PropertyPageType IPropertyPage.Type => PropertyPageType.Object;

  [Browsable(false)]
  public object Control => (object) new ClassWrapperForPropertyGrid((object) this);

  [Browsable(false)]
  public string PageName => "Клиентские настройки редактора документов";

  string IPropertyPage.HelpTopicID => "";

  string IPropertyPage.HeaderText => this.PageName;

  public event EventHandler Changed;

  public override void Apply() => base.Apply();

  public override void Cancel() => base.Cancel();

  List<string> IPropertyPageSearchOptionEvents.GetOptionNames()
  {
    return !(this.Control is ClassWrapperForPropertyGrid control) ? new List<string>() : IPropertyPageHelper.GetOptionNames((ICustomTypeDescriptor) control);
  }
}
