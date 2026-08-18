// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Editors
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using Intermech.Workflow.Design;
using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

#nullable disable
namespace Intermech.Workflow;

/// <summary></summary>
public static class Editors
{
  [NotNull]
  [ItemNotNull]
  public static EditorsList List
  {
    [MethodImpl(MethodImplOptions.AggressiveInlining)] get => Holder.Editors;
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void RegisterEditor([NotNull] Control form, [NotEmpty] long id, bool isEditMode)
  {
    Editors.List.RegisterEditor(form, id, isEditMode);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void UnregisterEditor([NotNull] Control form)
  {
    Editors.List.UnregisterEditor(form);
  }

  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Control FindEditor([NotEmpty] long id, bool isEditMode)
  {
    return Editors.FindEditor(id, isEditMode, false);
  }

  [ContractAnnotation("throwIfNotFound:false => CanBeNull; throwIfNotFound:true => NotNull")]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Control FindEditor([NotEmpty] long id, bool isEditMode, bool throwIfNotFound)
  {
    Control editor = Editors.List.FindEditor(id, isEditMode);
    return !(editor == null & throwIfNotFound) ? editor : throw new NullReferenceException($"Workflow editor with id = {id} not found!");
  }
}
