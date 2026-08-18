
// Type: Intermech.Navigator.VirtualNodes.ObjectsSelectionNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Infralution.Controls.VirtualTree;
using Intermech.Client.Core;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Navigator.Controls;
using Intermech.Navigator.DB;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;


namespace Intermech.Navigator.VirtualNodes;

/// <summary>Нода списка объектов, удовлетворяющих некоторому условию</summary>
public class ObjectsSelectionNode : CompositeNode, INode, INodeItems, IContextAware, INodeCustomUI
{
  /// <summary>Тип объектов</summary>
  protected readonly int _ObjTypeID;
  /// <summary>Провайдер списка условий выбора объектов</summary>
  [NotNull]
  protected readonly IConditionsProvider _ConditionsProvider;
  /// <summary>Контейнер сервисов</summary>
  [NotNull]
  protected readonly AdvancedServiceContainer _Services = new AdvancedServiceContainer();

  /// <summary>Constructor</summary>
  /// <param name="objTypeID">Тип объектов</param>
  /// <param name="conditionsProvider">Провайдер списка условий выбора объектов</param>
  public ObjectsSelectionNode(int objTypeID, [NotNull] IConditionsProvider conditionsProvider)
  {
    this._ObjTypeID = objTypeID;
    this._ConditionsProvider = conditionsProvider;
    this.Options = NodeOptions.CanContainsObjectsList;
  }

  /// <summary>Контекст (контейнер сервисов) для элемента пространства навигации</summary>
  [NotNull]
  public IServiceProvider Services
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return (IServiceProvider) this._Services;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)] set
    {
      this._Services.AdvancedProvider = value;
    }
  }

  /// <summary>Создать список слотов-не-папок</summary>
  /// <returns>Список слотов-не-папок</returns>
  [NotNull]
  protected override List<PartSlot> CreateNonFolderSlots()
  {
    ObjectsPart part = new ObjectsPart(this._ObjTypeID, this._ConditionsProvider, (IServiceProvider) this._Services);
    part.AcceptManagedEvents = false;
    return this.SlotsFromSinglePart((INodePart) part);
  }

  public Image GetMainIcon() => Images32x16_Cache.GetImage32x16(4, this._ObjTypeID, (object) this);

  public Image GetPrefixIcon() => (Image) null;

  public CellWidget GetCustomCellWidget(RowWidget rowWidget, NavigatorTreeColumn column)
  {
    return (CellWidget) null;
  }
}
