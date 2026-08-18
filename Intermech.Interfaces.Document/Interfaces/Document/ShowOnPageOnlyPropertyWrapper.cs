// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ShowOnPageOnlyPropertyWrapper
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using Intermech.Localization;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Document;

[TypeConverter(typeof (ShowOnPageOnlyConverter))]
public class ShowOnPageOnlyPropertyWrapper
{
  public PageElementNode OwnerNode;
  internal static string[] FieldsOrder = new string[3]
  {
    nameof (FirstDataPage),
    nameof (NextDataPage),
    nameof (LastDataPage)
  };

  public ShowOnPageOnlyPropertyWrapper(PageElementNode ownerNode) => this.OwnerNode = ownerNode;

  internal bool IsReadOnly
  {
    get
    {
      PageElementNode ownerNode = this.OwnerNode;
      return ownerNode == null || ownerNode.HasTemplate();
    }
  }

  [Browsable(false)]
  public bool ShowOnAllPages
  {
    get => this.OwnerNode != null && this.OwnerNode.ShowOnPageOnly == ShowOnPageOnly.All;
  }

  [CustomDisplayName("Attribute.Interfaces.Document_616")]
  [CustomDescription("Attribute.Interfaces.Document_617")]
  [CustomCategory("Attribute.Interfaces.Document_615")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool FirstDataPage
  {
    get => this.CheckFlags(ShowOnPageOnly.FirstDataPage);
    set => this.SetFlags(ShowOnPageOnly.FirstDataPage, value);
  }

  [CustomDisplayName("Attribute.Interfaces.Document_618")]
  [CustomDescription("Attribute.Interfaces.Document_619")]
  [CustomCategory("Attribute.Interfaces.Document_615")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool NextDataPage
  {
    get => this.CheckFlags(ShowOnPageOnly.NextDataPage);
    set => this.SetFlags(ShowOnPageOnly.NextDataPage, value);
  }

  [CustomDisplayName("Attribute.Interfaces.Document_620")]
  [CustomDescription("Attribute.Interfaces.Document_621")]
  [CustomCategory("Attribute.Interfaces.Document_615")]
  [TypeConverter(typeof (CustomBooleanConverter))]
  public bool LastDataPage
  {
    get => this.CheckFlags(ShowOnPageOnly.LastDataPage);
    set => this.SetFlags(ShowOnPageOnly.LastDataPage, value);
  }

  private bool CheckFlags(ShowOnPageOnly flag) => (this.OwnerNode.ShowOnPageOnly & flag) == flag;

  private void SetFlags(ShowOnPageOnly flags, bool value)
  {
    if (value)
      this.OwnerNode.SetShowOnPageOnly(this.OwnerNode.ShowOnPageOnly | flags, true);
    else
      this.OwnerNode.SetShowOnPageOnly(this.OwnerNode.ShowOnPageOnly & ~flags, true);
  }
}
