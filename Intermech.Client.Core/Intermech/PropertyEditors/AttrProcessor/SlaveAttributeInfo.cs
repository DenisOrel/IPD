
// Type: Intermech.PropertyEditors.AttrProcessor.SlaveAttributeInfo
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.PropertyEditors.AttrProcessor;

/// <summary>Информация по подчиненному атрибуту</summary>
public struct SlaveAttributeInfo(int aMasterId, int aSlaveId, int aSourceId)
{
  private int slaveId = aSlaveId;
  private int masterId = aMasterId;
  private int sourceId = aSourceId;

  public int SlaveId
  {
    get => this.slaveId;
    set => this.slaveId = value;
  }

  public int MasterId
  {
    get => this.masterId;
    set => this.masterId = value;
  }

  public int SourceId
  {
    get => this.sourceId != 0 ? this.sourceId : this.slaveId;
    set => this.sourceId = value;
  }
}
