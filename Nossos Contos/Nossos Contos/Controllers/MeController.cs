using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nossos_Contos.Model.MongoDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Nossos_Contos.Controllers
{

    [Route("[controller]")]
    [ApiController]


    public class MeController : Base.BaseController
    {

        private Repositories.MongoDB.PersistentRepository<Entities.Media> mediaRepository;
        private Services.MediaService mediaService;

        public MeController(DatabaseSettings databaseSettings, Model.Configurations.AWS.S3Configuration s3Configuration,
                            Model.Configurations.AWS.Credentials credentials) : base(databaseSettings)
        {
            mediaRepository = new Repositories.MongoDB.PersistentRepository<Entities.Media>(databaseSettings, "medias");
            mediaService = new Services.MediaService(mediaRepository, credentials, s3Configuration);

        }


        [HttpPost]
        public ActionResult<Entities.Media> Create(IFormFile file)
        {

            var info = new FileInfo(file.FileName);
            var extension = info.Extension.ToLower().Replace(".", "");
            var type = mediaService.GetTypeByExtension(extension);
            if (type == Entities.Media.Types.Unknown)
            {
                return BadRequest("INVALID_EXTENSIONS");
            }


            return mediaService.Create(file.OpenReadStream(), type, extension);


        }


    }
}
