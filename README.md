# PetCare Manager

## Visão geral
O sistema simula um sistema de Petshop, incluindo cadastro de usuários(clientes, veterinários e admin), animais, medicamentos, exames e realizar o agentdamento de consultas. Diferentes usuários da aplicação podem interagir com suas respectivas atribuições.

## Objetivo
Esse projeto foi desenvolvido utilizando o ASP.NET Core MVC containerizada com Docker. A criação desse projeto teve como objtivo o estudo da Framework e do uso da linguagem C# durante o seu desenvolvimento.

## Funcionalidades

- [x] Cadastro e login de usuários (administradores, veterinários e clientes)
- [x] [Cliente] Cadastro de animais com suas características (nome, raça, espécie, etc.)
- [x] [Administrador] Criação de consultas associando animal e veterinário
- [x] [Administrador] Criação/edição de medicamentos, exames, raças, espécies, especialidades, etc.
- [x] [Administrador] Gerenciamento de usuários da aplicação
- [x] [Veterinário] Visualização das suas consultas e informações
- [x] [Cliente] Visualização das consultas dos seus animais e informações relacionadas
- [ ] Criptografia de senha dos usuários no banco de dados
- [ ] Associação de medicamentos e exames às consultas

## Tecnologias Utilizadas

- Docker
- Docker Compose
- ASP.NET Core MVC
- C#

## Workflows CI/CD)

O projeto conta com workflows via GitHub Actions, acilitando testes, builds e releases. Atualmente, estão disponíveis:

- Construção e teste da aplicação em toda __pr__ e __push__ nas branchs __develop__ e __main__
- Criação de release com artefato .zip a cada __push__ na branch __main__

## Rodando com Docker

### Pré-requisitos
* Docker instalado
* Docker Compose instalado

### Passos para executar

1. Clone o projeto:
```bash
    git clone https://github.com/ViniciusCassemira/petcare-manager.git
```

2. Acesse o diretório:
```bash
    cd petcare-manager
```

3. Executando com Docker Compose em background:
```bash
    docker compose up --build -d
```
4. Acesse a aplicação pelo navegador:
```bash
    http://localhost:5030
```

## Usuário Admin padrão

* email: admin@admin.com
* senha: admin@123

## Observações
A aplicação está em desenvolvimento, futuras funcionalidades surgirão em breve