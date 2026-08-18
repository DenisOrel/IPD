// Decompiled with JetBrains decompiler
// Type: Intermech.AltiumDesigner.Interfaces.IParametrable
// Assembly: Intermech.AltiumDesigner.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 357260E7-5A80-47BF-ACBE-640FBCD2EDB1
// Assembly location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.AltiumDesigner.Interfaces.xml

using Intermech.Data;
using System;

#nullable disable
namespace Intermech.AltiumDesigner.Interfaces;

/// <summary>
/// Объекты (документы, элементы схем), содержащие параметры
/// </summary>
public interface IParametrable : IValueBagContainer, IIdentification
{
  /// <summary>Коллекция параметров</summary>
  Parameter[] Parameters { get; set; }

  /// <summary>Добавить новый параметр</summary>
  /// <param name="parameter"></param>
  void AddNewParameter(Parameter parameter);

  /// <summary>Установить новое значение параметру</summary>
  /// <param name="name">Имя параметра</param>
  /// <param name="type">Тип</param>
  /// <param name="parameterValue">Значение</param>
  void SetParameterValue(string name, Type type, object parameterValue);
}
