// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.RowItem
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

public class RowItem
{
  private List<AVSRow> rows = new List<AVSRow>();
  private Dictionary<AVSRow, AVSRow[]> formBNumbers = new Dictionary<AVSRow, AVSRow[]>();

  public List<AVSRow> Rows
  {
    get => this.rows;
    set => this.rows = value;
  }

  public Dictionary<AVSRow, AVSRow[]> FormBNumbers
  {
    get => this.formBNumbers;
    set => this.formBNumbers = value;
  }
}
