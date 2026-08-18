// Decompiled with JetBrains decompiler
// Type: Intermech.Project.ProjectFormBase
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Common;
using Intermech.Diagnostics;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using Intermech.Project.Controls;
using Intermech.Windows.Forms;
using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Project;

/// <summary>Абстрактная база для диалогов в контексте редактора проектов</summary>
public class ProjectFormBase : 
  IpsBaseForm,
  IContainerControl,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IComponent,
  IDisposable,
  IContextAware,
  IControlServiceContainer,
  IAdvancedServiceContainer,
  IServiceContainer,
  System.IServiceProvider,
  ISupportSaveLocks,
  INamedContext,
  ICanBeReadOnly,
  ICanBeReadOnly2,
  IClientProjectContext,
  IProjectViewContext
{
  public ProjectFormBase()
  {
  }

  public ProjectFormBase([CanBeNull] System.IServiceProvider ownerServices, [NotNull] string contextName)
    : base(ownerServices, contextName)
  {
  }

  /// <summary>Редактор проекта, в контексте которого вызван диалог</summary>
  [NotNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ProjectView ProjectView
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetService<ProjectView>();
    }
  }

  /// <summary>Проект, в контексте которого вызван диалог</summary>
  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ClientProject Project
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.GetService<ProjectView>().Project;
    }
  }

  /// <summary>Идентификатор версии проекта, в контексте которого вызван диалог</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long ProjectObjectVersionID
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.Project.ObjectID;
    }
  }
}
