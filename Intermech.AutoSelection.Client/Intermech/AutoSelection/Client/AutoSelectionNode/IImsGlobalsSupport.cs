// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.IImsGlobalsSupport
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

internal interface IImsGlobalsSupport
{
  IEnumerable<Guid> GetMetaDataGuids(IMSGlobals type);

  ICollection<Guid> CollectMetaDataGuids(IMSGlobals type, ICollection<Guid> collector);
}
