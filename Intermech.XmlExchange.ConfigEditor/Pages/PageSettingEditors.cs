// Decompiled with JetBrains decompiler
// Type: Intermech.XmlExchange.ConfigEditor.Pages.PageSettingEditors
// Assembly: Intermech.XmlExchange.ConfigEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D148B79A-64FF-4CB8-A129-56A9018E56E2
// Assembly location: D:\IPS\Client\Intermech.XmlExchange.ConfigEditor.dll

using Intermech.Interfaces.XmlExchange;
using Intermech.Interfaces.XmlExchange.Settings.Export.Extensions;
using Intermech.XmlExchange.ConfigEditor.ExportApplSetting;
using Intermech.XmlExchange.ConfigEditor.ImportConfig;
using Intermech.XmlExchange.ConfigEditor.ImportConfig.Common;
using Intermech.XmlExchange.ConfigEditor.PropertiesDescription;
using Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ExportItem;
using Intermech.XmlExchange.ConfigEditor.PropertiesDescription.ImportItem;
using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace Intermech.XmlExchange.ConfigEditor.Pages;

internal class PageSettingEditors
{
  internal static List<Type> GetEditors(object selectItem)
  {
    List<Type> editors = new List<Type>();
    switch (selectItem)
    {
      case XmlExchangeExportSettings _:
      case IList _:
      case XmlExchangeExportAttr _:
        editors.Add(typeof (PageSettings));
        break;
      case XmlExchangeExportAttributable _:
        editors.Add(typeof (PageSettings));
        editors.Add(typeof (PageAttributes));
        editors.Add(typeof (PageDefAttributes));
        break;
      case IExportApplType _:
        editors.Add(typeof (PageSettings));
        editors.Add(typeof (PageApplPartTypes));
        break;
      case XmlExchangeExportScript _:
      case XmlExchangeExportExtension _:
      case XmlExchangeImportObjectType _:
        editors.Add(typeof (PageSettings));
        break;
    }
    object obj = selectItem;
    if (obj != null && (obj is XmlExchangeImportRuleSearch _ || obj is XmlExchangeImportRuleImport importRuleImport && importRuleImport.Rule == ImportRuleMode.CreateByDictionary))
      editors.Add(typeof (PageSearchAttributes));
    if (selectItem is XmlExchangeImportItem)
      editors.Add(typeof (PageSettings));
    return editors;
  }

  public static IConfigItemProperties GetPropertyCollection(object selectItem, bool readOnly)
  {
    switch (selectItem)
    {
      case XmlExchangeExportSettings baseExportSettings:
        return (IConfigItemProperties) new GridViewBaseExportSettings(baseExportSettings, readOnly);
      case XmlExchangeExportAttr exportAttr:
        return (IConfigItemProperties) new GridViewSettingsExportAttr(exportAttr, readOnly, ConfigEditorHelper.GetHelper().AtrTypeInBase(exportAttr.TypeGuid, exportAttr.TypeID));
      case XmlExchangeExportObj exportObj:
        return (IConfigItemProperties) new GridViewSettingsExportObj(exportObj, readOnly, ConfigEditorHelper.GetHelper().ObjTypeInBase(exportObj.TypeGuid, exportObj.TypeID));
      case XmlExchangeExportRel exchangeExportRel:
        return (IConfigItemProperties) new GridViewSettingsExportAttributable((XmlExchangeExportAttributable) exchangeExportRel, readOnly, ConfigEditorHelper.GetHelper().RelTypeInBase(exchangeExportRel.TypeGuid, exchangeExportRel.TypeID));
      case XmlExchangeExportScript exportScript:
        return (IConfigItemProperties) new GridViewSettingsExportScript(exportScript, readOnly);
      case XmlExchangeExportExtension exportExtension:
        return (IConfigItemProperties) new GridViewSettingsExportExtension(exportExtension, readOnly);
      case IExportApplType exportApplType:
        return (IConfigItemProperties) new GridViewSettingsApplType(exportApplType, readOnly);
      case XmlExchangeImportRuleImport ruleImport:
        return (IConfigItemProperties) new GridViewSettingsImportRuleImport(ruleImport, readOnly, ConfigEditorHelper.GetHelper().ObjTypeInBase(ruleImport.Guid));
      case XmlExchangeImportRuleSearch ruleSearch:
        return (IConfigItemProperties) new GridViewSettingsImportRuleSearch(ruleSearch, readOnly, ConfigEditorHelper.GetHelper().ObjTypeInBase(ruleSearch.Guid));
      case XmlExchangeImportRuleCreate ruleCreate:
        return (IConfigItemProperties) new GridViewSettingsImportRuleCreate(ruleCreate, readOnly, ConfigEditorHelper.GetHelper().ObjTypeInBase(ruleCreate.Guid));
      case XmlExchangeImportImbase imbaseSettings:
        return (IConfigItemProperties) new GridViewSettingsImportImbase(imbaseSettings, readOnly);
      case XmlExchangeImportScript importScript:
        return (IConfigItemProperties) new GridViewSettingsImportScript(importScript, readOnly);
      case XmlExchangeImportExtension importExtension:
        return (IConfigItemProperties) new GridViewSettingsImportExtension(importExtension, readOnly);
      default:
        return (IConfigItemProperties) null;
    }
  }
}
