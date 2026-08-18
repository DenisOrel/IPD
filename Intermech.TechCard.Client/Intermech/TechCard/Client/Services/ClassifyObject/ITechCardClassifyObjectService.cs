// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.ClassifyObject.ITechCardClassifyObjectService
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;

#nullable disable
namespace Intermech.TechCard.Client.Services.ClassifyObject;

/// <summary>Интерфейс службы классификации объектов TechCard</summary>
public interface ITechCardClassifyObjectService
{
  /// <summary>Классификация объекта для указанного атрибута</summary>
  /// <param name="session">Пользовательская сессия</param>
  /// <param name="classifyParams">Параметры классификации</param>
  /// <param name="classifyStrategy">Стратегия для классификации</param>
  /// <param name="attributeValue"> Значение классифицируемого атрибута</param>
  bool ClassifyObjectAttribute(
    [NotNull] IUserSession session,
    [NotNull] TechCardClassifyObjectAttributeParams classifyParams,
    ITechCardClassifyObjectStrategy classifyStrategy,
    out string attributeValue);
}
