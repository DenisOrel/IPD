
// Type: Intermech.PropertyEditors.ChecksumPgPropertyClass
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Checksums;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using System;


namespace Intermech.PropertyEditors;

internal class ChecksumPgPropertyClass
{
  /// <summary>null, пока не получены результаты</summary>
  private ChecksumClass checksumClass;
  /// <summary>становится Guid.Empty по окончании вычисления</summary>
  private Guid checksumTaskGuid = Guid.Empty;
  /// <summary>прогресс вычисления</summary>
  private ChecksumTaskProgress checksumTaskProgress;

  public ChecksumClass ChecksumClass
  {
    get
    {
      this.RereadService();
      return this.checksumClass;
    }
  }

  public Guid ChecksumTaskGuid => this.checksumTaskGuid;

  public ChecksumTaskProgress ChecksumTaskProgress => this.checksumTaskProgress;

  public ChecksumPgPropertyClass(Guid checksumTaskGuid)
  {
    this.checksumTaskGuid = checksumTaskGuid;
    this.RereadService();
  }

  public override string ToString()
  {
    this.RereadService();
    if (this.checksumClass != null)
      return this.checksumClass.ToString();
    string str = string.Empty;
    if (this.checksumTaskProgress != null)
    {
      str = EnumDescConverter.GetEnumDescription((Enum) this.checksumTaskProgress.Operation);
      if (this.checksumTaskProgress.Operation == ChecksumOperationType.Calculating)
        str = $"{str}: {this.checksumTaskProgress.Percent.ToString()}%";
    }
    return str;
  }

  /// <summary>перечитать состояние процесса вычисления</summary>
  public void RereadService()
  {
    if (this.checksumClass != null || !((ApplicationServices.Container.GetService(typeof (IMServerService)) as IMServerService).GetCustomService(typeof (IChecksumsService)) is IChecksumsService customService))
      return;
    this.checksumTaskProgress = customService.GetChecksumTaskProgress(this.checksumTaskGuid);
    if (this.checksumTaskProgress != null && this.checksumTaskProgress.Operation == ChecksumOperationType.Finished)
    {
      this.checksumClass = customService.GetChecksum(this.checksumTaskGuid);
      customService.ChecksumFree(this.checksumTaskGuid);
      this.checksumTaskGuid = Guid.Empty;
    }
    if (this.checksumTaskProgress == null || this.checksumTaskProgress.Operation != ChecksumOperationType.Error)
      return;
    customService.ChecksumFree(this.checksumTaskGuid);
    this.checksumTaskGuid = Guid.Empty;
  }
}
