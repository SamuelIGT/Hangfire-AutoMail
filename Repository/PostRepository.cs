using Hangfire;
using HangfireEmailSchedule.Models;
using HangfireEmailSchedule.Service;
using HangfireEmailSchedule.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangfireEmailSchedule.Repository
{
    public class PostRepository : IPost
    {
        private DBContext db;

        public PostRepository(DBContext _db) {
            db = _db;
        }

        public IEnumerable<Post> GetPosts => db.Posts;
        
        public void Add(Post post)
        {

            db.Posts.Add(post);
            db.SaveChanges();

            BackgroundJob.Enqueue(() => notifyNewPost(post.Text));


            notifyNewPost(post.Text);
        }

        public Post GetPost(long id)
        {
            Post post = db.Posts.Find(id);

            return post;
        }

        public void notifyNewPost(string postText)
        {
            EmailManager emailManager = new EmailManager();
            DateTime now = DateTime.Now;
            int hour = now.Hour;
            int minute = now.Minute;

            string to = "samuel_alves@atlantico.com.br";
            string from = "samuel.br.igt@gmail.com";
            string text = $"Email teste: {now.ToString()} <br> Novo Post: <br>{postText}";

            emailManager.SendEmail("Samuel Alves", to, from, text);
        }
    }
}
