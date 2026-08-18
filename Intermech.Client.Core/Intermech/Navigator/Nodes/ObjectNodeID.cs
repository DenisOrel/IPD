
// Type: Intermech.Navigator.Nodes.ObjectNodeID
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Diagnostics;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace Intermech.Navigator.Nodes;

/// <summary>Реализация идентификатора ноды объекта (не обязательно реально существующего в БД),
/// с интерфейсами IObjectNodeID и IRelatedObjectNodeID</summary>
/// <summary>Конструктор идентификатора ноды объекта</summary>
/// <param name="e">Структура с параметрами для создания идентификатора ноды</param>
public class ObjectNodeID([NotNull] CreateObjectNodeParams createObjectNodeParams) : 
  NodeID(Intermech.Diagnostics.Check.ArgumentNotNull<CreateObjectNodeParams>(createObjectNodeParams, nameof (createObjectNodeParams))),
  INodeID,
  IObjectNodeID,
  IRelatedObjectNodeID
{
  /// <summary>Идентификатор версии объекта</summary>
  public long ObjectVersionID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.pars.ObjectID;
    }
  }

  /// <summary>Идентификатор объекта</summary>
  public new long ObjectID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get => this.pars.ID;
  }

  /// <summary>Тип объекта</summary>
  public int ObjTypeId
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.pars.ObjectTypeID;
    }
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this.pars.ObjectTypeID = value;
    }
  }
}
