// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Controls.ProjectSettings
// Assembly: Intermech.Project.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 800227AD-4498-4DB4-89F4-06C715004A90
// Assembly location: D:\IPS\Client\Intermech.Project.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Project.Controls.xml

using Intermech.Diagnostics;
using Intermech.Interfaces;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

#nullable disable
namespace Intermech.Project.Controls;

[Serializable]
public class ProjectSettings
{
  public static ProjectSettings Cfg { get; private set; }

  public static void Init() => ProjectSettings.Cfg = ProjectSettings.Load();

  public void Save()
  {
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBConfigurations configurations = sessionKeeper.Session.Configurations;
      MemoryStream serializationStream = new MemoryStream();
      new BinaryFormatter().Serialize((Stream) serializationStream, (object) this);
      BlobInformation config_info = new BlobInformation(serializationStream.Length, serializationStream.Length, DateTime.Now, nameof (ProjectSettings), ArcMethods.NotPacked, "b");
      byte[] array = serializationStream.ToArray();
      configurations.WriteConfigData(config_info, array);
      serializationStream.Close();
    }
  }

  [NotNull]
  protected static ProjectSettings Load()
  {
    ProjectSettings projectSettings = (ProjectSettings) null;
    try
    {
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        byte[] config_file;
        sessionKeeper.Session.Configurations.LoadConfigData(nameof (ProjectSettings), out BlobInformation _, out config_file);
        if (config_file.Length != 0)
        {
          MemoryStream serializationStream = new MemoryStream(config_file);
          serializationStream.Position = 0L;
          projectSettings = new BinaryFormatter().Deserialize((Stream) serializationStream) as ProjectSettings;
          serializationStream.Close();
        }
      }
    }
    catch
    {
    }
    return projectSettings ?? new ProjectSettings();
  }

  public static void Apply([NotNull] ProjectView view)
  {
  }
}
