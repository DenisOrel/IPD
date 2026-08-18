// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.AuthFileNeedGenerateEventArgs
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Interfaces.Client;

[Serializable]
/// <summary>
/// 
/// </summary>
/// <param name="objectType">Тип объекта</param>
/// <param name="objectId">Версия объекта</param>
/// <param name="pdfOnly">Формировать аутентичные файлы только в формате pdf</param>
public class AuthFileNeedGenerateEventArgs(int objectType, long objectId, bool pdfOnly) : 
  AuthFileAssignEventArgs(objectType, objectId, pdfOnly)
{
  private bool needGenerate;

  /// <summary>
  /// Требуется перегенерация документа, если null то подписчик не знает и это решает сервис
  /// </summary>
  public bool NeedGenerate
  {
    get => this.needGenerate;
    set => this.needGenerate = value;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="objectType">Тип объекта</param>
  /// <param name="objectId">Версия объекта</param>
  public AuthFileNeedGenerateEventArgs(int objectType, long objectId)
    : this(objectType, objectId, false)
  {
  }
}
