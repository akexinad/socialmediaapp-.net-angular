using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SocialMediaApp.API.Data;
using SocialMediaApp.API.Dtos;
using SocialMediaApp.API.Helpers;
using SocialMediaApp.API.Models;

namespace SocialMediaApp.API.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/photos")]
    public class PhotosController : ControllerBase
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public PhotosController(
            IDatingRepository repo,
            IMapper mapper,
            // CLOUDINARY OPTIONS
            IOptions<CloudinarySettings> cloudinaryConfig
            )
        {
            _repo = repo;
            _mapper = mapper;
            _cloudinaryConfig = cloudinaryConfig;

            // Making a new cloudinary account.
            Account acc = new Account(
                // From the CloudinarySettings helper class
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        // We are creating this get method as a requirement for the createdAtRoute method that we invole at the end of our AddPhotoForUser() method.
        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _repo.GetPhoto(id);

            var photo = _mapper.Map<PhotoForReturnDto>(photoFromRepo);

            return Ok()
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int id, PhotoForCreationDto photoForCreationDto)
        {
            // Check if the user making the update is only able to update his own profile.
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var userFromRepo = await _repo.GetUser(id);

            var file = photoForCreationDto.File;

            var uploadResult = new ImageUploadResult();

            // Checking if there is a file.
            if (file.Length > 0)
            {
                // Store the file into memory.
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        // Storing the file that is in memory into cloudinary uploadParams variable.
                        // And cropping the photo accordingly.
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation()
                            .Width(500)
                            .Height(500)
                            .Crop("fill")
                            .Gravity("face")
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoForCreationDto.Url = uploadResult.Uri.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;

            var photo = _mapper.Map<Photo>(photoForCreationDto);

            // Check if the user has a main photo. If notm make this photo his main photo.
            if (!userFromRepo.Photos.Any( u => u.IsMain ))
                photo.IsMain = true;
            
            userFromRepo.Photos.Add(photo);

            if (await _repo.SaveAll())
            {   
                // This needs to be done after the saveAll method because it is
                // SQLites responsibility to provide an id
                var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);
                // The following args are name of the above get request, 
                return CreatedAtRoute("GetPhoto", new { id = photo.Id }, photoToReturn);
            }

            return BadRequest("Could not add the photo.");
        }
    }
}