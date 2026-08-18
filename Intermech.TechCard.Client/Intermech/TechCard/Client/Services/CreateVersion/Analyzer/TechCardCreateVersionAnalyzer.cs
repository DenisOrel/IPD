// Decompiled with JetBrains decompiler
// Type: Intermech.TechCard.Client.Services.CreateVersion.Analyzer.TechCardCreateVersionAnalyzer
// Assembly: Intermech.TechCard.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2CB0EA14-C772-4814-AD48-94FC696AFE3E
// Assembly location: D:\IPS\Client\Intermech.TechCard.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.TechCard.Client.xml

using Intermech.Commands;
using Intermech.Diagnostics;
using Intermech.ECO.Client;
using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.TechCard;
using Intermech.Localization;
using Intermech.Navigator.Interfaces;
using Intermech.TechCard.Client.Common.Forms;
using Intermech.TechCard.Client.Navigator.Descriptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

#nullable disable
namespace Intermech.TechCard.Client.Services.CreateVersion.Analyzer;

/// <summary>
/// 
/// </summary>
internal class TechCardCreateVersionAnalyzer
{
  /// <summary>
  /// 
  /// </summary>
  private readonly TechCardObjectCreateVersionAnalyzerParam _params;
  private readonly System.IServiceProvider _serviceProvider;

  /// <summary>
  /// 
  /// </summary>
  /// <returns></returns>
  private bool GetStepData(out TechCardCreateVersionAnalyzerStepData stepData)
  {
    IEnumerable<RelObjInfoItem> collection = this._params.CompositionProvider != null ? this._params.CompositionProvider.Execute() : (IEnumerable<RelObjInfoItem>) null;
    List<RelObjInfoItem> relObjInfoItemList = collection != null ? new List<RelObjInfoItem>(collection) : new List<RelObjInfoItem>();
    GenericListHelper.MakeUnique<RelObjInfoItem>(relObjInfoItemList);
    List<RelObjInfoItem> list = relObjInfoItemList.Where<RelObjInfoItem>((Func<RelObjInfoItem, bool>) (item => (TypedInfoItem) item.ProjInfo != (TypedInfoItem) null && TechCardConsts.Utils.IsTechcardObjectType((object) item.ProjInfo.ObjTypeID))).ToList<RelObjInfoItem>();
    stepData = new TechCardCreateVersionAnalyzerStepData((IEnumerable<RelObjInfoItem>) this._params.RelObjInfoItems.ToList<RelObjInfoItem>(), (IEnumerable<RelObjInfoItem>) list);
    return stepData.RelObjInfoItems.Any<RelObjInfoItem>() && stepData.CompositionItems.Any<RelObjInfoItem>();
  }

  /// <summary>Проверка наличия "проблем"</summary>
  /// <param name="stepData"></param>
  /// <returns></returns>
  private bool CheckStepErrors(TechCardCreateVersionAnalyzerStepData stepData)
  {
    if (!stepData.ErrorDescriptors.Any<IDescriptor>())
      return true;
    ObjectCommandsOptionsHolder service = ServiceUtils.GetService<ObjectCommandsOptionsHolder>((object) this._serviceProvider, false);
    if (service != null && service.Value.HasFlag((Enum) ObjectCommandsOptions.NoConfirmation))
      return false;
    TechcardErrorObjForm techcardErrorObjForm = new TechcardErrorObjForm();
    IDescriptor descriptor = (IDescriptor) new TechDescriptor(LocalizationHolder.rm.GetString("TechCard.Client_471"), stepData.ErrorDescriptors);
    string errorMsg = LocalizationHolder.rm.GetString("TechCard.Client_469");
    if (stepData.RelObjInfoItems.Count > 0)
    {
      techcardErrorObjForm.ShowBtn_OK = true;
      errorMsg += LocalizationHolder.rm.GetString("TechCard.Client_470");
    }
    else
      techcardErrorObjForm.ShowBtn_OK = false;
    techcardErrorObjForm.LoadData(errorMsg, descriptor);
    return techcardErrorObjForm.ShowDialog() == DialogResult.OK;
  }

  /// <summary>
  /// 
  /// </summary>
  public TechCardCreateVersionAnalyzer(
    [NotNull] TechCardObjectCreateVersionAnalyzerParam param,
    System.IServiceProvider serviceProvider)
  {
    this._params = param;
    this._serviceProvider = serviceProvider;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="stepData"></param>
  /// <returns></returns>
  public bool Execute([NotNull] out TechCardCreateVersionAnalyzerStepData stepData)
  {
    if (!this.GetStepData(out stepData))
    {
      stepData.DefaultCreateVersionHandler = true;
      return false;
    }
    foreach (TechCardCreateVersionAnalyzerStep analyzerStep in this._params.AnalyzerSteps)
    {
      if (!analyzerStep.Execute(stepData))
        return false;
    }
    return this.CheckStepErrors(stepData);
  }

  /// <summary>
  /// Список подписываемых технологических типов объектов, допускающих изменение по ИИ (включение в состав ИИ)
  /// </summary>
  /// <returns></returns>
  internal static ICollection<int> GetSignedTechCardTypeIds()
  {
    List<int> intList = new List<int>();
    List<int> childrenIdRecursive1 = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) TechCardConsts.ObjectTypes.TechAllBaseObjTypes.ToList<int>());
    GenericListHelper.MakeUnique<int>(childrenIdRecursive1);
    List<int> childrenIdRecursive2 = MetaDataHelper.GetObjectTypeChildrenIDRecursive(new Guid(RevReqHelper.guidObj_II));
    int relationTypeId = MetaDataHelper.GetRelationTypeID("cad0036b-306c-11d8-b4e9-00304f19f545");
    foreach (int parObjTypeID in childrenIdRecursive2)
    {
      List<IMSObjectType> childObjectTypes = MetaDataHelper.GetApplicabilityChildObjectTypes(parObjTypeID, relationTypeId);
      if (childObjectTypes != null)
      {
        foreach (IMSObjectType imsObjectType in childObjectTypes)
        {
          if (childrenIdRecursive1.BinarySearch(imsObjectType.ObjectTypeID) >= 0)
            intList.Add(imsObjectType.ObjectTypeID);
        }
      }
    }
    if (intList.Count != 0)
    {
      List<int> childrenIdRecursive3 = MetaDataHelper.GetObjectTypeChildrenIDRecursive((IEnumerable<int>) intList);
      intList.Clear();
      intList.AddRange((IEnumerable<int>) childrenIdRecursive3);
    }
    intList.RemoveAll((Predicate<int>) (item => MetaDataHelper.IsObjectTypeChildOf(item, TechCardConsts.ObjectTypes.TechDocID)));
    intList.RemoveAll((Predicate<int>) (item => MetaDataHelper.IsObjectTypeChildOf(item, TechCardConsts.ObjectTypes.ComlectTechDocBaseID)));
    return (ICollection<int>) intList.ToHashSet<int>();
  }
}
