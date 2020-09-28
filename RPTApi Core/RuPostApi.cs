using BatchWSDL;
using RPTApi.Enums;
using RuPostWSDL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;

namespace RPTApi
{
    public class RuPostApi
    {
        private bool _isAuthorized;
        public bool Authorize(string login, string password)
        {
            //TODO: a test request to know if AuthData right.
            throw new NotImplementedException(nameof(Authorize));
        }
        public OperationHistoryResponse GetOperationsHistory(string barcode, Language language = Language.Russian, MessageType messageType = MessageType.Shipment)
        {
            //TODO: Impliment a logic of getting response.
            throw new NotImplementedException(nameof(GetOperationsHistory));
        }
        public PostalOrderEventsForMailResponse PostalOrderEventsFromMail(string barcode,Language language = Language.Russian)
        {
            //TODO: Impliment a logic of getting response.
            throw new NotImplementedException(nameof(PostalOrderEventsFromMail));
        }
        public TicketResponse GetTicket(IEnumerable<string> barcodes,Language language = Language.Russian)
        {
            //TODO: Impliment a logic of getting ticket response.
            throw new NotImplementedException(nameof(GetTicket));
        }
        public ResponseByTicket GetResponseByTicket(TicketResponse ticket)
        {
            //TODO: IMpliment a logic of a getting response.
            throw new NotImplementedException(nameof(GetResponseByTicket));
        }

    }
}
