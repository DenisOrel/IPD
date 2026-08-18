// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.PdmConfigurator.IDBConfiguratorOption
// Assembly: Intermech.Interfaces.PdmConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 6A3EF664-00FF-4A8A-A8E2-24964457B937
// Assembly location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.PdmConfigurator.xml

#nullable disable
namespace Intermech.Interfaces.PdmConfigurator;

/// <summary>
/// Интерфейс обработчика информационных объектов "Опции" конфигуратора составов IPS
/// </summary>
public interface IDBConfiguratorOption : IDBObject, IDBAttributable, IDBSessionable, IPluginsData
{
  /// <summary>Флажки опции</summary>
  OptionFlags OptionFlags { get; set; }

  /// <summary>Категория опции</summary>
  long OptionCategory { get; set; }

  /// <summary>Код опции</summary>
  string OptionCode { get; set; }

  /// <summary>Примечание</summary>
  string OptionDescription { get; set; }

  /// <summary>
  /// Тип данных опции. Допускаются значения ftString, ftInteger, ftDouble, ftDateTime, ftBoolean
  /// </summary>
  FieldTypes OptionDataType { get; set; }

  /// <summary>Коллекция значений опции</summary>
  OptionValuesCollection OptionValues { get; set; }
}
