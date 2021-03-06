Steps to run the project:

1. In docker command line, enter:

	docker-compose up
	
Make sure that you are in the folder that contains the docker-compose.yml file.
Wait until the mongo database and web server containers boot up.

2. That's it, you can send http requests to the server. The address should be 192.168.99.100:5000.
The ip address is the address of the docker machine. The port is 5000.


Request examples:

- POST to 192.168.99.100:5000/insertdata (with or without json, it doesn't matter)

The above request will insert some data into the database (data created by me). Do not send it multiple times (to avoid creating duplicate data).


- POST 192.168.99.100:5000/transactions with json:  (to insert a transaction)

{
   "sender":1,
   "receiver":2,
   "timestamp":1,
   "sum":3200
}

- GET 192.168.99.100:5000/transactions		(to see all transactions)
- GET 192.168.99.100:5000/transactions/?user=1&day=1&threshold=1000		(to filter transactions by userId, day, and threshold)
- GET 192.168.99.100:5000/balance/?user=1&since=1&until=10		(to see the balance of user over a period of time)
