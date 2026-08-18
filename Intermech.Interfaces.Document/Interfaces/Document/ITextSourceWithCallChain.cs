// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Document.ITextSourceWithCallChain
// Assembly: Intermech.Interfaces.Document, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: BAA3ECE6-453D-42EC-A7D3-172F1348C93D
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Document.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Document.xml

using System.Collections.Generic;

#nullable disable
namespace Intermech.Interfaces.Document;

/// <summary>Источник текстовых данных, который может ссылаться на другие ссылки с возможностью циклов</summary>
public interface ITextSourceWithCallChain : ITextSource
{
  /// <summary>Назначить значение Text</summary>
  /// <param name="value">Значение</param>
  /// <param name="saveUndo">Сохранять данные для Undo</param>
  /// <param name="updateUI">Обновить изображение</param>
  /// <param name="updateLayout">Обновить разбивку</param>
  /// <param name="callChain">Цепочка вызовов, для защиты от зацикливания</param>
  void SetText(
    string value,
    bool saveUndo,
    bool updateUI,
    bool updateLayout,
    List<DocumentTreeNode> callChain);

  /// <summary>Получить текст с защитой от циклических ссылок</summary>
  /// <param name="callChain">Цепочка вызовов, для защиты от зацикливания</param>
  /// <returns></returns>
  string GetAcyclicText(List<DocumentTreeNode> callChain);
}
