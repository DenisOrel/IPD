// Decompiled with JetBrains decompiler
// Type: Intermech.Document.UI.DocEditorPropertyPageType
// Assembly: Intermech.Document.Model, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: FEA44A44-A9AA-4CE5-9D41-60F8B1EE2840
// Assembly location: D:\IPS\Client\Intermech.Document.Model.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Model.xml

#nullable disable
namespace Intermech.Document.UI;

/// <summary>Тип страницы свойств</summary>
public enum DocEditorPropertyPageType
{
  /// <summary>Не определен</summary>
  Unknown,
  /// <summary>Страничка представляет собой элемент, основанный на UserControl</summary>
  Control,
  /// <summary>На страничке отображаются свойства объекта. Свойства объекта изменяются в PropertyGrid</summary>
  Object,
}
