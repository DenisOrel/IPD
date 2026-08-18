// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.Drawing2DArticleRootsCheck
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.Client.Core;
using Intermech.Interfaces;
using Intermech.Text;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators.CADInterface;
using Intermech.Tools.Settings;
using System;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class Drawing2DArticleRootsCheck : CADSettingsCheck
{
  protected override string DoPerformCheck(CADSettings settings, SettingsValidatorContext context)
  {
    if (context == SettingsValidatorContext.Generic)
    {
      K3DIntegratorSettings k3dSettings = (K3DIntegratorSettings) settings;
      if (k3dSettings.EnableDrawings2DSupport)
      {
        string str = this.CheckArticleRoots(k3dSettings);
        if (!string.IsNullOrEmpty(str))
          return str;
      }
    }
    return (string) null;
  }

  private string CheckArticleRoots(K3DIntegratorSettings k3dSettings)
  {
    string str1 = this.CheckArticleRoots(k3dSettings.PartDrawings2D);
    if (!string.IsNullOrEmpty(str1))
      return str1;
    string str2 = this.CheckArticleRoots(k3dSettings.AssemblyDrawings2D, "СБ");
    return !string.IsNullOrEmpty(str2) ? str2 : (string) null;
  }

  private string CheckArticleRoots(DocumentGroup documentGroup, string docTypeCodeFilter = null)
  {
    foreach (GlobalId<int> documentType in documentGroup.DocumentTypes)
    {
      DocumentTypeSettings settings = DocumentTypeSettingsCache.GetSettings(documentType.Id);
      if (docTypeCodeFilter != null)
      {
        string a = TextServices.Trim(settings.DocumentTypeCode);
        if (string.IsNullOrEmpty(a) || !string.Equals(a, docTypeCodeFilter, StringComparison.CurrentCultureIgnoreCase))
          continue;
      }
      string[] strArray = settings.OutputObjectTypes.Split(new string[1]
      {
        ","
      }, StringSplitOptions.RemoveEmptyEntries);
      if (strArray.Length == 0)
        return $"Список типов объектов, выпускаемых по документам '{documentType.Name}', должен содержать хотя бы один тип.";
      foreach (string g in strArray)
      {
        GlobalId<int> objectTypeGid = DBHelper.CreateObjectTypeGID(new Guid(g), false);
        if (objectTypeGid != null && !DBHelper.IsBasedOnType(objectTypeGid.Id, IDCache.Default.AllArticles.Id))
          return string.Format("Тип объектов '{1}', выпускаемых по документам '{0}', должен быть унаследован от типа '{2}'.", (object) documentType.Name, (object) objectTypeGid.Name, (object) IDCache.Default.AllArticles.Text);
      }
    }
    return (string) null;
  }
}
