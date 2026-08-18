// Decompiled with JetBrains decompiler
// Type: Intermech.Checksums.ChecksumsService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections;


namespace Intermech.Checksums;

public sealed class ChecksumsService : LongLifeObject, IChecksumsService
{
  private static Hashtable tasksHashtable = Hashtable.Synchronized(new Hashtable());
  private static Hashtable progressHashtable = Hashtable.Synchronized(new Hashtable());
  private static Hashtable valuesHashtable = Hashtable.Synchronized(new Hashtable());

  public Guid CalcChecksum(
    Guid sessionGuid,
    long elementId,
    AttributableElements kind,
    int attributeId,
    int index,
    ChecksumAlgorithm algorithm)
  {
    Guid guid = Guid.NewGuid();
    ChecksumInputStructure cis = new ChecksumInputStructure(elementId, kind, attributeId, index, algorithm);
    ChecksumTask checksumTask = new ChecksumTask(sessionGuid, guid, cis);
    ChecksumsService.tasksHashtable[(object) guid] = (object) checksumTask;
    checksumTask.SetChecksumProgressEvent += new SetChecksumProgressHandler(this.SetChecksumTaskProgress);
    checksumTask.ChecksumTaskFinishEvent += new ChecksumTaskFinishHandler(this.ChecksumTaskFinish);
    ChecksumTaskProgress checksumTaskProgress = new ChecksumTaskProgress(ChecksumOperationType.Idle);
    this.SetChecksumTaskProgress((object) this, guid, checksumTaskProgress);
    checksumTask.Calc();
    return guid;
  }

  public ChecksumTaskProgress GetChecksumTaskProgress(Guid taskGuid)
  {
    return (ChecksumTaskProgress) ChecksumsService.progressHashtable[(object) taskGuid];
  }

  public ChecksumClass GetChecksum(Guid taskGuid)
  {
    return !ChecksumsService.tasksHashtable.Contains((object) taskGuid) ? (ChecksumClass) ChecksumsService.valuesHashtable[(object) taskGuid] : throw new KernelException("Задача расчета контрольной суммы ещё не завершена. Используйте GetChecksumTaskProgress(Guid taskGuid).Operation для проверки на ChecksumOperationType.Finished");
  }

  public void ChecksumFree(Guid taskGuid)
  {
    if (ChecksumsService.tasksHashtable.Contains((object) taskGuid))
      throw new KernelException("Задача расчета контрольной суммы ещё не завершена. Используйте GetChecksumTaskProgress(Guid taskGuid).Operation для проверки на ChecksumOperationType.Finished");
    ChecksumsService.progressHashtable.Remove((object) taskGuid);
    ChecksumsService.valuesHashtable.Remove((object) taskGuid);
  }

  private void SetChecksumTaskProgress(
    object sender,
    Guid taskGuid,
    ChecksumTaskProgress checksumTaskProgress)
  {
    if (ChecksumsService.progressHashtable[(object) taskGuid] == null)
      ChecksumsService.progressHashtable.Add((object) taskGuid, (object) checksumTaskProgress);
    else
      ChecksumsService.progressHashtable[(object) taskGuid] = (object) checksumTaskProgress;
  }

  private void ChecksumTaskFinish(
    object sender,
    Guid taskGuid,
    ChecksumTaskProgress checksumTaskProgress,
    ChecksumClass сhecksumClass)
  {
    ChecksumsService.valuesHashtable[(object) taskGuid] = (object) сhecksumClass;
    this.SetChecksumTaskProgress((object) this, taskGuid, checksumTaskProgress);
    ChecksumsService.tasksHashtable.Remove((object) taskGuid);
  }
}
