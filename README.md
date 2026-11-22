# ProjetoVendas

Aplicação Console em C# (.NET 8) que simula um sistema de vendas totalmente em memória, com navegação entre objetos relacionados. Trabalho do Curso de C# - Parte 9 (Classes, Relacionamentos e Coleções).

## Membros do grupo

Os cinco membros são os clientes da carga de dados virtuais (`Data/DadosVirtuais.cs`):

- Pedro Arthur Santanna Pimentel
- Evellyn da Silva Rodrigues
- Tobias Henrique de Almeida Caetano
- Rhayssa Hyllari Lemos Rezende
- João Marcos de Almeida Albarello

## Como executar

Requisito: .NET SDK 8 ou superior.

```
dotnet run
```

Ou abra `ProjetoVendas.csproj` no Visual Studio e execute com F5.

## Estrutura

```
ProjetoVendas/
|-- Program.cs                  Menu e interação com o usuário
|-- Models/                     Categoria, Produto, Cliente, Endereco, Pedido, ItemPedido
|-- Data/                       BancoEmMemoria e DadosVirtuais (carga inicial)
|-- Services/                   PedidoService e ConsultaService (consultas LINQ e rankings)
|-- Exercicios/                 Exercícios propostos (Módulo 11) e demonstração dos Módulos 7 e 8
`-- Docs/QuestoesConceituais.md Respostas das 10 questões conceituais
```

## Relação das cardinalidades

```
Categoria 1 ---- N Produto
Cliente   1 ---- 1 Endereco (principal)
Cliente   1 ---- N Pedido
Pedido    1 ---- N ItemPedido
Produto   1 ---- N ItemPedido
Pedido    N ---- N Produto   (materializada por ItemPedido)
```

## Atendimento aos requisitos do laboratório (Módulo 10)

| Requisito | Onde |
|---|---|
| Classes Categoria, Produto, Cliente, Endereco, Pedido, ItemPedido | `Models/` |
| Navegação bidirecional Categoria <-> Produto e Cliente <-> Pedido | `Categoria.Produtos`, `Produto.Categoria`, `Cliente.Pedidos`, `Pedido.Cliente` |
| Pedido <-> Produto por ItemPedido | `Pedido.Itens`, `Produto.ItensPedido` |
| BancoEmMemoria e DadosVirtuais | `Data/` |
| 3 categorias, 8 produtos, 5 clientes, 6 pedidos (cada um com 1 ou mais itens) | `Data/DadosVirtuais.cs` |
| Produtos de uma categoria informada | Menu, opção 3 |
| Pedidos de um cliente com itens e total | Menu, opção 5 |
| Ranking de produtos por quantidade vendida | Menu, opção 7 |
| Ranking de clientes por valor comprado | Menu, opção 8 |
| Criar novo pedido em memória e adicionar itens | Menu, opção 9 |
| Exercícios propostos (inicial, básico, intermediário, avançado) | Menu, opção 10 |
