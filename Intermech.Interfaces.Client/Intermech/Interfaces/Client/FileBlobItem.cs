// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.FileBlobItem
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>структура с идентификатором файлового блоба</summary>
/// <summary>Создает структуру с идентификатором файлового блоба</summary>
/// <param name="objectId">Идентификатор объекта</param>
/// <param name="attId">Идентификатор атрибута</param>
/// <param name="valueIndex">Индекс в атрибуте</param>
public struct FileBlobItem(long objectId, int attId, int valueIndex)
{
  /// <summary>Идентификатор объекта</summary>
  public long ObjectId = objectId;
  /// <summary>Идентификатор атрибута</summary>
  public int AttId = attId;
  /// <summary>Индекс в атрибуте</summary>
  public int ValueIndex = valueIndex;
}
