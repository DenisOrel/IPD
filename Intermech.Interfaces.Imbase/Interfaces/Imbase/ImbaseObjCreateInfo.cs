// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Imbase.ImbaseObjCreateInfo
// Assembly: Intermech.Interfaces.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A581041C-8E97-4E18-8E61-00F942ADD7DC
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Imbase.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Imbase.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Imbase;

/// <summary>
/// Параметры создания объектов каталогов/справочников Imbase
/// </summary>
[Serializable]
/// <summary>
/// 
/// </summary>
/// <param name="objectType"></param>
/// <param name="createMode"></param>
public struct ImbaseObjCreateInfo(int objectType, ImbaseObjCreateMode createMode)
{
  /// <summary>Тип создаваемого объекта</summary>
  public int ObjectType = objectType;
  /// <summary>Режим создания объектов</summary>
  public ImbaseObjCreateMode CreateMode = createMode;
}
