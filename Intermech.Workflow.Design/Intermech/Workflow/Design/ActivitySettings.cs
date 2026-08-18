// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Design.ActivitySettings
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Expert;
using Intermech.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;

#nullable disable
namespace Intermech.Workflow.Design;

/// <summary>Общий класс со всеми настройками действий</summary>
public class ActivitySettings : INotifyPropertyChanged
{
  public string ActivityName { get; set; }

  public string ActivityDescription { get; set; }

  public int ActivityType { get; set; }

  public long ActivityObjectID { get; set; }

  public Image ActivityIcon { get; set; }

  public long ProcessID { get; set; }

  public ActivityStatus ActivityStatus { get; set; }

  public ParticipantList Participants { get; set; }

  public ActivityFlags ActivityFlags { get; set; }

  public Notifications Notifications { get; set; }

  public TempFormula ExpertCondition { get; set; }

  public ExpressionInfo ExpressionCondition { get; set; }

  public ConditionList ExpertConditions { get; set; }

  public ObservableCollection<ExpressionInfo> ExpressionConditions { get; set; }

  public RequiredSigns RequiredSigns { get; set; }

  public LCInfoList LcInfoList { get; set; }

  public PeriodInformation PeriodInformation { get; set; }

  public ScriptInfo[] ScriptInfos { get; set; }

  public Terms Terms { get; set; }

  public ExtProperties ExtProperties { get; set; }

  public long ObjectIDwithVars { get; set; }

  public List<int> PubFilteredTypes { get; set; }

  public List<int> PubFilteredRelTypes { get; set; }

  public List<Intermech.Expressions.Variable> ActivityExpressionAttributes { get; set; }

  public List<AttributeValues> ActivityAllAttributeValues { get; set; }

  public int SignsGroupID { get; set; }

  public ActivitySettings()
  {
    this.ScriptInfos = new ScriptInfo[2]
    {
      new ScriptInfo(),
      new ScriptInfo()
    };
    this.ActivityExpressionAttributes = new List<Intermech.Expressions.Variable>(0);
    this.ActivityAllAttributeValues = new List<AttributeValues>(0);
    this.ExpressionConditions = new ObservableCollection<ExpressionInfo>();
  }

  public event PropertyChangedEventHandler PropertyChanged;

  protected virtual void OnPropertyChanged(string propertyName = null)
  {
    PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
    if (propertyChanged == null)
      return;
    propertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
  }
}
