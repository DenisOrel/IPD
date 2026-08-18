// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.CADInterface.ArticleProcessingMethod
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Integrators.CADInterface;

/// <summary>
/// Описывает разновидность изделия и способ его обработки с точки зрения конструкторского документа, в котором это изделие описано.
/// </summary>
public enum ArticleProcessingMethod
{
  /// <summary>
  /// Изделие описано в самом обычном конструкторском документе. Такое изделие создается и изменяется интегратором
  /// с соответствующей CAD-системой
  /// </summary>
  NormalObject,
  /// <summary>
  /// Объект описан в самом обычном конструкторском документе, только вместо изделия создается материал. Такое объект
  /// создается и изменяется интегратором с соответствующей CAD-системой
  /// </summary>
  MinorMaterial,
  /// <summary>
  /// Изделие описано в документе, связанном с Imbase с помощью ключа. Такое изделие создается и изменяется только
  /// через сервисы Imbase
  /// </summary>
  ImbaseObject,
}
