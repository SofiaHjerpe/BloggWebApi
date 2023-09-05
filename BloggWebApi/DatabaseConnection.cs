using BloggWebApi.Models;
using System.Data.SqlClient;
using System.Reflection;

namespace BloggWebApi
{
    public class DatabaseConnection
    {
        public List<Post> GetAllPosts()
        {
            var cmd = GetSqlCommand();  
            List<Post> posts = new List<Post>();
            cmd.CommandText = "SELECT * FROM Post";
            var reader = cmd.ExecuteReader(); 
            while (reader.Read()) {
            
              var post = new Post { 
                  Id = int.Parse(reader["Id"].ToString()), 
                  Title = reader["Title"].ToString(),
                  Content = reader["Content"].ToString(),
                  Date = DateTime.Parse(reader["Date"].ToString())
               };
                posts.Add(post);    

            }
            return posts;   

        }
        public Post GetPostById(int id) { 
            var cmd = GetSqlCommand();
            cmd.CommandText = "SELECT * FROM Post WHERE Id= @id";
            cmd.Parameters.AddWithValue("id", id);
            var reader = cmd.ExecuteReader();   
            while (reader.Read()) {

                var post = new Post
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Title = reader["Title"].ToString(),
                    Content = reader["Content"].ToString(),
                    Date = DateTime.Parse(reader["Date"].ToString())
                };
                return post;
            }

            return null;

        }
        public void SavePost(Post post)
        {
            var cmd = GetSqlCommand();
            cmd.CommandText = "INSERT INTO Post(Title, Content, Date) VALUES (@title, @content, @date)";
            cmd.Parameters.AddWithValue("title", post.Title);
            cmd.Parameters.AddWithValue("content", post.Content);
            cmd.Parameters.AddWithValue("date", post.Date);
            cmd.ExecuteNonQuery();  
        }

        public void UpdatePost(int id, Post post)
        {
            var cmd = GetSqlCommand();
            cmd.CommandText = "UPDATE Post SET Title=@title, Content=@content, Date=@date WHERE Id= @id";
            cmd.Parameters.AddWithValue("title", post.Title);
            cmd.Parameters.AddWithValue("content", post.Content);
            cmd.Parameters.AddWithValue("date", post.Date);
            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();

        } 
        public void DeletePost(int id) {
            var cmd = GetSqlCommand();
            cmd.CommandText = "DELETE FROM Post WHERE Id = @id";
            cmd.Parameters.AddWithValue("id", id); 
            cmd.ExecuteNonQuery();   
        }
        private SqlCommand GetSqlCommand()
        {
            string connectionString = "Data Source=localhost;Initial Catalog=MyBlogg;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            return cmd;
        }
    }
}
