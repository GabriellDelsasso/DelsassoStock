# DelsassoStock

## Visão Geral

DelsassoStock é uma API desenvolvida em .NET 6 para gerenciamento de estoque, vendas, produtos e clientes. O sistema segue uma arquitetura em camadas, separando responsabilidades entre Application, Domain e Infraestrutura, e utiliza Entity Framework Core para persistência de dados.

---

## Estrutura do Projeto

- **Domain**: Contém as entidades de negócio, interfaces de repositório e serviços de domínio.
- **Application**: Implementa a lógica de aplicação, ViewModels e serviços de aplicação.
- **Infra.Data**: Responsável pela persistência de dados, contexto do banco e repositórios.
- **Controllers**: Endpoints expostos via API REST.

---

## Principais Funcionalidades

### Vendas

- **Registrar Venda**
  - Permite criar uma nova venda informando o cliente (opcional) e os itens vendidos.
  - Valida o estoque de cada produto, desconta as quantidades vendidas e registra o valor total da venda.

- **Listar Vendas**
  - Retorna todas as vendas realizadas, incluindo dados do cliente, valor total e detalhes dos itens (produto, quantidade, preço unitário).

- **Atualizar Venda**
  - Permite alterar os itens ou o cliente de uma venda existente.
  - O sistema ajusta automaticamente o estoque dos produtos conforme as alterações.

- **Excluir Venda**
  - Remove uma venda do sistema e devolve ao estoque as quantidades dos produtos que faziam parte da venda.

---

### Produtos

- **Cadastrar Produto**
  - Permite adicionar um novo produto ao estoque, informando nome, preço e quantidade inicial.

- **Listar Produtos**
  - Retorna todos os produtos cadastrados, exibindo nome, preço e quantidade disponível em estoque.

- **Atualizar Produto**
  - Permite alterar os dados de um produto existente, como nome, preço e quantidade.

- **Excluir Produto**
  - Remove um produto do sistema, desde que não esteja vinculado a vendas existentes.

---

### Clientes

- **Cadastrar Cliente**
  - Permite adicionar um novo cliente ao sistema, informando nome e CPF.
  - Valida o CPF e garante unicidade.

- **Listar Clientes**
  - Retorna todos os clientes cadastrados, exibindo nome e CPF.

- **Atualizar Cliente**
  - Permite alterar os dados de um cliente existente, como nome e CPF.

- **Excluir Cliente**
  - Remove um cliente do sistema, desde que não esteja vinculado a vendas existentes.

---

## Exemplo de Uso da API

### Vendas

#### Endpoints

- **Criar Venda**
  - `POST /Sale/CreateSale`
  - Cria uma nova venda, descontando o estoque dos produtos.
  - Exemplo de requisição:
    ```json
    {
      "customerId": "GUID_DO_CLIENTE",
      "items": [
        {
          "productItemId": "GUID_DO_PRODUTO",
          "quantity": 2
        }
      ]
    }
    ```
  - Resposta:
    ```json
    {
      "success": true,
      "message": "Sale created successfully!"
    }
    ```
    
- **Listar Vendas**
  - `GET /Sale/GetAllSales`
  - Retorna todas as vendas realizadas, incluindo itens e cliente.
  - Resposta:
    ```json
    {
      "success": true,
      "message": "Sales retrieved successfully.",
      "data": [
        {
          "id": "GUID",
          "customerId": "GUID",
          "customerName": "Nome",
          "totalSale": 100.0,
          "items": [
            {
              "productItemId": "GUID",
              "productName": "Produto",
              "quantity": 2,
              "unitPrice": 50.0
            }
          ]
        }
      ]
    }
    ```

- **Atualizar Venda**
  - `PUT /Sale/UpdateSale?id=GUID_DA_VENDA`
  - Atualiza uma venda existente, ajustando o estoque conforme necessário.
  - Exemplo de requisição:
    ```json
    {
      "customerId": "GUID_DO_CLIENTE",
      "items": [
        {
          "productItemId": "GUID_DO_PRODUTO",
          "quantity": 2
        }
      ]
    }
    ```
  - Resposta:
    ```json
    {
      "success": true,
      "message": "Sale updated successfully!"
    }
    ```
