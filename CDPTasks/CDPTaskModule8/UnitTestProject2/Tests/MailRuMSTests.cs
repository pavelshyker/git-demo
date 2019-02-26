namespace TestWebProject
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestWebProject.Forms;
    using TestWebProject.Webdriver;

    [TestClass]
    public class MailRuMSTests : BaseTest
    {
        private const string LogIn = "testuser.19";
        private const string Password = "testCDP123";
        private const string EmailAddress = "litmarsd@mail.ru";
        private const string EmailSubject = "Subject";
        private const string EmailText = "EmailTestText";

        [TestMethod, TestCategory("Login")]
        public void LoginTest()
        {
            var startForm = new StartPage();
            var user = new User(LogIn, Password);
            startForm.Login(user);

            Assert.IsTrue(startForm.LoginSuccessMarker());
        }

        [TestMethod, TestCategory("Login")]
        public void LoginTestUsingTabs()
        {
            var startForm = new StartPage();
            var user = new User(LogIn, Password);
            startForm.LoginUsingTabs(user);

            Assert.IsTrue(startForm.LoginSuccessMarker());
        }

        [TestMethod, TestCategory("EmailCreating")]
        public void CreateDraftEmailTest()
        {
            var startForm = new StartPage();
            var user = new User(LogIn, Password);
            startForm.Login(user);
            var inboxForm = new InboxPage();
            inboxForm.GoToNewEmailPage();
            var emailForm = new EmailPage();
            var email = new Email (EmailAddress, EmailSubject, EmailText);
            emailForm.CreateANewEmail(email);
            emailForm.SaveAsADraft();
            emailForm.GoToDraftPage();
            var draftForm = new DraftPage();

            Assert.AreEqual(EmailAddress, draftForm.GetEmailAddress());
        }

        [TestMethod, TestCategory("VerificationOfTheDraftContent")]
        public void CompareDraftEmailAddressTest()
        {
            var startForm = new StartPage();
            var user = new User(LogIn, Password);
            startForm.Login(user);
            var inboxForm = new InboxPage();
            inboxForm.GoToNewEmailPage();
            var emailForm = new EmailPage();
            var email = new Email (EmailAddress, EmailSubject, EmailText);
            emailForm.CreateANewEmail(email);
            emailForm.SaveAsADraft();
            emailForm.GoToDraftPage();
            var draftForm = new DraftPage();
            draftForm.OpenEmail();

            Assert.AreEqual(EmailAddress, emailForm.GetDraftEmailAddress());
        }
        [TestMethod, TestCategory("VerificationOfTheDraftContent")]
        public void CompareDraftEmailSubjectTest()
        {
            var startForm = new StartPage();
            var user = new User(LogIn, Password);
            startForm.Login(user);
            var inboxForm = new InboxPage();
            inboxForm.GoToNewEmailPage();
            var emailForm = new EmailPage();
            var email = new Email(EmailAddress, EmailSubject, EmailText);
            emailForm.CreateANewEmail(email);
            emailForm.SaveAsADraft();
            emailForm.GoToDraftPage();
            var draftForm = new DraftPage();
            draftForm.OpenEmail();

            Assert.AreEqual(EmailSubject, emailForm.GetDraftEmailSubject());
        }

        [TestMethod, TestCategory("VerificationOfTheDraftContent")]
        public void CompareDraftEmailTextTest()
        {
            var startForm = new StartPage();
            var user = new User(LogIn, Password);
            startForm.Login(user);
            var inboxForm = new InboxPage();
            inboxForm.GoToNewEmailPage();
            var emailForm = new EmailPage();
            var email = new Email(EmailAddress, EmailSubject, EmailText);
            emailForm.CreateANewEmail(email);
            emailForm.SaveAsADraft();
            emailForm.GoToDraftPage();
            var draftForm = new DraftPage();
            draftForm.OpenEmail();

            Assert.IsTrue(emailForm.GetDraftEmailText().Contains(EmailSubject));
        }

        [TestMethod, TestCategory("EmailSending")]
        public void DraftFolderAfterSendingTest()
        {
            var startForm = new StartPage();
            var user = new User(LogIn, Password);
            startForm.Login(user);
            var inboxForm = new InboxPage();
            inboxForm.GoToDraftPage();
            var draftForm = new DraftPage();
            draftForm.DeleteAllDraft();
            draftForm.GoToNewEmailPage();
            var emailForm = new EmailPage();
            var email = new Email(EmailAddress, EmailSubject, EmailText);
            emailForm.CreateANewEmail(email);
            emailForm.SaveAsADraft();
            emailForm.GoToDraftPage();
            draftForm.OpenEmail();
            emailForm.SendEmail();
            emailForm.GoToDraftPage();

            Assert.IsFalse(draftForm.DraftEmailExist());
        }

        [TestMethod, TestCategory("EmailSending")]
        public void SendFolderAfterSendingTest()
        {
            var startForm = new StartPage();
            var user = new User(LogIn, Password);
            startForm.Login(user);
            var inboxForm = new InboxPage();
            inboxForm.GoToSentPage();
            var sentForm = new SentPage();
            sentForm.DeleteAllSent();
            sentForm.GoToNewEmailPage();
            var emailForm = new EmailPage();
            var email = new Email(EmailAddress, EmailSubject, EmailText);
            emailForm.CreateANewEmail(email);
            emailForm.SaveAsADraft();
            emailForm.GoToDraftPage();
            var draftForm = new DraftPage();
            draftForm.OpenEmail();
            emailForm.SendEmail();
            emailForm.GoToSentPage();

            Assert.IsTrue(sentForm.SentEmailExist());
        }

        [TestMethod, TestCategory("Logout")]
        public void LogoutTest()
        {
            var startForm = new StartPage();
            var user = new User(LogIn, Password);
            startForm.Login(user);
            var inboxForm = new InboxPage();
            inboxForm.LogOut();

            Assert.IsTrue(startForm.LogoutSuccessMarker());
        }

        [TestMethod, TestCategory("DeleteEmail")]
        public void DragAndDropEmailTest()
        {
            var startForm = new StartPage();
            var user = new User(LogIn, Password);
            startForm.Login(user);
            var inboxForm = new InboxPage();
            inboxForm.GoToSentPage();
            var sentForm = new SentPage();
            sentForm.DeleteAllSent();
            sentForm.GoToNewEmailPage();
            var emailForm = new EmailPage();
            var email = new Email(EmailAddress, EmailSubject, EmailText);
            emailForm.CreateANewEmail(email);
            emailForm.SendEmail();
            emailForm.GoToSentPage();
            sentForm.DragAndDropFromSentToDelete();

            Assert.IsFalse(sentForm.SentEmailExist());
        }

        [TestMethod, TestCategory("EmailSending")]
        public void SendAnEmailToRandom()
        {
            var RandomEmail = GetRandomEmailAddress.GetRandomEmail();
            var startForm = new StartPage();
            var user = new User(LogIn, Password);
            startForm.Login(user);
            var inboxForm = new InboxPage();
            inboxForm.GoToSentPage();
            var sentForm = new SentPage();
            sentForm.DeleteAllSent();
            sentForm.GoToNewEmailPage();
            var emailForm = new EmailPage();
            var email = new Email(RandomEmail, EmailSubject, EmailText);
            emailForm.CreateANewEmail(email);
            emailForm.SaveAsADraft();
            emailForm.GoToDraftPage();
            var draftForm = new DraftPage();
            draftForm.OpenEmail();
            emailForm.SendEmail();
            emailForm.GoToSentPage();

            Assert.IsTrue(sentForm.SentEmailExist());
        }



    }
}
