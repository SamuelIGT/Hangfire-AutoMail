using HangfireEmailSchedule.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HangfireEmailSchedule.Service
{
    public interface IPost {

        void Add(Post post);
        IEnumerable<Post> GetPosts { get; }
        Post GetPost(long id);
    }
}
