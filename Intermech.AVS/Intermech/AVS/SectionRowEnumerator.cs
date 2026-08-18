// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.SectionRowEnumerator
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.AVS;

/// <summary>Enumerator для записей спецификации в SpecificationSection</summary>
public class SectionRowEnumerator : IEnumerator<AVSRow>, IDisposable, IEnumerator
{
  private SpecificationSection section;
  private AVSRow currentSpecRow;
  private int rowIndex = -1;

  public SectionRowEnumerator(SpecificationSection section)
  {
    this.section = section != null ? section : throw new ArgumentNullException(nameof (section));
  }

  public AVSRow Current
  {
    [DebuggerStepThrough] get
    {
      if (this.section == null)
        throw new InvalidOperationException("section == null");
      if (this.rowIndex < 0)
        throw new InvalidOperationException("Call to the MoveNext method first");
      return this.currentSpecRow != null ? this.currentSpecRow : throw new InvalidOperationException("Call to the Reset and MoveNext method first");
    }
  }

  public void Dispose()
  {
    this.section = (SpecificationSection) null;
    this.Reset();
  }

  object IEnumerator.Current
  {
    [DebuggerStepThrough] get => (object) this.Current;
  }

  public bool MoveNext()
  {
    if (this.section == null)
      throw new InvalidOperationException("section == null");
    if (this.currentSpecRow != null)
    {
      if (this.rowIndex >= 0 && this.rowIndex < this.section.Rows.Count && this.currentSpecRow != this.section.Rows[this.rowIndex])
        throw new InvalidOperationException("Спецификация была изменена! /currentSpecRow != section.Rows[rowIndex]/");
    }
    else if (this.rowIndex >= 0)
      return false;
    ++this.rowIndex;
    if (this.rowIndex >= 0 && this.rowIndex < this.section.Rows.Count)
    {
      this.currentSpecRow = this.section.Rows[this.rowIndex];
      return true;
    }
    this.currentSpecRow = (AVSRow) null;
    return false;
  }

  public void Reset()
  {
    this.rowIndex = -1;
    this.currentSpecRow = (AVSRow) null;
  }
}
