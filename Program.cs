using Microsoft.EntityFrameworkCore;
using PontuaFlow.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(
    options => options.UseNpgsql(connectionString)
 );

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "PontuaFlow API",
        Version = "v1.0.0",
        Description = """
        <a href="https://learn.microsoft.com/pt-br/dotnet/csharp/">
           <img src="https://img.shields.io/badge/C%23_12-239120?style=for-the-badge&logo=c-sharp&logoColor=white" alt="CSharp-badge" style="max-width: 50%;">
        </a>
        <a href="https://dotnet.microsoft.com/pt-br/download/dotnet/8.0">
           <img src="https://img.shields.io/badge/.NET_8-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="DotNet-badge" style="max-width: 50%;">
        </a>
        <a href="https://www.postgresql.org/">
           <img src="https://img.shields.io/badge/PostgreSQL-316192?style=for-the-badge&logo=postgresql&logoColor=white" alt="PostgreSQL-badge" style="max-width: 50%;">
        </a>
        <a href="https://swagger.io/">
           <img src="https://img.shields.io/badge/Swagger-85EA2D?style=for-the-badge&logo=swagger&logoColor=black" alt="Swagger-badge" style="max-width: 50%;">
        </a>
        <br><br>
        API para suporte à aplicação de gestão de projetos, desenvolvedores e tarefas com gamificação por pontos.
        
        ---

        **1. Estrutura Base:**
        - Dentro de um `Project` (projeto), são cadastrados:
          - `Devs` (Desenvolvedores).
          - `Weeks` (Semanas, com restrição de _NumeroSemana_ único por projeto).
        - As `Tasks` (Tarefas) amarram o fluxo: pertencem a um Projeto, atribuídas a um `Dev` e vinculadas a uma `Week`.

        **2. Regras de Pontuação (Gamificação):**
        A pontuação de uma Tarefa é validada em valores fixos:
        - 🔴 `0` : *Zerou / Não deu*
        - 🟠 `2` : *Saiu algo*
        - 🟡 `3` : *Quase*
        - 🟢 `5` : *Deu bom* (Meta regular)
        - 🔵 `8` : *Extra* (Superação)

        **3. Métricas e Ranking:**
        - **Total de Pontos**: É a soma matemática de todos os pontos de tarefas finalizadas de um dev no projeto.
        - **Aproveitamento (%)**:
           - Base teórica de sucesso: `1 Task = 5 Pontos (100% de aproveitamento)`.
           - Cálculo: `((Pontos Obtidos) / (Quantidade de Tasks Cadastradas * 5)) * 100`.
           - *Nota técnica: Pode ultrapassar os `100%` se o dev acumular pontos Extra (8).*
        - **Ranking**: Lista de Devs do projeto ordenada de forma decrescente pelo seu *Total de Pontos*.

        ---

        **Made by**

        <table>
          <tr>
            <td valign="middle" width="25%">
              <div align="center">  
                <img src="https://avatars.githubusercontent.com/u/76459155?s=400&u=4b9bd87cae92eea4fc154c28eafe226ed034a1d8&v=4" width="100" alt="Profile Pic - Juletopi"/>
                <br>
                <sub><strong>Júlio Cézar | Juletopi</strong></sub>
                <br>
              </div>
            </td>
            <td valign="middle" width="75%">
              <ul style="list-style: none; padding-left: 0; margin: 0;">
                <li>
                  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/linkedin/linkedin-original.svg" width="15" alt="LinkedIn" style="vertical-align:middle;">
                  LinkedIn — 
                  <a href="https://www.linkedin.com/in/julio-cezar-pereira-camargo/" target="_blank" rel="noopener noreferrer" aria-label="LinkedIn - Júlio Cézar P. Camargo">
                    Júlio Cézar P. Camargo
                  </a>
                </li>
                <li>
                  <img src="https://pngimg.com/uploads/email/email_PNG100738.png" width="15" alt="Email" style="vertical-align:middle;">
                  Email — 
                  <a href="mailto:juliocezarpvh@hotmail.com" aria-label="Send email - juliocezarpvh@hotmail.com">
                    juliocezarpvh@hotmail.com
                  </a>
                </li>
                <li>
                  <img src="https://github.com/user-attachments/assets/a3e6ca25-6035-4a7a-94b9-f35cb9d24a96" width="15" alt="Portfolio" style="vertical-align:middle;">
                  Portfolio — 
                  <a href="https://juletopi.github.io/JCPC_Portfolio/" target="_blank" rel="noopener noreferrer" aria-label="Portfolio - Juletopi">
                    juletopi.github.io/JCPC_Portfolio
                  </a>
                </li>
                <li>
                  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon@latest/icons/github/github-original.svg" width="15" alt="GitHub" style="vertical-align:middle;">
                  GitHub — 
                  <a href="https://github.com/juletopi" target="_blank" rel="noopener noreferrer" aria-label="GitHub - Juletopi">
                    github.com/juletopi
                  </a>
                </li>
              </ul>
            </td>
          </tr>
        </table>
        """
    });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = System.IO.Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
    c.CustomSchemaIds(x => x.FullName);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
