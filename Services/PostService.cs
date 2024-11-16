using AutoMapper;
using EcoTrack.Blog.Data.Repositories.Interfaces;
using EcoTrack.Blog.Models.DTOs;
using EcoTrack.Blog.Models.Entities;
using EcoTrack.Blog.Services.Interfaces;

namespace EcoTrack.Blog.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PostDto>> GetAllAsync()
        {
            var posts = await _postRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }

        public async Task<PostDto> GetByIdAsync(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
                throw new ApplicationException("Post não encontrado.");

            return _mapper.Map<PostDto>(post);
        }

        public async Task<IEnumerable<PostDto>> GetByCategoryIdAsync(int categoryId)
        {
            var posts = await _postRepository.GetByCategoryIdAsync(categoryId);
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }

        public async Task<IEnumerable<PostDto>> GetByAuthorIdAsync(int authorId)
        {
            var posts = await _postRepository.GetByAuthorIdAsync(authorId);
            return _mapper.Map<IEnumerable<PostDto>>(posts);
        }

        public async Task<PostDto> CreateAsync(int authorId, PostCreateDto createDto)
        {
            var category = await _categoryRepository.GetByIdAsync(createDto.CategoryId);
            if (category == null)
                throw new ApplicationException("Categoria não encontrada.");

            var post = _mapper.Map<Post>(createDto);
            post.AuthorId = authorId;

            await _postRepository.CreateAsync(post);
            return _mapper.Map<PostDto>(post);
        }

        public async Task<PostDto> UpdateAsync(int authorId, PostUpdateDto updateDto)
        {
            var post = await _postRepository.GetByIdAsync(updateDto.Id);
            if (post == null)
                throw new ApplicationException("Post não encontrado.");

            if (post.AuthorId != authorId)
                throw new ApplicationException("Você não tem permissão para editar este post.");

            var category = await _categoryRepository.GetByIdAsync(updateDto.CategoryId);
            if (category == null)
                throw new ApplicationException("Categoria não encontrada.");

            _mapper.Map(updateDto, post);
            await _postRepository.UpdateAsync(post);
            return _mapper.Map<PostDto>(post);
        }

        public async Task DeleteAsync(int authorId, int postId)
        {
            var post = await _postRepository.GetByIdAsync(postId);
            if (post == null)
                throw new ApplicationException("Post não encontrado.");

            if (post.AuthorId != authorId)
                throw new ApplicationException("Você não tem permissão para excluir este post.");

            await _postRepository.DeleteAsync(postId);
        }
    }
}