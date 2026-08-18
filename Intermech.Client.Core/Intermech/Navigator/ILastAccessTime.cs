
// Type: Intermech.Navigator.ILastAccessTime
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Navigator;

/// <summary>
/// Интерфейс позволяет определять время и дату последнего доступа к объекту
/// </summary>
public interface ILastAccessTime
{
  /// <summary>Время и дата последнего доступа к объекту</summary>
  DateTime LastAccess { get; }

  /// <summary>Обновить время и дату последнего доступа к объекту</summary>
  void Hit();
}
