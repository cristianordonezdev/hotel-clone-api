﻿using AutoMapper;
using hotel_clone_api.Libs;
using hotel_clone_api.Models.Domain;
using hotel_clone_api.Models.DTOs;
using hotel_clone_api.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace hotel_clone_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly IOffersRepository offersRepository;
        private readonly IMapper mapper;
        private readonly IImageRepository imageRepository;
        private readonly ILogger<OfferController> logger;
        private readonly Utils utils;

        public OfferController(IOffersRepository offersRepository, IMapper mapper, IWebHostEnvironment webHostEnvironment,
            IImageRepository imageRepository, ILogger<OfferController> logger)
        {
            this.offersRepository = offersRepository;
            this.mapper = mapper;
            this.imageRepository = imageRepository;
            this.logger = logger;
            this.utils = new Utils(webHostEnvironment);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var offersDto = await offersRepository.GetOffers();
            return Ok(offersDto);
        }

        [HttpGet]
        [Authorize(Roles = "Writer")]
        [Route("{Id:Guid}")]

        public async Task<IActionResult> GetOneOffer([FromRoute] Guid Id)
        {
            var offer = await offersRepository.GetOffer(Id);
            if (offer == null)
            {
                return NotFound();
            }
            return Ok(offer);
        }

        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateOffer([FromForm] OfferCreateUpdateDto offerCreateUpdate)
        {
            utils.ValidateFileUpload(new List<IFormFile> { offerCreateUpdate.File }, ModelState);
            if (ModelState.IsValid)
            {
                var offerDomain = mapper.Map<Offer>(offerCreateUpdate);
                await offersRepository.CreateOffer(offerDomain);

                var imageDomain = new Image
                {
                    File = offerCreateUpdate.File,
                    RelativeRelationId = offerDomain.Id,
                    ImageTypeId = Guid.Parse("8929b4bf-5be3-4002-8ad6-b9f46f782f16"),
                };
                var imageUploaded = await imageRepository.UploadImage(imageDomain);

                var offerDto = mapper.Map<OfferDto>(offerDomain);
                offerDto.imagePath = imageUploaded.FilePath;

                return Ok(offerDto);
            }
            var errorResponse = utils.BuildErrorResponse(ModelState);
            return BadRequest(errorResponse);
        }

        [HttpPut]
        [Authorize(Roles = "Writer")]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> UpdateOffer([FromRoute] Guid Id, [FromForm] UpdateOfferDto updateOfferDto)
        {
            var offerDomain = await offersRepository.UpdateOffer(Id, mapper.Map<Offer>(updateOfferDto));
            if (offerDomain == null) { return NotFound(); }

            return Ok(offerDomain);
        }

        [HttpDelete]
        [Authorize(Roles = "Writer")]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> DeleteOffer([FromRoute] Guid Id)
        {
            var offerDomain = await offersRepository.DeleteOffer(Id);
            if (offerDomain == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<OfferDto>(offerDomain));
        }
    }
}
