// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Server.AutoSelectionCache.AutoSelectionRuleCache
// Assembly: Intermech.AutoSelection.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 89DFCE1C-C473-4D66-BEC0-EFA8A5FDFD64
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.AutoSelection.Server.dll

using System;

#nullable disable
namespace Intermech.AutoSelection.Server.AutoSelectionCache;

internal class AutoSelectionRuleCache
{
  private long _selectionRuleID;
  private Guid _objectTypeGuid;
  private Guid _attributeTypeGuid;
  private int _typeLinked;
  private long _orderID;

  private void InitData()
  {
    this._typeLinked = 0;
    this._orderID = 0L;
  }

  public AutoSelectionRuleCache(long selectionRuleID, Guid objectTypeGuid, Guid attributeTypeGuid)
  {
    this._selectionRuleID = selectionRuleID;
    this._objectTypeGuid = objectTypeGuid;
    this._attributeTypeGuid = attributeTypeGuid;
    this.InitData();
  }

  public long SelectionRuleID
  {
    get => this._selectionRuleID;
    set => this._selectionRuleID = value;
  }

  public Guid ObjectTypeGuid
  {
    get => this._objectTypeGuid;
    set => this._objectTypeGuid = value;
  }

  public Guid AttributeTypeGuid
  {
    get => this._attributeTypeGuid;
    set => this._attributeTypeGuid = value;
  }

  public int TypeLinked
  {
    get => this._typeLinked;
    set => this._typeLinked = value;
  }

  public long OrderID
  {
    get => this._orderID;
    set => this._orderID = value;
  }
}
