// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Helpers.UniversalDescriptor
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections;
using System.Drawing;

#nullable disable
namespace Intermech.Navigator.Helpers;

public class UniversalDescriptor : ListDescriptor
{
  private static IGuidMapper _guidMapper;
  private static IFactory _factory;
  private ICategoryTypeIconService _iconService;
  public ConditionStructure[] AdditionalConditions;

  public new int TypeID => this._typeID;

  public static IGuidMapper GuidMapper
  {
    get
    {
      if (UniversalDescriptor._guidMapper == null)
        UniversalDescriptor._guidMapper = ApplicationServices.Container.GetService(typeof (IGuidMapper)) as IGuidMapper;
      return UniversalDescriptor._guidMapper;
    }
  }

  public static IFactory Factory
  {
    get
    {
      if (UniversalDescriptor._factory == null)
        UniversalDescriptor._factory = ApplicationServices.Container.GetService(typeof (IFactory)) as IFactory;
      return UniversalDescriptor._factory;
    }
  }

  internal ICategoryTypeIconService IconService
  {
    get
    {
      if (this._iconService == null)
        this._iconService = ApplicationServices.Container.GetService(typeof (ICategoryTypeIconService)) as ICategoryTypeIconService;
      return this._iconService;
    }
  }

  public UniversalDescriptor(int categoryID, int typeID, string caption, IList objectIDs)
    : base(categoryID, typeID, caption, objectIDs)
  {
  }

  /// <summary>
  /// Сам регистрирует категорию, если нужно + навешивает показ состава
  /// </summary>
  /// <param name="CategoryGuid"></param>
  /// <param name="typeID">Отсюда берется иконка</param>
  /// <param name="caption">Заголовок узла</param>
  /// <param name="objectIDs">Список идентификаторов входящих объектов</param>
  public UniversalDescriptor(Guid CategoryGuid, int typeID, string caption, IList objectIDs)
    : base(0, typeID, caption, objectIDs)
  {
    this._categoryID = UniversalDescriptor.GuidMapper[CategoryGuid];
    if (this._categoryID != 0)
      return;
    this._categoryID = UniversalDescriptor.GuidMapper.Register(CategoryGuid);
    UniversalDescriptor.Factory.AddNodeType(this._categoryID, typeof (UniversalNode));
    UniversalDescriptor.Factory.AddViewsProvider(this._categoryID, (IViewsProvider) new UniversalViewsProvider());
    Icon icon = this.IconService.GetIcon(4, typeID);
    if (icon == null)
      return;
    this.IconService.AddIcon(icon, this._categoryID);
  }

  public event UniversalDescriptor.GetDefaultColumnsHandler OnGetDefaultColumns;

  public override INode GetChild(INodeID nodeID)
  {
    return (INode) new UniversalNode(this, this._objectIDs);
  }

  public NodeColumnCollection GetDefaultColumns()
  {
    UniversalDescriptor.GetDefaultColumnsHandler getDefaultColumns = this.OnGetDefaultColumns;
    return getDefaultColumns != null ? getDefaultColumns() : (NodeColumnCollection) null;
  }

  public delegate NodeColumnCollection GetDefaultColumnsHandler();
}
