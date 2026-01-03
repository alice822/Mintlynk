using MintLynk.Application.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MintLynk.Application.Dtos
{
    public class MiniPageDto
    {
        public new Guid Id { get; set; }
        public int Type { get; set; }
        public string Title { get; set; }
        public string Alias { get; set; }

        public string? PageContent { get; set; }

        public string Template { get; set; }
        public int Status { get; set; }

        public DateTimeOffset? Created { get; set; }

        public string CreatedBy { get; set; }

        public DateTimeOffset? Modified { get; set; }

        public string LastModifiedBy { get; set; }
    }

    public class TemplateType
    {
        public string Alias { get; set; }
        public string Name { get; set; }
        public List<PageTemplate> PageTemplates { get; set; } = new();
    }

    public class PageTemplate
    {
        public string Alias { get; set; }
        public string Name { get; set; }
        public string Intro { get; set; }
        public string TemplateContent { get; set; }
    }

    public class MiniPageCommon
    {
        public string? Title { get; set; }

        public string? Intro { get; set; }

        public string? Bio { get; set; }

        public string? BannerImage { get; set; }

        public string? ImageUrl { get; set; }

        public string? ImageAlt { get; set; }

        public string? Address { get; set; }

        public string? ContactNumber { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Url]
        public string? Website { get; set; }

        [UniqueAlias]
        public string? Alias { get; set; }

        public List<SocialMediaLink>? SocialLinks { get; set; } = new();
    }

    // 1️⃣ User Profile (like Linktree)
    public class UserProfile : MiniPageCommon
    {
        public string FullName { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string Bio { get; set; }
    }

    public class ProfessionalProfile : MiniPageCommon
    {
        public string? Profession { get; set; }

        public List<ProfileSupport>? Qualification { get; set; } = new();
        public List<ProfileSupport>? Experience { get; set; } = new();

        public List<ProfileSupport>? Achievements { get; set; } = new();
    }

    public class ProfessionalService : MiniPageCommon
    {
        public string? Profession { get; set; }
        public List<Item> Items { get; set; } = new();
    }

    public class SocialMediaLink
    {
        public string Platform { get; set; }
        public string Url { get; set; }
        public int SortOrder { get; set; }
    }

    // 2️⃣ Restaurant Menu
    public class RestaurantMenu
    {
        public string Name { get; set; }
        public string Intro { get; set; }

        public List<Category> Categories { get; set; } = new();
    }

    public class Category
    {
        public string Name { get; set; }
        public List<Item> Items { get; set; } = new();
    }

    public class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        public string Url { get; set; }
    }

    // 3️⃣ Hospital Service
    public class HospitalService
    {
        public string HospitalName { get; set; }
        public string Address { get; set; }
        public List<Service> Services { get; set; } = new();
    }

    public class Service
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    // 4️⃣ Doctor Profile
    public class DoctorProfile
    {
        public string Name { get; set; }
        public string Specialty { get; set; }
        public string Qualification { get; set; }
        public string ProfilePictureUrl { get; set; }
        public List<AvailableSlot> AvailableSlots { get; set; } = new();
    }

    public class AvailableSlot
    {
        public DateTime Date { get; set; }
        public string Time { get; set; }
    }

    // 5️⃣ Retail Shop
    public class RetailShop
    {
        public string ShopName { get; set; }
        public string Description { get; set; }
        public List<RetailProduct> Products { get; set; } = new();
    }

    public class RetailProduct
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
    }

    // 6️⃣ Pathology Lab
    public class PathologyLab
    {
        public string LabName { get; set; }
        public string Address { get; set; }
        public List<Test> TestsOffered { get; set; } = new();
    }

    public class Test
    {
        public string TestName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }

    public class ProfileSupport
    {
        public string? Year { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public int? SortOrder { get; set; }
    }
}
