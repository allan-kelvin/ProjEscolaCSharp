# 🏫 Escola ERP — Sistema Escolar Desktop

Sistema desktop para gestão escolar desenvolvido em **C# + .NET + WPF**, com autenticação, controle de acesso e cadastros essenciais (Cursos, Alunos, Funcionários e Usuários).

O projeto foi construído com foco em **arquitetura organizada**, **boas práticas** e **aprendizado real de desenvolvimento desktop**.

---

## 📌 Visão Geral

O **Escola ERP** é um sistema desktop que permite:

- Autenticação de usuários
- Controle de permissões
- Dashboard com atalhos (cards)
- Cadastro e gerenciamento de:
  - Cursos
  - Alunos
  - Funcionários
  - Usuários do sistema
- Alteração de senha
- Logout seguro

---

## 🧠 Arquitetura do Projeto

O projeto utiliza uma **arquitetura em camadas**, inspirada em **Layered Architecture** + **MVVM (WPF)**.

### 📁 Estrutura de Pastas



---

## 📂 Responsabilidade de Cada Camada

### 🔹 Data
Responsável **exclusivamente** pela conexão com o banco de dados.

- Centraliza a string de conexão
- Fornece conexões MySQL para os Services

Exemplo:
- `MySqlContext.cs`

---

### 🔹 Models
Representam as **entidades do sistema** (espelho das tabelas do banco).

Características:
- Apenas propriedades
- Sem regras de negócio
- Sem SQL
- Sem UI

Exemplos:
- `Aluno`
- `Curso`
- `Funcionario`
- `Usuario`

---

### 🔹 Services
Camada responsável por:

- Regras de negócio
- Acesso ao banco (CRUD)
- Segurança (hash de senha)
- Login e permissões

Exemplos:
- `AlunoService`
- `UsuarioService`

📌 Nenhuma tela acessa o banco diretamente.

---

### 🔹 ViewModels
Funcionam como o **cérebro da tela** (MVVM).

Responsabilidades:
- Fornecer dados para a View
- Executar Commands
- Controlar estado da tela
- Comunicar-se com Services

Exemplo:
- `AlunoViewModel`

---

### 🔹 Views
Camada de **interface gráfica (WPF)**.

Responsabilidades:
- Exibir dados
- Capturar interações do usuário
- Não contém regra de negócio

Exemplos:
- `LoginView`
- `MainWindow`
- Telas de cadastro

---

## 🔐 Autenticação e Segurança

### ✔ Login
- Usuário e senha armazenados no MySQL
- Senhas protegidas com **SHA256**
- Login retorna o usuário autenticado

### ✔ Sessão do Usuário
- Classe estática `SessaoUsuario`
- Armazena o usuário logado durante a execução

### ✔ Permissões
Perfis disponíveis:
- **ADMIN**
- **PADRAO**

Regras:
- Apenas ADMIN pode acessar cadastro de usuários
- Menus são exibidos/ocultados conforme perfil

---

## 👤 Funcionalidades Implementadas

### 🔑 Login
- Tela moderna
- Ícone visual
- Suporte a tecla **ENTER**
- Validação de credenciais

---

### 🧭 Dashboard
- Menu superior
- Usuário logado visível
- Cards de acesso rápido:
  - Cursos
  - Alunos
  - Funcionários

---

### 📚 Cursos
- Cadastro
- Edição
- Exclusão
- Listagem em DataGrid

---

### 🎓 Alunos
- Cadastro
- Edição
- Exclusão
- Associação com Curso (FK)
- ComboBox de cursos

---

### 👩‍🏫 Funcionários
- Cadastro
- Edição
- Exclusão

---

### 👤 Usuários
- Cadastro de usuários do sistema
- Controle de perfil (ADMIN / PADRAO)
- Senhas criptografadas

---

### 🔄 Troca de Senha
- Usuário logado pode alterar sua senha
- Confirmação de senha
- Persistência segura no banco

---

### 🚪 Logout
- Encerra a sessão
- Retorna para a tela de login

---

## 🛠 Tecnologias Utilizadas

- **C#**
- **.NET 8**
- **WPF**
- **MySQL**
- **MySqlConnector**
- **MVVM (conceito aplicado)**
- **SHA256 para senhas**

---

## 🎯 Objetivo do Projeto

Este projeto foi desenvolvido com foco em:

- Aprendizado prático de WPF
- Organização de código
- Arquitetura profissional
- Boas práticas de desenvolvimento desktop
- Base sólida para evolução futura

---

## 🚀 Possíveis Evoluções Futuras

- Dashboard com gráficos
- Contadores dinâmicos nos cards
- Tema claro / escuro
- Logs de acesso
- Controle avançado de permissões
- Migração para ASP.NET (Web)

---

## 📸 Screenshots

> (adicione imagens do sistema aqui, se desejar)

---

## 👨‍💻 Autor

Projeto desenvolvido para fins de **estudo e aprendizado**, simulando um **ERP escolar real**, com foco em boas práticas e arquitetura.

---

## 📄 Licença

Uso livre para fins educacionais.

