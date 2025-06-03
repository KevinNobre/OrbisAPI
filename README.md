# Orbis API

Projeto desenvolvido por alunos do segundo ano do curso de Análise e Desenvolvimento de Sistemas da FIAP, para a Global Solution 01/2025.

JULIANA MOREIRA DA SILVA – RM: 554113

KEVIN CHRISTIAN NOBRE – RM: 552590

SABRINA COUTO XAVIER – RM: 552728

---
## 🏗️ Arquitetura do Sistema  

### 🔹 **Escolha da Arquitetura: Monolítica vs Microservices**  
O **Orbis** foi desenvolvido utilizando uma **arquitetura monolítica**, onde toda a lógica reside em um único código-base.  

🟢 **Motivos da escolha:**  
- **Simplicidade no desenvolvimento e manutenção** ✅  
- **Menor complexidade operacional** 🚀  
- **Facilidade de integração com o banco de dados Oracle** 💾  
- **Escalabilidade futura planejada**, permitindo migração para microsserviços se necessário 🔄  

🔹 **Estrutura da API**  
A API segue **boas práticas de desenvolvimento**, utilizando:  
- **Princípios SOLID** para modularidade e manutenção eficiente.  
- **Design Patterns** como **Repository Pattern** e **Service Layer** para separação de responsabilidades.  

### Escopo
O projeto abrange o desenvolvimento de um sistema que:
- Realiza o gerenciamento de usuário, incluindo registro e autenticação.
- Fornece funcionalidades de CRUD (Create, Read, Update, Delete) para gerenciar os dados dos usuários pacientes.
- Implementa a lógica de negócios necessária para validações e operações específicas, como a validações de segurança.
- Estabelece uma estrutura de repositório para o acesso e manipulação de dados no banco de dados.
- Utiliza mapeamento de entidades para garantir que as operações do banco de dados sejam realizadas de maneira eficaz.
  
---

## 📚 Design Patterns Utilizados  

### 🔹 Repository Pattern  
Utilizado para abstrair a lógica de acesso ao banco de dados, permitindo um código mais desacoplado e testável.  

### 🔹 Service Layer  
Separa a lógica de negócios da camada de API, facilitando a manutenção e testes unitários.  

### 🔹 Dependency Injection  
Melhora a modularidade e facilita a inversão de dependência dentro do projeto.  

---
## 🔍 Aplicação dos Princípios SOLID

### ✅ S — Single Responsibility Principle
Cada classe possui **uma única responsabilidade**.  

### ✅ O — Open/Closed Principle
O sistema é **aberto para extensão, mas fechado para modificação**.  
Exemplo: podemos adicionar novos tipos de análise (ex: análise de sarcasmo) sem modificar a lógica existente.

### ✅ L — Liskov Substitution Principle
Interfaces podem ser substituídas por suas implementações sem causar falhas.  
Exemplo: Repositórios implementam interfaces da camada Domain, respeitando contratos previsíveis.

### ✅ I — Interface Segregation Principle
Interfaces são **específicas e enxutas**, evitando a obrigatoriedade de implementar métodos que não fazem sentido para a classe.  
Exemplo: repositórios definem apenas métodos necessários à sua entidade.

### ✅ D — Dependency Inversion Principle
Camadas superiores dependem de **abstrações**, não de implementações concretas.  
Exemplo: Controllers dependem de serviços via interfaces (`IUsuarioService`), injetadas pelo DI do .NET.

---

## Como Rodar o Projeto:

Certifique-se de que o SQL Developer esteja instalado e configurado.
Atualize a string de conexão no arquivo appsettings.json para o seu banco de dados.

## Tecnologias Utilizadas
- .NET 8.0
- Entity Framework Core
- Oracle SQL Developer Server
- C#
- ASP.NET Core
- Swagger/OpenAPI para documentação da API

### Pré-requisitos
Antes de iniciar, certifique-se de ter os seguintes requisitos instalados:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Developer Server (Oracle)](https://www.oracle.com/database/sqldeveloper/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (ou outro IDE compatível)
- [Git](https://git-scm.com/)
