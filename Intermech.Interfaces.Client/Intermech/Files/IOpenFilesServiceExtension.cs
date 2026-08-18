// Decompiled with JetBrains decompiler
// Type: Intermech.Files.IOpenFilesServiceExtension
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Files;

/// <summary>
/// Позволяет реализовать расширение сервиса открытых файлов. Посредством таких расширений сервис
/// взаимодействует с конкретными приложениями. Реализация этого интерфейса должна быть thread-safe.
/// </summary>
public interface IOpenFilesServiceExtension : IOpenFiles
{
}
