# Configurando Commitizen, Husky e Commitlint em um Projeto #

Este guia explica como instalar e configurar o **Commitizen**, **Husky** e **Commitlint**
para padronizar e automatizar mensagens de commit em um projeto.

---

## **1. Pré-requisitos** ##

- **Node.js** e **npm** instalados.  
  Verifique com:

  ```shell
  node -v
  npm -v
  ```

- Um repositório Git inicializado no projeto:

  ```shell
  git init
  ```

---

## **2. Instalando Dependências** ##

Execute o comando abaixo para instalar as dependências necessárias:

```shell
npm install --save-dev husky "@commitlint/config-conventional" "@commitlint/cli"
```

- **commitizen**: Ferramenta para criar mensagens de commit interativas.
- **cz-conventional-changelog**: Adapta mensagens de commit ao formato **Conventional
  Commits**.
- **husky**: Configura hooks Git para automação.
- **@commitlint/cli**: Valida mensagens de commit.
- **@commitlint/config-conventional**: Conjunto de regras para mensagens convencionais.

---

## **3. Configurando o Commitizen** ##

Adicione o arquivo `package.json`:

```shell
npm init -y
```

Adicione a configuração para usar o adaptador **cz-conventional-changelog**:

```json
"config": {
  "commitizen": {
    "path": "./node_modules/cz-conventional-changelog"
  }
}
```

---

## **4. Configurando o Commitlint** ##

Adicione a configuração abaixo ao `package.json` para integrar o Commitlint:

```json
"commitlint": {
  "extends": [
    "@commitlint/config-conventional"
  ]
}
```

Essa configuração aplica as regras do **Conventional Commits**.

---

## **5. Configurando o Husky** ##

1. **Instalar hooks com Husky:**
   No terminal, execute:

   ```shell
   npx husky install
   ```

   Isso cria uma pasta `.husky` no projeto, onde os hooks serão configurados.

2. **Adicionar o hook de validação de commit:**
   Configure o Husky para verificar mensagens de commit:

   ```shell
   npx husky add .husky/commit-msg 'npx --no-install commitlint --edit $1'
   ```

3. **Adicionar o script de preparação no `package.json`:**
   Certifique-se de que o script `prepare` esteja configurado:

   ```json
   "scripts": {
     "prepare": "husky install",
     "commit": "cz"
   }
   ```

---

## **6. Testando a Configuração** ##

**Iniciar um commit com o Commitizen:**

   ```shell
   npm run commit
   ```

   Isso abrirá um assistente interativo. Responda às perguntas para criar a mensagem
   de commit.
   obs.: É preciso ter ao menos um arquivo em staged para realizar o processo.

**Exemplo de mensagem gerada:**

   Ao utilizar o **Commitizen** para criar um commit, você será guiado por perguntas
    interativas. Veja um exemplo de como preencher os campos:

```shell
  ? Select the type of change that you're committing: `chore`
  ? What is the scope of this change (e.g. component or file name): (press enter
    to skip) `package.json`
  ? Write a short, imperative tense description of the change (max 76 chars):
    `add dependencies for Commitizen, Husky, and Commitlint`
  ? Provide a longer description of the change: (press enter to skip) `Installed
  and configured Commitizen, Husky, and Commitlint to enforce conventional commit
  messages and improve commit practices.`
  ? Are there any breaking changes? (y/N) `N`
  ? Does this change affect any open issues? (y/N) `N`
```

  Após responder as perguntas, o Commitizen gera a seguinte mensagem de commit:

  ```shell
  chore(package.json): add dependencies for Commitizen, Husky, and Commitlint

  Installed and configured Commitizen, Husky, and Commitlint to enforce conventional
   commit messages and improve commit practices.
  ```

**Testar a validação de mensagens:**  
   Faça um commit fora do padrão e observe
   a validação do Husky:

  ```shell
  git commit -m "wrong message"
  ```

  Saída esperada:

  ```shell
  ❯ git commit -m "wrong message"
  ⧗ input: wrong message
  ✖ subject may not be empty [subject-empty]
  ✖ type may not be empty [type-empty]
  ```

---

## **7. Estrutura Final do Projeto** ##

Após a configuração, seu projeto deve conter:

- `package.json` com:
- Scripts configurados.
- Configurações de Commitizen e Commitlint.
- `.husky/` contendo:
- `commit-msg`: Hook para validar mensagens de commit.
- Dependências instaladas no `node_modules`.

---

## **8. Comandos Úteis** ##

- **Iniciar um commit com o Commitizen:**

```shell
npm run commit
```

- **Adicionar um hook personalizado do Husky:**

```shell
npx husky add .husky/<hook-name> "<command>"
```

- **Reinstalar Husky (se necessário):**

```shell
npx husky install
```

---

## **Dicas** ##

- **Problemas com TLS na instalação do npm?**  
Certifique-se de que a versão do Node.js seja recente e use HTTPS no registro:

```shell
npm config set registry https://registry.npmjs.org/
```

- **Manter consistência em commits:**  
Use o `commitlint` integrado ao pipeline CI/CD para garantir que todos os commits
sejam validados.
