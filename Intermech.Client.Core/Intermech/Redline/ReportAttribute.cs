
// Type: Intermech.Redline.ReportAttribute
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using System;


namespace Intermech.Redline;

[AttributeUsage(AttributeTargets.Field)]
public class ReportAttribute : Attribute
{
  public ReportAttribute(string name, string tipText, string imgName)
  {
    this.Name = LocalizationHolder.rm.GetString(name) ?? name;
    this.TipText = LocalizationHolder.rm.GetString(tipText) ?? tipText;
    this.ImgName = imgName;
  }

  public string Name { get; protected set; }

  public string TipText { get; protected set; }

  public string ImgName { get; protected set; }
}