- **Excluir Venda**
  - `DELETE /Sale/DeleteSale?id=GUID_DA_VENDA`
  - Remove uma venda e devolve os itens ao estoque.
  - Resposta:
    ```json
    {
      "success": true,
      "message": "Sale deleted successfully!"
    }
    ```

---

### Produtos

#### Endpoints

- **Criar Produto**
  - `POST /Product/CreateProduct`
  - Cria um novo produto no estoque.
  - Exemplo de requisição:
    ```json
    {
      "name": "Produto X",
      "price": 10.5,
      "quantity": 100
    }
    ```
  - Resposta:
    ```json
    {
      "success": true,
      "message": "Produto criado com sucesso!"
    }
    ```

- **Listar Produtos**
  - `GET /Product/GetAllProducts`
  - Retorna todos os produtos cadastrados.
  - Resposta:
    ```json
    {
      "success": true,
      "data": [
        {
          "id": "GUID",
          "name": "Produto X",
          "price": 10.5,
          "quantity": 100
        }
      ]
    }
    ```

- **Atualizar Produto**
  - `PUT /Product/UpdateProduct?id=GUID_DO_PRODUTO`
  - Atualiza os dados de um produto.
  - Exemplo de requisição:
    ```json
    {
      "name": "Produto X",
      "price": 10.5,
      "quantity": 100
    }
    ```
  - Resposta:
    ```json
    {
      "success": true,
      "message": "Produto atualizado com sucesso!"
    }
    ```

- **Excluir Produto**
  - `DELETE /Product/DeleteProduct?id=GUID_DO_PRODUTO`
  - Remove um produto do estoque.
  - Resposta:
    ```json
    {
      "success": true,
      "message": "Produto removido com sucesso!"
    }
    ```

---

### Clientes

#### Endpoints

- **Criar Cliente**
  - `POST /Customer/CreateCustomer`
  - Cadastra um novo cliente.
  - Exemplo de requisição:
    ```json
    {
      "name": "Cliente Y",
      "cpf": "12345678901"
    }
    ```
  - Resposta:
    ```json
    {
      "success": true,
      "message": "Cliente criado com sucesso!"
    }
    ```

- **Listar Clientes**
  - `GET /Customer/GetAllCustomers`
  - Retorna todos os clientes cadastrados.
  - Resposta:
    ```json
    {
      "success": true,
      "data": [
        {
          "id": "GUID",
          "name": "Cliente Y",
          "cpf": "12345678901"
        }
      ]
    }
    ```

- **Atualizar Cliente**
  - `PUT /Customer/UpdateCustomer?id=GUID_DO_CLIENTE`
  - Atualiza os dados de um cliente.
  - Exemplo de requisição:
    ```json
    {
      "name": "Cliente Y",
      "cpf": "12345678901"
    }
    ```
  - Resposta:
    ```json
    {
      "success": true,
      "message": "Cliente atualizado com sucesso!"
    }
    ```

- **Excluir Cliente**
  - `DELETE /Customer/DeleteCustomer?id=GUID_DO_CLIENTE`
  - Remove um cliente do sistema.
  - Resposta:
    ```json
    {
      "success": true,
      "message": "Cliente removido com sucesso!"
    }
    ```
    
---

## Configuração

- **Banco de Dados**: MySQL (configurado em `appsettings.json`).
- **Swagger**: Disponível em ambiente de desenvolvimento para testes dos endpoints.

---

## Observações

- O controle de estoque é automático ao criar, atualizar ou excluir vendas.
- Todas as operações retornam um objeto `ResultViewModel` indicando sucesso ou falha, além de mensagens detalhadas.

---

## Requisitos

- .NET 6 SDK
- MySQL

---

## Inicialização

1. Configure a string de conexão no arquivo `appsettings.json`.
2. Execute as migrações do banco de dados (se necessário).
3. Inicie a aplicação com `dotnet run` ou via Visual Studio 2022.

---

## Contato

Para dúvidas ou sugestões, entre em contato com o mantenedor do projeto.
