// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.XmlExchange.XmlExportExtraDataMode
// Assembly: Intermech.Interfaces.XmlExchange, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 28E8BDE9-A52D-45A9-B86E-D22E5A0BD9E6
// Assembly location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.XmlExchange.xml

using Intermech.Localization.Xml;
using System;
using System.ComponentModel;

#nullable disable
namespace Intermech.Interfaces.XmlExchange;

/// <summary>Режимы выгрузки дополнительных данных</summary>
[Flags]
[TypeConverter(typeof (EnumDescConverter))]
public enum XmlExportExtraDataMode
{
  /// <summary>Дополнительные данные не выгружаются</summary>
  [CustomDescription("Attribute.Interfaces.XmlExchange_7")] None = 0,
  /// <summary>Выгрузка объектов для ссылочных атрибутов</summary>
  [CustomDescription("Attribute.Interfaces.XmlExchange_8")] RefObj4Attributes = 1,
  /// <summary>Права доступа</summary>
  Rights = 2,
  /// <summary>Шаги ЖЦ</summary>
  LcStep = 4,
  /// <summary>История шагов ЖЦ</summary>
  LcStepHist = 8,
  /// <summary>Рабочие копии объектов</summary>
  [CustomDescription("Attribute.Interfaces.XmlExchange_9")] ObjectWorkCopies = 16, // 0x00000010
  /// <summary>Выгрузка связей для ссылочных объектов</summary>
  [CustomDescription("Attribute.Interfaces.XmlExchange_10")] RefObjectRelation = 32, // 0x00000020
  /// <summary>Выгрузка информации о ссылках для ссылочных объектов</summary>
  [CustomDescription("Attribute.Interfaces.XmlExchange_11")] RefObjectReference = 64, // 0x00000040
  /// <summary>
  /// Выгрузка объектов, связей, атрибутов у которых прописаны пользовательские псевдонимы (UserID)
  /// </summary>
  [CustomDescription("Attribute.Interfaces.XmlExchange_12")] UserDataOnly = 128, // 0x00000080
  /// <summary>Выгружать файлы ico для типов объектов</summary>
  [CustomDescription("Attribute.Interfaces.XmlExchange_13")] IconFiles = 256, // 0x00000100
}
