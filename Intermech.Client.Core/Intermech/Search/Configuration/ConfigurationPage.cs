
// Type: Intermech.Search.Configuration.ConfigurationPage
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using System;
using System.Collections.Generic;


namespace Intermech.Search.Configuration;

public sealed class ConfigurationPage : IPropertyPage, IPropertyPageSearchOptionEvents
{
  private Intermech.Search.Configuration.Configuration _configuration;

  public ConfigurationPage(string pageName, List<ConfigurationOptionInfo> optionsInfo)
  {
    if (pageName == null)
      throw new ArgumentNullException(nameof (pageName));
    if (optionsInfo == null)
      throw new ArgumentNullException(nameof (optionsInfo));
    this.PageName = pageName;
    this.OptionsInfo = new List<ConfigurationOptionInfo>((IEnumerable<ConfigurationOptionInfo>) optionsInfo);
  }

  public List<ConfigurationOptionInfo> OptionsInfo { get; private set; }

  public event EventHandler Changed;

  public PropertyPageType Type => PropertyPageType.Object;

  public object Control
  {
    get
    {
      if (this._configuration == null)
      {
        this._configuration = Intermech.Search.Configuration.Configuration.Load(this.OptionsInfo);
        this._configuration.Changed += new EventHandler(this.Configuration_Changed);
      }
      return (object) this._configuration;
    }
  }

  public string PageName { get; private set; }

  public void Apply()
  {
    if (this._configuration == null || !this._configuration.IsChanged)
      return;
    this._configuration.ApplyChanges();
  }

  public void Cancel()
  {
    if (this._configuration == null)
      return;
    this._configuration.CancelChanges();
  }

  public string HelpTopicID => "0";

  public string HeaderText => this.PageName;

  /// <summary>
  /// Возвращает список имен настроек, содержащихся в контроле
  /// </summary>
  public List<string> GetOptionNames()
  {
    List<string> optionNames = new List<string>();
    foreach (ConfigurationOptionInfo configurationOptionInfo in this.OptionsInfo)
    {
      if (configurationOptionInfo != null && configurationOptionInfo.DisplayName != null)
        optionNames.Add(configurationOptionInfo.DisplayName);
    }
    return optionNames;
  }

  private void Configuration_Changed(object sender, EventArgs e) => this.OnChanged();

  private void OnChanged()
  {
    if (this.Changed == null)
      return;
    this.Changed((object) this, new EventArgs());
  }
}
