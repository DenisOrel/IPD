// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.IPropertyValueWithLoader
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

#nullable disable
namespace Experimental.Kernel.Entities;

/// <summary>
/// Интерфейс для значений свойств доменных объектов, реализующих ленивую загрузку из базы данных IPS.
/// Такие свойства соответствуют атрибутам IPS сложных типов: файловым или двоичным, многозначным атрибутам.
/// </summary>
internal interface IPropertyValueWithLoader
{
  /// <summary>
  /// Задает доменный объект, которому принадлежит значение свойства.
  /// Это объект используется как источник для чтения из базы данных.
  /// </summary>
  /// <param name="entityTypeDescriptor">Дескриптор доменного объекта</param>
  /// <param name="entity">Доменный объект</param>
  /// <exception cref="T:System.ArgumentNullException">параметр <paramref name="entityTypeDescriptor" /> содержит null; параметр <paramref name="entityTypeDescriptor" /> содержит null</exception>
  void SetEntity(IDBEntityTypeDescriptor entityTypeDescriptor, object entity);
}
