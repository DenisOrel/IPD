// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.TableWizard.ImbaseTableCreator
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using ImSSP;
using Intermech.Imbase.BackgroundTask;
using Intermech.Imbase.Indexes;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using System;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Imbase.TableWizard;

internal class ImbaseTableCreator : IObjectCreatorCustomService
{
  public long CreateObjectDialog(
    int objectTypeID,
    long templateObjectID,
    int[] relationTypeIDs,
    long[] relatedObjectIDs,
    DateTime startDate,
    bool isVersion)
  {
    long sourceID = 0;
    templateObjectID = templateObjectID == -1L ? 0L : templateObjectID;
    int relationTypeID = -1;
    long parentObjectID = 0;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (relatedObjectIDs.Length > 1)
      {
        IDBObjectType objectType = sessionKeeper.Session.GetObjectType(objectTypeID, true);
        throw new ArgumentException(string.Format(LocalizationHolder.rm.GetString(sc_8001.ssp_imbase_8002()), (object) objectType.ObjectTypeName));
      }
      if (relatedObjectIDs.Length == 1)
      {
        relationTypeID = relationTypeIDs[0];
        parentObjectID = relatedObjectIDs[0];
      }
    }
    using (ImbaseTableWizardForm imbaseTableWizardForm = new ImbaseTableWizardForm(objectTypeID, relationTypeID, parentObjectID, templateObjectID, isVersion))
    {
      if (imbaseTableWizardForm.ShowDialog() == DialogResult.OK)
      {
        sourceID = imbaseTableWizardForm.ObjectID;
        if (sourceID != 0L)
        {
          if (imbaseTableWizardForm.ObjectTypeID == Intermech.Imbase.Consts.ImbaseTableRefTypeID)
          {
            if (ServicesManager.GetService(typeof (IBackgroundTaskView)) is IBackgroundTaskView service)
            {
              IndexesHelper helper = new IndexesHelper(sourceID)
              {
                Actions = IndexesStatus.UpdateLinkData
              };
              service.AddTask((IBackgroundTask) new ImbaseIndexesBackgroundTask(helper));
            }
          }
        }
      }
    }
    return sourceID;
  }
}
