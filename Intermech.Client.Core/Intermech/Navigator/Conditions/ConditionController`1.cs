
// Type: Intermech.Navigator.Conditions.ConditionController`1
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions;

/// <summary>Базовый контроллер условия.</summary>
public abstract class ConditionController<TEditorForm> : IConditionController where TEditorForm : ConditionForm, new()
{
  protected IConditionsFormService conditionsFormService;
  private List<SelectionType> _supportedTypes;

  public ConditionController()
  {
    this.conditionsFormService = ServicesManager.GetService<IConditionsFormService>();
  }

  public abstract string VisibleName { get; }

  public virtual SelectionDataSource SupportedDataSource => SelectionDataSource.DataBase;

  public ConditionStructure CreateCondition(long selectionID, int[] objectTypeIDs)
  {
    TEditorForm editorForm = new TEditorForm();
    try
    {
      editorForm.InitializeData(selectionID, this.dataProvider, objectTypeIDs);
      return editorForm.ShowDialog() == DialogResult.OK ? editorForm.Result : ConditionStructure.Empty;
    }
    finally
    {
      if ((object) editorForm != null)
        ((IDisposable) editorForm).Dispose();
    }
  }

  public ConditionStructure EditCondition(
    long selectionID,
    ConditionStructure current,
    int[] objectTypeIDs)
  {
    TEditorForm editorForm = new TEditorForm();
    try
    {
      editorForm.InitializeData(selectionID, this.dataProvider, current, objectTypeIDs);
      return editorForm.ShowDialog() == DialogResult.OK ? editorForm.Result : current;
    }
    finally
    {
      if ((object) editorForm != null)
        ((IDisposable) editorForm).Dispose();
    }
  }

  public abstract bool IsHandleConditionStructure(ConditionStructure conditionStructure);

  public virtual bool HandleConditionCaption(
    ConditionStructure conditionStructure,
    out string condition,
    out string value)
  {
    condition = string.Empty;
    value = string.Empty;
    return false;
  }

  protected IConditionDataProvider dataProvider
  {
    get
    {
      return ServicesManager.GetService<IConditionDataProviderService>().GetDataProvider(this.SupportedDataSource);
    }
  }

  public virtual SelectionType[] SupportedTypes
  {
    get
    {
      if (this._supportedTypes == null)
      {
        this._supportedTypes = new List<SelectionType>();
        foreach (SelectionType selectionType in Enum.GetValues(typeof (SelectionType)))
          this._supportedTypes.Add(selectionType);
      }
      return this._supportedTypes.ToArray();
    }
  }

  public virtual bool AttributesCondition => false;

  public virtual bool IsInnerSupported => true;
}
