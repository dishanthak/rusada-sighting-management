using AutoMapper;
using Dishantha.Application.Helper;
using Dishantha.Application.Sightings.DTOs;
using Dishantha.Application.Sightings.Requests;
using Dishantha.Domain.Entities;
using Dishantha.Domain.Repositories;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Threading.Tasks;

namespace Dishantha.Application.Sightings.Services
{
    public class SightingService : ISightingService
    {
        private readonly IRepository<Sighting, int> _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostingEnvironment _env;

        public SightingService(IRepository<Sighting, int> sightingRepository, IMapper mapper, IUnitOfWork unitOfWork, IHostingEnvironment env)
        {
            _repository = sightingRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public async Task<List<SightingDTO>> GetAllAsync()
        {
            var entities = await _repository.GetAllAsync();
            return _mapper.Map<List<SightingDTO>>(entities);
        }

        public async Task<SightingDTO> GetByIdAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            var dto = _mapper.Map<SightingDTO>(entity);

            if (entity.AircraftPhotoName != null)
            {
                byte[] imageArray = System.IO.File.ReadAllBytes(System.IO.Path.Combine(_env.ContentRootPath + "\\" + "AircraftPhotos", entity.AircraftPhotoName));
                dto.AircraftPhoto = String.Format("data:image/png;base64,{0}", Convert.ToBase64String(imageArray));
            }

            return dto;
        }

        public async Task<SightingDTO> AddAsync(CreateSightingRequest request)
        {

            var imageParts = request.AircraftPhoto.Split(',').ToList<string>();
            Image aircraftPhoto = Base64ToImage.ConvertToImage(imageParts[1]);
            var aircraftPhotoURL = string.Empty;
            if (aircraftPhoto != null)
                aircraftPhotoURL = SaveImage(aircraftPhoto);

            var entity = _mapper.Map<Sighting>(request);
            entity.AircraftPhotoName = aircraftPhotoURL;

            entity = await _repository.AddOrUpdateAsync(entity);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<SightingDTO>(entity);
        }

        public async Task<SightingDTO> UpdateAsync(UpdateSightingRequest request)
        {

            var imageParts = request.AircraftPhoto.Split(',').ToList<string>();
            Image aircraftPhoto = Base64ToImage.ConvertToImage(imageParts[1]);
            var aircraftPhotoURL = string.Empty;
            if (aircraftPhoto != null)
                aircraftPhotoURL = SaveImage(aircraftPhoto);

            var entity = _mapper.Map<Sighting>(request);
            entity.AircraftPhotoName = aircraftPhotoURL;

            entity = await _repository.AddOrUpdateAsync(entity);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<SightingDTO>(entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _repository.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();
            return result;
        }

        public async Task<List<SightingDTO>> Search(SearchSightingRequest request)
        {
            var entities = await _repository.GetAsync(t => (t.Make.Contains(request.SearchText) || t.Model.Contains(request.SearchText) || t.Registration.Contains(request.SearchText)));
            return _mapper.Map<List<SightingDTO>>(entities);
        }

        public string SaveImage(Image image)
        {
            string contentRootPath = _env.ContentRootPath;
            Guid imageName = Guid.NewGuid();

            var imagePath = System.IO.Path.Combine(contentRootPath + "\\" + "AircraftPhotos", imageName.ToString() + ".png");
            image.Save(imagePath, ImageFormat.Png);

            return imageName.ToString() +".png";
        }

    }
}
