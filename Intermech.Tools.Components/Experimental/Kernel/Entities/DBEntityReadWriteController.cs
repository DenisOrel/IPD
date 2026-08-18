// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBEntityReadWriteController
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;

#nullable disable
namespace Experimental.Kernel.Entities;

internal sealed class DBEntityReadWriteController
{
  private bool readingIsAllowed;
  private bool writingIsAllowed;

  public DBEntityReadWriteController() => this.AllowAll();

  public void DisallowAll()
  {
    this.readingIsAllowed = false;
    this.writingIsAllowed = false;
  }

  public void AllowAll()
  {
    this.readingIsAllowed = true;
    this.writingIsAllowed = true;
  }

  public void CheckReadingIsAllowed()
  {
    if (!this.readingIsAllowed)
      throw this.OperationIsNotAllowedException();
  }

  public void CheckWritingIsAllowed()
  {
    if (!this.writingIsAllowed)
      throw this.OperationIsNotAllowedException();
  }

  private EntityException OperationIsNotAllowedException()
  {
    return new EntityException($"Обращения к базе данных запрещены, пока имеются активные объекты {"IEntityChangeTracker"}.");
  }
}
