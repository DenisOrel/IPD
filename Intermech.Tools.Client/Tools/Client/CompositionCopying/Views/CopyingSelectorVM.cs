// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Views.CopyingSelectorVM
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Tools.Client.CompositionCopying.Model;
using Intermech.Tools.Client.CompositionCopying.Model.Operations;
using Intermech.UI;
using System;
using System.ComponentModel;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Views;

internal sealed class CopyingSelectorVM : ViewModel
{
  private DBObjectGraphVertexReference vertexReference;
  private string hint;
  private static readonly CopyingSelectorEntry userChoiceEntry = CopyingSelectorEntry.CreateByUserChoise();

  public CopyingSelectorVM(DBObjectGraphVertexReference vertexReference)
  {
    this.vertexReference = vertexReference != null ? vertexReference : throw new ArgumentNullException(nameof (vertexReference));
    this.hint = string.Empty;
    this.UpdateHint();
    this.AttachToVertexEvents();
  }

  private void AttachToVertexEvents()
  {
    CopyingSelector copyingSelector = this.Vertex.CopyingSelector;
    PropertyChangedEventManager.AddHandler((INotifyPropertyChanged) copyingSelector, new EventHandler<PropertyChangedEventArgs>(this.OnVertexIsSelectedChanged), "IsSelected");
    PropertyChangedEventManager.AddHandler((INotifyPropertyChanged) copyingSelector, new EventHandler<PropertyChangedEventArgs>(this.OnVertexIsSelectedByRuleChanged), "IsSelectedByRule");
  }

  private void OnVertexIsSelectedChanged(object sender, PropertyChangedEventArgs e)
  {
    this.RaisePropertyChanged("IsSelected");
  }

  private void OnVertexIsSelectedByRuleChanged(object sender, PropertyChangedEventArgs e)
  {
    this.RaisePropertyChanged("IsUserEditable");
    this.UpdateHint();
  }

  private void UpdateHint()
  {
    string hint = this.CalculateHint();
    if (!(this.hint != hint))
      return;
    this.hint = hint;
    this.RaisePropertyChanged("Hint");
    this.RaisePropertyChanged("HasHint");
  }

  private string CalculateHint()
  {
    if (this.Vertex.CopyingSelector.IsSelectedByRule)
    {
      CopyingSelectorEntry firstEntryByRule = this.Vertex.CopyingSelector.TryGetFirstEntryByRule();
      if (firstEntryByRule != null)
        return firstEntryByRule.StartVertex == null || this.Vertex.Equals(firstEntryByRule.StartVertex) ? $"Применено правило '{firstEntryByRule.Description}'" : string.Format("Связан с документом '{1}', к которому применено правило '{0}'", (object) firstEntryByRule.Description, (object) firstEntryByRule.StartVertex.Caption);
    }
    return string.Empty;
  }

  public CopyingSession Session => this.vertexReference.Session;

  public DBObjectGraphVertex Vertex => this.vertexReference.Vertex;

  public bool IsSelected
  {
    [DebuggerStepThrough] get => this.Vertex.CopyingSelector.IsSelected;
    set
    {
      if (!this.IsUserEditable)
        return;
      if (value)
        new AddCopyingSelectorEntryRecursive().Invoke(this.Session, this.Vertex, CopyingSelectorVM.userChoiceEntry);
      else
        new RemoveCopyingSelectorEntryRecursive().Invoke(this.Session, this.Vertex, CopyingSelectorVM.userChoiceEntry);
    }
  }

  public bool IsUserEditable
  {
    [DebuggerStepThrough] get => !this.Vertex.CopyingSelector.IsSelectedByRule;
  }

  public bool HasHint
  {
    [DebuggerStepThrough] get => this.hint != string.Empty;
  }

  public string Hint
  {
    [DebuggerStepThrough] get => this.hint;
  }
}
