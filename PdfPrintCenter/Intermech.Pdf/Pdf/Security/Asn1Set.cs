// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Security.Asn1Set
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Syncfusion.Pdf.Security;

internal class Asn1Set : AsnObject, IEnumerable
{
  private List<AsnObject> m_objects;

  public Asn1Set() => this.m_objects = new List<AsnObject>();

  public Asn1Set(List<AsnObject> sequence)
  {
    this.m_objects = new List<AsnObject>();
    foreach (AsnObject asnObject in sequence)
      this.m_objects.Add(asnObject);
  }

  public IEnumerator GetEnumerator() => (IEnumerator) this.m_objects.GetEnumerator();

  public AsnObject this[int index] => this.m_objects[index];

  public List<AsnObject> Objects => this.m_objects;
}
