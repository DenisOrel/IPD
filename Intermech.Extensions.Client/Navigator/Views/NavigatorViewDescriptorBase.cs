// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Views.NavigatorViewDescriptorBase
// Assembly: Intermech.Extensions.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8EE4EE90-67E9-496B-9E84-18C409B882FC
// Assembly location: D:\IPS\Client\Intermech.Extensions.Client.dll

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Navigator.Views;

public abstract class NavigatorViewDescriptorBase
{
  private bool _viewWasRegistered;

  [NotNull]
  public Type ViewType { get; }

  [NotNull]
  [NotWhitespace]
  public string Name { get; }

  [NotNull]
  public string Caption { get; }

  [NotNull]
  public string Hint { get; }

  [NotNull]
  public string Module { get; }

  [NotNull]
  public string ImageName { get; }

  public int OrderID { get; }

  public int TriggerPriority { get; }

  public int HelpTopicID { get; }

  public bool SupportMultipleSelection { get; }

  protected NavigatorViewDescriptorBase(
    [NotNull] Type viewType,
    [CanBeNull, NotEmpty] string name = null,
    [NotNull] string caption = "",
    [NotNull] string hint = "",
    [NotNull] string module = "",
    [NotNull] string imageName = "",
    int orderID = 0,
    int triggerPriority = 0,
    int helpTopicID = 0,
    bool supportMultipleSelection = false)
  {
    this.ViewType = viewType;
    this.Name = name ?? viewType.Name;
    this.Caption = caption;
    this.Hint = hint;
    this.Module = module;
    this.ImageName = imageName;
    this.OrderID = orderID;
    this.TriggerPriority = triggerPriority;
    this.HelpTopicID = helpTopicID;
    this.SupportMultipleSelection = supportMultipleSelection;
  }

  public override int GetHashCode() => this.ViewType.GetHashCode();

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  internal void CheckViewRegistered()
  {
    if (this._viewWasRegistered)
      return;
    AdjustableViewsHelper.RegisterView(this.Name, this.Caption, this.Hint, this.Module, this.ImageName, true, this.OrderID);
    this._viewWasRegistered = true;
  }

  public override string ToString() => this.Name;
}
