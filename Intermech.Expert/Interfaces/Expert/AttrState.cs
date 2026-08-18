// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Expert.AttrState
// Assembly: Intermech.Expert, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 23A627F6-725A-4579-B6EF-74B0D09DF1F0
// Assembly location: D:\IPS\Client\Intermech.Expert.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.Expert;

/// <summary>Тип рассчитанного значения</summary>
[TypeConverter(typeof (EnumDescConverter))]
[Category("Expert System")]
[Serializable]
public enum AttrState
{
  /// <summary>Значение атрибута неизвестно</summary>
  Unknown,
  /// <summary>Значение установлено пользователем</summary>
  SetByUser,
  /// <summary>Значение рассчитано экспертной системой</summary>
  Calculated,
}
