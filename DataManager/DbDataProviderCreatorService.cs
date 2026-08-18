// Decompiled with JetBrains decompiler
// Type: Intermech.Server.Data.DbDataProviderCreatorService
// Assembly: DataManager, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: E7B48B20-48DA-43CF-8D62-6AD3E6FD5CCD
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\DataManager.dll

using Intermech.Diagnostics;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

#nullable disable
namespace Intermech.Server.Data;

public sealed class DbDataProviderCreatorService
{
  private readonly IEventLogWriter _eventLogWriter;
  private readonly Dictionary<string, IDbDataProviderCreator> _dataProviderNameList;
  private readonly bool _isInitialized;

  public DbDataProviderCreatorService(
    IEventLogWriter eventLogWriter,
    IEnumerable<string> dataProviderAssemblyFiles)
  {
    if (eventLogWriter == null)
      throw new ArgumentNullException(nameof (eventLogWriter));
    if (dataProviderAssemblyFiles == null)
      throw new ArgumentNullException(nameof (dataProviderAssemblyFiles));
    this._eventLogWriter = eventLogWriter;
    this._dataProviderNameList = new Dictionary<string, IDbDataProviderCreator>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    this.TryAddDataProviderCreator((IDbDataProviderCreator) new SqlDataProviderCreator());
    foreach (string providerAssemblyFile in dataProviderAssemblyFiles)
    {
      if (!string.IsNullOrEmpty(providerAssemblyFile))
        this.AddDataProvidersFromAssembly(providerAssemblyFile);
    }
    this._isInitialized = true;
  }

  private void AddDataProvidersFromAssembly(string assemblyFile)
  {
    try
    {
      foreach (Type exportedType in Assembly.LoadFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyFile)).GetExportedTypes())
      {
        if (!exportedType.IsAbstract && !exportedType.IsGenericTypeDefinition && typeof (IDbDataProviderCreator).IsAssignableFrom(exportedType))
        {
          ConstructorInfo constructor = exportedType.GetConstructor(Type.EmptyTypes);
          if (constructor != (ConstructorInfo) null)
            this.TryAddDataProviderCreator((IDbDataProviderCreator) constructor.Invoke((object[]) null));
        }
      }
    }
    catch (Exception ex)
    {
      this._eventLogWriter.Write($"Ошибка загрузки провайдера данных {assemblyFile}: {ex.Message}");
    }
  }

  private void TryAddDataProviderCreator(IDbDataProviderCreator dataProviderCreator)
  {
    if (this._dataProviderNameList.ContainsKey(dataProviderCreator.Name))
      return;
    this._dataProviderNameList.Add(dataProviderCreator.Name, dataProviderCreator);
  }

  public bool CanCreate(string dataProviderName)
  {
    return dataProviderName != null ? this._dataProviderNameList.ContainsKey(dataProviderName) : throw new ArgumentNullException(nameof (dataProviderName));
  }

  public IDbDataProvider Create(string dataProviderName)
  {
    if (dataProviderName == null)
      throw new ArgumentNullException(nameof (dataProviderName));
    IDbDataProviderCreator dataProviderCreator;
    if (this._dataProviderNameList.TryGetValue(dataProviderName, out dataProviderCreator))
      return dataProviderCreator.CreateDataProvider();
    throw new KernelException($"Unable to find an instance of type {"IDbDataProviderCreator"} by name '{dataProviderName}'.");
  }
}
