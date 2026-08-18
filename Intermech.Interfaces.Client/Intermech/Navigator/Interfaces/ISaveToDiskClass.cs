// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.ISaveToDiskClass
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Класс для взаимосвязи дополнительных параметров сохранения с основными при выполнении команды "Сохранить на диск"
/// (процесс непосредственного сохранения)
/// </summary>
public interface ISaveToDiskClass
{
  /// <summary>Путь к папке, куда выполняется сохранение</summary>
  string SelectedPath { get; }

  /// <summary>Формат документа для сохранения</summary>
  ImDocumentFormat DocumentFormat { get; }
}
