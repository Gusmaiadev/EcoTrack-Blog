using AutoMapper;
using EcoTrack.Blog.Data.Repositories.Interfaces;
using EcoTrack.Blog.Models.DTOs;
using EcoTrack.Blog.Models.Entities;
using EcoTrack.Blog.Services.Interfaces;

namespace EcoTrack.Blog.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDto>>(categories);
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new ApplicationException("Categoria não encontrada.");

            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> CreateAsync(CategoryCreateDto createDto)
        {
            if (await _categoryRepository.NameExistsAsync(createDto.Name))
                throw new ApplicationException("Já existe uma categoria com este nome.");

            var category = _mapper.Map<Category>(createDto);
            await _categoryRepository.CreateAsync(category);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task<CategoryDto> UpdateAsync(int id, CategoryCreateDto updateDto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new ApplicationException("Categoria não encontrada.");

            if (await _categoryRepository.NameExistsAsync(updateDto.Name) && category.Name != updateDto.Name)
                throw new ApplicationException("Já existe uma categoria com este nome.");

            _mapper.Map(updateDto, category);
            await _categoryRepository.UpdateAsync(category);
            return _mapper.Map<CategoryDto>(category);
        }

        public async Task DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new ApplicationException("Categoria não encontrada.");

            await _categoryRepository.DeleteAsync(id);
        }
    }
}