// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.GrabHandleZone
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Зона захвата</summary>
public enum GrabHandleZone
{
  /// <summary>Центр, управляет перемещением всего элемента</summary>
  Center,
  /// <summary>Верхняя граница</summary>
  Top,
  /// <summary>Правая граница</summary>
  Right,
  /// <summary>Нижняя граница</summary>
  Bottom,
  /// <summary>Левая граница</summary>
  Left,
}
