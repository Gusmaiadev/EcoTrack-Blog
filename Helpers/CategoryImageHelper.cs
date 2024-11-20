// Em EcoTrack Blog/Helpers/CategoryImageHelper.cs

using System;

namespace EcoTrack.Blog.Helpers
{
    public static class CategoryImageHelper
    {
        public static string GetCategoryImage(string categoryName)
        {
            return categoryName?.ToLower() switch
            {
                "microondas" => "/images/categories/post-microondas.jpg",
                "geladeira" => "/images/categories/post-geladeira.jpg",
                "chuveiro" => "/images/categories/post-chuveiro.jpg",
                "fogão" => "/images/categories/post-fogao.jpg",
                "luz" => "/images/categories/post-lampada.jpg",
                "televisão" => "/images/categories/post-tv.jpg",
                _ => "/images/categories/post-default.jpg",
            };
        }
    }
}