# Documentação de Configuração do VS Code #

Esta documentação descreve as configurações do Visual Studio Code para o projeto.
 Os arquivos de configuração na pasta `.vscode` são essenciais para o
 desenvolvimento e o fluxo de trabalho dentro deste ambiente.

## Arquivos de Configuração ##

### 1. `extensions.json` ###

Este arquivo define as extensões recomendadas para o seu projeto. As extensões
ajudam a melhorar a produtividade e a qualidade do código. Aqui estão as extensões
recomendadas:

- **ms-dotnettools.csharp**: Suporta a linguagem C# com IntelliSense, debugging
e outros recursos.
- **ms-vscode-remote.remote-wsl**: Permite desenvolver em um ambiente Linux dentro
do Windows Subsystem for Linux (WSL).
- **formulahendry.dotnet**: Suporta ferramentas .NET no VS Code, como a execução
de projetos .NET.
- **ms-dotnettools.vscode-dotnet-runtime**: Instala o runtime do .NET no VS Code.
- **ms-dotnettools.vscodeintellicode-csharp**: IntelliCode para C#, oferecendo
sugestões inteligentes de código.
- **formulahendry.dotnet-test-explorer**: Ferramenta para explorar e rodar testes
.NET.
- **github.vscode-github-actions**: Integração com GitHub Actions, permitindo
visualizar e gerenciar fluxos de trabalho CI/CD.
- **github.copilot-chat**: Suporte para GitHub Copilot, assistente de inteligência
artificial.
- **github.vscode-pull-request-github**: Facilita a gestão de pull requests no GitHub.
- **pkief.material-icon-theme**: Aplica um tema de ícones de arquivos baseado no
Material Design.
- **vscode-icons-team.vscode-icons**: Adiciona ícones adicionais para arquivos e
pastas.
- **fernandoescolar.vscode-solution-explorer**: Oferece um explorador de soluções
para projetos .NET.
- **patcx.vscode-nuget-gallery**: Permite gerenciar pacotes NuGet dentro do VS Code.
- **jmrog.vscode-nuget-package-manager**: Gerencia pacotes NuGet diretamente no
VS Code.
- **streetsidesoftware.code-spell-checker**: Um verificador ortográfico para
código e arquivos de texto.

### 2. `launch.json` ###

Este arquivo configura as opções de execução e depuração no VS Code. Abaixo estão
os detalhes do que está configurado:

- **version**: Especifica a versão do esquema de configuração (neste caso, `0.2.0`).
- **configurations**:
    - **name**: Define o nome da configuração de lançamento (neste caso, `.NET
    Core Launch (web)`).
    - **type**: O tipo de depuração, que é `coreclr` para aplicações .NET Core.
    - **request**: Especifica o tipo de solicitação, que é `launch` (lançamento
    do aplicativo).
    - **preLaunchTask**: Especifica uma tarefa que deve ser executada antes do
    lançamento (neste caso, `build`).
    - **program**: O caminho para o executável do aplicativo. Aqui, é o arquivo
    DLL gerado após a construção do projeto.
    - **args**: Lista de argumentos a serem passados para o aplicativo durante
    a execução (vazio neste caso).
    - **cwd**: Diretório de trabalho onde o aplicativo será executado (geralmente
    o diretório do projeto).
    - **stopAtEntry**: Define se a execução deve parar na primeira linha do código.
    - **serverReadyAction**: Ação executada quando o servidor estiver pronto.
    Neste caso, abre automaticamente a URL quando o servidor começar a escutar.
    - **env**: Define variáveis de ambiente. Aqui, a variável `ASPNETCORE_ENVIRONMENT`
    é configurada como `Development`.
    - **sourceFileMap**: Mapeamento entre diretórios locais e fontes externas,
    permitindo depurar o código em `Views`.

### 3. `settings.json` ###

Este arquivo contém várias configurações do editor que afetam o comportamento do
VS Code:

- **editor.tabSize**: Define o número de espaços para a tabulação (neste caso,
4 espaços).
- **files.exclude**: Exclui certas pastas ou arquivos do explorador de arquivos
do VS Code, como as pastas `bin` e `obj`.
- **editor.formatOnSave**: Habilita a formatação automática do código sempre que
o arquivo for salvo.
- **editor.codeActionsOnSave**: Organiza automaticamente os imports ao salvar o arquivo.
- **markdownlint.config**: Configurações de linting para arquivos Markdown,
garantindo um estilo consistente.
    - **MD003**: Define o estilo dos títulos como "atx_closed".
    - **MD007**: Define o tamanho da indentação de listas como 4 espaços.
    - **MD029**: Define a ordenação de listas ordenadas.
    - **MD040**: Habilita a verificação de palavras duplicadas.
- **csharp.format.enable**: Habilita a formatação automática de código C#.
- **cSpell.words**: Adiciona palavras customizadas ao corretor ortográfico do
VS Code (ex: `commitlint`).

### 4. `tasks.json` ###

Este arquivo define as tarefas que podem ser executadas diretamente no VS Code.
As duas tarefas principais são:

- **build**:
    - **command**: O comando a ser executado, que é `dotnet build`, para compilar
    o projeto.
    - **args**: Os argumentos passados ao comando (`build` neste caso).
    - **group**: Define esta tarefa como a tarefa de construção padrão.
    - **problemMatcher**: Configura um matcher de problemas para capturar erros
    de compilação (usando `$msCompile`).
    - **test**:
    - **command**: O comando a ser executado, que é `dotnet test`, para rodar os
    testes.
    - **args**: Os argumentos passados ao comando (`test` neste caso).
    - **group**: Define esta tarefa como uma tarefa de teste.
    - **problemMatcher**: Configura um matcher de problemas para capturar erros
    nos testes (usando `$msCompile`).

---

## Como Usar ##

### Extensões Recomendadas ###

Certifique-se de instalar as extensões recomendadas no arquivo `extensions.json`
para uma experiência de desenvolvimento mais produtiva. O VS Code irá sugerir automaticamente
essas extensões quando você abrir o projeto.

### Execução e Depuração ###

Use a configuração `launch.json` para executar e depurar o projeto no VS Code.
Para iniciar a depuração, basta pressionar `F5` ou usar a opção "Iniciar Depuração"
no menu de execução.

### Tarefas de Build e Testes ###

As tarefas definidas no arquivo `tasks.json` podem ser executadas diretamente do
VS Code. Você pode compilar o projeto usando `Ctrl+Shift+B` ou rodar os testes
com `Ctrl+Shift+P` e digitando `Run Task`, seguido de "test".
