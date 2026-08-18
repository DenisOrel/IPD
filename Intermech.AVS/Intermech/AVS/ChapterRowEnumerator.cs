// Decompiled with JetBrains decompiler
// Type: Intermech.AVS.ChapterRowEnumerator
// Assembly: Intermech.AVS, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 2C1E6CF8-5894-477E-BC90-F77341E46DAF
// Assembly location: D:\IPS\Client\Intermech.AVS.dll
// XML documentation location: D:\IPS\Client\Intermech.AVS.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.AVS;

/// <summary>Enumerator для записей спецификации в Chapter</summary>
public class ChapterRowEnumerator : IEnumerator<AVSRow>, IDisposable, IEnumerator
{
  private int currChapterIndex = -1;
  private IEnumerator<AVSRow> currChapterEnumerator;
  private Chapter ownerChapter;

  public ChapterRowEnumerator(Chapter chapter) => this.ownerChapter = chapter;

  public AVSRow Current
  {
    [DebuggerStepThrough] get
    {
      if (this.currChapterEnumerator == null && this.currChapterIndex != -1)
        throw new InvalidOperationException("Call to the Reset and MoveNext method first");
      if (this.currChapterEnumerator == null && this.currChapterIndex == -1)
        throw new InvalidOperationException("Call to the MoveNext method first");
      return this.currChapterEnumerator?.Current;
    }
  }

  public void Dispose() => this.Reset();

  object IEnumerator.Current
  {
    [DebuggerStepThrough] get => (object) this.Current;
  }

  public bool MoveNext()
  {
    if (this.currChapterEnumerator == null)
    {
      this.currChapterIndex = 0;
      if (this.ownerChapter.Chapters.Count <= 0)
        return false;
      this.currChapterEnumerator = this.ownerChapter.Chapters[0].GetEnumerator();
    }
    for (; this.currChapterEnumerator != null && !this.currChapterEnumerator.MoveNext(); this.currChapterEnumerator = this.ownerChapter.Chapters[this.currChapterIndex].GetEnumerator())
    {
      if (this.currChapterIndex < this.ownerChapter.Chapters.Count - 1)
      {
        this.currChapterEnumerator.Dispose();
        ++this.currChapterIndex;
      }
      else
      {
        this.currChapterEnumerator.Dispose();
        this.currChapterEnumerator = (IEnumerator<AVSRow>) null;
        return false;
      }
    }
    return true;
  }

  public void Reset()
  {
    this.currChapterIndex = -1;
    if (this.currChapterEnumerator == null)
      return;
    this.currChapterEnumerator.Dispose();
    this.currChapterEnumerator = (IEnumerator<AVSRow>) null;
  }
}
