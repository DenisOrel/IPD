
// Type: Intermech.Navigator.Conditions.ConditionForm
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Client.Core;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using System.Windows.Forms;


namespace Intermech.Navigator.Conditions;

public class ConditionForm : Form
{
  protected IConditionDataProvider dataProvider;
  protected long selectionID;
  protected ConditionStructure conditionStructure;
  protected int[] objectTypeIDs;

  public ConditionForm()
  {
    if (!FormStorage.LoadLayout((Control) this))
      return;
    this.StartPosition = FormStartPosition.Manual;
  }

  public void InitializeData(
    long selectionID,
    IConditionDataProvider dataProvider,
    int[] objectTypeIDs)
  {
    this.InitializeData(selectionID, dataProvider, ConditionStructure.Empty, objectTypeIDs);
  }

  public void InitializeData(
    long selectionID,
    IConditionDataProvider dataProvider,
    ConditionStructure conditionStructure,
    int[] objectTypeIDs)
  {
    this.dataProvider = dataProvider;
    this.selectionID = selectionID;
    this.conditionStructure = conditionStructure;
    this.objectTypeIDs = objectTypeIDs;
    this.OnInitialized();
  }

  protected virtual void OnInitialized()
  {
  }

  public virtual ConditionStructure Result => ConditionStructure.Empty;

  protected override void OnFormClosing(FormClosingEventArgs e)
  {
    FormStorage.SaveLayout((Control) this);
    base.OnFormClosing(e);
  }
}
