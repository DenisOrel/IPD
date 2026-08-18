// Decompiled with JetBrains decompiler
// Type: Intermech.Document.Client.Report.TableReportCreatorForm
// Assembly: Intermech.Document.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 143DCF5E-E3F9-48A6-BC7A-E754B20C8CE6
// Assembly location: D:\IPS\Client\Intermech.Document.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Document.Client.xml

using Intermech.Interfaces.Client;
using Intermech.PropertyEditors;
using System;

#nullable disable
namespace Intermech.Document.Client.Report;

internal class TableReportCreatorForm : IObjectCreatorCustomService
{
  public long CreateObjectDialog(
    int ObjectTypeID,
    long TemplateObjectID,
    int[] RelationTypeIDs,
    long[] RelatedObjectIDs,
    DateTime StartDate,
    bool isVersion)
  {
    return TableReportEditor.Execute(ObjectTypeID, TemplateObjectID);
  }

  /// <summary>Зарегистрировать класс</summary>
  /// <param name="service"></param>
  public static void Attach(IObjectCreatorService service)
  {
    service.RegisterCreatorCustomService(ObjectTypesHelper.GetObjTypeID("cad00289-306c-11d8-b4e9-00304f19f545"), typeof (TableReportCreatorForm));
    service.RegisterCreatorCustomService(ObjectTypesHelper.GetObjTypeID("cad0028a-306c-11d8-b4e9-00304f19f545"), typeof (TableReportCreatorForm));
  }

  /// <summary>Разрегистрировать класс</summary>
  /// <param name="service"></param>
  public static void Detach(IObjectCreatorService service)
  {
    service.UnregisterCreatorCustomService(ObjectTypesHelper.GetObjTypeID("cad00289-306c-11d8-b4e9-00304f19f545"), typeof (TableReportCreatorForm));
    service.UnregisterCreatorCustomService(ObjectTypesHelper.GetObjTypeID("cad0028a-306c-11d8-b4e9-00304f19f545"), typeof (TableReportCreatorForm));
  }
}
