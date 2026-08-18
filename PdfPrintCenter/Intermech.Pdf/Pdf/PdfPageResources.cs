// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.PdfPageResources
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Syncfusion.Pdf;

public class PdfPageResources
{
  private Dictionary<string, FontStructure> fontCollection = new Dictionary<string, FontStructure>();
  private Dictionary<string, object> m_resources = new Dictionary<string, object>();

  public void Add(string resourceName, object resource)
  {
    if (string.Equals(resourceName, "ProcSet") || this.m_resources.ContainsKey(resourceName))
      return;
    this.m_resources.Add(resourceName, resource);
    if (!(resource.GetType().Name == "FontStructure"))
      return;
    this.fontCollection.Add(resourceName, resource as FontStructure);
  }

  public bool ContainsKey(string key) => this.m_resources.ContainsKey(key);

  public bool isSameFont()
  {
    int num = 0;
    foreach (KeyValuePair<string, FontStructure> font1 in this.fontCollection)
    {
      foreach (KeyValuePair<string, FontStructure> font2 in this.fontCollection)
      {
        if (font1.Value.FontName != font2.Value.FontName)
          num = 1;
      }
    }
    return num == 0;
  }

  public object this[string key]
  {
    get
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      return this.m_resources.ContainsKey(key) ? this.m_resources[key] : (object) null;
    }
    set
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value));
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      this.m_resources[key] = value;
    }
  }

  public Dictionary<string, object> Resources => this.m_resources;
}
