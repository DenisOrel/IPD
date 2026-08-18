// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj.TechCardBaseEditingContextsCommandsProvider
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Interfaces.Contexts;
using Intermech.Interfaces.TechCard;
using Intermech.Navigator.DBObjects;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Extensions;
using System;

#nullable disable
namespace Intermech.TechCard.Client.TcObjectsTypes.TechCardBaseObj;

/// <summary>
/// Провайдер команд контекстов редактирования для технологических объектов
/// </summary>
internal class TechCardBaseEditingContextsCommandsProvider : EditingContextsCommandsProvider
{
  /// <summary>
  /// 
  /// </summary>
  private ISelectedItems _selectedItems;

  /// <summary>
  /// 
  /// </summary>
  /// <param name="selectedItems"></param>
  /// <param name="serviceProvider"></param>
  /// <param name="additionalInfo"></param>
  protected override void EditingContextReplaceVersion(
    ISelectedItems selectedItems,
    IServiceProvider serviceProvider,
    object additionalInfo)
  {
    this._selectedItems = selectedItems;
    base.EditingContextReplaceVersion(selectedItems, serviceProvider, additionalInfo);
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="editingContextsObjectContainer"></param>
  /// <param name="objectVersionId"></param>
  /// <param name="replacementObjectVersionId"></param>
  protected override void BeforeReplaceObjectVersionInEditingContextObjectContainer(
    EditingContextsObjectContainer editingContextsObjectContainer,
    long objectVersionId,
    long replacementObjectVersionId)
  {
    base.BeforeReplaceObjectVersionInEditingContextObjectContainer(editingContextsObjectContainer, objectVersionId, replacementObjectVersionId);
    if (!(this._selectedItems.GetItemData(0, typeof (IDBRelationID)) is IDBRelationID itemData))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation relation = sessionKeeper.Session.GetRelation(itemData.Value, false);
      if (relation == null)
        return;
      IDBAttribute attributeById = relation.GetAttributeByID(TechCardConsts.AttributeTypes.ContextVersionID);
      if ((attributeById != null ? attributeById.AsInteger : 0L) != objectVersionId)
        return;
      relation.SetAttributesValues(new AttributeValues[1]
      {
        new AttributeValues(TechCardConsts.AttributeTypes.ContextVersionID, (object) replacementObjectVersionId)
      });
    }
  }

  /// <summary>Регистрация провайдера команд</summary>
  /// <param name="factory"></param>
  internal static void RegisterCommandProvider(IFactory factory)
  {
    if (factory == null)
      throw new ArgumentNullException(nameof (factory));
    new TechCardBaseEditingContextsCommandsProvider().RegisterForAllBaseTypes(factory);
  }
}
