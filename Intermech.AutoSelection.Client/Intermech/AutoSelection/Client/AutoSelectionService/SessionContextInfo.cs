// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionService.SessionContextInfo
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using System.Collections.Generic;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionService;

internal class SessionContextInfo
{
  private readonly IList<long> _objectIds = (IList<long>) new List<long>();
  private readonly IList<long> _relationIds = (IList<long>) new List<long>();

  public IList<long> ObjectIds => this._objectIds;

  public IList<long> RelationIds => this._relationIds;
}
