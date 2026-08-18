
// Type: Intermech.Client.Core.FormDesigner.ClassifyAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.DataFormats;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.PropertyEditors;
using Intermech.Search.Classifiers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner;

/// <summary>
/// 
/// </summary>
internal class ClassifyAction : IFormDesignerActionHandler
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="button"></param>
  /// <param name="form"></param>
  /// <returns></returns>
  public bool ButtonEnabled(object button, object form)
  {
    bool flag = false;
    DesForm desForm = form as DesForm;
    if (button is AttrButton attrButton && desForm != null)
    {
      IElementInfo info = desForm.Info;
      if (info != null && info.ElementKind == AttributableElements.Object)
      {
        flag = attrButton.AlwaysEnabled;
        if (!flag)
          flag = !desForm.InfoReadonly;
      }
    }
    return flag;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="button"></param>
  /// <param name="form"></param>
  public void ButtonPressed(object button, object form)
  {
    DesForm desForm = form as DesForm;
    List<long> longList = new List<long>();
    long elementIdentifier = desForm.Info.ElementIdentifier;
    long objID = elementIdentifier;
    using (ClassifySelectionForm classifySelectionForm = new ClassifySelectionForm(this.GetClassifiers(objID, new List<Guid>()
    {
      new Guid("cad0014e-306c-11d8-b4e9-00304f19f545"),
      new Guid("cad0014f-306c-11d8-b4e9-00304f19f545")
    }).ToArray()))
    {
      if (classifySelectionForm.ShowDialog() == DialogResult.OK)
      {
        if (classifySelectionForm.SelectedItems.Count > 0)
        {
          for (int index = 0; index < classifySelectionForm.SelectedItems.Count; ++index)
          {
            IDBObjectID itemData = classifySelectionForm.SelectedItems.GetItemData(index, typeof (IDBObjectID)) as IDBObjectID;
            longList.Add(itemData.Value);
          }
        }
      }
    }
    if (longList.Count <= 0)
      return;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      if (!(sessionKeeper.Session.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService))
        return;
      foreach (long num in longList)
      {
        IObjectClassificator objectClassificator = customService.GetObjectClassificator((object) sessionKeeper.Session.SessionGUID, num);
        if (objectClassificator != null)
        {
          desForm.IncludedClassificators.Add(num);
          AttributeValues[] clasificatorAttributes = objectClassificator.GetClasificatorAttributes(elementIdentifier);
          if (clasificatorAttributes != null && clasificatorAttributes.Length != 0)
          {
            List<AttributeValues> list = ((IEnumerable<AttributeValues>) clasificatorAttributes).ToList<AttributeValues>();
            desForm.AttributeChanging((IEnumerable<AttributeValues>) list);
          }
        }
        if (!desForm.IsCreationMode && !customService.ExistsObject((object) sessionKeeper.Session.SessionGUID, num, elementIdentifier))
          customService.IncludeObjects((object) sessionKeeper.Session.SessionGUID, num, new long[1]
          {
            elementIdentifier
          });
      }
    }
  }

  /// <summary>
  /// Получить список идентификаторов доступных классификаторов.
  /// </summary>
  /// <param name="objID">Идентификатор объекта</param>
  /// <param name="classifierTypeGuids">Список глобальных идентификаторов типов доступных классификаторов</param>
  /// <returns>Список идентификаторов классификаторов</returns>
  private List<long> GetClassifiers(long objID, List<Guid> classifierTypeGuids)
  {
    List<long> source1 = new List<long>();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject objectActualCopy = sessionKeeper.Session.GetObjectActualCopy(objID, false);
      if (objectActualCopy != null)
      {
        if (sessionKeeper.Session.GetCustomService(typeof (ISelectionsService)) is ISelectionsService customService)
        {
          long[] classifierForObjType = customService.GetClassifierForObjType((object) sessionKeeper.Session.SessionGUID, objectActualCopy.TypeID);
          if (classifierForObjType != null && classifierForObjType.Length != 0)
            source1.AddRange((IEnumerable<long>) classifierForObjType);
        }
        List<ConditionStructure> conditionStructureList = new List<ConditionStructure>(2);
        if (MetaDataHelper.GetObjectTypeChildrenIDRecursive(ClassifiersConstants.DocumentObjectTypeID).Contains(objectActualCopy.TypeID))
        {
          IDBAttribute attributeById = objectActualCopy.GetAttributeByID(ClassifiersConstants.ArchiveAttributeTypeID);
          if (attributeById != null && attributeById.Value != null && attributeById.Value != DBNull.Value)
            conditionStructureList.Add(new ConditionStructure(ClassifiersConstants.ArchivesAttributeTypeID, RelationalOperators.Equal, attributeById.Value, LogicalOperators.OR, 0, false));
          conditionStructureList.Add(new ConditionStructure(ClassifiersConstants.ClassifierTypeAttributeTypeID, RelationalOperators.Equal, (object) 2, LogicalOperators.NONE, 0, false));
          DBRecordSetParams dbRecordSetParams = new DBRecordSetParams(conditionStructureList.ToArray(), new object[1]
          {
            (object) ObligatoryObjectAttributes.F_OBJECT_ID
          });
          dbRecordSetParams.Tags = new HybridDictionary()
          {
            {
              (object) "{7FB30639-2F65-4407-B78E-523547B1B133}",
              (object) true
            }
          };
          foreach (Guid classifierTypeGuid in classifierTypeGuids)
          {
            DataTable source2 = sessionKeeper.Session.ObjectsSelect(classifierTypeGuid, dbRecordSetParams);
            if (source2 != null)
              source1.AddRange((IEnumerable<long>) source2.AsEnumerable().Select<DataRow, long>((System.Func<DataRow, long>) (x => Convert.ToInt64(x[0]))));
          }
          source1 = source1.Distinct<long>().ToList<long>();
        }
      }
    }
    return source1;
  }
}
