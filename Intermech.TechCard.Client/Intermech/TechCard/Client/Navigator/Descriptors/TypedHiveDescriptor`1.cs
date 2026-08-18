// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Navigator.Descriptors.TypedHiveDescriptor`1
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using Intermech.Navigator.VirtualNodes;
using System;

#nullable disable
namespace Intermech.TechCard.Client.Navigator.Descriptors;

/// <summary>
/// Обобщенный дескриптор для хранение дополнительной информации
/// </summary>
/// <typeparam name="T"></typeparam>
internal class TypedHiveDescriptor<T> : HiveDescriptor
{
  /// <summary>
  /// 
  /// </summary>
  private const string PropertyData = "Data";

  /// <summary>Создает дескриптор виртуального элемента навигации.</summary>
  /// <param name="categoryId"></param>
  /// <param name="typeId"></param>
  /// <param name="caption"></param>
  public TypedHiveDescriptor(int categoryId, int typeId, string caption, T data = null)
    : base(categoryId, typeId, caption)
  {
    this.Data = data;
  }

  /// <summary>
  /// Специальный конструктор, используемый для десериализации дескриптора
  /// </summary>
  /// <param name="state"></param>
  public TypedHiveDescriptor(PersistentState state)
    : base(state)
  {
    this.Data = (T) state.GetValue(nameof (Data));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="state"></param>
  public override void GetObjectData(PersistentState state)
  {
    base.GetObjectData(state);
    state.AddValue("Data", (object) this.Data);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="nodeId"></param>
  /// <param name="dataFormat"></param>
  /// <returns></returns>
  public override object GetData(INodeID nodeId, Type dataFormat)
  {
    return !(dataFormat == typeof (IDescriptor)) ? base.GetData(nodeId, dataFormat) : (object) new TypedHiveDescriptor<T>(this._categoryID, this._typeID, this._caption, this.Data);
  }

  /// <summary>
  /// 
  /// </summary>
  public T Data { get; }
}
