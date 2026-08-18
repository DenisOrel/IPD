// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.Table.ExpertTableColorsService
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using Intermech.Interfaces;
using System;
using System.IO;
using System.Xml;

#nullable disable
namespace Intermech.Expert.Editor.Table;

internal class ExpertTableColorsService : IExpertTableColorsService
{
  private IExpertTableColors _current;

  public ExpertTableColorsService() => this._current = this.LoadFromBase();

  public IExpertTableColors Current
  {
    get => this._current;
    set
    {
      this._current = value;
      this.SaveToBase(this._current);
    }
  }

  public void SaveToBase(IExpertTableColors value)
  {
    using (MemoryStream outStream = new MemoryStream())
    {
      XmlDocument xmlDoc = new XmlDocument();
      XmlNode element = (XmlNode) xmlDoc.CreateElement("ExpertTableColors");
      element.AppendChild(value.Save(xmlDoc));
      xmlDoc.AppendChild(element);
      xmlDoc.Save((Stream) outStream);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        sessionKeeper.Session.Configurations.WriteConfigData(new BlobInformation(outStream.Length, outStream.Length, DateTime.Now, "ExpertTableColors", ArcMethods.NotPacked, string.Empty), outStream.ToArray());
    }
  }

  public IExpertTableColors LoadFromBase()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      BlobInformation config_info;
      byte[] config_file;
      sessionKeeper.Session.Configurations.LoadConfigData("ExpertTableColors", out config_info, out config_file);
      if (config_info.RealFileSize <= 0L || (long) config_file.Length < config_info.PackedFileSize)
        return (IExpertTableColors) new ExpertTableColors();
      MemoryStream inStream = new MemoryStream(config_file);
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load((Stream) inStream);
      XmlNode firstChild = xmlDocument.FirstChild;
      return firstChild.Name.Equals("ExpertTableColors") && firstChild.ChildNodes.Count.Equals(1) ? (IExpertTableColors) ExpertTableColors.Load(firstChild.ChildNodes[0]) : (IExpertTableColors) new ExpertTableColors();
    }
  }
}
