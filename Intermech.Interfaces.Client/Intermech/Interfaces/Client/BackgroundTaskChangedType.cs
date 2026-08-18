// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.BackgroundTaskChangedType
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Какие изменения произошли в фоновой задаче</summary>
public enum BackgroundTaskChangedType
{
  /// <summary>Любое из допустимых изменений</summary>
  All,
  /// <summary>Изменился текст</summary>
  Text,
  /// <summary>Изменился индекс изображения</summary>
  ImageIndex,
  /// <summary>Изменилось значение</summary>
  Value,
  /// <summary>Изменилось состояние</summary>
  State,
  /// <summary>Изменился результат</summary>
  Result,
  /// <summary>Задача удаляется</summary>
  Dispose,
}
