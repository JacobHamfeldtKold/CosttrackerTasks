Change branch to master

Test the API with these two Request calls.

curl --request GET
--url https://localhost:7159/currencyConversion
--header 'Content-Type: application/json'
--header 'User-Agent: insomnia/10.0.0'
--data ' { "Amount":5, "SellingCurrency": "EUR", "BuyingCurrency":"USD"}'

curl --request GET
--url https://localhost:7159/currencyConversion/2024-09-20
--header 'Content-Type: application/json'
--header 'User-Agent: insomnia/10.0.0'
--data ' { "Amount":7, "SellingCurrency": "EUR", "BuyingCurrency": "AUD" }'

Post Request used in hangfire job

curl --request POST
--url https://localhost:7159/InsertHistoricRate
--header 'Content-Type: application/json'
--header 'User-Agent: insomnia/10.0.0'
--data '{ "id": 0, "date": "0001-01-01T00:00:00", "baseCurrency": "EUR", "usd": 1.0, "eur": 2.0, "aud": 0.4, "cad": 0.5, "pln": 0.6, "mxn": 0.7 }'




Build a application using Fixer:
1. Basic currency conversion:
o Use the Fixer.io API, which provides currency exchange rates in JSON
format. Sign up at Fixer.io to get access to the WebAPI and review its
documentation.
o Create a WebApi that takes two currency codes and an amount as
input. The amount represents a value in the first currency. The program
will calculate the equivalent amount in the second currency, using the
latest exchange rates from the API.
o Ensure that all calculations are performed within the program itself,
without using any external calculation APIs.
2. Currency conversion with historical rates:
o Extend the functionality to accept an optional date input.
o If a date is provided, use the exchange rates from that specific date to
perform the conversion.
o Refer to the Fixer.io documentation to determine the appropriate API
endpoint for retrieving exchange rates for a given date.
3. Daily exchange rate storage:
o Create a job that runs daily, retrieving the latest exchange rates from
the Fixer.io API and storing them in a RDS database.
4. Web Interface for currency conversion:
o Build a simple web interface in React that interacts with the WebAPI
from task 2 to allow users to perform currency conversions via a web
browser. The React application can either be a part of the same project
(Ruby on Rails supports this) or a separate one.
