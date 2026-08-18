// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ICreatorFileHandler
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс для перекрывающего создателя объектов, позволяющий управлять файловыми атрибутами
/// </summary>
public interface ICreatorFileHandler
{
  /// <summary>
  /// Флаг, отображать ли при создании объекта по прототипу закладку переименования файлов
  /// вместо закладки с файловыми атрибутами. По-умолчанию TRUE.
  /// </summary>
  bool ShowPage { get; }
}
