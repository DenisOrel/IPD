// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.OrganizerPlugin
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Client.Core.Organizer;
using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Metadata;
using Intermech.Navigator.Interfaces;
using Intermech.Project.Controls.Properties;
using System;

#nullable disable
namespace Intermech.Project.Controls;

internal class OrganizerPlugin
{
  public static readonly Guid OrganizerNodeGuid = new Guid("{5983180F-A29C-4158-AACD-4ED8FD62C599}");
  public static readonly Guid OrganizerChiefNodeGuid = new Guid("{085AFFE1-4C08-424a-84AA-C43D972ADCF0}");
  [NotNull]
  private static readonly InitOnceGuardian _initOnce = new InitOnceGuardian();

  public static void Init()
  {
    OrganizerPlugin._initOnce.Invoke((Action) (() =>
    {
      IOrganizerService service1 = ApplicationServices.Container.GetService<IOrganizerService>(false);
      if (service1 == null)
        return;
      NodeColumnCollection columns = new NodeColumnCollection();
      Guid columnSchemeGuid = Intermech.Navigator.Consts.ObjectObligatoryColumnSchemeGuid;
      IColumnSchemes service2 = ApplicationServices.Container.GetService<IColumnSchemes>();
      columns.Add(service2.CreateColumn(columnSchemeGuid, (object) ObligatoryObjectAttributes.CAPTION));
      ConditionStructure[] conditions1 = new ConditionStructure[4]
      {
        new ConditionStructure(-4, RelationalOperators.In, (object) new int[2]
        {
          (int) (IpsMetadataEntityBase<int>) Intermech.Project.LCStep.Sent,
          (int) (IpsMetadataEntityBase<int>) Intermech.Project.LCStep.Executing
        }, LogicalOperators.AND, 0, false),
        new ConditionStructure(-22, RelationalOperators.Equal, (object) CurrentUser.ID, LogicalOperators.AND, 0, false),
        new ConditionStructure((int) (IpsMetadataEntityBase<int>) Intermech.Project.Attributes.ResourceIsChief, RelationalOperators.NotExistsOrEmpty, (object) 0, LogicalOperators.OR, 1, false),
        new ConditionStructure((int) (IpsMetadataEntityBase<int>) Intermech.Project.Attributes.ResourceIsChief, RelationalOperators.NotEqual, (object) true, LogicalOperators.AND, -1, false)
      };
      ConditionStructure[] conditions2 = new ConditionStructure[3]
      {
        new ConditionStructure(-4, RelationalOperators.In, (object) new int[3]
        {
          (int) (IpsMetadataEntityBase<int>) Intermech.Project.LCStep.Sent,
          (int) (IpsMetadataEntityBase<int>) Intermech.Project.LCStep.Executing,
          (int) (IpsMetadataEntityBase<int>) Intermech.Project.LCStep.Validating
        }, LogicalOperators.AND, 0, false),
        new ConditionStructure(-22, RelationalOperators.Equal, (object) CurrentUser.ID, LogicalOperators.AND, 0, false),
        new ConditionStructure((int) (IpsMetadataEntityBase<int>) Intermech.Project.Attributes.ResourceIsChief, RelationalOperators.Equal, (object) true, LogicalOperators.AND, 0, false)
      };
      try
      {
        service1.RegisterNode(OrganizerPlugin.OrganizerNodeGuid, (int) (IpsMetadataEntityBase<int>) Intermech.Project.RelationTypes.Resources, (int) (IpsMetadataEntityBase<int>) Intermech.Metadata.ObjectTypes.User, new int[2]
        {
          (int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Task,
          (int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Project
        }, conditions1, columns, Resources.OrganizerNodeMyTasks, Intermech.Client.Services.IconService.IndexOf(4, (int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Task));
        service1.RegisterNode(OrganizerPlugin.OrganizerChiefNodeGuid, (int) (IpsMetadataEntityBase<int>) Intermech.Project.RelationTypes.Resources, (int) (IpsMetadataEntityBase<int>) Intermech.Metadata.ObjectTypes.User, new int[2]
        {
          (int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Task,
          (int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Project
        }, conditions2, columns, Resources.OrganizerNodeTasksIControl, Intermech.Client.Services.IconService.IndexOf(4, (int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Task));
      }
      catch
      {
      }
    }));
  }
}
