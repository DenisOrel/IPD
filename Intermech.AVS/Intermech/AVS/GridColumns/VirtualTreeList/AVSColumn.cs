// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.GridColumns.VirtualTreeList.AVSColumn
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using Infralution.Controls.VirtualTree;
using Intermech.Interfaces.AVS;
using System;

#nullable disable
namespace Intermech.AVS.GridColumns.VirtualTreeList;

/// <summary>Колонка</summary>
public class AVSColumn : Column
{
  private ColumnTag tag;
  private bool checkEdit;
  private bool readOnly;

  public AVSColumn() => this.Sortable = false;

  public AVSColumn(AvsRowAttributeInfo attrInfo, int width)
  {
    this.Caption = attrInfo != null ? attrInfo.Name : throw new ArgumentNullException(nameof (attrInfo));
    this.Name = attrInfo.Name;
    this.Width = width;
    this.Tag = new ColumnTag(attrInfo.Clone());
  }

  public ColumnTag Tag
  {
    get => this.tag;
    set => this.tag = value;
  }

  public bool CheckEdit
  {
    get => this.checkEdit;
    set => this.checkEdit = value;
  }

  public bool ReadOnly
  {
    get => this.readOnly;
    set => this.readOnly = value;
  }

  public override bool Pinned
  {
    get => base.Pinned;
    set => base.Pinned = value;
  }
}
