# DelsassoStock

Gostaria de apresentar o projeto que desenvolvi como parte do desafio técnico para a vaga de desenvolvedor C#. Meu objetivo foi construir uma solução robusta, organizada e de fácil manutenção, utilizando as melhores práticas do mercado.

## Estrutura e Tecnologias Principais
O projeto foi construído com uma arquitetura em camadas bem definida, separando claramente as responsabilidades:

- **Domain**: Contém as regras de negócio puras e as entidades principais (como Sale, Client, ProductItem), isoladas de detalhes de infraestrutura.
- **Application**: Orquestra a lógica da aplicação, conectando as regras de negócio com o acesso aos dados.
- **Infra.Data**: Responsável pela persistência dos dados, utilizando o Entity Framework Core.
- **Controllers**: A camada de apresentação, expondo APIs RESTful para integração.

Para a implementação, utilizei o .NET 6 (LTS), garantindo estabilidade e performance, e o ASP.NET Core para a construção das APIs, que oferece um suporte robusto para injeção de dependência e segurança.

## Decisões de Design e Justificativas
As principais decisões de design visaram a qualidade e a sustentabilidade do projeto:

- **Separação de Camadas**: Essa abordagem facilita a manutenção, os testes e a evolução do sistema, pois cada camada tem uma responsabilidade única, minimizando o acoplamento entre elas.
- **Injeção de Dependência**: Essencial para o desacoplamento entre as implementações e as interfaces, tornando o código mais testável e flexível a futuras mudanças.
- **ViewModels**: Utilizados para expor apenas os dados necessários nas APIs, protegendo os detalhes internos das entidades de domínio.
- **Serviços de Domínio e Repositórios**: Centralizam regras de negócio complexas e abstraem o acesso aos dados, respectivamente, garantindo que a lógica principal seja clara e isolada.
  
Essas escolhas promovem manutenibilidade, testabilidade e escalabilidade, além de aderirem a boas práticas amplamente reconhecidas no mercado. Optei por não seguir um caminho monolítico simples ou de acesso direto ao banco nas Controllers para evitar acoplamento excessivo e dificuldades futuras de manutenção.

## Abordagem de Design (DDD-inspired)
Embora o projeto não seja uma implementação completa do DDD clássico, ele incorpora vários de seus princípios para garantir organização e clareza:

- **Separação de Responsabilidades**: As camadas de domínio, aplicação, infraestrutura e apresentação estão bem definidas.
- **Modelos de Domínio**: Entidades claras como Sale, Client e ProductItem representam conceitos de negócio.
- **Serviços de Domínio e Repositórios**: Encapsulam regras de negócio e abstraem o acesso a dados, permitindo um foco maior no domínio.
  
A escolha de uma abordagem inspirada no DDD, em vez de uma implementação completa, permite obter os benefícios de organização, clareza e testabilidade do DDD, sem a complexidade total que nem sempre é necessária para projetos de médio porte ou testes técnicos. Essa decisão foi estratégica para demonstrar domínio de boas práticas sem sobrecarregar a solução com padrões que poderiam ser excessivos para o escopo.

Espero que esta apresentação resuma bem o trabalho desenvolvido. Estou à disposição para quaisquer perguntas.
