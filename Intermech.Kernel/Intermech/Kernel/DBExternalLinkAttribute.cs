// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBExternalLinkAttribute
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System.Data;


namespace Intermech.Kernel;

internal class DBExternalLinkAttribute : DBAdditionalAttribute
{
  public DBExternalLinkAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, parent)
  {
  }

  public DBExternalLinkAttribute(
    UserSession uSession,
    DataRow attributeTypeRow,
    DataTable valuesTable,
    int values_index,
    bool temporary,
    DBAttributable parent)
    : base(uSession, attributeTypeRow, valuesTable, values_index, temporary, parent)
  {
  }

  protected override string GetInViewFieldName(AttributeValueField fldType)
  {
    string inViewFieldName;
    switch (fldType)
    {
      case AttributeValueField.Integer:
        inViewFieldName = $"F{this.AttributeID.ToString()}ID";
        break;
      case AttributeValueField.Double:
        inViewFieldName = $"F{this.AttributeID.ToString()}ID2";
        break;
      case AttributeValueField.String:
        inViewFieldName = "F" + this.AttributeID.ToString();
        break;
      default:
        inViewFieldName = string.Empty;
        break;
    }
    return inViewFieldName;
  }
}
