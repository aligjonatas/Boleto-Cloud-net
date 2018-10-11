# Boleto Cloud net

A implementação esta contida na classe Service que expõe os métodos GeraBoleto() e ConsultaBoleto().
Exemplos de consumo dos métodos no pojeto BoletoCloudApiTest, Form1.cs

Antes de rodar a aplicação, é necessário criar o token da api no sandbox do Boleto Cloud e configurar as chaves abaixo no arquivo .config:

    <add key="BoletoCloud:Token" value="COLOCAR_TOKEN_AQUI" />
    <add key="BoletoCloud:Url" value="https://sandbox.boletocloud.com/api/v1/boletos" />

# Dependências
- Newtonsoft.Json.11.0.2
- RestSharp.106.5.2

# Licença
[MIT](http://en.wikipedia.org/wiki/MIT_License)
