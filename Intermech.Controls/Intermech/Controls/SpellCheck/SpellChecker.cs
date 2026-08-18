
// Type: Intermech.Controls.SpellCheck.SpellChecker
// Assembly: Intermech.Controls, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B2BFE6FF-0AA3-422C-A374-1A460CB041DD
:\IPS\Client\Intermech.Controls.dll
// XML documentation location: D:\IPS\Client\Intermech.Controls.xml

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;


namespace Intermech.Controls.SpellCheck;

public class SpellChecker
{
  private static SpellChecker instance;
  private WordDict dict;
  public bool WorkInThread;
  private List<Thread> workingThreads = new List<Thread>();

  /// <summary>Оснровной экземпляр класса</summary>
  public static SpellChecker Instance
  {
    get
    {
      if (SpellChecker.instance == null)
        SpellChecker.instance = new SpellChecker();
      return SpellChecker.instance;
    }
  }

  public WordDict Dict
  {
    get
    {
      if (this.dict == null)
        this.dict = new WordDict("");
      return this.dict;
    }
    set => this.dict = value;
  }

  /// <summary>Check word in dictionaries</summary>
  /// <param name="InputWord">(input) The input word to spell check. The input word may have any combination of upper/lower case letters 'a' through 'z' and an apostrophe character</param>
  /// <returns>This function returns a true value if the word is found in the dictionary. The function also returns a true value if the currently incorrect word was previously ignored by the user</returns>
  public bool SpellWord(string InputWord)
  {
    if (this.dict == null)
      this.dict = new WordDict("");
    return this.dict.Contains(InputWord.Trim().ToLower()) != Struct.TestResult.UnknownWord;
  }

  /// <summary>
  /// Use this routine to parse a buffer containing words to be spell checked. Each call returns a word
  /// </summary>
  /// <param name="CurLine">(input) Pointer to the string containing the words to extract.</param>
  /// <param name="OutCurWord">(output) Pointer to the string where the extracted word is to be copied</param>
  /// <param name="WordIndex">(output) Starting position of the extracted word with respect to the beginning of the buffer</param>
  /// <param name="CurIndex">(input/output) The function begins examining the buffer location as given by this argument. When a word is extracted, this location is updated to contain the pointer after the end of the word. Therefore, the next call to the StParseLine routine will automatically begin the search where the previous call ended.</param>
  /// <param name="LineLen">(input) The length of the buffer to examine. The length is counted from the beginning of the buffer. If the calling routine inserts or deletes a word in the buffer, it should update this variable appropriately to reflect the updated length of the buffer</param>
  /// <returns>The function returns the length of the extracted word. A zero length indicates the end of the buffer</returns>
  public int StParseLine(
    string CurLine,
    ref string OutCurWord,
    ref int WordIndex,
    ref int CurIndex,
    int LineLen)
  {
    try
    {
      int length1 = CurLine.Length - CurIndex;
      string input = CurLine.Substring(CurIndex, length1);
      Match match = Struct._wordEx.Match(input);
      if (match != null)
      {
        if (match.Value != "")
        {
          int num1 = CurLine.IndexOf(match.Value, CurIndex);
          int length2 = match.Length;
          int num2 = num1 + length2;
          OutCurWord = match.Value;
          WordIndex = num1;
          CurIndex = num2;
          return length2;
        }
      }
    }
    catch
    {
      return 0;
    }
    return 0;
  }

