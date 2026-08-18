
// Type: Intermech.Navigator.LifeCycle.LifeCycleSchemeDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using System;


namespace Intermech.Navigator.LifeCycle;

/// <summary>Дескриптор узла "Схема жизненного цикла"</summary>
public class LifeCycleSchemeDescriptor : HiveDescriptor
{
  /// <summary>Идентификатор схемы ЖЦ</summary>
  protected int id;

  /// <summary>Создает дескриптор</summary>
  /// <param name="id">Идентификатор схемы ЖЦ</param>
  public LifeCycleSchemeDescriptor(int id)
    : base(Intermech.Navigator.Consts.CategoryLifeCycleSchemeNode, id, MetaDataHelper.GetLCSchemaName(id))
  {
    this.id = id;
  }

  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора.
  /// </summary>
  /// <param name="state">Строка с сохранённым состоянием дескриптора</param>
  protected LifeCycleSchemeDescriptor(PersistentState state)
    : base(state)
  {
  }

  /// <summary>Вернуть данные указанного формата</summary>
  /// <param name="nodeID">Идентификатор элемента пространства навигации</param>
  /// <param name="dataFormat">Запрашиваемый формат данных</param>
  /// <returns>Данные указанного формата или null</returns>
  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    if (dataFormat == typeof (IDescriptor))
      return (object) new LifeCycleSchemeDescriptor(this.id);
    return dataFormat == typeof (ICanOpenInNewWindow) ? (object) new CanOpenInNewWindow() : base.GetData(nodeID, dataFormat);
  }

  /// <summary>
  /// Создать дочерний элемент пространства навигации по его идентификатору
  /// </summary>
  /// <param name="nodeID">Идентификатор дочернего элемента пространства навигации</param>
  /// <returns>Дочерний элемент пространства навигации по его идентификатору</returns>
  public override INode GetChild(INodeID nodeID) => base.GetChild(nodeID);
}
