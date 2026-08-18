// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Integrators.Electrical.ImbaseSyncAttributeEditor
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Client.Core;
using Intermech.Tools.Settings.PropertyEditors;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.Integrators.Electrical;

internal class ImbaseSyncAttributeEditor : AttributeTypeUIEditor
{
  protected override void BeforeShowDialog(AttributesSelectDlg dlg)
  {
    dlg.AllowedAttrsTypesFilter.AddRange((IEnumerable<FieldTypes>) new FieldTypes[1]
    {
      FieldTypes.ftString
    });
    dlg.LoadAttrDialogForObjectsTypes(new Guid("cad0038d-306c-11d8-b4e9-00304f19f545"));
  }
}
