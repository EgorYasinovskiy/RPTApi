using BatchWSDL;
using RPTApi.Enums;
using RPTApi.Exceptions;
using RPTApi.Extensions;
using RuPostWSDL;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPTApi
{
    public class RuPostApi
    {
        public bool IsAuthorized 
        { 
            get=>_isAuthorized;
            private set=>_isAuthorized=value; 
        }
        private bool _isAuthorized = false;
        private AuthorizationHeader authorizationHeader;
        /// <summary>
        /// Auth method. Makes the test request to check if login and password is valid.
        /// </summary>
        /// <param name="login"> Login. </param>
        /// <param name="password"> Password. </param>
        /// <returns></returns>
        public async Task AuthorizeAsync(string login, string password)
        {
            AuthorizationHeader header = new AuthorizationHeader() { login = login, password = password };
            OperationHistoryRequest request = new OperationHistoryRequest() { Barcode = "RA644000001RU", Language = "RUS", MessageType = 0 };
            OperationHistoryClient client = new OperationHistoryClient();
            try
            {
                await client.OpenAsync();
                var resp = await client.GetOperationHistoryAsync(request, header);
                _isAuthorized =  true;
                authorizationHeader = new AuthorizationHeader() { login = login, password = password };
                await client.CloseAsync();
            }
            catch
            {
                _isAuthorized = false;
                throw;
            }

        }

        /// <summary>
        /// This method is used to receive information on specific shipment.
        /// </summary>
        /// <param name="barcode"> Barcode </param>
        /// <param name="language"> Prefferd language </param>
        /// <param name="messageType">Message Type(Shipment or )</param>
        /// <returns> Detailed information about all the operations during the delivery of the shipment. </returns>
        public async Task<OperationHistoryResponse> GetOperationsHistoryAsync(string barcode, Language language = Language.Russian, MessageType messageType = MessageType.Shipment)
        {
            if (_isAuthorized)
            {
                OperationHistoryRequest request = new OperationHistoryRequest() { Barcode = barcode, Language = language.ToParameter(), MessageType = (int)messageType };
                OperationHistoryClient client = new OperationHistoryClient();
                await client.OpenAsync();
                var response = await client.GetOperationHistoryAsync(request, authorizationHeader);
                await client.CloseAsync();
                return response;
            }
            else
            {
                throw new NotAuthorizedException();
            }
        }

        /// <summary>
        /// This method is used to receive information of operations on COD payments.
        /// </summary>
        /// <param name="barcode"> Barcode. </param>
        /// <param name="language"> Preffered language. </param>
        /// <returns> History of operations on COD payments. </returns>
        public async Task<PostalOrderEventsForMailResponse> PostalOrderEventsFromMailAsync(string barcode,Language language = Language.Russian)
        {
            if (_isAuthorized)
            {
                OperationHistoryClient client = new OperationHistoryClient();
                PostalOrderEventsForMailInput mailInput = new PostalOrderEventsForMailInput() { Barcode = barcode, Language = language.ToParameter() };
                await client.OpenAsync();
                var response = await client.PostalOrderEventsForMailAsync(authorizationHeader, mailInput);
                await client.CloseAsync();
                return response;
            }
            else
            {
                throw new NotAuthorizedException();
            }
        }

        /// <summary>
        /// GetTicket is used to receive the ticket to the information according to the list of tracking numbers. The request contains the list of tracking numbers.
        /// </summary>
        /// <param name="barcodes"> Collection of barcodes. </param>
        /// <param name="language"> Prefferd language in response. </param>
        /// <returns> Upon successful completion, the method returns ticket identifier. </returns>
        public async Task<TicketResponse> GetTicketAsync(IEnumerable<string> barcodes, Language language = Language.Russian)
        {
            if (_isAuthorized)
            {
                FederalClient client = new FederalClient();
                var Items = barcodes.Select(bc => new Item() { Barcode = bc }).ToArray();
                TicketRequest request = new TicketRequest()
                {
                    language = language.ToParameter(),
                    login = authorizationHeader.login,
                    password = authorizationHeader.password,
                    request = new File() { Item = Items }
                };
                await client.OpenAsync();
                var response = await client.GetTicketAsync(request);
                await client.CloseAsync();
                return response;
            }
            else
            {
                throw new NotAuthorizedException();
            }
        }

        /// <summary>
        /// This method is used to get information about the shipments according to the ticket, which was issued earlier.
        /// </summary>
        /// <param name="ticket"> Recieved ticket </param>
        /// <returns>Response by Ticket, info about array of operations. </returns>
        public async Task<ResponseByTicket> GetResponseByTicket(TicketResponse ticket)
        {
            if (_isAuthorized)
            {
                FederalClient client = new FederalClient();
                await client.OpenAsync();
                var response = await client.GetResponseByTicketAsync(new ResponseByTicketRequest()
                {
                    login = authorizationHeader.login,
                    password = authorizationHeader.password,
                    ticket = ticket.value
                });
                await client.CloseAsync();
                return response;
            }
            else
            {
                throw new NotAuthorizedException();
            }
        }
        public RuPostApi()
        {

        }
        public RuPostApi(string login,string pasword)
        {
            AuthorizeAsync(login,pasword);
        }
    }
}
