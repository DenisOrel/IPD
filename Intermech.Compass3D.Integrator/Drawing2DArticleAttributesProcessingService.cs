// Decompiled with JetBrains decompiler
// Type: Intermech.Compass3D.Integrator.Drawing2DArticleAttributesProcessingService
// Assembly: Intermech.Compass3D.Integrator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E9700F29-129D-4EBE-8417-980BAD3DC32C
// Assembly location: D:\IPS\Client\Intermech.Compass3D.Integrator.dll

using Intermech.Client.Core;
using Intermech.Data;
using Intermech.Data.SectionEntities;
using Intermech.Tools.DataExchange;
using Intermech.Tools.Integrators.Mechanical;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

#nullable disable
namespace Intermech.Compass3D.Integrator;

internal sealed class Drawing2DArticleAttributesProcessingService(
  K3DCaptureChangesDriver driver,
  CaptureChangesDriverContext driverContext) : ArticleAttributesProcessingService((MechanicalDriver) driver, driverContext)
{
  private K3DCaptureChangesDriver K3DDriver
  {
    [DebuggerStepThrough] get => (K3DCaptureChangesDriver) this.Driver;
  }

  protected override void DoPostprocessAttributes(
    SectionEntity articleItem,
    ValueBag workingSet,
    ValueBag databaseSet)
  {
    base.DoPostprocessAttributes(articleItem, workingSet, databaseSet);
    if (!this.K3DDriver.Drawing2DOperations.IsComponentArticle(articleItem) || ObjectSection.IsNewObject(articleItem))
      return;
    ICollection<StringKey> articleSyncAttributes = this.K3DDriver.GetArticleApiService(articleItem).GetArticleSyncAttributes(articleItem);
    foreach (ValueRecord changedItem in databaseSet.GetChangedItems())
    {
      if (articleSyncAttributes.Contains(changedItem.Key))
      {
        string objectNameInMessages = DBHelper.GetObjectNameInMessages(ObjectSection.GetObjectId(articleItem));
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat("У изделия '{0}' значение атрибута '{1}', полученное из спецификации Компас 3D (равное '{2}'), не совпадает со значением этого атрибута, прочитанного из базы IPS.", (object) objectNameInMessages, (object) changedItem.Key, changedItem.Value);
        stringBuilder.Append(' ');
        stringBuilder.Append("Если необходимо изменить значение атрибута у изделия в спецификации Компас 3D, то предварительно его нужно изменить у соответствующего изделия в базе IPS.");
        stringBuilder.Append(' ');
        stringBuilder.AppendFormat("Иначе, исправьте значение атрибута '{0}' в спецификации Компас 3D, а затем повторите текущую операцию.", (object) changedItem.Key);
        throw new FaultException(stringBuilder.ToString());
      }
    }
  }
}
