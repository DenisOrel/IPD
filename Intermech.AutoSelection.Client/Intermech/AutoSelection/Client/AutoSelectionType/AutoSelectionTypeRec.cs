// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.AutoSelectionType.AutoSelectionTypeRec
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using System;

#nullable disable
namespace Intermech.AutoSelection.Client.AutoSelectionType;

internal class AutoSelectionTypeRec
{
  private AutoSelectionNodeType _type;
  private string _name = string.Empty;

  public AutoSelectionTypeRec(AutoSelectionNodeType type) => this.Type = type;

  public AutoSelectionNodeType Type
  {
    get => this._type;
    set
    {
      this._type = value;
      this._name = EnumDescConverter.GetEnumDescription((Enum) value);
    }
  }

  public string Name => this._name;
}
