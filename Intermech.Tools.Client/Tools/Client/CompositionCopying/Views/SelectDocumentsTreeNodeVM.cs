// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Views.SelectDocumentsTreeNodeVM
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.UI;
using System;
using System.Collections.ObjectModel;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Views;

internal sealed class SelectDocumentsTreeNodeVM : ViewModel
{
  private string caption;
  private SelectDocumentsTreeNodeVM parentNode;
  private ObservableCollection<SelectDocumentsTreeNodeVM> nodes;
  private DBObjectGraphVertexReference vertexReference;
  private bool isVirtual;
  private CopyingSelectorVM copyingSelector;
  private bool isExpanded;

  public SelectDocumentsTreeNodeVM(string caption, DBObjectGraphVertexReference vertexReference = null)
  {
    this.caption = caption != null ? caption : throw new ArgumentNullException(nameof (caption));
    this.nodes = new ObservableCollection<SelectDocumentsTreeNodeVM>();
    this.vertexReference = vertexReference;
    if (this.vertexReference != null)
      this.copyingSelector = new CopyingSelectorVM(this.vertexReference);
    else
      this.isVirtual = true;
  }

  internal void InitializeParentNode(SelectDocumentsTreeNodeVM parentNode)
  {
    if (parentNode == null)
      throw new ArgumentNullException(nameof (parentNode));
    this.parentNode = this.parentNode == null ? parentNode : throw new InvalidOperationException();
  }

  public string Caption => this.caption;

  public DBObjectGraphVertexReference VertexReference => this.vertexReference;

  public bool IsVirtual => this.isVirtual;

  public SelectDocumentsTreeNodeVM ParentNode => this.parentNode;

  public ObservableCollection<SelectDocumentsTreeNodeVM> Nodes => this.nodes;

  public bool IsExpanded
  {
    get => this.isExpanded;
    set
    {
      if (this.isExpanded == value)
        return;
      this.isExpanded = value;
      this.RaisePropertyChanged(nameof (IsExpanded));
    }
  }

  public CopyingSelectorVM CopyingSelector => this.copyingSelector;
}
