// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.OurDataObject
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

using System.Windows.Forms;

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class OurDataObject : DataObject
{
  private CCtl ctl;
  private ImRtfEditor e;

  internal OurDataObject(ImRtfEditor tern)
  {
    this.e = tern;
    this.ctl = this.e.ctl;
  }

  public override object GetData(string fmt) => this.GetData(fmt, false);

  public override object GetData(string fmt, bool convert)
  {
    switch (fmt)
    {
      case "Rich Text Format":
        this.ctl.RtfWrite(2, "", out string _);
        if (this.e.RtfClipData != null)
          return (object) new string(this.e.RtfClipData);
        break;
      case "UnicodeText":
      case "Text":
        this.e.OurPrintf((object) "unicode");
        return (object) this.e.TerGetTextSel();
      case "SSClipInfo":
        return (object) this.e.ClipInfo;
    }
    return (object) null;
  }

  public override bool GetDataPresent(string fmt) => this.GetDataPresent(fmt, false);

  public override bool GetDataPresent(string fmt, bool convert)
  {
    return fmt == "Text" || fmt == "UnicodeText" || fmt == "Rich Text Format" || fmt == "SSClipInfo";
  }
}
