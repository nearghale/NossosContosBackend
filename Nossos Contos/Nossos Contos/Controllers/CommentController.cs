using Microsoft.AspNetCore.Mvc;
using Nossos_Contos.Model;
using Nossos_Contos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nossos_Contos.Controllers
{

    [Route("[controller]")]
    [ApiController]

    public class CommentController : Controller
    {

        private Repositories.MongoDB.PersistentRepository<Entities.Comment> commentRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.Tale> taleRepository;
        private CommentService commentService;

        public CommentController(DatabaseSettings databaseSettings)
        {

            commentRepository = new Repositories.MongoDB.PersistentRepository<Entities.Comment>(databaseSettings, "comment");
            taleRepository = new Repositories.MongoDB.PersistentRepository<Entities.Tale>(databaseSettings, "tale");
            commentService = new CommentService(taleRepository, commentRepository);

        }

        [HttpPost]
        public ActionResult<Entities.Comment> Create(Entities.Comment comment)
        {
            var tale = taleRepository.FirstOrDefault(t => t.id == comment.IDTale);
            if (tale == null)
                return this.Unauthorized("TALE_NOT_FOUNDED");

            return commentService.Create(comment);

        }



    }
}
