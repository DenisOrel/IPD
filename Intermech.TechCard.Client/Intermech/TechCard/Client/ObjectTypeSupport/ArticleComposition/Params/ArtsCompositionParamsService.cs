// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params.ArtsCompositionParamsService
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.ArticleComposition.Params;

/// <summary>
/// 
/// </summary>
internal class ArtsCompositionParamsService : IArtsCompositionParamsService
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="settings"></param>
  /// <returns></returns>
  public bool SaveSettings(IArtsCompositionParams settings)
  {
    if (settings == null)
      throw new ArgumentNullException(nameof (settings));
    XmlDocument xmlDocument = new XmlDocument();
    XmlNode element1 = (XmlNode) xmlDocument.CreateElement("ArtsCompositionParams");
    XmlAttribute attribute1 = xmlDocument.CreateAttribute("ShowRemainQyy");
    attribute1.Value = settings.ShowRemainQty ? "1" : "0";
    element1.Attributes?.Append(attribute1);
    XmlAttribute attribute2 = xmlDocument.CreateAttribute("DesignQuantityMode");
    attribute2.Value = Convert.ToString((int) settings.DesignQuantityMode);
    element1.Attributes?.Append(attribute2);
    foreach (IArtsCompositionStatusParams statusParam in (IEnumerable<IArtsCompositionStatusParams>) settings.StatusParams)
    {
      XmlNode element2 = (XmlNode) xmlDocument.CreateElement("ArtsCompositionStatusParams");
      element1.AppendChild(element2);
      XmlAttribute attribute3 = xmlDocument.CreateAttribute("Status");
      attribute3.Value = ((int) statusParam.Status).ToString();
      element2.Attributes?.Append(attribute3);
      XmlAttribute attribute4 = xmlDocument.CreateAttribute("Color");
      attribute4.Value = ColorTranslator.ToHtml(statusParam.Color);
      element2.Attributes?.Append(attribute4);
    }
    xmlDocument.AppendChild(element1);
    using (MemoryStream outStream = new MemoryStream())
    {
      xmlDocument.Save((Stream) outStream);
      using (SessionKeeper sessionKeeper = new SessionKeeper())
        sessionKeeper.Session.Configurations.WriteConfigData(new BlobInformation(outStream.Length, outStream.Length, DateTime.Now, "ArtsCompositionParams", ArcMethods.NotPacked, string.Empty), outStream.ToArray());
    }
    return true;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="settings"></param>
  /// <returns></returns>
  public bool LoadSettings(out IArtsCompositionParams settings)
  {
    settings = (IArtsCompositionParams) new ArtsCompositionParams();
    byte[] config_file;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      BlobInformation config_info;
      sessionKeeper.Session.Configurations.LoadConfigData("ArtsCompositionParams", out config_info, out config_file);
      if (config_info.RealFileSize != 0L)
      {
        if ((long) config_file.Length >= config_info.PackedFileSize)
          goto label_7;
      }
      return true;
    }
label_7:
    using (MemoryStream inStream = new MemoryStream(config_file))
    {
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load((Stream) inStream);
      XmlNode firstChild = xmlDocument.FirstChild;
      if (!firstChild.Name.Equals("ArtsCompositionParams"))
        return false;
      XmlAttribute attribute1 = firstChild.Attributes?["ShowRemainQyy"];
      int result1;
      if (attribute1 != null && int.TryParse(attribute1.Value, out result1))
        settings.ShowRemainQty = result1 != 0;
      XmlAttribute attribute2 = firstChild.Attributes?["DesignQuantityMode"];
      int result2;
      if (attribute2 != null && int.TryParse(attribute2.Value, out result2))
        settings.DesignQuantityMode = (ArtsCompositionQuantityMode) result2;
      foreach (XmlNode childNode in firstChild.ChildNodes)
      {
        if (childNode.Name.Equals("ArtsCompositionStatusParams"))
        {
          ArtsCompositionItemStatus itemStatus = ArtsCompositionItemStatus.None;
          XmlAttribute attribute3 = childNode.Attributes?["Status"];
          int result3;
          if (attribute3 != null && int.TryParse(attribute3.Value, out result3))
            itemStatus = (ArtsCompositionItemStatus) result3;
          Color color = Color.Empty;
          XmlAttribute attribute4 = childNode.Attributes?["Color"];
          if (attribute4 != null)
            color = ColorTranslator.FromHtml(attribute4.Value);
          IArtsCompositionStatusParams compositionStatusParams = settings.StatusParams.FirstOrDefault<IArtsCompositionStatusParams>((Func<IArtsCompositionStatusParams, bool>) (item => item != null && item.Status == itemStatus));
          if (compositionStatusParams != null)
            compositionStatusParams.Color = color;
        }
      }
      return true;
    }
  }
}
