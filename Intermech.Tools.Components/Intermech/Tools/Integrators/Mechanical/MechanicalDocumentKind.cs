// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Mechanical.MechanicalDocumentKind
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Integrators.Mechanical;

/// <summary>
/// Группы документов, известные обработчику конструкторских документов.
/// </summary>
/// <remarks>
/// У разных интеграторов конструкторская схема обработки документов имеет разную
/// структуру. Например, в 2D все документы делятся на две группы - модели и
/// вспомогательные документы, а в 3D документы делятся на пять групп - модели,
/// модели стандартных, чертежи моделей, прочие документы, вспомогательные документы.
/// Это перечисление устанавливает соответствие между группами документов, поддерживаемых
/// обработчиком конструкторских документов, и конструкторской схемой конкретного
/// интегратора.
/// </remarks>
public enum MechanicalDocumentKind
{
  /// <summary>
  /// Сборочная модель (конструкторский документ, по которому выпускаются изделия)
  /// </summary>
  AssemblyModel,
  /// <summary>
  /// Модель детали (конструкторский документ, по которому выпускаются изделия)
  /// </summary>
  PartModel,
  /// <summary>
  /// Модель стандартного, созданная автоматически на основе справочной информации (например, модель стандартного CADMECH)
  /// </summary>
  StandardModel,
  /// <summary>Сборочный чертеж (связан с моделью по имени файла)</summary>
  AssemblyDrawing,
  /// <summary>
  /// Чертеж на модель детали (связан с моделью по имени файла)
  /// </summary>
  PartDrawing,
  /// <summary>
  /// Нестандартный документ, не являющийся частью общем модели обработки конструкторских документов. Обрабатывается интегратором специальным образом
  /// </summary>
  GenericDocument,
}
