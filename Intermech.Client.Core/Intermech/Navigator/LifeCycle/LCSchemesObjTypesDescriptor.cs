
// Type: Intermech.Navigator.LifeCycle.LCSchemesObjTypesDescriptor
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using System;
using System.Diagnostics;


namespace Intermech.Navigator.LifeCycle;

/// <summary>Дескриптор корневого узла "Схемы жизненных циклов"</summary>
public class LCSchemesObjTypesDescriptor : HiveDescriptor
{
  /// <summary>Заголовок</summary>
  public new static string Caption
  {
    [DebuggerStepThrough] get => LocalizationHolder.rm.GetString("Client.Core_1336");
  }

  /// <summary>Создает дескриптор</summary>
  public LCSchemesObjTypesDescriptor()
    : base(Intermech.Navigator.Consts.CategoryLCSchemesObjTypesNode, 0, LCSchemesObjTypesDescriptor.Caption)
  {
  }

  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора
  /// </summary>
  /// <param name="state">Строка с сохранённым состоянием</param>
  protected LCSchemesObjTypesDescriptor(PersistentState state)
    : base(Intermech.Navigator.Consts.CategoryLCSchemesObjTypesNode, 0, LCSchemesObjTypesDescriptor.Caption)
  {
  }

  /// <summary>Выполняет сериализацию дескриптора</summary>
  /// <param name="state">Строка для сохранения состояния дескриптора</param>
  public override void GetObjectData(PersistentState state)
  {
  }

  /// <summary>
  /// Вернуть данные определённого формата по указанному описанию узла
  /// </summary>
  /// <param name="nodeID">Описание узла</param>
  /// <param name="dataFormat">Формат запрашиваемых данных</param>
  /// <returns>Данные определённого формата по указанному описанию узла</returns>
  public override object GetData(INodeID nodeID, Type dataFormat)
  {
    if (dataFormat == typeof (IDescriptor))
      return (object) new LifeCycleSchemesDescriptor();
    return dataFormat == typeof (ICanOpenInNewWindow) ? (object) new CanOpenInNewWindow() : base.GetData(nodeID, dataFormat);
  }

  /// <summary>Вернуть дочерний узел согласно его описанию</summary>
  /// <param name="nodeID">Описание дочернего узла</param>
  /// <returns>Дочерний узел</returns>
  public override INode GetChild(INodeID nodeID) => base.GetChild(nodeID);
}
