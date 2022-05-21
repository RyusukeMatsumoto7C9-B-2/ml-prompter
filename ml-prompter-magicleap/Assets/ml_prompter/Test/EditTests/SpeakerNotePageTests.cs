using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

using ml_promter.SpeakerNote;


public class SpeakerNotePageTests
{
    
    [Test]
    public void SpeakerNotePageIsInvalidTest()
    {
        var emptyPage = new SpeakerNotePage("");
        Assert.IsFalse(emptyPage.IsValidPage);
        
        var nullPage = new SpeakerNotePage(null);
        Assert.IsFalse(emptyPage.IsValidPage);
    }


    [Test]
    public void SpeakerNotePageInvalidTest()
    {
        var page = new SpeakerNotePage("hoge");
        Assert.IsTrue(page.IsValidPage);
    }


    /// <summary>
    /// 初期化時に設定した文字列と同じ文字列が格納されるのを確認.
    /// </summary>
    [Test]
    public void SpeakerNotePageTextEqualInitialTexttTest()
    {
        const string INITIAL_TEXT = "Hoge";
        var page = new SpeakerNotePage(INITIAL_TEXT);
        Assert.IsTrue(INITIAL_TEXT.Equals(page.Value));
    }

}
