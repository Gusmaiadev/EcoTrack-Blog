using EcoTrack.Blog.Data.Repositories.Interfaces;
using EcoTrack.Blog.Models.DTOs;
using EcoTrack.Blog.Models.Entities;
using EcoTrack.Blog.Services.Interfaces;
using AutoMapper;
using BCrypt.Net;

namespace EcoTrack.Blog.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, ITokenService tokenService, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<AuthorDto> RegisterAsync(AuthorRegisterDto registerDto)
        {
            if (await _authorRepository.EmailExistsAsync(registerDto.Email))
                throw new ApplicationException("Email já está em uso.");

            if (registerDto.Password != registerDto.ConfirmPassword)
                throw new ApplicationException("As senhas não coincidem.");

            var author = _mapper.Map<Author>(registerDto);
            // Usando BCrypt para hash da senha
            author.Password = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            await _authorRepository.CreateAsync(author);
            return _mapper.Map<AuthorDto>(author);
        }

        public async Task<string> LoginAsync(AuthorLoginDto loginDto)
        {
            var author = await _authorRepository.GetByEmailAsync(loginDto.Email);
            if (author == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, author.Password))
                throw new ApplicationException("Email ou senha inválidos.");

            return _tokenService.GenerateToken(author);
        }

        public async Task<AuthorDto> GetByIdAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
                throw new ApplicationException("Autor não encontrado.");

            return _mapper.Map<AuthorDto>(author);
        }
    }
}