
// Type: Intermech.UI.Wpf.Controls.SharedResourceDictionary
// Assembly: Intermech.Bcl.Wpf, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 91600B17-2177-4703-BAB9-56FCFFBCBBA2
:\IPS\Client\Intermech.Bcl.Wpf.dll
// XML documentation location: D:\IPS\Client\Intermech.Bcl.Wpf.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;


namespace Intermech.UI.Wpf.Controls;

/// <summary>
/// Расширяет базовый класс <see cref="T:System.Windows.ResourceDictionary" />, добавляя кэширование
/// совместно используемых словарей.
/// </summary>
[Ambient]
[UsableDuringInitialization(true)]
public class SharedResourceDictionary : ResourceDictionary
{
  private bool inDesignMode;
  private Uri cachedSource;
  private ResourceDictionary cachedDictionary;
  private static readonly Dictionary<Uri, WeakReference<ResourceDictionary>> sharedDictionaries = new Dictionary<Uri, WeakReference<ResourceDictionary>>();

  /// <summary>
  /// Возвращает или задает режим работы текущего объекта в дизайнере.
  /// В этом случае кэширование выключается, чтобы можно было редактировать
  /// содержимое словаря и сразу видеть изменения.
  /// </summary>
  public bool InDesignMode
  {
    [DebuggerStepThrough] get => this.inDesignMode;
    set
    {
      if (this.inDesignMode == value)
        return;
      this.inDesignMode = value;
      this.UpdateMergedDictionaries();
    }
  }

  /// <summary>
  /// Возвращает или задает uri для загрузки содержимого словаря.
  /// </summary>
  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public Uri CachedSource
  {
    [DebuggerStepThrough] get => this.cachedSource;
    set
    {
      if (!(this.cachedSource != value))
        return;
      this.cachedSource = value;
      this.UpdateMergedDictionaries();
    }
  }

  private void UpdateMergedDictionaries()
  {
    if (this.cachedDictionary != null)
    {
      this.MergedDictionaries.Remove(this.cachedDictionary);
      this.cachedDictionary = (ResourceDictionary) null;
    }
    if (!(this.cachedSource != (Uri) null))
      return;
    ResourceDictionary target;
    if (this.InDesignMode)
    {
      target = this.LoadResourceDictionaryInContext((IUriContext) this, this.cachedSource);
    }
    else
    {
      WeakReference<ResourceDictionary> weakReference;
      if (!SharedResourceDictionary.sharedDictionaries.TryGetValue(this.cachedSource, out weakReference) || !weakReference.TryGetTarget(out target))
      {
        target = this.LoadResourceDictionaryInContext((IUriContext) this, this.cachedSource);
        SharedResourceDictionary.sharedDictionaries[this.cachedSource] = new WeakReference<ResourceDictionary>(target);
      }
    }
    this.cachedDictionary = target;
    this.MergedDictionaries.Add(this.cachedDictionary);
  }

  private ResourceDictionary LoadResourceDictionaryInContext(IUriContext baseContext, Uri uri)
  {
    ResourceDictionary resourceDictionary = new ResourceDictionary();
    resourceDictionary.BeginInit();
    ((IUriContext) resourceDictionary).BaseUri = baseContext.BaseUri;
    resourceDictionary.Source = this.cachedSource;
    resourceDictionary.EndInit();
    return resourceDictionary;
  }
}
