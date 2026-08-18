// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.ClassifyObject.TechCardClassifyObjectNameStrategy
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.TechCard;

#nullable disable
namespace Intermech.TechCard.Client.Services.ClassifyObject;

/// <summary>
/// Стратегия классификации атрибута "Наименования" для технологических объектов
/// </summary>
public class TechCardClassifyObjectNameStrategy : ITechCardClassifyObjectStrategy
{
  /// <summary>Получение шаблона классификации атрибута</summary>
  /// <param name="session"></param>
  /// <param name="classifyParams"></param>
  /// <returns></returns>
  public string GetClassifyTemplate(
    [NotNull] IUserSession session,
    [NotNull] TechCardClassifyObjectParams classifyParams)
  {
    return session.GetObject(classifyParams.ContextObjectItem.ObjectID, false)?.GetAttributeByID(TechCardConsts.AttributeTypes.NameAttrTypeID)?.AsString ?? string.Empty;
  }
}
