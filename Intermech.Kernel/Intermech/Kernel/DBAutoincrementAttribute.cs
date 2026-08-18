// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBAutoincrementAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using System.Data;


namespace Intermech.Kernel;

internal class DBAutoincrementAttribute : DBIntegerAttribute
{
  public DBAutoincrementAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, parent)
  {
  }

  public DBAutoincrementAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    bool temporary,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, temporary, parent)
  {
  }

  protected override object GetDefaultValue()
  {
    return (object) this.UserSession.DataManager.DataProvider.NextGeneratorValue($"IMT_A{this.AttributeID.ToString()}_GEN", this.UserSession.DataManager);
  }
}
