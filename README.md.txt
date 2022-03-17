# API Rest para converter valores em diferentes moedas
Por exemplo, dado um valor de origem em reais converter o valor em dolares.
São suportadas quatro tipos de moedas BRL(Real Brasileiro) , USD (Dólar Americano), EUR (Euro) e JPY (Iene Japonês).

# Como a API obtem as taxas de conversão?
A API através de outra API(rs) "Exchangeratesapi" obtem as taxas de conversão e tem como base o EUR (Euro) no plano de uso gratuito, não é possivel até o momento com a conta gratuita pegar as taxas tendo o BRL (Real Brasileiro) como base, por exemplo. Então para fazer a conversão de BRL (Real Brasileiro) para USD (Dólar Americano) primeiro faz a conversão do valor em BRL (Real Brasileiro) para EUR (EURO) e depois de EUR (EURO) para USD (Dólar Americano).

# Tecnologias envolvidas
 - .NET 5, ASP.NET Core API
 - MongoDB
 - Swagger
 - Docker
 - JWT

# Autenticação
API também tem um mecanismo de autenticação de usuário. 
Usuário se autentica na API e recebe um token, passando esse token para os serviços da API ele poderá realizar as conversões de moeda. 

# Rodar o projeto
Ter o docker instalado na maquina e no terminal dar o comando docker-compose up 
Acessar url: http://localhost:8080/swagger/index.html