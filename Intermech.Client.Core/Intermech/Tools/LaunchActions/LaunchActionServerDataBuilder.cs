
// Type: Intermech.Tools.LaunchActions.LaunchActionServerDataBuilder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Tools.Settings;
using System;
using System.Xml;


namespace Intermech.Tools.LaunchActions;

public sealed class LaunchActionServerDataBuilder
{
  private const string ServerDataElement = "LookupData";
  private string actionDisplayName;

  public string ActionDisplayName
  {
    get => this.actionDisplayName;
    set => this.actionDisplayName = value;
  }

  private void Validate()
  {
    if (string.IsNullOrEmpty(this.actionDisplayName))
      throw new InvalidOperationException("Не задано имя действия");
  }

  public void UpdateXml(SettingsXmlBuilder settingsBuilder)
  {
    if (settingsBuilder == null)
      throw new ArgumentNullException(nameof (settingsBuilder));
    this.Validate();
    XmlNode topLevelElement = (XmlNode) this.EmitServerData(settingsBuilder);
    XmlNode oldChild = settingsBuilder.SelectSingleNode("LookupData");
    oldChild?.ParentNode.RemoveChild(oldChild);
    settingsBuilder.AppendElement(topLevelElement);
  }

  private XmlElement EmitServerData(SettingsXmlBuilder settingsBuilder)
  {
    XmlElement element = settingsBuilder.CreateElement("LookupData");
    settingsBuilder.AppendAttribute((XmlNode) element, "displayName", (object) this.actionDisplayName);
    return element;
  }
}
