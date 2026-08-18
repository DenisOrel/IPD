// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Pdm.IInstancesClientService
// Assembly: Intermech.Interfaces.Pdm, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: C981BCB9-CF2A-447D-A8BE-B05ADE22BCE8
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Pdm.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Pdm.xml

#nullable disable
namespace Intermech.Interfaces.Pdm;

public interface IInstancesClientService
{
  /// <summary>Создает группу исполнений</summary>
  /// <param name="objectVersionID">Идентификатор версии изделия</param>
  /// <returns>Идентификаторы версий созданных исполенений</returns>
  long[] CreateInstances(long objectVersionID, long specFID = -1);
}
