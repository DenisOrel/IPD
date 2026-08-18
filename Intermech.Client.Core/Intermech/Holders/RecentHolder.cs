
// Type: Intermech.Holders.RecentHolder
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Interfaces.Configuration;
using System;
using System.Collections.Generic;


namespace Intermech.Holders;

/// <summary>
/// Контейнер хранения списков истории на локальной машине для актуального пользователя.
/// </summary>
public class RecentHolder
{
  /// <summary>Максимальное количество элементов</summary>
  protected int MaxCount = 10;
  /// <summary>
  /// ID пользователя, он же используется в качестве имени секции
  /// </summary>
  private long userID;
  /// <summary>
  /// Параметр, в котором сохраняется история, он же имя подсекции.
  /// </summary>
  protected string paramName = string.Empty;
  /// <summary>Список значений параметра - история значений</summary>
  protected List<string> paramValues = new List<string>();

  public string ParamName => this.paramName;

  public List<string> ParamValues => this.paramValues;

  /// <summary>Загрузить параметры</summary>
  public void Load()
  {
    this.Clear();
    if (!(ServicesManager.GetService(typeof (ILocalConfigurationManager)) is ILocalConfigurationManager service))
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      this.userID = sessionKeeper.Session.UserID;
    IConfiguration configuration1 = service.Open("User" + this.userID.ToString());
    if (configuration1 == null)
      return;
    IConfiguration configuration2 = configuration1.Open(this.paramName);
    if (configuration2 == null)
      return;
    if (!configuration2.HasProperty("Count"))
      return;
    int int32 = Convert.ToInt32(configuration2.GetProperty("Count"));
    for (int index = 0; index < int32; ++index)
    {
      if (configuration2.HasProperty("v" + index.ToString()))
        this.paramValues.Add(configuration2.GetProperty("v" + index.ToString()));
    }
  }

  /// <summary>Сохранить параметры</summary>
  public void Save()
  {
    if (this.userID == 0L || !(ServicesManager.GetService(typeof (ILocalConfigurationManager)) is ILocalConfigurationManager service))
      return;
    IConfiguration configuration1 = service.Open("User" + this.userID.ToString()) ?? service.Create("User" + this.userID.ToString());
    IConfiguration configuration2 = configuration1.Open(this.paramName);
    if (configuration2 == null)
      configuration2 = configuration1.Add(this.paramName);
    else
      configuration2.Clear();
    if (this.paramValues.Count > this.MaxCount)
      this.paramValues.RemoveRange(this.MaxCount, this.paramValues.Count - this.MaxCount);
    configuration2.SetProperty("Count", this.paramValues.Count.ToString());
    for (int index = 0; index < this.paramValues.Count; ++index)
      configuration2.SetProperty("v" + index.ToString(), this.paramValues[index]);
  }

  private void Clear()
  {
    this.userID = 0L;
    this.paramValues.Clear();
  }
}
