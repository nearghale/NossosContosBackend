using Microsoft.AspNetCore.Mvc;
using Nossos_Contos.Models;
using Nossos_Contos.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nossos_Contos.Models.MongoDB;
using Microsoft.AspNetCore.Authorization;

namespace Nossos_Contos.Controllers
{

    [Route("[controller]")]
    [ApiController]
    [Authorize]

    public class CommentController : Base.BaseController
    {

       
        public CommentController(DatabaseSettings databaseSettings) : base(databaseSettings)
        {

        }

        [HttpPost]
        public ActionResult<Entities.Comment> Create(Entities.Comment comment)
        {
            var tale = taleRepository.FirstOrDefault(t => t.IDTale == comment.IDTale);
            if (tale == null)
                return this.Unauthorized("TALE_NOT_FOUNDED");

            return commentService.Create(CognitoUser.sub ,comment);

        }

        [HttpPut("{id}")]
        public ActionResult Update(Guid id, CommentUpdate commentUpdate)
        {
            var comment = commentRepository.FirstOrDefault(c => c.IDComment == id);
            if (comment == null)
                return this.Unauthorized("COMMENT_NOT_FOUNDED");

            commentService.Update(comment, commentUpdate);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(Guid id)
        {
            var comment = commentRepository.FirstOrDefault(c => c.IDComment == id);
            if (comment == null)
                return this.Unauthorized("COMMENT_NOT_FOUNDED");

            commentService.Delete(comment);
            return Ok();
        }



    }
}
