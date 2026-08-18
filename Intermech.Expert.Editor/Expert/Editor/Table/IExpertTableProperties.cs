// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.Table.IExpertTableProperties
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using System;

#nullable disable
namespace Intermech.Expert.Editor.Table;

/// <summary>Интерфейс для хранения настроек отображения таблиц</summary>
public interface IExpertTableProperties
{
  /// <summary>Использовать краткие имена для типов объектов</summary>
  bool UseShortName4ObjectType { get; set; }

  /// <summary>Использовать краткие имена для типов атрибутов</summary>
  bool UseShortName4AttributeType { get; set; }

  /// <summary>Сообщение об изменении</summary>
  event EventHandler Changed;
}
