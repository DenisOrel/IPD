// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.ViewType
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;

#nullable disable
namespace Intermech.AVS;

/// <summary>Тип отображения аттрибутов</summary>
[Flags]
public enum ViewType
{
  /// <summary>Только объекты</summary>
  Objects = 1,
  /// <summary>Только связи</summary>
  Links = 2,
  /// <summary>Все</summary>
  All = Links | Objects, // 0x00000003
}
