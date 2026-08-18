// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.AttributesDescribers.TableRecordRefPropertyClass
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using System;
using System.Diagnostics;

#nullable disable
namespace Intermech.Imbase.AttributesDescribers;

[DebuggerDisplay("TableRecordRef: {TableRecordRef,nq}")]
[Serializable]
public class TableRecordRefPropertyClass
{
  private bool keyProceed;
  private string tableRecordRefToString = string.Empty;
  private string tableRecordRef = string.Empty;

  public string TableRecordRef => this.tableRecordRef;

  public TableRecordRefPropertyClass(string aTableRecordRef)
  {
    this.tableRecordRef = aTableRecordRef;
  }

  public override bool Equals(object obj)
  {
    return obj is TableRecordRefPropertyClass ? ((TableRecordRefPropertyClass) obj).TableRecordRef.Equals(this.tableRecordRef) : base.Equals(obj);
  }

  [DebuggerStepThrough]
  public override string ToString()
  {
    if (!this.keyProceed)
    {
      this.tableRecordRefToString = (string) new TableRecordRefFlagConverter().ConvertTo((object) this.tableRecordRef, typeof (string));
      this.keyProceed = true;
    }
    return this.tableRecordRefToString;
  }
}
