// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IDatabaseLoadSave
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс, позволяющий выполнять сохранение и загрузку данных в базе данных
/// </summary>
public interface IDatabaseLoadSave
{
  /// <summary>Загрузить данные из настроек указанного пользователя</summary>
  /// <param name="userID">Идентификатор пользователя</param>
  void Load(long userID);

  /// <summary>Сохранить данные в настройки указанного пользователя</summary>
  /// <param name="userID">Идентификатор пользователя</param>
  void Save(long userID);
}
