Web API desenvolvida em .NET 10 focada no gerenciamento, controle e monitoramento do ciclo de vida de chamados de atendimento técnico.

# Tecnologias Utilizadas:
* C# (.NET 10)
* ASP.NET Core Web API
* Entity Framework Core
* Banco de Dados SQLite
* Documentação Interativa com Swagger

# Funcionalidades Principais
* **Gerenciamento de Chamados:** Fluxo completo com abertura de chamados (incluindo descrição detalhada do problema), Check-in (início do atendimento), Check-out (finalização com solução) e Cancelamento.
* **Manutenção do Sistema:** Endpoints de `DELETE` para setores e prioridades com validação de integridade referencial (impede a exclusão se houver chamados ativos vinculados).
* **Relatório Dinâmico de SLA:** Geração de relatórios com cálculo exato em horas do tempo total de atendimento e indicador automatizado de atraso com base na prioridade definida.

## 🏃 Como Executar o Projeto
1. SDK do .NET 10 instalado.
2. Clone o repositório,
3. Execute com dotnet run
4. Acesse a interface em http://localhost:5256/swagger