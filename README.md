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
