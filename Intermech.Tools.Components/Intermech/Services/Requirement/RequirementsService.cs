// Decompiled with JetBrains decompiler
// Type: Intermech.Services.Requirement.RequirementsService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Search.MSOfficeAddins;
using Intermech.Tools.Data;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.Notifications;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Services.Requirement;

internal sealed class RequirementsService : IRequirementsService
{
  private IIntegratorRegistry _integratorRegistry;
  private IOutputView _outputView;
  /// <summary>Индекс тех требования</summary>
  private string _requirementIndex = "cadd99f4-306c-11d8-b4e9-00304f19f545";
  /// <summary>GUID требования</summary>
  private string _requirementRelationAttrGuid = "cadd9b32-306c-11d8-b4e9-00304f19f545";
  /// <summary>Ссылка на документ в котором это требование было</summary>
  private string _requirementLinkObject = "cadd99f5-306c-11d8-b4e9-00304f19f545";

  public RequirementsService(IIntegratorRegistry integratorRegistry, IOutputView outputView)
  {
    this._integratorRegistry = integratorRegistry;
    this._outputView = outputView;
  }

  /// <summary>Обновить тех. требования для заданного документа</summary>
  /// <param name="documentInfo">Контейнер со свединями о сохраняемом документе</param>
  public void UpdateRequirements(
    CaptureChangesDocumentInfo documentInfo,
    IIntegrator integrator,
    ITechRequirementsService requirementSupport)
  {
    if (integrator == null)
      throw new ArgumentNullException(nameof (integrator));
    if (requirementSupport == null)
      throw new ArgumentNullException(nameof (requirementSupport));
    bool isError = false;
    List<RequirementExternalData> requirementExternalDataList;
    List<long> longList;
    using (IDisposable apiSession = requirementSupport.CreateApiSession())
    {
      requirementExternalDataList = new CadmechRequirementsReader().ReadRequirementData(requirementSupport.GetIMTextDocumentProvider(documentInfo.ObjectId, documentInfo.FilePath, apiSession));
      longList = this.ScanSourceDocumentArticles(documentInfo.ObjectId);
    }
    string empty1 = string.Empty;
    string empty2 = string.Empty;
    string empty3 = string.Empty;
    if (longList.Count > 0)
    {
      foreach (long num in longList)
      {
        this._outputView.WriteString("Вывод", $"Начата обработка изделия с идентификатором '{num}'.");
        using (SessionKeeper sk = new SessionKeeper())
        {
          IDBRelationCollection relationCollection = sk.Session.GetRelationCollection(MSOfficeAddinsConstants.ObjectsAddedByReferenceRelationTypeID);
          DBRecordSetParams paramSet = new DBRecordSetParams(new ConditionStructure[2]
          {
            new ConditionStructure(new Guid(this._requirementRelationAttrGuid), RelationalOperators.NotEmpty, (object) "", LogicalOperators.AND, 0),
            new ConditionStructure(new Guid(this._requirementLinkObject), RelationalOperators.Equal, (object) Math.Abs(documentInfo.ObjectId), LogicalOperators.NONE, 0)
          }, new object[3]
          {
            (object) ObligatoryObjectAttributes.F_PRJLINK_ID,
            (object) ObligatoryObjectAttributes.F_PART_ID,
            (object) MetaDataHelper.GetAttributeID((object) this._requirementRelationAttrGuid)
          });
          paramSet.AddColumnDescriptors(new ColumnDescriptor[1]
          {
            new ColumnDescriptor((object) MetaDataHelper.GetAttributeID((object) this._requirementLinkObject), AttributeSourceTypes.Relation, ColumnContents.ID, ColumnNameMapping.Name, SortOrders.ASC, 0)
          }, new List<int>());
          DataTable dataTable = relationCollection.ConsistFrom(paramSet, num);
          List<(long, long, string, long)> valueTupleList1 = new List<(long, long, string, long)>();
          foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
          {
            IDBObject objectById = sk.Session.GetObjectByID(Convert.ToInt64(row.ItemArray[1]), false);
            if (objectById != null)
              valueTupleList1.Add((Convert.ToInt64(row.ItemArray[0]), objectById.ObjectID, row.ItemArray[2].ToString(), Convert.ToInt64(row.ItemArray[3])));
          }
          List<(long, long, string, long)> source = new List<(long, long, string, long)>();
          if (valueTupleList1.Count > 0 && requirementExternalDataList.Count == 0)
            source.AddRange((IEnumerable<(long, long, string, long)>) valueTupleList1);
          foreach (RequirementExternalData requirementExternalData in requirementExternalDataList)
          {
            if (valueTupleList1.Count > 0 && requirementExternalData.Requirements.Count == 0)
            {
              source.AddRange((IEnumerable<(long, long, string, long)>) valueTupleList1);
            }
            else
            {
              foreach (Intermech.Services.Requirement.Requirement requirement1 in requirementExternalData.Requirements)
              {
                Intermech.Services.Requirement.Requirement requirement = requirement1;
                List<(long, long, string, long)> valueTupleList2 = new List<(long, long, string, long)>();
                if (valueTupleList1.Count > 0)
                  valueTupleList2 = valueTupleList1.Where<(long, long, string, long)>((System.Func<(long, long, string, long), bool>) (x => x.requirementGuid == requirement.Guid)).ToList<(long, long, string, long)>();
                string[] refs = requirement.Refs;
                if ((refs != null ? (refs.Length != 0 ? 1 : 0) : 0) != 0)
                {
                  foreach (string imbaseKey in requirement.Refs)
                  {
                    Tuple<long, int, string> result = Intermech.Tools.Data.ImbaseHelper.FindOrCreateImbaseObject(imbaseKey, empty1, empty2, empty3);
                    if (result != null)
                    {
                      List<(long, long, string, long)> list1 = source.Where<(long, long, string, long)>((System.Func<(long, long, string, long), bool>) (x => x.childID == result.Item1)).ToList<(long, long, string, long)>();
                      if (list1.Count > 0)
                      {
                        if (this.ChangeRequirementInDataBase(sk, list1[0].Item1, requirement, ref isError))
                          source.RemoveAll((Predicate<(long, long, string, long)>) (x => x.childID == result.Item1));
                      }
                      else if (valueTupleList1.Count == 0 || valueTupleList2.Count == 0)
                      {
                        try
                        {
                          this.CreateNewIncludeByLinkRelation(documentInfo, num, relationCollection, result.Item1, requirement);
                        }
                        catch (Exception ex)
                        {
                          this._outputView.WriteString("Ошибки", $"Ошибка обработки технического требования{(requirement.Index > 0 ? $" №{requirement.Index}" : "")}: {ex.Message}");
                          isError = true;
                        }
                      }
                      else
                      {
                        List<(long, long, string, long)> list2 = valueTupleList2.Where<(long, long, string, long)>((System.Func<(long, long, string, long), bool>) (x => x.childID == Math.Abs(result.Item1))).ToList<(long, long, string, long)>();
                        if (list2.Count > 0)
                        {
                          this.ChangeRequirementInDataBase(sk, list2[0].Item1, requirement, ref isError);
                        }
                        else
                        {
                          List<(long, long, string, long)> list3 = valueTupleList1.Where<(long, long, string, long)>((System.Func<(long, long, string, long), bool>) (x => x.childID == Math.Abs(result.Item1))).ToList<(long, long, string, long)>();
                          if (list3.Count > 0)
                          {
                            this.ChangeRequirementInDataBase(sk, list3[0].Item1, requirement, ref isError);
                          }
                          else
                          {
                            source.AddRange((IEnumerable<(long, long, string, long)>) valueTupleList2);
                            try
                            {
                              this.CreateNewIncludeByLinkRelation(documentInfo, num, relationCollection, result.Item1, requirement);
                            }
                            catch (Exception ex)
                            {
                              this._outputView.WriteString("Ошибки", $"Ошибка обработки технического требования{(requirement.Index > 0 ? $" №{requirement.Index}" : "")}: {ex.Message}");
                              isError = true;
                            }
                          }
                        }
                      }
                    }
                    else
                    {
                      this._outputView.WriteString("Ошибки", $"Ошибка обработки технического требования{(requirement.Index > 0 ? $" №{requirement.Index}" : "")}. Ссылка с GUID '{imbaseKey}' не найдена.");
                      isError = true;
                    }
                  }
                }
                else if (valueTupleList2.Count > 0)
                  source.AddRange((IEnumerable<(long, long, string, long)>) valueTupleList2);
                valueTupleList1.RemoveAll((Predicate<(long, long, string, long)>) (x => x.requirementGuid == requirement.Guid));
              }
            }
          }
          if (source.Count > 0)
          {
            foreach ((long, long, string, long) valueTuple in source)
              sk.Session.GetRelation(valueTuple.Item1, false)?.Delete(0L);
            source.Clear();
          }
          if (valueTupleList1.Count > 0)
          {
            foreach ((long, long, string, long) valueTuple in valueTupleList1)
              sk.Session.GetRelation(valueTuple.Item1, false)?.Delete(0L);
            valueTupleList1.Clear();
          }
        }
        this._outputView.WriteString("Вывод", $"Обработка изделия с идентификатором '{num}' завершена.");
      }
    }
    if (!isError)
      return;
    int num1 = (int) MessageBox.Show("В процессе получения технических требований возникли ошибки. Подробности смотрите в окне 'Ошибки'.", "Внимание");
    this._outputView.Activate("Ошибки");
    this._outputView.ShowView();
  }

