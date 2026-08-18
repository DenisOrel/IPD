// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Snapshots.IObjectInSnapshotContext
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Interfaces.Client.Snapshots;

#nullable disable
namespace Intermech.Navigator.Snapshots;

/// <summary>Интерфейс ноды объекта, сохранённого в итерации</summary>
public interface IObjectInSnapshotContext : ISnapshotContext
{
  /// <summary>Идентификатор версии объекта</summary>
  long ObjectVersionID { get; }
}