  private void GerErrorsInThread(object args)
  {
    string str1 = (string) ((Array) args).GetValue(0) ?? "";
    string str2 = (string) ((Array) args).GetValue(1);
    int num1 = (int) ((Array) args).GetValue(2);
    int num2 = (int) ((Array) args).GetValue(3);
    SpellChecker.SetErrorsDelegate setErrorsDelegate = (SpellChecker.SetErrorsDelegate) ((Array) args).GetValue(4);
    List<ErrorStruct> errors = new List<ErrorStruct>();
    lock (this)
    {
      try
      {
        int startIndex1 = 0;
        int num3 = str1.Length - 1;
        if (num1 != -1)
        {
          if (num1 > num3)
            num1 = num3;
          if (num2 > num1)
            num2 = num1;
          for (int index = num2 - 10; index >= 0; --index)
          {
            char c = str1[index];
            if (char.IsSeparator(c) || char.IsPunctuation(c))
            {
              startIndex1 = index;
              break;
            }
          }
          for (int index = num1 + 10; index < str1.Length; ++index)
          {
            char c = str1[index];
            if (char.IsSeparator(c) || char.IsPunctuation(c))
            {
              num3 = index;
              break;
            }
          }
        }
        int length1 = num3 - startIndex1 + 1;
        if (this.dict == null)
          this.dict = new WordDict("");
        string input = str1.Substring(startIndex1, length1);
        MatchCollection matchCollection = Struct._wordEx.Matches(input);
        int startIndex2 = 0;
        for (int i = 0; i < matchCollection.Count; ++i)
        {
          int num4 = input.IndexOf(matchCollection[i].Value, startIndex2);
          int length2 = matchCollection[i].Length;
          startIndex2 = num4 + length2;
          if (this.dict.Contains(matchCollection[i].Value.Trim().ToLower()) == Struct.TestResult.UnknownWord)
            errors.Add(new ErrorStruct()
            {
              Start = startIndex1 + num4,
              End = startIndex1 + num4 + length2 - 1
            });
        }
        setErrorsDelegate(errors, startIndex1, length1);
      }
      catch (Exception ex)
      {
      }
    }
    if (!this.workingThreads.Contains(Thread.CurrentThread))
      return;
    this.workingThreads.Remove(Thread.CurrentThread);
  }

  /// <summary>Получение ошибок из строки</summary>
  /// <param name="text">Строка</param>
  /// <param name="oldText">Старый текст, который был проверен</param>
  /// <param name="cursorPos">Текущая позиция курсора</param>
  /// <param name="oldCursorPos">Предыдущая позиция курсора</param>
  /// <param name="setErrors">Иетод в котором будет производится обработка</param>
  public void GerErrors(
    string text,
    string oldText,
    int cursorPos,
    int oldCursorPos,
    SpellChecker.SetErrorsDelegate setErrors)
  {
    if (this.WorkInThread)
    {
      if (this.workingThreads.Count != 0)
        return;
      Thread thread = new Thread(new ParameterizedThreadStart(this.GerErrorsInThread));
      this.workingThreads.Add(thread);
      thread.Start((object) new object[5]
      {
        (object) text,
        (object) oldText,
        (object) cursorPos,
        (object) oldCursorPos,
        (object) setErrors
      });
    }
    else
      this.GerErrorsInThread((object) new object[5]
      {
        (object) text,
        (object) oldText,
        (object) cursorPos,
        (object) oldCursorPos,
        (object) setErrors
      });
  }

  private object InvokeMethod(string name, object obj, object[] arg)
  {
    return obj.GetType().InvokeMember(name, BindingFlags.Public | BindingFlags.InvokeMethod, (Binder) null, obj, arg);
  }

  private object GetProperty(string name, object obj)
  {
    return obj.GetType().InvokeMember(name, BindingFlags.Public | BindingFlags.GetProperty, (Binder) null, obj, (object[]) null);
  }

  private object SetProperty(string name, object obj, object value)
  {
    return obj.GetType().InvokeMember(name, BindingFlags.Public | BindingFlags.SetProperty, (Binder) null, obj, new object[1]
    {
      value
    });
  }

  /// <summary>Делегат обработки ошибок</summary>
  /// <param name="errors">список ошибок начиная от начала текста</param>
  /// <param name="errors">стартовый индекс с которого шла проверка</param>
  /// <param name="errors">длина строки в которой шла проверка</param>
  public delegate void SetErrorsDelegate(List<ErrorStruct> errors, int startIndex, int length);
}