  private bool ChangeRequirementInDataBase(
    SessionKeeper sk,
    long relationID,
    Intermech.Services.Requirement.Requirement requirement,
    ref bool isError)
  {
    IDBRelation relation = sk.Session.GetRelation(relationID, false);
    if (relation == null)
      return false;
    try
    {
      relation.Attributes.AddAttribute(new Guid(this._requirementRelationAttrGuid), false, new object[1]
      {
        (object) requirement.Guid
      });
      relation.Attributes.AddAttribute(new Guid(this._requirementIndex), false, new object[1]
      {
        (object) requirement.Index
      });
    }
    catch (Exception ex)
    {
      this._outputView.WriteString("Ошибки", $"Ошибка обработки технического требования{(requirement.Index > 0 ? $" №{requirement.Index}" : "")}: {ex.Message}");
      isError = true;
      return false;
    }
    return true;
  }

  /// <summary>
  /// Создаём новую связь "Объект, добавленный в состав по ссылке"
  /// </summary>
  /// <param name="documentInfo"></param>
  /// <param name="articleObjectID">Идентификатор изделия с которым будет создаваться связь</param>
  /// <param name="relCollection"></param>
  /// <param name="partObjectID"></param>
  /// <param name="requirement"></param>
  private void CreateNewIncludeByLinkRelation(
    CaptureChangesDocumentInfo documentInfo,
    long articleObjectID,
    IDBRelationCollection relCollection,
    long partObjectID,
    Intermech.Services.Requirement.Requirement requirement)
  {
    IDBRelation dbRelation = relCollection.Create(articleObjectID, partObjectID);
    dbRelation.Attributes.AddAttribute(Intermech.Imbase.Consts.IncludeInCompositionByLinkAttId, false, new object[1]
    {
      (object) true
    });
    dbRelation.Attributes.AddAttribute(MetaDataHelper.GetAttributeID((object) this._requirementIndex), false, new object[1]
    {
      (object) requirement.Index
    });
    dbRelation.Attributes.AddAttribute(MetaDataHelper.GetAttributeID((object) this._requirementLinkObject), false, new object[1]
    {
      (object) Math.Abs(documentInfo.ObjectId)
    });
    dbRelation.Attributes.AddAttribute(MetaDataHelper.GetAttributeID((object) this._requirementRelationAttrGuid), false, new object[1]
    {
      (object) requirement.Guid
    });
  }

  private List<long> ScanSourceDocumentArticles(long objectID)
  {
    List<long> longList = new List<long>();
    DataTable documentArticles = DBDocumentHelper.FindDocumentArticles(objectID, VersionsRuleSources.GetEditorRule(), false);
    for (int index = 0; index < documentArticles.Rows.Count; ++index)
      longList.Add(Convert.ToInt64(documentArticles.Rows[index][1]));
    return longList;
  }
}
