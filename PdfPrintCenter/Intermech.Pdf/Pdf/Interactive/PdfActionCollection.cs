// Decompiled with JetBrains decompiler
// Type: Syncfusion.Pdf.Interactive.PdfActionCollection
// Assembly: Intermech.Pdf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0C070FAE-F25E-47C5-A369-CE57AB4187A4
// Assembly location: D:\IPS\Client\Intermech.Pdf.dll
// XML documentation location: D:\IPS\Client\Intermech.Pdf.xml

using Syncfusion.Pdf.Primitives;
using System;

#nullable disable
namespace Syncfusion.Pdf.Interactive;

public class PdfActionCollection : PdfCollection
{
  private PdfArray m_actions = new PdfArray();

  public int Add(PdfAction action)
  {
    return action != null ? this.DoAdd(action) : throw new ArgumentNullException(nameof (action));
  }

  public void Clear() => this.DoClear();

  public bool Contains(PdfAction action)
  {
    return action != null ? this.List.Contains((object) action) : throw new ArgumentNullException(nameof (action));
  }

  private int DoAdd(PdfAction action)
  {
    this.m_actions.Add((IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) action));
    return this.List.Add((object) action);
  }

  private void DoClear()
  {
    this.m_actions.Clear();
    this.List.Clear();
  }

  private void DoInsert(int index, PdfAction action)
  {
    this.m_actions.Insert(index, (IPdfPrimitive) new PdfReferenceHolder((IPdfWrapper) action));
    this.List.Insert(index, (object) action);
  }

  private void DoRemove(PdfAction action)
  {
    this.m_actions.RemoveAt(this.List.IndexOf((object) action));
    this.List.Remove((object) action);
  }

  private void DoRemoveAt(int index)
  {
    this.m_actions.RemoveAt(index);
    this.List.RemoveAt(index);
  }

  public int IndexOf(PdfAction action)
  {
    return action != null ? this.List.IndexOf((object) action) : throw new ArgumentNullException(nameof (action));
  }

  public void Insert(int index, PdfAction action)
  {
    if (action == null)
      throw new ArgumentNullException(nameof (action));
    this.DoInsert(index, action);
  }

  public void Remove(PdfAction action)
  {
    if (action == null)
      throw new ArgumentNullException(nameof (action));
    this.DoRemove(action);
  }

  public void RemoveAt(int index) => this.DoRemoveAt(index);

  private PdfAction this[int index] => (PdfAction) this.List[index];
}
