using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MintLynk.Application.Dtos;
using MintLynk.Application.Extensions;
using MintLynk.Application.Interfaces;
using MintLynk.Domain.Enums;
using MintLynk.Domain.Interfaces;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MintLynk.Application.Services
{
    public class MiniPageService : IMiniPageService
    {
        private readonly IMiniPageRepository _pageRepository;  

        public MiniPageService(IMiniPageRepository pageRepository) 
        { 
            _pageRepository = pageRepository;
        }

        public async Task<bool> AliasExistsAsync(string alias)
        {
            var data = await _pageRepository.GetAsync(alias);
            if (data == null)
            {
                return false;
            }
            return true;
        }

        public async Task<MiniPageDto> CreateAsync(MiniPageDto page)
        {
            var data = page.ToMiniPage();
            var createdData = await _pageRepository.CreateAsync(data);
            return createdData.ToMiniPageDto();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var data = GetAsync(id).Result;
            if (data != null)
            {
                data.Status = (int)MiniPageStatus.Deleted;
                var updatedData = await _pageRepository.UpdateAsync(data.ToMiniPage());
                return updatedData != null;
            }
            else 
            { 
                return false;
            }
        }

        public async Task<IEnumerable<MiniPageDto>> GetAllAsync()
        {
            var data = await _pageRepository.GetAllAsync();
            if (data == null || !data.Any())
            {
                return Enumerable.Empty<MiniPageDto>();
            }
            return data.Select(x => x.ToMiniPageDto());
        }


        public async Task<IEnumerable<MiniPageDto>> GetAllAsync(string userId)
        {
            var data = await _pageRepository.GetAllAsync(userId);
            if (data == null || !data.Any())
            {
                return Enumerable.Empty<MiniPageDto>();
            }
            return data.Select(x => x.ToMiniPageDto());
        }

        public async Task<MiniPageDto?> GetAsync(Guid id)
        {
            var data = await _pageRepository.GetAsync(id);
            if (data == null)
            {
                return null;
            }
            return data.ToMiniPageDto();
        }

        public async Task<MiniPageDto?> GetAsync(string alias)
        {
            var data = await _pageRepository.GetAsync(alias);
            if (data == null)
            {
                return null;
            }
            return data.ToMiniPageDto();
        }

        public async Task<MiniPageDto> UpdateAsync(MiniPageDto page)
        {
           var data = page.ToMiniPage();
            var updatedData = await _pageRepository.UpdateAsync(data);
            return updatedData.ToMiniPageDto();
        }
    }
}