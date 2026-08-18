// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.MRP.Consts
// Assembly: Intermech.Interfaces.MRP, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 450A2767-EF3B-475F-B784-5AB5004E9964
// Assembly location: D:\IPS\Client\Intermech.Interfaces.MRP.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.MRP.xml

#nullable disable
namespace Intermech.Interfaces.MRP;

/// <summary>Константы плагина Intermech.Interfaces.MRP</summary>
public static class Consts
{
  /// <summary>
  /// Атрибут "Входимость - Сборка", ссылка на объект, тип - "Изделие"
  /// </summary>
  public const string attributeEntersInAssm = "cad001d5-306c-11d8-b4e9-00304f19f545";
  /// <summary>Атрибут "Маршрут обработки по умолчанию", строка</summary>
  public const string attributeIsDefaultRoute = "cad005b9-306c-11d8-b4e9-00304f19f545";
  /// <summary>
  /// Атрибут "Признак изготовления" (1 - Собственное, 2 - Покупное, 3 - По кооперации, 4 - Не изготавливать)
  /// </summary>
  public const string attributeManufacturingSign = "cad0038f-306c-11d8-b4e9-00304f19f545";
  /// <summary>
  /// Атрибут "Учёт изделий в производстве", целочисленный тип, одно значение из списка разрешённых:
  /// 0 - Партиями, 1 - Экземплярами
  /// </summary>
  public const string attributeProductionAccountingOfParts = "cad0058a-306c-11d8-b4e9-00304f19f545";
  /// <summary>Атрибут "Номер производственного заказа"</summary>
  public const string attributeProductionOrderNumber = "cadd93c3-306c-11d8-b4e9-00304f19f545";
  /// <summary>Атрибут "Наименование производственного заказа"</summary>
  public const string attributeProductionOrderName = "cadd93c4-306c-11d8-b4e9-00304f19f545";
  /// <summary>Тип связи "Состав экземпляров и партий изделий"</summary>
  public const string reltypeInstancesAndParties = "cad00584-306c-11d8-b4e9-00304f19f545";
  /// <summary>Признак изготовления изделия - "Собственное"</summary>
  public const long articleIsOwn = 1;
  /// <summary>Признак изготовления изделия - "Покупное"</summary>
  public const long articleIsBought = 2;
  /// <summary>Признак изготовления изделия - "По кооперации"</summary>
  public const long articleIsCooperation = 3;
  /// <summary>Признак изготовления изделия - "Не изготавливать"</summary>
  public const long articleIsDeprecated = 4;
}
