// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.JBIG2BaseFlags
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Syncfusion.Pdf;

public abstract class JBIG2BaseFlags
{
  protected internal IDictionary flags = (IDictionary) new Dictionary<string, int>();
  protected internal int flagsAsInt;

  public int GetFlagValue(string key) => ((int?) this.flags[(object) key]).Value;

  public abstract void setFlags(int flagsAsInt);
}
