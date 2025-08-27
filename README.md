# CapiWear API

Bem-vindo à CapiWear API, o backend para a loja de vestuário e acessórios temáticos de capivaras. Esta API RESTful foi construída com .NET 9 e segue os princípios de arquitetura em camadas para garantir um código limpo, organizado e escalável.

## 🚀 Status do Projeto

Atualmente, o projeto conta com a estrutura completa para a entidade **Product**, incluindo:
* CRUD completo (Create, Read, Update, Delete) para produtos.
* Conexão com banco de dados PostgreSQL.
* Documentação interativa da API com Swagger.

As demais entidades (`Category`, `Order`, `User`, etc.) possuem seus modelos criados, mas a lógica de negócio e os endpoints ainda não foram implementados.

## 🛠️ Tecnologias Utilizadas

* **.NET 9**: Framework principal para a construção da API.
* **ASP.NET Core**: Para criação de aplicações web e APIs.
* **Entity Framework Core**: ORM para interação com o banco de dados.
* **PostgreSQL**: Sistema de gerenciamento de banco de dados relacional.
* **Swagger (Swashbuckle)**: Para documentação e teste de endpoints da API.

## 📂 Estrutura do Projeto

O projeto é organizado em uma arquitetura de camadas para separar as responsabilidades, facilitando a manutenção e o desenvolvimento de novas funcionalidades.

```
/CapiWear_API
|-- Controllers/    # Camada de Apresentação
|-- Data/
|   |-- Repositories/ # Camada de Acesso a Dados
|-- DTOs/           # Objetos de Transferência de Dados
|-- Models/         # Entidades do Domínio
|-- Services/       # Camada de Lógica de Negócio
|-- appsettings.json  # Configurações da Aplicação
|-- Program.cs        # Ponto de Entrada e Configuração
```

#### `Models`
Representam as entidades do nosso domínio e o esquema do banco de dados. São classes "puras" que contêm apenas propriedades.
* **Exemplo:** `Product.cs`, `Category.cs`, `User.cs`.

#### `DTOs` (Data Transfer Objects)
São objetos que moldam os dados que serão enviados ou recebidos pela API. Eles nos permitem controlar quais informações são expostas, evitando expor nossos modelos de domínio diretamente e ajudando a previnir ataques de *over-posting*.
* **Exemplo:** `ProductDTO.cs` pode conter apenas as propriedades `Id`, `Name` e `Price`, omitindo `CreatedAt`.

#### `Data/Repositories` (Repositórios)
Esta camada é a única que se comunica diretamente com o banco de dados. Sua responsabilidade é abstrair a lógica de acesso aos dados (consultas, inserções, atualizações).
* **Exemplo:** `ProductRepository.cs` implementa métodos como `GetByIdAsync`, `GetAllAsync`, etc., usando o `ApiDbContext`.

#### `Services` (Serviços)
Contém a lógica de negócio da aplicação. Ela atua como uma ponte entre os *Controllers* e os *Repositories*. Qualquer regra, validação ou orquestração de operações deve ser feita aqui.
* **Exemplo:** `ProductService.cs` orquestra a criação de um produto, chamando o repositório para salvar os dados.

#### `Controllers`
A camada de apresentação da API. É responsável por receber as requisições HTTP, chamar os serviços apropriados e retornar uma resposta HTTP (como JSON + Status Code). Eles não contêm lógica de negócio.
* **Exemplo:** `ProductController.cs` define os endpoints como `GET /api/product`, `POST /api/product`, etc.

## ⚙️ Como Rodar Localmente

Para executar este projeto em sua máquina, siga os passos abaixo.

### Pré-requisitos
* [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
* Um servidor de banco de dados PostgreSQL em execução.

### 1. Clone o Repositório
```bash
git clone https://github.com/lucasBorille/CapiWear-Backend.git
cd capiwear-backend
```

### 2. Configure a Connection String
A configuração de conexão com o banco de dados é o passo mais importante.

1.  Abra o arquivo `appsettings.json`.
2.  Localize a seção `ConnectionStrings`.
3.  **Altere os valores** para corresponder às credenciais do seu banco de dados PostgreSQL local ou em nuvem.

    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=<SEU_SERVIDOR>;Port=<SUA_PORTA>;Database=<NOME_DO_BANCO>;User Id=<SEU_USUARIO>;Password=<SUA_SENHA>;"
    }
    ```

### 3. Instale as Dependências e Rode a Aplicação
O .NET restaurará as dependências automaticamente ao executar o projeto.

```bash
dotnet run
```

A API estará em execução e ouvindo nas URLs exibidas no terminal (geralmente `http://localhost:5036`).

## 🧪 Testando com o Swagger

Uma vez que a aplicação esteja rodando, a maneira mais fácil de testar os endpoints é através da interface do Swagger.

1.  Abra seu navegador.
2.  Navegue até a URL `https` da sua aplicação e adicione `/swagger` ao final.
    * **Exemplo:** `http://localhost:5036/swagger`

Você verá uma página interativa com todos os endpoints disponíveis, onde poderá testar cada uma das operações CRUD para os produtos.
