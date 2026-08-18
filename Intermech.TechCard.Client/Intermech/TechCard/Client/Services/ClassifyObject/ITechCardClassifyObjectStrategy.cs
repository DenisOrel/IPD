// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.ClassifyObject.ITechCardClassifyObjectStrategy
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Interfaces;

#nullable disable
namespace Intermech.TechCard.Client.Services.ClassifyObject;

/// <summary>
/// Интерфейс стратегии для классификации атрибутов технологических объектов
/// </summary>
public interface ITechCardClassifyObjectStrategy
{
  /// <summary>Получение шаблона / значения классификации атрибута</summary>
  /// <param name="session"></param>
  /// <param name="classifyParams"></param>
  /// <param name="objectName"></param>
  string GetClassifyTemplate(IUserSession session, TechCardClassifyObjectParams classifyParams);
}
