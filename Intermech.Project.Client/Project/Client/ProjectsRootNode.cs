// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Client.ProjectsRootNode
// Assembly: Intermech.Project.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D968BDD9-29F0-4E24-8F57-6E851EE47258
// Assembly location: D:\IPS\Client\Intermech.Project.Client.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Interfaces.Client;
using Intermech.Metadata;
using Intermech.Navigator.DBObjectTypes;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Persistence;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Project.Client;

public class ProjectsRootNode : ObjectTypeNode, INode, INodeItems, IContextAware, INodeNotifications
{
  public const string GuidStr = "E40E0222-8A4F-48DA-B12E-6DD1813AE9FD";
  private static readonly Guid _globalNodeGuid = new Guid("C3E8CC27-EC37-4B00-AF0C-E3D14A4F763E");
  private const string GlobalNodeGuidStr = "C3E8CC27-EC37-4B00-AF0C-E3D14A4F763E";
  private const int OrderID = 30;
  [NotNull]
  private static readonly InitOnceGuardian _initOnceGuardian = new InitOnceGuardian();

  public static int ID
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Intermech.Navigator.Consts.IMProjectRootNodeTypeID;
    [MethodImpl(MethodImplOptions.AggressiveInlining)] internal set
    {
      Intermech.Navigator.Consts.IMProjectRootNodeTypeID = value;
    }
  }

  public static Guid Guid
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Intermech.Navigator.Consts.IMProjectRootNodeGuid;
  }

  public ProjectsRootNode()
    : base((int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Project, AccessRights.Enabled)
  {
  }

  public static void Register()
  {
    ProjectsRootNode._initOnceGuardian.Invoke((Action) (() =>
    {
      ProjectsRootNode.ID = ServicesManager.ServiceContainer.GetService<IGuidMapper>().Register(ProjectsRootNode.Guid);
      Intermech.Client.Services.Factory.AddNodeType(ProjectsRootNode.ID, typeof (ProjectsRootNode));
      Intermech.Client.Services.Factory.AddGlobalNode(ProjectsRootNode._globalNodeGuid, (IDescriptor) new ProjectsRootNode.Descriptor(), 30);
    }));
  }

  public class Descriptor : Intermech.Navigator.DBObjectTypes.Descriptor
  {
    public Descriptor()
      : base((int) (IpsMetadataEntityBase<int>) Intermech.Project.ObjectTypes.Project)
    {
    }

    protected Descriptor([NotNull] PersistentState state)
      : base(state)
    {
    }
  }
}
