// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBBlobAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System;
using System.Data;


namespace Intermech.Kernel;

internal class DBBlobAttribute : DBStorageAttribute
{
  public DBBlobAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, parent)
  {
  }

  public DBBlobAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    bool temporary,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, temporary, parent)
  {
  }

  public override DateTime AsDateTime
  {
    set => throw new OperationNotApplicableException();
  }

  public override int AddValue(object newValue)
  {
    if (newValue is FileTypes)
      newValue = (object) null;
    return base.AddValue(newValue);
  }
}
