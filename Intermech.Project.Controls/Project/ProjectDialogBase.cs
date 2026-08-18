// Decompiled with JetBrains decompiler
// Type: Intermech.Project.ProjectDialogBase
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
public class ProjectDialogBase : 
  IpsBaseDialog,
  IComponent,
  IDisposable,
  IDropTarget,
  ISynchronizeInvoke,
  IWin32Window,
  IBindableComponent,
  IContainerControl,
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
  public ProjectDialogBase()
  {
  }

  public ProjectDialogBase([CanBeNull] System.IServiceProvider ownerServices, [NotNull, NotEmpty] string contextName)
    : base(ownerServices, Intermech.Diagnostics.Check.Optional.ArgumentNotNullOrWhitespace(contextName, nameof (contextName)))
  {
  }

  public ProjectDialogBase([CanBeNull] Form centerOnForm, [NotNull] string contextName)
    : base(centerOnForm, contextName: Intermech.Diagnostics.Check.Optional.ArgumentNotNullOrWhitespace(contextName, nameof (contextName)))
  {
  }

  public ProjectDialogBase([CanBeNull] Form centerOnForm, [CanBeNull] System.IServiceProvider ownerServices, [NotNull] string contextName)
    : base(centerOnForm, ownerServices, Intermech.Diagnostics.Check.Optional.ArgumentNotNullOrWhitespace(contextName, nameof (contextName)))
  {
  }

  /// <summary>Редактор проекта, в контексте которого вызван диалог</summary>
  [CanBeNull]
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public ProjectView ProjectView
  {
    [DebuggerStepThrough, MethodImpl(MethodImplOptions.AggressiveInlining)] get
    {
      return this.InDesignMode ? (ProjectView) null : this.GetService<ProjectView>();
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
      return this.InDesignMode ? (ClientProject) null : this.GetService<ProjectView>().Project;
    }
  }

  /// <summary>Идентификатор версии проекта, в контексте которого вызван диалог</summary>
  [Browsable(false)]
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public long ProjectObjectVersionID
  {
    get
    {
      if (this.InDesignMode)
        return 0;
      ClientProject project = this.Project;
      return project == null ? 0L : project.ObjectID;
    }
  }
}
