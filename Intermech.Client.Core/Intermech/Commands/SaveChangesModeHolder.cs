
// Type: Intermech.Commands.SaveChangesModeHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;


namespace Intermech.Commands;

/// <summary>
/// Контейнер для значений типа<see cref="T:SaveChangesMode" />.
/// </summary>
[Serializable]
public sealed class SaveChangesModeHolder : ServiceProviderValueHolder<SaveChangesMode>
{
  /// <summary>Создает объект.</summary>
  public SaveChangesModeHolder()
    : base(SaveChangesMode.Default)
  {
  }

  /// <summary>Создает объект.</summary>
  /// <param name="value">Начальное значение</param>
  public SaveChangesModeHolder(SaveChangesMode value)
    : base(value)
  {
  }
}
