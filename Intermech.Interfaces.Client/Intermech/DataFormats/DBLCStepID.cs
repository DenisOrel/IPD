// Decompiled with JetBrains decompiler
// Type: Intermech.DataFormats.DBLCStepID
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Diagnostics;

#nullable disable
namespace Intermech.DataFormats;

/// <summary>
/// Объект-формат для передачи данных шаге жизненного цикла
/// через clipboard, а также между различными частями
/// универсального клиента.
/// </summary>
[DebuggerDisplay("LCStep = {_lcStep}")]
public class DBLCStepID : IDBLCStepID
{
  /// <summary>Шаг жизненного цикла</summary>
  private int _lcStepID;
  /// <summary>Название шага жизненного цикла</summary>
  private string _lcStep;

  /// <summary>Шаг жизненного цикла</summary>
  public int LCStepID
  {
    [DebuggerStepThrough] get => this._lcStepID;
  }

  /// <summary>Название шага жизненного цикла</summary>
  public string LCStep
  {
    [DebuggerStepThrough] get => this._lcStep;
  }

  /// <summary>Создать экземпляр класса</summary>
  /// <param name="lcStepID">Шаг жизненного цикла</param>
  /// <param name="lcStep">Название шага жизненного цикла</param>
  public DBLCStepID(int lcStepID, string lcStep)
  {
    this._lcStepID = lcStepID;
    this._lcStep = lcStep;
  }

  /// <summary>Сравнить экземпляр класса с указанным объектом</summary>
  /// <param name="obj">Объект для сравнения</param>
  /// <returns>true, если объекты равны</returns>
  public override bool Equals(object obj)
  {
    return !(obj is DBLCStepID dblcStepId) ? base.Equals(obj) : this._lcStepID == dblcStepId._lcStepID;
  }

  /// <summary>Вернуть 32-битный хэш-код экземпляра объекта</summary>
  /// <returns>32-битный хэш-код экземпляра объекта</returns>
  [DebuggerStepThrough]
  public override int GetHashCode() => this._lcStepID.GetHashCode();
}
