// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.SingleTypedObjectViewProvider`1
// Assembly: Intermech.Extensions.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8EE4EE90-67E9-496B-9E84-18C409B882FC
// Assembly location: D:\IPS\Client\Intermech.Extensions.Client.dll

using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.Navigator;

public class SingleTypedObjectViewProvider<TView> : IViewsProvider where TView : class, IView
{
  [CanBeNull]
  private readonly SingleTypedObjectViewProvider<TView>.DoShowViewDelegate _doShowView;
  [NotNull]
  [NotWhitespace]
  private readonly string _name;
  [NotNull]
  private readonly string _caption;
  [NotNull]
  private readonly string _hint;
  [NotNull]
  private readonly string _module;
  [NotNull]
  private readonly string _imageName;
  private readonly bool _visible;
  private readonly int _orderID;
  private readonly int _triggerPriority;
  private readonly int _helpTopicID;
  private static bool _viewWasRegistered;

  [NotEmpty]
  public int ObjectTypeID { get; }

  private SingleTypedObjectViewProvider(
    [NotNull, NotEmpty] string name,
    [NotNull] string caption,
    [CanBeNull] SingleTypedObjectViewProvider<TView>.DoShowViewDelegate doShowView,
    [NotNull] string hint,
    [NotNull] string module,
    [NotNull] string imageName,
    bool visible,
    int orderID,
    int triggerPriority,
    int helpTopicID)
  {
    this._name = name;
    this._caption = caption;
    this._doShowView = doShowView;
    this._hint = hint;
    this._module = module;
    this._imageName = imageName;
    this._visible = visible;
    this._orderID = orderID;
    this._triggerPriority = triggerPriority;
    this._helpTopicID = helpTopicID;
  }

  public SingleTypedObjectViewProvider(
    [NotEmpty] int objectTypeID,
    [NotNull, NotEmpty] string name = "",
    [NotNull] string caption = "",
    [CanBeNull] SingleTypedObjectViewProvider<TView>.DoShowViewDelegate doShowView = null,
    [NotNull] string hint = "",
    [NotNull] string module = "",
    [NotNull] string imageName = "",
    bool visible = true,
    int orderID = 0,
    int triggerPriority = 0,
    int helpTopicID = 0)
    : this(name, caption, doShowView, hint, module, imageName, visible, orderID, triggerPriority, helpTopicID)
  {
    this.ObjectTypeID = objectTypeID;
  }

  public SingleTypedObjectViewProvider(
    [NotEmpty] Guid objectTypeGuid,
    [NotNull, NotEmpty] string name = "",
    [NotNull] string caption = "",
    [CanBeNull] SingleTypedObjectViewProvider<TView>.DoShowViewDelegate doShowView = null,
    [NotNull] string hint = "",
    [NotNull] string module = "",
    [NotNull] string imageName = "",
    bool visible = true,
    int orderID = 0,
    int triggerPriority = 0,
    int helpTopicID = 0)
    : this(name, caption, doShowView, hint, module, imageName, visible, orderID, triggerPriority, helpTopicID)
  {
    this.ObjectTypeID = MetaDataHelperService.Instance.GetObjectTypeID(objectTypeGuid);
  }

  public ViewsInfo GetViews([CanBeNull] ISelectedItems items, IServiceProvider services)
  {
    if (items != null && items.Count == 1)
    {
      IDBTypedObjectID itemData = items.GetItemData<IDBTypedObjectID>(0, false);
      if (itemData == null || this._doShowView != null && !this._doShowView(itemData))
        return ViewsInfo.Empty;
      int objectType = itemData.ObjectType;
      if (objectType != -1 && objectType == this.ObjectTypeID || MetaDataHelperService.Instance.IsObjectTypeChildOf(objectType, this.ObjectTypeID))
      {
        if (!SingleTypedObjectViewProvider<TView>._viewWasRegistered)
        {
          AdjustableViewsHelper.RegisterView(this._name, this._caption, this._hint, this._module, this._imageName, this._visible, this._orderID);
          SingleTypedObjectViewProvider<TView>._viewWasRegistered = true;
        }
        new ViewsInfo().Add(nameof (TView), this._helpTopicID != 0 ? new ViewInfo(this._triggerPriority, this._helpTopicID, typeof (TView)) : new ViewInfo(this._triggerPriority, typeof (TView)));
      }
    }
    return ViewsInfo.Empty;
  }

  public delegate bool DoShowViewDelegate([NotNull] IDBTypedObjectID objID) where TView : class, IView;
}
