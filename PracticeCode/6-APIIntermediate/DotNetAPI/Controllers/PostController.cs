
using DotNetAPI.Data;
using DotNetAPI.Models;
using DotNetAPI.UserModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class PostController : ControllerBase
    {
        // Setup dapper for database operations
        private readonly DataContextDapper _dapper;
        public PostController(IConfiguration config)
        {
            _dapper = new DataContextDapper(config);
        }

        // API endpoints
        [HttpGet("Posts")]
        public IEnumerable<Post> GetPosts()
        {
            string sql = @"SELECT [PostId],
                    [UserId],
                    [PostTitle],
                    [PostContent],
                    [PostCreated],
                    [PostUpdated] 
                FROM TutorialAppSchema.Posts";

            return _dapper.LoadData<Post>(sql);
        }

        [HttpGet("PostSingle/{postId}")]
        public Post GetPostSingle(int postId)
        {
            string sql = @"SELECT [PostId],
                    [UserId],
                    [PostTitle],
                    [PostContent],
                    [PostCreated],
                    [PostUpdated] 
                FROM TutorialAppSchema.Posts
                WHERE PostId =" + postId.ToString();

            return _dapper.LoadDataSingle<Post>(sql);
        }

        [HttpGet("PostsByUser/{userId}")]
        public IEnumerable<Post> GetPostByUser(int userId)
        {
            string sql = @"SELECT [PostId],
                    [UserId],
                    [PostTitle],
                    [PostContent],
                    [PostCreated],
                    [PostUpdated] 
                FROM TutorialAppSchema.Posts
                WHERE UserId =" + userId.ToString();

            return _dapper.LoadData<Post>(sql);
        }

        [HttpGet("MyPosts")]
        public IEnumerable<Post> GetMyPosts()
        {
            string sql = @"SELECT [PostId],
                    [UserId],
                    [PostTitle],
                    [PostContent],
                    [PostCreated],
                    [PostUpdated] 
                FROM TutorialAppSchema.Posts
                    WHERE UserId =" + this.User.FindFirst("userId")?.Value;


            return _dapper.LoadData<Post>(sql);
        }

        [HttpPost("Post")]
        public IActionResult AddPost(PostToAddDto postToAddDto)
        {
            string sql = @"INSERT INTO TutorialAppSchema.Posts(
                [UserId],
                [PostTitle],
                [PostContent],
                [PostCreated],
                [PostUpdated]) VALUES(" + this.User.FindFirst("userId")?.Value
                + ",'" + postToAddDto.PostTitle
                + "','" + postToAddDto.PostContent
                + "'," + "GETDATE(), GETDATE() )";

            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }

            throw new Exception("Failed to add post");

        }

        [HttpPut("Post")]
        public IActionResult EditPost(PostToEditDto postToEdit)
        {
            string sql = @"
            UPDATE TutorialAppSchema.Posts
                SET PostTitle = '" + postToEdit.PostTitle +
                "', PostContent = '" + postToEdit.PostContent +
                @"', PostUpdated = GETDATE()
                    WHERE PostId = " + postToEdit.PostId.ToString() +
                    "AND UserId = " + this.User.FindFirst("userId")?.Value;  // To check user ID is the same as the post owner

            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }

            throw new Exception("Failed to edit post");

        }

        [HttpGet("PostsBySearch/{searchParam}")]
        public IEnumerable<Post> PostsBySearch(string searchParam)
        {
            string sql = @"SELECT [PostId],
                    [UserId],
                    [PostTitle],
                    [PostContent],
                    [PostCreated],
                    [PostUpdated] 
                FROM TutorialAppSchema.Posts
                    WHERE PostTitle LIKE '%" + searchParam + @"%'
                        OR PostContent LIKE '%" + searchParam + "%'";

            return _dapper.LoadData<Post>(sql);
        }

        [HttpDelete("Post/{postId}")]
        public IActionResult DeletePost(int postId)
        {
            string sql = @"DELETE FROM TutorialAppSchema.Posts
                WHERE PostId = " + postId.ToString();

            if (_dapper.ExecuteSql(sql))
            {
                return Ok();
            }

            throw new Exception("Failed to delete post");
        }
    }
}