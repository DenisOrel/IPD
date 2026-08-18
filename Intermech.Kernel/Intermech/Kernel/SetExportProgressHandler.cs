// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.SetExportProgressHandler
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Briefcase;
using System;


namespace Intermech.Kernel;

public delegate void SetExportProgressHandler(
  object sender,
  Guid NumOfBriefcase,
  BriefcaseExportProgress briefcaseExportProgress);
