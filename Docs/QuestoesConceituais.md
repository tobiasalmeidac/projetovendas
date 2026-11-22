# Questões conceituais (item 63 do documento)

**1. Qual é a diferença entre uma classe e um objeto?**
A classe é a definição do tipo: descreve quais dados e quais comportamentos existem. O objeto é uma instância concreta dessa definição, criada com `new` e que existe em memória durante a execução. Em `Produto produto = new Produto(...)`, `Produto` é a classe e `produto` aponta para o objeto.

**2. Por que uma propriedade de navegação é diferente de um simples Id?**
O Id é apenas um valor que identifica um registro. A propriedade de navegação guarda a referência para o próprio objeto relacionado, então é possível acessar seu estado e comportamento direto, como `produto.Categoria.Nome`, sem procurar em uma lista pelo Id.

**3. O que significa navegabilidade bidirecional?**
Significa que os dois objetos conseguem alcançar um ao outro por propriedades. Exemplo: `produto.Categoria` e `categoria.Produtos`.

**4. Quando uma coleção representa cardinalidade 1:N?**
Quando o objeto do lado "1" guarda uma coleção de objetos do lado "N", e cada elemento da coleção pertence àquele objeto. Exemplo: `Cliente.Pedidos`, em que um cliente possui vários pedidos e cada pedido pertence a um único cliente.

**5. Por que ItemPedido resolve uma relação N:N?**
Um pedido tem vários produtos e um produto aparece em vários pedidos. O ItemPedido quebra essa relação em duas relações 1:N (Pedido 1:N ItemPedido e Produto 1:N ItemPedido) e ainda guarda os dados do vínculo, como Quantidade e PrecoUnitario.

**6. Qual problema pode ocorrer se um lado da navegação for atualizado e o outro não?**
O grafo de objetos fica incoerente. Por exemplo, se `produto.Categoria` aponta para uma categoria, mas o produto não está em `categoria.Produtos`, uma consulta a partir da categoria não encontra o produto. Para evitar isso usamos métodos de fábrica e métodos como `AdicionarFuncionario`, que atualizam as duas pontas.

**7. Qual é a diferença prática entre List<T> e IEnumerable<T>?**
`List<T>` é uma implementação concreta e mutável: permite `Add`, `Remove` e acesso por índice. `IEnumerable<T>` apenas expõe a capacidade de percorrer a sequência, sem permitir alterá-la, o que é ideal para leitura e para retorno de consultas.

**8. Por que dados virtuais são úteis antes de introduzir banco de dados?**
Eles permitem exercitar objetos, relacionamentos e consultas sem depender de SQL Server, Entity Framework ou outra infraestrutura. Assim o foco fica na modelagem e na navegabilidade.

**9. Em que situação uma classe associativa deve possuir atributos próprios?**
Quando o vínculo entre os dois lados tem informações que não pertencem a nenhum deles isoladamente, como quantidade e preço (ItemPedido), data e situação (Matricula), status ou papel.

**10. Quando é melhor evitar uma navegação bidirecional?**
Quando o caminho inverso não é necessário para os casos de uso, ou quando a sincronização das duas pontas aumenta a complexidade sem trazer benefício. Exemplo do curso: `Funcionario` conhece `Departamento`, mas `Departamento` não precisa ter a lista de funcionários.
