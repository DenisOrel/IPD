// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.Copies.DocumentNode
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Archives.Copies;

/// <summary>Узел=для документа</summary>
public class DocumentNode : ObjectNode
{
  /// <summary>тип выбранного документа</summary>
  public int DocTypeID => this._objTypeID;

  /// <summary>id версии выбранного документа</summary>
  public long DocID => this._objID;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objTypeID"></param>
  /// <param name="objID"></param>
  public DocumentNode(int objTypeID, long objID)
    : base(objTypeID, objID)
  {
    this.options = NodeOptions.None;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objTypeID"></param>
  /// <param name="objID"></param>
  /// <param name="conditions"></param>
  public DocumentNode(int objTypeID, long objID, ConditionStructure[] conditions)
    : this(objTypeID, objID)
  {
  }

  /// <summary>
  /// Создает и возвращает часть элемента, отвечающую за копии, созданные для данного документа
  /// </summary>
  /// <returns>Интерфейс части</returns>
  protected override List<PartSlot> CreateNonFolderSlots()
  {
    return this.SlotsFromSinglePart((INodePart) new CopyNode(this._objID, (IConditionsProvider) (this.Services.GetService(typeof (CopiesConditionsProvider)) as CopiesConditionsProvider), this.Services));
  }

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  protected override List<PartSlot> CreateFolderSlots() => (List<PartSlot>) null;
}
