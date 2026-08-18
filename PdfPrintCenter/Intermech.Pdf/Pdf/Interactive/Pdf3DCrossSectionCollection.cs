// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.Pdf3DCrossSectionCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Collections.Generic;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class Pdf3DCrossSectionCollection : List<Pdf3DCrossSection>
{
  public int Add(Pdf3DCrossSection value)
  {
    base.Add(value);
    return base.IndexOf(value);
  }

  public new bool Contains(Pdf3DCrossSection value) => base.Contains(value);

  public new int IndexOf(Pdf3DCrossSection value) => base.IndexOf(value);

  public new void Insert(int index, Pdf3DCrossSection value) => base.Insert(index, value);

  public void Remove(Pdf3DCrossSection value) => base.Remove(value);

  public new Pdf3DCrossSection this[int index]
  {
    get => base[index];
    set => base[index] = value;
  }
}
