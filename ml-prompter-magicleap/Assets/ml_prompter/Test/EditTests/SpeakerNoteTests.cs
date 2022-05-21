using ml_promter.SpeakerNote;
using NUnit.Framework;

public class SpeakerNoteTests
{
    
    
    [Test]
    public void AddPageTest()
    {
        var speakerNote = new SpeakerNote();

        // まずは0.
        Assert.Zero(speakerNote.PageCount);

        // 1ページ追加.
        speakerNote.AddPage(new SpeakerNotePage("Hoge"));
        Assert.NotZero(speakerNote.PageCount);
    }


    [Test]
    public void CanNotAddInvalidPageTest()
    {
        var speakerNote = new SpeakerNote();

        // 1ページ空文字ページで追加,ページは追加されない.
        speakerNote.AddPage(new SpeakerNotePage(""));
        Assert.Zero(speakerNote.PageCount);

        // 1ページnullでページで追加,ページは追加されない.
        speakerNote.AddPage(new SpeakerNotePage(null));
        Assert.Zero(speakerNote.PageCount);

        // AddPageでnullを指定してもページは追加されない.
        speakerNote.AddPage(null);
        Assert.Zero(speakerNote.PageCount);
    }


    [Test]
    public void PagingTest()
    {
        const string PAGE_1 = "Hoge";
        const string PAGE_2 = "Fuga";
        
        var speakerNote = new SpeakerNote();
        speakerNote.AddPage(new SpeakerNotePage(PAGE_1));
        speakerNote.AddPage(new SpeakerNotePage(PAGE_2));

        // まずは1ページ進む.
        speakerNote.Next();
        Assert.IsTrue(speakerNote.Index == 1);
        Assert.IsTrue(speakerNote.CurrentPage().Value == PAGE_2);
        
        // 1ページ戻る.
        speakerNote.Previous();
        Assert.IsTrue(speakerNote.Index == 0);
        Assert.IsTrue(speakerNote.CurrentPage().Value == PAGE_1);
    }


    [Test]
    public void PagingSafetyTest()
    {
        const string PAGE_1 = "Hoge";
        
        var speakerNote = new SpeakerNote();
        speakerNote.AddPage(new SpeakerNotePage(PAGE_1));

        // 1ページのみのノートでひたすらNextを行ってもインデックスは範囲内に収まる.
        speakerNote.Next();
        speakerNote.Next();
        speakerNote.Next();
        speakerNote.Next();
        Assert.IsTrue(speakerNote.Index == 1);
        
        // 1ページのみのノートでひたすらPreviousを行ってもインデックスは範囲内に収まる.
        speakerNote.Previous();
        speakerNote.Previous();
        speakerNote.Previous();
        speakerNote.Previous();
        speakerNote.Previous();
        Assert.IsTrue(speakerNote.Index == 0);
    }


    [Test]
    public void PageAllRemoveTest()
    {
        const string PAGE_1 = "Hoge";
        const string PAGE_2 = "Fuga";
        const string PAGE_3 = "Jeje";

        var speakerNote = new SpeakerNote();
        speakerNote.AddPage(new SpeakerNotePage(PAGE_1));
        speakerNote.AddPage(new SpeakerNotePage(PAGE_2));
        speakerNote.AddPage(new SpeakerNotePage(PAGE_3));
        Assert.IsTrue(speakerNote.PageCount == 3);
        
        speakerNote.RemoveAll();
        Assert.Zero(speakerNote.PageCount);
    }


    [Test]
    public void NonRegisteredPageReturnToInvalidPage()
    {
        var speakerNote = new SpeakerNote();
        Assert.IsNotNull(speakerNote.CurrentPage());
        Assert.IsFalse(speakerNote.CurrentPage().IsValidPage);
    }

}
