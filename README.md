# api Simular Custo Ligacao

Para rodar aplicação

dotnet run --project .\src\tel-api\tel-api.csproj

Navegador: http://localhost:5000

Rotas:
```
/api/Plano
/api/Localidade
/api/SimuladorCustoLigacao
```

Para rodar os testes:

dotnet test .\src\test\test.csproj
```
Test Run Successful.
Total tests: 22
     Passed: 22
 Total time: 1.6515 Seconds
 
 ```
 
 Swagger UI
 
 ![image](https://user-images.githubusercontent.com/8823480/112387748-e64fae80-8cd0-11eb-87ac-40979b821e8f.png)
 
 ![image](https://user-images.githubusercontent.com/8823480/112388675-44c95c80-8cd2-11eb-909d-b2aa08147230.png)


```
Localidade funcionando na simulação
Origem - Destino
011    - 016
011    - 017
018    - 011

Request
http://localhost:5000/api/SimuladorCustoLigacao?plano=FALE_MAIS_60&dddOrigem=011&dddDestino=017&tempo=80

Response
{
  "dddOrigem": "011",
  "dddDestino": "017",
  "tempo": 80,
  "planoFaleMais": "FALE_MAIS_60",
  "valorComPlanoFaleMais": 37.4,
  "valorSemPlanoFaleMais": 136
}


Planos
[
  "TARIFA_FIXA",
  "FALE_MAIS_30",
  "FALE_MAIS_60",
  "FALE_MAIS_120"
]

Localidades (ddd)
[
  "011",
  "012",
  "013",
  "014",
  "015",
  "016",
  "017",
  "018",
  "019",
  "021",
  "022",
  "024",
  "027",
  "028",
  "031",
  "032",
  "033",
  "034",
  "035",
  "037",
  "038",
  "041",
  "042",
  "043",
  "044",
  "045",
  "046",
  "047",
  "048",
  "049",
  "051",
  "053",
  "054",
  "055",
  "061",
  "062",
  "063",
  "064",
  "065",
  "066",
  "067",
  "068",
  "069",
  "071",
  "073",
  "074",
  "075",
  "077",
  "079",
  "081",
  "082",
  "083",
  "084",
  "085",
  "086",
  "087",
  "088",
  "089",
  "091",
  "092",
  "093",
  "094",
  "095",
  "096",
  "097",
  "098",
  "099"
]
```

