
// Type: Intermech.Navigator.LifeCycle.LifeCycleSchemesNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace Intermech.Navigator.LifeCycle;

/// <summary>
/// Реализует элемент пространства навигации "Схемы жизненных циклов". Все дочерние элементы являются папками.
/// </summary>
internal sealed class LifeCycleSchemesNode : CompositeNode, IContextAware
{
  /// <summary>Коллекция дескрипторов - схемы ЖЦ</summary>
  private static DescriptorCollection _schemas;
  /// <summary>Контейнер сервисов</summary>
  private AdvancedServiceContainer _services = new AdvancedServiceContainer();

  /// <summary>Создать корневой узел</summary>
  public LifeCycleSchemesNode() => this.BuildSchemas();

  /// <summary>Вернуть слоты-папки</summary>
  /// <returns>Слоты-папки</returns>
  protected override List<PartSlot> CreateFolderSlots()
  {
    return this.SlotsFromSinglePart((INodePart) new LifeCycleSchemesPart(this.Services));
  }

  /// <summary>Вернуть слоты-не папки</summary>
  /// <returns>Слоты-не папки</returns>
  protected override List<PartSlot> CreateNonFolderSlots() => (List<PartSlot>) null;

  /// <summary>
  /// Возвращает данные дочернего элемента в указанном формате. Если
  /// формат не поддерживается, то результатом будет null.
  /// </summary>
  /// <param name="nodeID">Идентификатор дочернего элемента.</param>
  /// <param name="dataFormat">Тип формата данных.</param>
  /// <returns>Результирующий объект указанного типа.</returns>
  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    return base.GetData(nodeID, dataFormat);
  }

  /// <summary>Построить список схем ЖЦ</summary>
  private void BuildSchemas()
  {
    if (LifeCycleSchemesNode._schemas != null)
      return;
    LifeCycleSchemesNode._schemas = new DescriptorCollection();
    List<IMSLifeCycleScheme> lcSchemesList = MetaDataHelper.GetLCSchemesList();
    for (int index = 0; index < lcSchemesList.Count; ++index)
      LifeCycleSchemesNode._schemas.Add((IDescriptor) new LifeCycleSchemeDescriptor(lcSchemesList[index].SchemaID));
  }

  /// <summary>Контейнер сервисов узла</summary>
  public IServiceProvider Services
  {
    [DebuggerStepThrough] get => (IServiceProvider) this._services;
    [DebuggerStepThrough] set => this._services.AdvancedProvider = value;
  }
}
