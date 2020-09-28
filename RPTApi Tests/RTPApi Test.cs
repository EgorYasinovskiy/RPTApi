using NUnit.Framework;
using RPTApi.Exceptions;
using BatchWSDL;
using NUnit.Framework.Internal;
using System.Threading.Tasks;
using RuPostWSDL;

namespace RPTApi.Tests
{
    public class RTPApiTest
    {
        RuPostApi ruPostApi;
        [SetUp]
        public void Setup()
        {
            ruPostApi = new RuPostApi();
        }

        [Test]
        public void GetOperationsHistoryAsyncTest_TestData_NotAuthException()
        {
            Assert.That(async () =>
            await ruPostApi.GetOperationsHistoryAsync("any barcode"),
            Throws.TypeOf<NotAuthorizedException>());
        }
        [Test]
        public void PostalOrderEventsFromMailAsyncTest_TestData_NotAuthException()
        {
            Assert.That(async () =>
            await ruPostApi.PostalOrderEventsFromMailAsync("any barcode"),
            Throws.TypeOf<NotAuthorizedException>());
        }
        [Test]
        public void GetTicketAsyncTest_TestData_NotAuthException()
        {
            Assert.That(async () =>
            await ruPostApi.GetTicketAsync(new string[] { "any barcode" }),
            Throws.TypeOf<NotAuthorizedException>());
        }
        [Test]
        public void GetResponseByTicketAsyncTest_TestData_NotAuthException()
        {
            Assert.That(async () =>
            await ruPostApi.GetResponseByTicket(new TicketResponse() { value = "any barcode" }),
            Throws.TypeOf<NotAuthorizedException>());
        }
        [Test]
        public void GetOperationsHistoryAsyncTest_TestDataLang_NotAuthException()
        {
            Assert.That(async () =>
            await ruPostApi.GetOperationsHistoryAsync("any barcode",Enums.Language.English),
            Throws.TypeOf<NotAuthorizedException>());
        }
        [Test]
        public void PostalOrderEventsFromMailAsyncTest_TestDataLang_NotAuthException()
        {
            Assert.That(async () =>
            await ruPostApi.PostalOrderEventsFromMailAsync("any barcode",Enums.Language.English),
            Throws.TypeOf<NotAuthorizedException>());
        }
        [Test]
        public void GetTicketAsyncTest_TestDataLang_NotAuthException()
        {
            Assert.That(async () =>
            await ruPostApi.GetTicketAsync(new string[] { "any barcode" },Enums.Language.English),
            Throws.TypeOf<NotAuthorizedException>());
        }
        [Test]
        public void GetOperationsHistoryAsyncTest_TestDataLangMessageType_NotAuthException()
        {
            Assert.That(async () =>
            await ruPostApi.GetOperationsHistoryAsync("any barcode", Enums.Language.English,Enums.MessageType.RegisteredNotification),
            Throws.TypeOf<NotAuthorizedException>());
        }
        [Test]
        public async Task AuthorizeAsyncTest_CorrectLoginCorrectPasswordExampleBarcode_true()
        {
            bool expected = true;
            await ruPostApi.AuthorizeAsync("jQeAXwhSPbgMna", "jnhzUth4Awf2");
            bool actual = ruPostApi.IsAuthorized;
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void AuthorizeAsyncTest_WrongLoginWrongPasswordExampleBarcode_Exception()
        {
            Assert.CatchAsync(async () =>
             await ruPostApi.AuthorizeAsync("test", "test"), 
             "You are not authorized to make this request");
        }
        [Test]
        public async Task GetOperationHistoryAsyncTest_RA644000001RU_History()
        {
            await ruPostApi.AuthorizeAsync("jQeAXwhSPbgMna", "jnhzUth4Awf2");
            var expected = typeof(OperationHistoryResponse);
            var response = await ruPostApi.GetOperationsHistoryAsync("RA644000001RU");
            var actual = response.GetType();
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public async Task GetOperationHistoryAsyncTest_RA644000001RU_Destination()
        {
            await ruPostApi.AuthorizeAsync("jQeAXwhSPbgMna", "jnhzUth4Awf2");
            var expected = "644008";
            var actual = (await ruPostApi.GetOperationsHistoryAsync("RA644000001RU")).OperationHistoryData[0].AddressParameters.DestinationAddress.Index;
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public async Task GetTicketAsyncTest_ExampleBarcode_TicketReturned()
        {
            ruPostApi.AuthorizeAsync("jQeAXwhSPbgMna", "jnhzUth4Awf2");
            var excepted = typeof(TicketResponse);
            var actual = (await ruPostApi.GetTicketAsync(new string[] { "RA644000001RU" })).GetType();
            Assert.AreEqual(excepted, actual);
        }


    }
}
