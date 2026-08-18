// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.GrabHandlePoint
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Перечисление точек перетаскивания по часовой стрелке</summary>
public enum GrabHandlePoint
{
  /// <summary>Левый верхний угол</summary>
  LeftTop,
  /// <summary>Центр верхней границы</summary>
  TopMiddle,
  /// <summary>Правый верхний угол</summary>
  RightTop,
  /// <summary>Центр правой границы</summary>
  RightMiddle,
  /// <summary>Центр нижней границы</summary>
  BottomMiddle,
  /// <summary>Левый нижний угол</summary>
  LeftBottom,
  /// <summary>Центр левой границы</summary>
  LeftMiddle,
  /// <summary>Правый нижний угол</summary>
  RightBottom,
  /// <summary>Центр, управляет перемещением всего элемента</summary>
  Center,
}
