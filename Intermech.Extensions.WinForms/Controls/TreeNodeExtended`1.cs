// Decompiled with JetBrains decompiler
// Type: Intermech.Controls.TreeNodeExtended`1
// Assembly: Intermech.Extensions.WinForms, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3916F87A-AB63-4AB0-AEED-84AD5AFAF5F4
// Assembly location: D:\IPS\Client\Intermech.Extensions.WinForms.dll

using Intermech.Diagnostics;
using System.Runtime.Serialization;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Controls;

public class TreeNodeExtended<TChildNode> : TreeNodeExtendedBase where TChildNode : TreeNodeExtendedBase
{
  [CanBeNull]
  [NotNullAfter("CreateAndLoadTreeNodeCollection()")]
  [ItemNotNull]
  private TreeNodeExtendedCollection<TChildNode> _nodes;

  protected TreeNodeExtended()
  {
  }

  protected TreeNodeExtended([NotNull] string text)
    : base(text)
  {
  }

  protected TreeNodeExtended([NotNull] string text, [CanBeEmpty] int imageIndex)
    : base(text, imageIndex)
  {
  }

  protected TreeNodeExtended([NotNull] SerializationInfo serializationInfo, StreamingContext context)
    : base(serializationInfo, context)
  {
  }

  [NotNull]
  [ItemNotNull]
  public virtual TreeNodeExtendedCollection<TChildNode> Nodes
  {
    get => this._nodes ?? this.CreateAndLoadTreeNodeCollection();
  }

  [NotNull]
  [ItemNotNull]
  private TreeNodeExtendedCollection<TChildNode> CreateAndLoadTreeNodeCollection()
  {
    this._nodes = new TreeNodeExtendedCollection<TChildNode>(this);
    if (this.HasNestedNodes)
      this.LoadNestedNodes();
    return this._nodes;
  }

  [NotNull]
  [ItemNotNull]
  protected internal TreeNodeCollection OriginalNodes => base.Nodes;
}
