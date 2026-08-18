
// Type: Intermech.Navigator.DBObjects.OrganizationalUnitsNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.DBObjects;

/// <summary>
/// Узел для отображения организационных единиц предприятия в виде дерева
/// </summary>
public class OrganizationalUnitsNode : CompositeNode, IContextAware
{
  private AdvancedServiceContainer _services = new AdvancedServiceContainer();

  protected override List<PartSlot> CreateFolderSlots()
  {
    return new List<PartSlot>()
    {
      new PartSlot(Intermech.Navigator.Selections.Consts.ContentPartGuid, (INodePart) new TopObjectsPart(MetaDataHelper.GetObjectTypeID("cadd9232-306c-11d8-b4e9-00304f19f545"), this.Services)),
      new PartSlot(Intermech.Navigator.Selections.Consts.ContentPartGuid, (INodePart) new TopObjectsPart(MetaDataHelper.GetObjectTypeID("cadd9231-306c-11d8-b4e9-00304f19f545"), this.Services))
    };
  }

  public override NodeColumnCollection GetDefaultColumns(ContentType content)
  {
    return base.GetDefaultColumns(content);
  }

  /// <summary>Контейнер сервисов.</summary>
  public IServiceProvider Services
  {
    get => (IServiceProvider) this._services;
    set => this._services.AdvancedProvider = value;
  }
}
