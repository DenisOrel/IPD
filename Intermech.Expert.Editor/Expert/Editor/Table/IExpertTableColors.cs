// Decompiled with JetBrains decompiler
// Type: Intermech.Expert.Editor.Table.IExpertTableColors
// Assembly: Intermech.Expert.Editor, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 3CFAE7BC-E854-46EE-B57C-5E15FC8B5CD5
// Assembly location: D:\IPS\Client\Intermech.Expert.Editor.dll
// XML documentation location: D:\IPS\Client\Intermech.Expert.Editor.xml

using System;
using System.Xml;

#nullable disable
namespace Intermech.Expert.Editor.Table;

/// <summary>Интерфейс раскраски таблиц</summary>
public interface IExpertTableColors
{
  /// <summary>Цвета для раскраски входных вертикальных данных</summary>
  IExpertTableDestColors InputVert { get; }

  /// <summary>Цвета для раскраски входных горизонтальных данных</summary>
  IExpertTableDestColors InputHorz { get; }

  /// <summary>Цвета для раскраски выходных данных</summary>
  IExpertTableItemColors Output { get; }

  /// <summary>Цвета для раскраски данных</summary>
  IExpertTableItemColors Data { get; }

  /// <summary>Событие на изменения цветов</summary>
  event EventHandler Changed;

  /// <summary>Метод сохранения данных в XML</summary>
  /// <param name="xmlDoc">Ссылка на XmlDocument</param>
  /// <returns>XmlNode c данными</returns>
  XmlNode Save(XmlDocument xmlDoc);
}
