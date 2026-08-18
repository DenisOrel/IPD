// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.TemplateInfo
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Xml;

#nullable disable
namespace Intermech.Workflow.Design;

internal class TemplateInfo : Dictionary<string, string>
{
  public readonly string Directory = "";
  private Color _imgBGColor = Color.Empty;

  public TemplateInfo(string templateDir)
  {
    this.Directory = FileFuncs.IncludeTrailingPathDelimiter(templateDir);
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.Load(templateDir + "\\template.xml");
    XmlNode firstChild = xmlDocument.FirstChild;
    if (!firstChild.HasChildNodes)
      return;
    for (int i = 0; i < firstChild.ChildNodes.Count; ++i)
    {
      XmlNode childNode = firstChild.ChildNodes[i];
      this.Add(childNode.Name.ToLower(), childNode.InnerText);
    }
  }

  public static bool IsValidTemplate(string templateDir)
  {
    return File.Exists(templateDir + "\\template.xml");
  }

  public string GetVal(string name)
  {
    name = name.ToLower();
    string val = "";
    if (!this.TryGetValue(name, out val))
      val = "";
    return val;
  }

  public string Name => this.GetVal("name");

  public Color ImgBGColor
  {
    get
    {
      if (this._imgBGColor == Color.Empty)
      {
        string lower = this.GetVal("imgbgcolor").Trim().ToLower();
        this._imgBGColor = lower == "" || lower == "transparent" ? Color.Transparent : Color.FromArgb(-16777216 /*0xFF000000*/ | (int) uint.Parse(lower, NumberStyles.HexNumber));
      }
      return this._imgBGColor;
    }
  }
}
