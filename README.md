<div align="center">
   <h2 align="center">PontuaFlow API</h2>
   <p align="center">
      API para suporte à aplicação de gestão de projetos, desenvolvedores e tarefas com gamificação por pontos.
   </p>
</div>

<div align="center">
   <a href="https://learn.microsoft.com/pt-br/dotnet/csharp/">
      <img src="https://img.shields.io/badge/C%23_12-239120?style=for-the-badge&logo=c-sharp&logoColor=white" alt="CSharp-badge" style="max-width: 100%;">
   </a>
   <a href="https://dotnet.microsoft.com/pt-br/download/dotnet/8.0">
      <img src="https://img.shields.io/badge/.NET_8-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="DotNet-badge" style="max-width: 100%;">
   </a>
   <a href="https://www.postgresql.org/">
      <img src="https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white" alt="PostgreSQL-badge" style="max-width: 100%;">
   </a>
   <a href="https://swagger.io/">
      <img src="https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black" alt="Swagger-badge" style="max-width: 100%;">
   </a>
</div>

<br>

<div align="center">
   <a href="#sobre-o-projeto">Sobre</a> &#xa0; • &#xa0;
   <a href="#estrutura-do-projeto">Estrutura</a> &#xa0; • &#xa0;
   <a href="#instalação">Instalação</a> &#xa0;
</div>

---

## Sobre o projeto

O **PontuaFlow API** é uma aplicação backend construída em .NET 8 que suporta a gestão de projetos, tarefas, desenvolvedores e equipes. A aplicação integra um sistema de gamificação, incentivando o progresso através de pontuações bem definidas.

### Funcionalidades

- **Módulo de Projetos e Semanas**: Controle hierárquico onde `Weeks` (semanas) possuem numeração única garantida por projeto.
- **Módulo de Desenvolvedores (`Devs`) e Equipes (`DevTeams`)**: Cadastro unificado e associação direta com os projetos.
- **Ciclo e Avaliação de Tarefas (`Tasks`)**: Atribuição de tarefas ao dev e vinculação à semana específica do projeto.
- **Regras de Pontuação (Gamificação)**: Validação fixa de pontos baseada no resultado da tarefa:
   - 🔴 `0` : *Zerou / Não deu*
   - 🟠 `2` : *Saiu algo*
   - 🟡 `3` : *Quase*
   - 🟢 `5` : *Deu bom* (Meta regular)
   - 🔵 `8` : *Extra* (Superação)
- **Métricas e Ranking**: 
   - **Total de Pontos**: Soma simples de todos os pontos ganhos em tarefas fechadas por um dev num projeto.
   - **Aproveitamento (%)**: Cálculo fundamentado em `((Pontos Obtidos) / (Quantidade de Tasks Cadastradas * 5)) * 100`.
   - **Ranking**: Lista de Devs de um determinado projeto ordenada do maior para o menor multiplicador/pontuação.

### Tecnologias utilizadas

<a href="https://learn.microsoft.com/pt-br/dotnet/csharp/">
   <img src="https://img.shields.io/badge/C%23_12-239120?style=for-the-badge&logo=c-sharp&logoColor=white" alt="CSharp-badge">
</a>
<a href="https://dotnet.microsoft.com/pt-br/download/dotnet/8.0">
   <img src="https://img.shields.io/badge/.NET_8-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="DotNet-badge">
</a>
<a href="https://learn.microsoft.com/en-us/ef/core/">
   <img src="https://img.shields.io/badge/EF_Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="EFCore-badge">
</a>
<a href="https://www.postgresql.org/">
   <img src="https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white" alt="PostgreSQL-badge">
</a>

<div align="left">
   <h6><a href="#pontuaflow-api"> Voltar para o início ↺</a></h6>
</div>

## Estrutura do projeto

```text
PontuaFlowAPI/
├── Controllers/               # Endpoints REST expostos
├── Data/                      # Entity Framework e AppDbContext
├── Migrations/                # Controle de versionamento do BD gerados pelo EF Core
├── Models/                    # Entidades (Project, Dev, Task, Week, DevTeam)
├── appsettings.json
├── Program.cs                 # Registro de dependências, setup da API e documentação Swagger
├── PontuaFlow.csproj
└── README.md
```

<div align="left">
   <h6><a href="#pontuaflow-api"> Voltar para o início ↺</a></h6>
</div>

## Instalação

### Iniciando o projeto

> [!IMPORTANT]
> Certifique-se de ter os seguintes requisitos antes de iniciar:
>
> <a href="https://dotnet.microsoft.com/pt-br/download/dotnet/8.0">
>    <img src="https://img.shields.io/badge/.NET_8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="DotNet-badge">
> </a>
> <a href="https://www.postgresql.org/download/">
>    <img src="https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white" alt="PostgreSQL-badge">
> </a>

1. Clone o repositório

```bash
git clone https://github.com/juletopi/PontuaFlowAPI.git
cd PontuaFlowAPI
```

2. Restaure as dependências do projeto

```bash
dotnet restore
```

3. Configure a string de conexão

Edite o arquivo `appsettings.json` na raiz da API ou os User Secrets, ajustando com os seus dados locais do PostgreSQL:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=PontuaFlowDB;Username=postgres;Password=suasenha"
}
```

4. Aplique as Migrations

```bash
dotnet ef database update
```

5. Inicie o projeto

```bash
dotnet run
```

> [!NOTE]
> O Swagger irá gerar automaticamente a documentação UI dos endpoints da API. 
> Ele estará acessível através da rota `http://localhost:<PORTA_LOCAL>/swagger/index.html`.

<div align="left">
   <h6><a href="#pontuaflow-api"> Voltar para o início ↺</a></h6>
</div>
