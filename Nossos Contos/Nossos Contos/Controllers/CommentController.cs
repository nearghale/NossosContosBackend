using Microsoft.AspNetCore.Mvc;
using Nossos_Contos.Model;
using Nossos_Contos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nossos_Contos.Model.MongoDB;

namespace Nossos_Contos.Controllers
{

    [Route("[controller]")]
    [ApiController]

    public class CommentController : Base.BaseController
    {

       
        public CommentController(DatabaseSettings databaseSettings) : base(databaseSettings)
        {

        }

        [HttpPost]
        public ActionResult<Entities.Comment> Create(Entities.Comment comment)
        {
            var tale = taleRepository.FirstOrDefault(t => t.id == comment.IDTale);
            if (tale == null)
                return this.Unauthorized("TALE_NOT_FOUNDED");

            return commentService.Create(comment);

        }

        [HttpPut("{id}")]
        public ActionResult Update(string id, Model.CommentUpdate commentUpdate)
        {
            var comment = commentRepository.FirstOrDefault(c => c.id == id);
            if (comment == null)
                return this.Unauthorized("COMMENT_NOT_FOUNDED");

            commentService.Update(comment, commentUpdate);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var comment = commentRepository.FirstOrDefault(c => c.id == id);
            if (comment == null)
                return this.Unauthorized("COMMENT_NOT_FOUNDED");

            commentService.Delete(comment);
            return Ok();
        }



    }
}
