// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ITextSource
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Источник текстовых данных</summary>
public interface ITextSource
{
  /// <summary>Текст</summary>
  string Text { get; set; }

  /// <summary>Назначить значение Text</summary>
  /// <param name="value">Значение</param>
  /// <param name="saveUndo">Сохранять данные для Undo</param>
  /// <param name="updateUI">Обновить изображение</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  void SetText(string value, bool saveUndo, bool updateUI, bool updateLayout);

  /// <summary>Присвоить значение переменной Text без вызова обработчиков. Для внутреннего пользования!</summary>
  /// <param name="value">Значение</param>
  void AssignText(string value);

  /// <summary>Только для чтения</summary>
  bool ReadOnly { get; }

  /// <summary>Событие изменения текста</summary>
  event TextChanged_EventHandler TextChanged;
}
