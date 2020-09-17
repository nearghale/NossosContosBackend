using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nossos_Contos.Services
{
    public class CommentService
    {

        private Repositories.MongoDB.PersistentRepository<Entities.Comment> _commentRepository;
        private Repositories.MongoDB.PersistentRepository<Entities.Tale> _taleRepository;


        public CommentService(Repositories.MongoDB.PersistentRepository<Entities.Tale> taleRepository, Repositories.MongoDB.PersistentRepository<Entities.Comment> commentRepository)
        {

            _commentRepository = commentRepository;
            _taleRepository = taleRepository;

        }


        public Entities.Comment Create(Guid userId,Entities.Comment comment)
        {

            var newComment = new Entities.Comment();

            newComment.Commentary = comment.Commentary;
            newComment.IDComment = Guid.NewGuid();
            newComment.IDUser = userId;
            newComment.IDTale = comment.IDTale;
            newComment.TitleComment = comment.TitleComment;
            newComment.CreationDateTime = DateTime.Now;

            return _commentRepository.Create(newComment);

        }

        public void Update(Entities.Comment comment, Models.CommentUpdate commentUpdate)
        {
            comment.Commentary = commentUpdate.commentary;
            comment.TitleComment = commentUpdate.title_comment;
            comment.UpdateDateTime = DateTime.Now;

            _commentRepository.Update(comment.id, comment);

        }

        public void Delete(Entities.Comment comment)
        {
            _commentRepository.Remove(comment);

        }
    }
}
