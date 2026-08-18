// Decompiled with JetBrains decompiler
// Type: Intermech.Cadmech.Integrator.IImportStructureFromCadService
// Assembly: Intermech.Cadmech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: A35B043F-5773-4DBE-81D3-C3E493F8C825
// Assembly location: D:\IPS\Client\Intermech.Cadmech.Interfaces.dll
// XML documentation location: D:\IPS\Client\Intermech.Cadmech.Interfaces.xml

#nullable disable
namespace Intermech.Cadmech.Integrator;

/// <summary>
/// Интерфейс AVS для редактирования состава сборочного чертежа. Основное назначение - простановка позиций на чертеже.
/// </summary>
public interface IImportStructureFromCadService
{
  /// <summary>
  /// Вызывается после того как интегратор обработал обменный файл и обновил состав изделия в базе данных.
  /// </summary>
  /// <param name="structData">Данные о сборочном чертеже и составе сборочной единицы</param>
  void EditDrawingSpec(StructData structData);
}
