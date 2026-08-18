
// Type: Intermech.Client.Core.FormDesigner.ScriptForButtonsAction
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core.FormDesigner.Controls;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Localization;
using Intermech.PropertyEditors;
using Intermech.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;


namespace Intermech.Client.Core.FormDesigner;

/// <summary>Выполнить сценарий.</summary>
internal class ScriptForButtonsAction : IFormDesignerActionHandler
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
    if ((button as AttrButton).FormDesignerActionParams is ScriptForButtonsActionParams designerActionParams && designerActionParams.Script != Guid.Empty)
    {
      if (designerActionParams.ButtonEnabled == EnabledScriptForButtons.Always)
        flag = true;
      else if (designerActionParams.ButtonEnabled == EnabledScriptForButtons.DataChanged)
        flag = (form as DesForm).Modified;
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
    DesForm owner = form as DesForm;
    AttrButton attrButton = button as AttrButton;
    try
    {
      if (!(attrButton.FormDesignerActionParams is ScriptForButtonsActionParams designerActionParams) || designerActionParams.Script == Guid.Empty)
        throw new ApplicationException(LocalizationHolder.rm.GetString("FormDesigner_Scenario_Null"));
      string scriptCode = this.GetScriptCode(designerActionParams.Script);
      if (string.IsNullOrEmpty(scriptCode))
        throw new ApplicationException(LocalizationHolder.rm.GetString("FormDesigner_Scenario_EmptyText"));
      long ID1 = 0;
      List<AttributeValues> attributeValuesList1 = new List<AttributeValues>(0);
      List<AttributeValues> attributeValuesList2 = new List<AttributeValues>(0);
      long ID2 = 0;
      List<AttributeValues> attributeValuesList3 = new List<AttributeValues>(0);
      List<AttributeValues> attributeValuesList4 = new List<AttributeValues>(0);
      IElementInfo info = owner.Info;
      if (info != null && info.ElementKind == AttributableElements.Object)
      {
        ID1 = info.ElementIdentifier;
        attributeValuesList1 = owner.GetAttributeValuesFromControls(ID1);
        attributeValuesList2 = owner.GetAdditionalValues(ID1);
        IElementInfo relationInfo = owner.RelationInfo;
        if (relationInfo != null && relationInfo.ElementKind == AttributableElements.Relation)
        {
          ID2 = relationInfo.ElementIdentifier;
          attributeValuesList3 = owner.GetAttributeValuesFromControls(ID2);
          attributeValuesList4 = owner.GetAdditionalValues(ID2);
        }
      }
      else
      {
        ID2 = info.ElementIdentifier;
        attributeValuesList3 = owner.GetAttributeValuesFromControls(ID2);
        attributeValuesList4 = owner.GetAdditionalValues(ID2);
      }
      List<AttributeValues> newAVs1 = (List<AttributeValues>) null;
      List<AttributeValues> newAVs2 = (List<AttributeValues>) null;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        AttributeValidationScriptParameters scriptParameters1 = new AttributeValidationScriptParameters()
        {
          UserSession = sessionKeeper.Session,
          ObjectID = ID1,
          RelationID = ID2,
          ObjectAttributeValues = new List<AttributeValues>()
        };
        scriptParameters1.ObjectAttributeValues.AddRange((IEnumerable<AttributeValues>) this.CloneAttributeValues(attributeValuesList1));
        scriptParameters1.ObjectAttributeValues.AddRange((IEnumerable<AttributeValues>) this.CloneAttributeValues(attributeValuesList2));
        scriptParameters1.RelationAttributeValues = new List<AttributeValues>();
        scriptParameters1.RelationAttributeValues.AddRange((IEnumerable<AttributeValues>) this.CloneAttributeValues(attributeValuesList3));
        scriptParameters1.RelationAttributeValues.AddRange((IEnumerable<AttributeValues>) this.CloneAttributeValues(attributeValuesList4));
        AttributeValidationScriptParameters scriptParameters2 = (AttributeValidationScriptParameters) ServiceUtils.GetService<ICSharpScriptExecutor>((object) ApplicationServices.Container, false).Execute(scriptCode, CSharpScriptInvocationOptions.Default, (object) scriptParameters1);
        newAVs1 = scriptParameters2.ObjectAttributeValues;
        newAVs2 = scriptParameters2.RelationAttributeValues;
      }
      List<AttributeValues> changedAttribute1 = this.GetChangedAttribute(attributeValuesList1, attributeValuesList2, newAVs1);
      List<AttributeValues> changedAttribute2 = this.GetChangedAttribute(attributeValuesList3, attributeValuesList4, newAVs2);
      owner.AttributeChanging((IEnumerable<AttributeValues>) changedAttribute1, (IEnumerable<AttributeValues>) changedAttribute2);
    }
    catch (Exception ex)
    {
      string messageOnly = this.TryConvertToMessageOnly(ex);
      if (messageOnly != null)
      {
        int num = (int) MessageBox.Show((IWin32Window) owner, messageOnly, LocalizationHolder.rm.GetString("FormDesigner_Scenario_Error"), MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
      else
        throw;
    }
  }

  private string TryConvertToMessageOnly(Exception exception)
  {
    if (exception is ScriptInvocationException && exception.InnerException != null)
      exception = exception.InnerException;
    return exception is ApplicationException || exception is ISimpleMessageException ? exception.Message : (string) null;
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="scriptGuid"></param>
  /// <returns></returns>
  private string GetScriptCode(Guid scriptGuid)
  {
    string empty = string.Empty;
    QuickObjectInfo objectInfo = ApplicationServices.Container.GetService<IObjectsInfoCache>().GetObjectInfo(scriptGuid);
    if (objectInfo.Empty)
      throw new Exception(LocalizationHolder.rm.GetString("FormDesigner_Scenario_Info_Null"));
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return ((sessionKeeper.Session.GetObjectActualCopy(objectInfo.ObjectID, false) ?? throw new Exception(string.Format(LocalizationHolder.rm.GetString("FormDesigner_Scenario_Object_Null"), (object) objectInfo.ObjectID))).GetAttributeByGuid(new Guid("cad00366-306c-11d8-b4e9-00304f19f545")) ?? throw new Exception(LocalizationHolder.rm.GetString("FormDesigner_Scenario_Attribute_Null"))).Value.ToString();
  }

  /// <summary>
  /// 
  /// </summary>
  /// <param name="avs"></param>
  /// <returns></returns>
  private List<AttributeValues> CloneAttributeValues(List<AttributeValues> avs)
  {
    List<AttributeValues> result = new List<AttributeValues>();
    avs?.ForEach((Action<AttributeValues>) (x => result.Add(x.Clone() as AttributeValues)));
    return result;
  }

  /// <summary>Получить список измененных атрибутов.</summary>
  /// <param name="AVs">Список атрибутов объекта/связи</param>
  /// <param name="addAVs">Список дополнительных атрибутов объекта/связи</param>
  /// <param name="newAVs">Список новых атриьутов объекта/связи</param>
  /// <returns>Список измененных атрибутов</returns>
  private List<AttributeValues> GetChangedAttribute(
    List<AttributeValues> AVs,
    List<AttributeValues> addAVs,
    List<AttributeValues> newAVs)
  {
    List<AttributeValues> changedAttribute = (List<AttributeValues>) null;
    if (newAVs != null && newAVs.Count > 0)
    {
      changedAttribute = new List<AttributeValues>(newAVs.Count);
      foreach (AttributeValues newAv in newAVs)
      {
        AttributeValues av = newAv;
        AttributeValues attributeValues1 = AVs.FirstOrDefault<AttributeValues>((Func<AttributeValues, bool>) (x => x.AttributeID == av.AttributeID));
        if (attributeValues1 != null)
        {
          if (AttributeValues.ValuesEquals(attributeValues1.Values, av.Values))
            continue;
        }
        else
        {
          AttributeValues attributeValues2 = addAVs.FirstOrDefault<AttributeValues>((Func<AttributeValues, bool>) (x => x.AttributeID == av.AttributeID));
          if (attributeValues2 != null && AttributeValues.ValuesEquals(attributeValues2.Values, av.Values))
            continue;
        }
        changedAttribute.Add(av);
      }
    }
    return changedAttribute;
  }
}
