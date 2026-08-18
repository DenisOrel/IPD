// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.ITechCardObjectCreateAnalyzingService
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.TechCard.Client.ObjectTypeSupport.TechCardObject.Creator;

#nullable disable
namespace Intermech.TechCard.Client.Services;

/// <summary>
/// Интерфейс сервиса для анализа доступности создания, добавления в состав родительских типов
/// </summary>
public interface ITechCardObjectCreateAnalyzingService
{
  /// <summary>
  /// Проверка на допустимость создания / добавления в состав родительского типа
  /// </summary>
  /// <param name="creatorArgs"></param>
  /// <param name="creatorParams"></param>
  /// <returns></returns>
  bool AllowObjectCreation(TechObjectCreatorArgs creatorArgs, TechObjectCreatorParams creatorParams);
}
