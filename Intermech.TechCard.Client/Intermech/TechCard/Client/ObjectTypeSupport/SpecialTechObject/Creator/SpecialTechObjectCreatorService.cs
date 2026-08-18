// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.ObjectTypeSupport.SpecialTechObject.Creator.SpecialTechObjectCreatorService
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;
using Intermech.Localization;
using Intermech.Protection;
using Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;
using System;

#nullable disable
namespace Intermech.TechCard.Client.ObjectTypeSupport.SpecialTechObject.Creator;

/// <summary>
/// Класс реализации службы создания "специальных" технологических объектов,
/// для которых при создании объектов требуется показывать карточку и диалог выбора из Imbase по команде "Добавить"
/// </summary>
internal class SpecialTechObjectCreatorService : TechCardBaseObjectCreatorService
{
  /// <summary>Вызывать ли собственный диалог ?</summary>
  /// <param name="objectTypeId"></param>
  /// <param name="templateObjectId"></param>
  /// <param name="relationTypeIDs"></param>
  /// <param name="relatedObjectIDs"></param>
  /// <param name="startDate"></param>
  /// <param name="isVersion"></param>
  /// <returns></returns>
  public override bool AcceptDialog(
    int objectTypeId,
    long templateObjectId,
    int[] relationTypeIDs,
    long[] relatedObjectIDs,
    DateTime startDate,
    bool isVersion)
  {
    IProtectionKey service = ServiceUtils.GetService<IProtectionKey>((object) ApplicationServices.Container, true);
    int index = (Environment.TickCount & 15) * 2;
    byte[] numArray = TechCardProtectionKey.Key[index];
    byte[] inArray = new byte[numArray.Length];
    int appId = TechCardProtectionKey.appId;
    byte[] queryData = numArray;
    byte[] response = inArray;
    int num = service.Query(true, appId, queryData, response);
    if (!num.Equals(0) || !Convert.ToBase64String(inArray).Equals(Convert.ToBase64String(TechCardProtectionKey.Key[index + 1])))
      throw new ProtectionException(string.Format(LocalizationHolder.rm.GetString("TechCard.Client_252"), (object) num));
    return this._creatorExtraParams != null && base.AcceptDialog(objectTypeId, templateObjectId, relationTypeIDs, relatedObjectIDs, startDate, isVersion);
  }
}
