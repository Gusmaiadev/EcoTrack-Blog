# EcoTrack Blog 🌱

## 💡 Visão Geral
O EcoTrack Blog é uma plataforma web integrada com aplicativo Android, focada em promover sustentabilidade e eficiência energética através de conteúdo educativo sobre uso consciente de eletrodomésticos.

## 🎯 Objetivos
- Redução do consumo energético residencial
- Diminuição da emissão de CO2
- Educação sobre uso eficiente de eletrodomésticos
- Integração com aplicativo móvel de monitoramento

## 🚀 Tecnologias

### Backend
- ASP.NET Core 7.0 MVC
- Entity Framework Core
- Oracle Database
- AutoMapper
- JWT Authentication
- BCrypt.NET

### Frontend
- Razor Views
- Bootstrap 5
- jQuery
- SweetAlert2
- Font Awesome

## 📁 Estrutura do Projeto

```
EcoTrack.Blog/
├── Configuration/           # Configurações (AutoMapper, DI, JWT)
├── Controllers/            # Controllers MVC e API
├── Data/                   # Acesso a dados
│   ├── Context/           # DbContext
│   └── Repositories/      # Repositories
├── Helpers/               # Classes auxiliares
├── Models/                # Models
│   ├── DTOs/             # DTOs
│   └── Entities/         # Entidades
├── Services/              # Serviços
├── Views/                 # Views Razor
│   ├── Author/           # Views de autor
│   ├── Home/             # Views da home
│   └── Shared/           # Layouts e partials
└── wwwroot/              # Arquivos estáticos
    ├── css/              # Estilos
    ├── js/               # Scripts
    └── images/           # Imagens
```

## 🔑 Funcionalidades

### Sistema de Blog
- Visualização de posts por categoria
- Interface responsiva
- Modal de leitura de posts
- Gerenciamento de conteúdo

### Área Administrativa
- Login de autores
- CRUD de posts
- Gestão de categorias
- Dashboard de autor

### Categorias de Conteúdo
- Geladeira
- Ar Condicionado
- Máquina de Lavar
- Microondas
- Fogão
- Chuveiro Elétrico
- TV
- Iluminação

## 💻 Interface

### Home
- Banner principal
- Cards de categorias
- Seção sobre o projeto
- Área de benefícios

### Posts
- Cards com imagens
- Preview do conteúdo
- Modal de leitura completa
- Navegação por categoria

### Área do Autor
- Login seguro
- Gerenciamento de posts
- Editor de conteúdo
- Logout

## 🔒 Segurança
- Autenticação JWT
- Cookies seguros
- Hash de senhas BCrypt
- Proteção CSRF
- Validação de formulários

## ⚙️ Instalação

1. Clone o repositório
2. Configure a string de conexão em `appsettings.json`
3. Execute as migrations:
```bash
dotnet ef database update
```
4. Execute o projeto:
```bash
dotnet run
```

## 📝 Acessos

### Usuário 1
- E-mail: gustavoblog@gmail.com
- Senha: gustavoMaia

### Usuário 2
- E-mail: caioblog@gmail.com  
- Senha: caioBlog

## 👥 Integrantes

- Gustavo Maia (RM: 553270)
- Kauã Almeida (RM: 552618) 
- Rafael Vida (RM: 553721)

## 📈 Melhorias Futuras
- Sistema de comentários
- Upload de imagens
- Newsletter
- Integração com métricas
- Dashboard de impacto ambiental
- Sistema de recomendação