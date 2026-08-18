// Decompiled with JetBrains decompiler
// Type: Intermech.Project.ParcipiantInfo
// Assembly: Intermech.Project, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 567C9AEE-D835-426E-92F2-8965F6504E2D
// Assembly location: D:\IPS\Client\Intermech.Project.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.xml

using Intermech.Diagnostics;
using System;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Project;

/// <summary>Структура с данными на исполнителя задачи/проекта</summary>
[Serializable]
public class ParcipiantInfo
{
  /// <summary>Идентификатор объекта пользователя (Не идентификатор версии объекта!)</summary>
  [NotEmpty]
  public long ID { get; private set; }

  /// <summary>Идентификатор версии объекта пользователя</summary>
  [NotEmpty]
  public long ObjectVerID { get; private set; }

  /// <summary>Заголовок объекта пользователя</summary>
  [NotNull]
  public string Caption { get; private set; }

  /// <summary>Признак руководителя</summary>
  public bool IsChief { get; private set; }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public ParcipiantInfo([NotEmpty] in long id, [NotEmpty] in long objectVerID, [NotNull] in string caption, in bool isChief)
  {
    Intermech.Diagnostics.Check.ArgumentValueNotEmpty<long>(id, nameof (id));
    Intermech.Diagnostics.Check.ArgumentValueNotEmpty<long>(objectVerID, nameof (objectVerID));
    Intermech.Diagnostics.Check.ArgumentNotNull<string>(caption, nameof (caption));
    this.ID = id;
    this.ObjectVerID = objectVerID;
    this.Caption = caption;
    this.IsChief = isChief;
  }
}
