# EcoTrack Blog API 🌱

## 💡 Visão Geral
O EcoTrack Blog é parte de um ecossistema maior voltado para a sustentabilidade e eficiência energética. Integrado ao nosso aplicativo Android, o blog serve como uma plataforma educativa essencial para conscientizar e orientar usuários sobre economia de energia através do uso consciente de eletrodomésticos.

### Objetivo Principal
Nosso objetivo é promover a redução do consumo energético residencial e, consequentemente, diminuir a emissão de CO2, através de:
- Dicas práticas de economia de energia
- Guias específicos por tipo de eletrodoméstico
- Informações sobre impacto ambiental
- Estratégias de uso eficiente de aparelhos domésticos

### Integração com App Android
- Complementa as funcionalidades do aplicativo móvel
- Fornece conteúdo detalhado e atualizado
- Permite que usuários aprofundem seu conhecimento
- Cria uma comunidade engajada em sustentabilidade

### Categorias de Conteúdo
- Refrigeradores e Freezers
- Máquinas de Lavar e Secar
- Ar Condicionado e Climatização
- Iluminação
- Aparelhos de Cozinha
- Eletrônicos e Entretenimento
- Aquecedores de Água
- Dicas Gerais de Economia

## 🚀 Tecnologias Utilizadas

- **.NET Core 7.0**
- **Entity Framework Core**
- **Oracle Database**
- **JWT (JSON Web Tokens)**
- **AutoMapper**
- **Swagger/OpenAPI**
- **BCrypt.NET**

## 📁 Estrutura do Projeto

```
EcoTrack.Blog/
├── Configuration/           # Configurações da aplicação
├── Controllers/            # Controllers da API
├── Data/                   # Camada de acesso a dados
│   ├── Context/           # Contexto do EF Core
│   └── Repositories/      # Implementação dos repositories
├── Models/                # Classes de modelo
│   ├── DTOs/             # Data Transfer Objects
│   └── Entities/         # Entidades do banco de dados
├── Services/              # Camada de serviços
└── Migrations/            # Migrações do banco de dados
```

## 🔑 Principais Funcionalidades

### Autores
- Registro de novos autores
- Autenticação via JWT
- Gerenciamento de perfil

### Categorias
- CRUD completo de categorias
- Validação de nomes únicos
- Associação com posts

### Posts
- Criação e edição de posts
- Filtros por categoria e autor
- Controle de acesso baseado no autor

## 🏗️ Arquitetura

### Camada de Entidades
- **Author**: Gerencia informações dos autores do blog
- **Category**: Organiza posts em categorias
- **Post**: Armazena o conteúdo dos artigos

### Padrão Repository
Implementação completa do padrão repository com:
- `IBaseRepository<T>`: Interface base genérica
- Repositories específicos para cada entidade
- Unit of Work implícito via DbContext

### Camada de Serviços
- Lógica de negócios separada dos controllers
- Validações e regras de negócio
- Mapeamento entre DTOs e entidades

## 🔒 Segurança

### Autenticação
- JWT (JSON Web Tokens)
- Senhas hash com BCrypt
- Tokens com expiração configurável

### Autorização
- Endpoints protegidos por [Authorize]
- Validação de propriedade de recursos
- CORS configurado

## 📝 Documentação da API

### Swagger UI
A documentação completa da API está disponível através do Swagger UI em:
```
https://localhost:7280/swagger
```

### Endpoint Raiz
A rota raiz (`/`) fornece um endpoint de boas-vindas que:
- Confirma que a API está funcionando
- Mostra a versão atual
- Direciona para a documentação

```json
GET /
Resposta:
{
    "message": "EcoTrack Blog API",
    "version": "1.0",
    "documentation": "/swagger"
}
```

### Endpoints Principais

#### Autores
```
POST /api/authors/register    # Registro de autor
POST /api/authors/login      # Login de autor
GET  /api/authors/{id}       # Detalhes do autor
```

#### Categorias
```
GET    /api/categories       # Lista todas categorias
POST   /api/categories      # Cria nova categoria
PUT    /api/categories/{id} # Atualiza categoria
DELETE /api/categories/{id} # Remove categoria
```

#### Posts
```
GET    /api/posts           # Lista todos posts
GET    /api/posts/{id}      # Detalhes do post
POST   /api/posts          # Cria novo post
PUT    /api/posts/{id}     # Atualiza post
DELETE /api/posts/{id}     # Remove post
```

## ⚙️ Configuração e Instalação

### Pré-requisitos
- .NET Core 7.0 SDK
- Oracle Database
- Visual Studio 2022 ou VS Code

### Configuração do Banco de Dados
1. Atualize a string de conexão em `appsettings.json`
2. Execute as migrações:
```bash
dotnet ef database update
```

### Executando o Projeto
```bash
dotnet restore
dotnet run
```

## 🔧 Configurações Personalizáveis

O projeto permite configurar:
- Tempo de expiração do JWT
- Origens CORS permitidas
- Configurações de banco de dados
- Níveis de log

Através do arquivo `appsettings.json`.

## 🧪 Testes

O projeto está estruturado para facilitar testes com:
- Injeção de dependência
- Interfaces bem definidas
- Separação de responsabilidades

## 📈 Melhorias Futuras

- [ ] Implementação de cache
- [ ] Sistema de comentários
- [ ] Upload de imagens
- [ ] Tags para posts
- [ ] Sistema de newsletter
- [ ] Integração com métricas de economia de energia
- [ ] Dashboard de impacto ambiental
- [ ] Calculadora de economia de energia
- [ ] Gamificação para engajamento dos usuários
- [ ] Sistema de recomendação personalizada

## 🌍 Impacto Ambiental

O EcoTrack Blog contribui para:
- Redução do consumo de energia elétrica
- Diminuição da emissão de CO2
- Conscientização ambiental
- Promoção de hábitos sustentáveis
- Educação sobre eficiência energética

