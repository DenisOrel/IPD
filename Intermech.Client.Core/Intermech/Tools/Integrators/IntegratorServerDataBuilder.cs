
// Type: Intermech.Tools.Integrators.IntegratorServerDataBuilder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Collections;
using Intermech.Localization;
using Intermech.Tools.Settings;
using System;
using System.Collections.Generic;
using System.Xml;


namespace Intermech.Tools.Integrators;

public sealed class IntegratorServerDataBuilder
{
  private const string ServerDataElement = "LookupData";
  private string integratorName;
  private bool skipFileManagement;
  private ICollection<IntegratorServerDataBuilder.ObjectTypeData> objectTypes;

  public IntegratorServerDataBuilder()
  {
    this.objectTypes = (ICollection<IntegratorServerDataBuilder.ObjectTypeData>) new List<IntegratorServerDataBuilder.ObjectTypeData>(16 /*0x10*/);
  }

  public string IntegratorName
  {
    get => this.integratorName;
    set => this.integratorName = value;
  }

  public bool SpecialFileManagement
  {
    get => this.skipFileManagement;
    set => this.skipFileManagement = value;
  }

  public void AddObjectType(Guid guid, params string[] flags)
  {
    if (flags == null)
      throw new ArgumentNullException(nameof (flags));
    this.AddObjectTypeInternal(guid, (IEnumerable<string>) flags);
  }

  public void AddObjectType(Guid guid, IEnumerable<string> flags)
  {
    if (flags == null)
      throw new ArgumentNullException(nameof (flags));
    this.AddObjectTypeInternal(guid, flags);
  }

  private void AddObjectTypeInternal(Guid guid, IEnumerable<string> flags)
  {
    if (CollectionUtils.Exists<IntegratorServerDataBuilder.ObjectTypeData>((IEnumerable<IntegratorServerDataBuilder.ObjectTypeData>) this.objectTypes, (Predicate<IntegratorServerDataBuilder.ObjectTypeData>) (item => item.Guid == guid)))
      throw new ArgumentException($"The object type with id '{guid}' is already added.");
    this.objectTypes.Add(new IntegratorServerDataBuilder.ObjectTypeData(guid, flags));
  }

  private void Validate()
  {
    if (string.IsNullOrEmpty(this.integratorName))
      throw new InvalidOperationException(LocalizationHolder.rm.GetString("SR_1641"));
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
    XmlElement element1 = settingsBuilder.CreateElement("LookupData");
    settingsBuilder.AppendAttribute((XmlNode) element1, "displayName", (object) this.integratorName);
    if (this.skipFileManagement)
      settingsBuilder.AppendAttribute((XmlNode) element1, "skipFileManagement", (object) true);
    foreach (IntegratorServerDataBuilder.ObjectTypeData objectType in (IEnumerable<IntegratorServerDataBuilder.ObjectTypeData>) this.objectTypes)
    {
      XmlElement element2 = settingsBuilder.CreateElement("ObjectType");
      settingsBuilder.AppendAttribute((XmlNode) element2, "guid", (object) objectType.Guid);
      foreach (string flag in objectType.Flags)
      {
        XmlElement element3 = settingsBuilder.CreateElement("Flag");
        settingsBuilder.AppendAttribute((XmlNode) element3, "name", (object) flag);
        element2.AppendChild((XmlNode) element3);
      }
      element1.AppendChild((XmlNode) element2);
    }
    return element1;
  }

  private sealed class ObjectTypeData
  {
    public ObjectTypeData(Guid guid, IEnumerable<string> flags)
    {
      this.Guid = guid;
      this.Flags = flags;
    }

    public Guid Guid { get; private set; }

    public IEnumerable<string> Flags { get; private set; }
  }
}
