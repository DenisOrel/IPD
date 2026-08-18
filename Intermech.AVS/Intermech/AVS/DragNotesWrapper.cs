// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.DragNotesWrapper
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Intermech.Interfaces.Document;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AVS;

/// <summary>Вспомогательный клас для дрег дропа примечаний</summary>
public class DragNotesWrapper
{
  private List<DocumentTreeNode> notes;
  private bool isHeaders;
  private List<AVSRow> rows;

  public List<DocumentTreeNode> Notes => this.notes;

  public bool IsHeaders => this.isHeaders;

  public List<AVSRow> Rows
  {
    get => this.rows;
    set => this.rows = value;
  }

  public DragNotesWrapper(List<DocumentTreeNode> Notes, bool IsHeaders)
  {
    this.notes = Notes;
    this.isHeaders = IsHeaders;
  }
}
