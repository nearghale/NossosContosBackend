using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Nossos_Contos.Services
{
    public class MediaService
    {
        private static Dictionary<Entities.Media.Types, string[]> EXTENSIONS;

        private Repositories.MongoDB.PersistentRepository<Entities.Media> repository;
        private Services.AWS.S3Service s3Service;
        private Model.Configurations.AWS.S3Configuration _s3Configuration;

        public MediaService(Repositories.MongoDB.PersistentRepository<Entities.Media> mediaRepository, 
            Model.Configurations.AWS.Credentials awsCredentials, Model.Configurations.AWS.S3Configuration s3Configuration)
        {
            _s3Configuration = s3Configuration;

            repository = mediaRepository;
            s3Service = new Services.AWS.S3Service(awsCredentials, s3Configuration);

            EXTENSIONS = new Dictionary<Entities.Media.Types, string[]>();
            EXTENSIONS.Add(Entities.Media.Types.Image, new string[] { "jpg", "jpeg", "png", "gif", "svg" });
            EXTENSIONS.Add(Entities.Media.Types.Video, new string[] { "mp4", "3gp", "m4a" });
            EXTENSIONS.Add(Entities.Media.Types.Audio, new string[] { "aac", "mp3", "ogg" });
            EXTENSIONS.Add(Entities.Media.Types.File, new string[] { "pdf", "doc", "docx", "xls", "xlsx", "ppt", "pptx" });
        }

        public Entities.Media.Types GetTypeByExtension(string extension)
        {
            if (EXTENSIONS.Any(e => e.Value.Contains(extension)))
                return EXTENSIONS.FirstOrDefault(i => i.Value.Contains(extension)).Key;

            return Entities.Media.Types.Unknown;
        }

        public Entities.Media Create(Stream stream, Entities.Media.Types type, string extension)
        {
            var media = new Entities.Media(type, extension, $"https://{_s3Configuration.BucketName}.s3.amazonaws.com");

            s3Service.Upload(stream, $"{media.Key}.{extension}", Amazon.S3.S3CannedACL.PublicRead);

            repository.Create(media);
            return media;
        }

        public Stream Get( Entities.Media media)
        {

            var fileName = string.Format("{0}.{1}", media.Key.ToString(), media.Extension.ToString());
            return s3Service.Read(fileName);

        }



    }
}
