# RPTApi Core
RTPApi Core - (Russian Post Tracking Api) is a .NET Core library that implements the API methods of the Russian post in a convenient, object-oriented format.

___
## Example of use

### Public methods
___
[Authorize](# authorization)
[GetOperationsHistory](# getoperationshistory)
[PostalOrderEventsForMail](# postalordereventsformail)
[ GetTicket](#  getticket)
[GetResponseByTicket](# getresponsebyticket)
___
####  Authorization
___
	RuPostApi api = new RuPostApi("your login","your password");
	
	/*
	*	You can also go this way.
	*/
	
	RuPostApi api = new RuPostApi();
	api.Authorize("your login", "your password")
___
You can get a login and password to access the API [here](https://tracking.pochta.ru/main).
___
#### GetOperationsHistory
___
Method GetOperationHistory is used to receive information on specific shipment. 
Method returns detailed information about all the operations during the delivery of the shipment.
___
	var barcode = "RA644000001RU" 	// Track number.
	var barcode1 = "14102192069353" // Also track number.
	// Getting the history of operations.
	var response = api.GetOperationHistory(barcode);
	
	/*
	*	You can add the message type(shipment or registered notification) 
	*	and preferred language(Russian or English).
	*	
	*	Default values is MessageType.Shipment, Language.Russian
	*/
	
	response = api.GetOperationsHistory(barcode, MessageType.Shipment, Language.Russian)
___
#### PostalOrderEventsForMail\
___
Method PostalOrderEventsForMail  is used to receive information of operations on COD payments.
Returns the history of operations on COD payments.
___
	var barcode = "141021920693530";
	var response = api.PostalOrderEventsForMail(barcode);
	/*
	* You can add preffered language to request(Russian or English).
	*  
	* Default language is Russian.
	*/
	response = api.PostalOrderEventsForMail(barcode,Language.English);
___
#### GetTicket
___
Method GetTicket is used to receive the ticket to the information according to the list of tracking numbers. 
The request contains the list of tracking numbers. Upon successful completion, the method returns ticket identifier.
___

	var codes = new List<string> codes();
	codes = FillCodesFunc(); // Fill our codes to track (max count is 3000).
	var ticket = api.GetTicket(codes);

___
#### GetResponseByTicket
___
This method is used to get information about the shipments according to the ticket, which was issued earlier.
___

	// Using ticket from the previous example.
	var response = api.GetResponseByTicket(ticket)
	/*
	*	However you can add preffered language to this request.
	*	 
	*	Default language is Russian.
	*/
	response = api.GetResponseByTicket(ticket,Language.English);
___
#### Full info about api and methods
You can find more info on the official site of [Russian post](https://tracking.pochta.ru/specification).

___
### About
___
This is a course work on the subject "Basics of programming". 
Performed by Yegor Yasinovsky, a student of group 4917 (SUAI).

___

##### TG [![Telegram](https://a.deviantart.net/avatars/t/o/tomazzo.png?6)](https://t.me/nide_1241) 
##### VK [![VK](https://sun9-29.userapi.com/c830108/v830108098/89ab/VzZ4pTxSNig.jpg?ava=1)](https://vk.com/yasinovskiy_egor)
	