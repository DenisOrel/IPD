// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.AVS.IAVSServerService
// Assembly: Intermech.Interfaces.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7D4BF5C8-6CC8-4C83-BD5A-984562FE5544
// Assembly location: D:\IPS\Client\Intermech.Interfaces.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.AVS.xml

#nullable disable
namespace Intermech.Interfaces.AVS;

/// <summary>Интерфейс серверного сервиса AVS</summary>
public interface IAVSServerService
{
  /// <summary>Добавить в коллекцию создатель для объектов заданного типа</summary>
  /// <param name="creatorType">Тип создаваемых объектов(Guid)</param>
  /// <param name="sessionID">Guid сессии</param>
  void AddAvsDBObjectCreator(object creatorType);
}
