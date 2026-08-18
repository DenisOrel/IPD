// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionNode.IItemCommon
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionNode;

internal interface IItemCommon
{
  AS_Guid ObjTypeGuid { get; set; }

  AS_Guid RelTypeGuid { get; set; }
}
