// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.IPropertiesCollection
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

/// <summary>Интерфейс объекта, описывающего коллекцию параметров</summary>
public interface IPropertiesCollection
{
  /// <summary>Получить значение параметра компонента</summary>
  /// <param name="propertyName">Имя параметра</param>
  /// <returns>Значение параметра или null при отсутствии у компонента параметра с таким именем</returns>
  object GetPropertyValue(string propertyName);

  /// <summary>Установить значение параметру компонента</summary>
  /// <param name="propertyName">Имя параметра</param>
  /// <param name="value">Новое значение</param>
  void SetPropertyValue(string propertyName, object value);

  /// <summary>Получить интерфейс на параметр компонента</summary>
  /// <param name="attributeName">Имя параметра</param>
  IComponentProperty GetProperty(string propertyName);
}
