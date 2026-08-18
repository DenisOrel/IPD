// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.Table.ExpertTableProperties
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Localization;
using System;
using System.ComponentModel;
using System.IO;
using System.Xml;

#nullable disable
namespace Intermech.Expert.Editor.Table;

internal class ExpertTableProperties : IExpertTableProperties
{
  private bool _use4ot;
  private bool _use4at;

  public void SaveToBase()
  {
    using (MemoryStream outStream = new MemoryStream())
    {
      XmlDocument xmlDocument = new XmlDocument();
      XmlElement element1 = xmlDocument.CreateElement(nameof (ExpertTableProperties));
      XmlElement element2 = xmlDocument.CreateElement("UseShortName4ObjectType");
      element2.InnerText = this._use4ot.ToString();
      XmlElement element3 = xmlDocument.CreateElement("UseShortName4AttrobuteType");
      element3.InnerText = this._use4at.ToString();
      element1.AppendChild((XmlNode) element2);
      element1.AppendChild((XmlNode) element3);
      xmlDocument.AppendChild((XmlNode) element1);
      xmlDocument.Save((Stream) outStream);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        sessionKeeper.Session.Configurations.WriteConfigData(new BlobInformation(outStream.Length, outStream.Length, DateTime.Now, nameof (ExpertTableProperties), ArcMethods.NotPacked, string.Empty), outStream.ToArray());
    }
  }

  public static ExpertTableProperties LoadFromBase()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      BlobInformation config_info;
      byte[] config_file;
      sessionKeeper.Session.Configurations.LoadConfigData(nameof (ExpertTableProperties), out config_info, out config_file);
      if (config_info.RealFileSize <= 0L)
        return new ExpertTableProperties();
      MemoryStream inStream = new MemoryStream(config_file);
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load((Stream) inStream);
      XmlNode firstChild = xmlDocument.FirstChild;
      if (!firstChild.Name.Equals(nameof (ExpertTableProperties)) || !firstChild.ChildNodes.Count.Equals(2))
        return new ExpertTableProperties();
      return new ExpertTableProperties()
      {
        _use4ot = Convert.ToBoolean(firstChild.ChildNodes[0].InnerText),
        _use4at = Convert.ToBoolean(firstChild.ChildNodes[1].InnerText)
      };
    }
  }

  private void OnChanged()
  {
    EventHandler changed = this.Changed;
    if (changed == null)
      return;
    changed((object) this, EventArgs.Empty);
  }

  [CustomDisplayName("Attribute.Expert.Editor_9")]
  [TypeConverter(typeof (YesNoBooleanConverter))]
  public bool UseShortName4ObjectType
  {
    get => this._use4ot;
    set
    {
      this._use4ot = value;
      this.OnChanged();
    }
  }

  [CustomDisplayName("Attribute.Expert.Editor_10")]
  [TypeConverter(typeof (YesNoBooleanConverter))]
  public bool UseShortName4AttributeType
  {
    get => this._use4at;
    set
    {
      this._use4at = value;
      this.OnChanged();
    }
  }

  public event EventHandler Changed;

  public override int GetHashCode() => base.GetHashCode();

  public override bool Equals(object obj)
  {
    if (!(obj is ExpertTableProperties))
      return base.Equals(obj);
    ExpertTableProperties expertTableProperties = obj as ExpertTableProperties;
    return expertTableProperties._use4at.Equals(this._use4at) && expertTableProperties._use4ot.Equals(this._use4ot);
  }
}
