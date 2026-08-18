
// Type: Intermech.Client.Core.Organizer.OrganizerChildNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using Intermech.Navigator.Selections;
using System;
using System.Collections.Generic;


namespace Intermech.Client.Core.Organizer;

/// <summary>Класс для реализации подузлов узла "Органайзер".</summary>
public class OrganizerChildNode : CompositeNode, IContextAware
{
  /// <summary>
  /// Идентификатор типа объектов, среди которых осуществляется выбор данных
  /// </summary>
  private int _typeID = -1;
  private INodePart _part;

  /// <summary>Идентификатор категории узла.</summary>
  public int CategoryID { get; private set; }

  /// <summary>
  /// Часть элемента навигации, работающую со списком объектов.
  /// </summary>
  public INodePart Part
  {
    get
    {
      if (this._part == null && ServicesManager.GetService(typeof (IOrganizerService)) is OrganizerService service && this._typeID != -1)
        this._part = service.GetDescriptor(this.CategoryID)?.GetPart(this.Services);
      return this._part;
    }
  }

  /// <summary>Конструктор.</summary>
  /// <param name="categoryID">Идентификатор категории (идентификатор узла)</param>
  /// <param name="typeID">Идентификатор типа объектов, среди которых осуществляется выбор данных</param>
  public OrganizerChildNode(int categoryID, int typeID)
  {
    this.CategoryID = categoryID;
    this._typeID = typeID;
  }

  /// <summary>Контейнер сервисов.</summary>
  public IServiceProvider Services { get; set; }

  /// <summary>Формирование слотов-папок.</summary>
  /// <returns>Коллекция слотов-папок</returns>
  protected override List<PartSlot> CreateFolderSlots()
  {
    DescriptorCollection specialDescriptors = this.GetSpecialDescriptors(true, false);
    return new List<PartSlot>()
    {
      new PartSlot(Intermech.Navigator.Selections.Consts.SelectionsPartGuid, (INodePart) new DescriptorsPart(specialDescriptors))
    };
  }

  protected override ITopBinding GetBinding(BindingType bindingType)
  {
    return (ITopBinding) new OrganizerChildNodeBinding(this.CategoryID, this.Part, bindingType);
  }

  /// <summary>Формирование слотов-непапок.</summary>
  /// <returns>Коллекция слотов-непапок</returns>
  protected override List<PartSlot> CreateNonFolderSlots()
  {
    return this.Part == null ? (List<PartSlot>) null : this.SlotsFromSinglePart(this._part);
  }
}
