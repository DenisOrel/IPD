// Decompiled with JetBrains decompiler
// Type: Intermech.Document.RtfEditor.COle
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

#nullable disable
namespace Intermech.Document.RtfEditor;

internal class COle : COp
{
  internal COle(ImRtfEditor ImRtfEditor)
    : base(ImRtfEditor)
  {
  }

  internal new bool ExitOle() => true;

  internal new bool OlePostProcessing()
  {
    int num = this.e.TerFlags & 268435456 /*0x10000000*/;
    return true;
  }

  internal new bool TerEditOle(bool edit) => false;
}
