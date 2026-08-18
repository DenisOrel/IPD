
// Type: Intermech.Navigator.Snapshots.SnapshotInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using System;


namespace Intermech.Navigator.Snapshots;

/// <summary>Класс для хранения информации об итерации.</summary>
public class SnapshotInfo : IComparable
{
  /// <summary>ИД итерации</summary>
  public long ID { get; private set; }

  /// <summary>наименование итерации</summary>
  public string Name { get; private set; }

  /// <summary>Конструктор</summary>
  /// <param name="ID">ID.</param>
  /// <param name="name">Наименование.</param>
  public SnapshotInfo(long ID, string name)
  {
    this.ID = ID;
    this.Name = name;
  }

  public override string ToString() => this.Name;

  public int CompareTo(object obj) => string.CompareOrdinal(this.Name, ((SnapshotInfo) obj).Name);
}
