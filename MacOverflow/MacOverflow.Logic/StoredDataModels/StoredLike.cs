using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MacOverflow.Logic.StoredDataModels
{
    public class StoredLike
    {
        public Guid LikeId { get; set; }

        public Guid UserId { get; set; }

        public Guid ResponseId { get; set; }

        public bool IsLike { get; set; }

        public DateTime CreatedOnDt { get; set; }

        public StoredLike(Guid likeId,
            Guid userId,
            Guid responseId,
            bool isLike,
            DateTime createdOnDt)
        {
            LikeId = likeId;
            UserId = userId;
            ResponseId = responseId;
            IsLike = isLike;
            CreatedOnDt = createdOnDt;
        }

        public StoredLike()
        {

        }

        public static List<StoredLike> Load()
        {
            var result = new List<StoredLike>();

            var sql = @"SELECT
                              [LikeId]
                              ,[UserId]
                              ,[ResponseId]
                              ,[IsLike]
                              ,[CreatedOnDt]
                               FROM[dbo].[StoredReaction]";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            var dt = sqlCmd.ExecuteReader();

            while (dt.Read())
            {
                var likeId = (Guid)dt["LikeId"];
                var userId = (Guid)dt["UserId"];
                var responseId = (Guid)dt["ResponseId"];
                var isLike = (bool)dt["IsLike"];
                var createdOnDt = (DateTime)dt["CreatedOnDt"];

                var temp = new StoredLike(
                    likeId,
                    userId,
                    responseId,
                    isLike,
                    createdOnDt);
                result.Add(temp);
            }

            sqlConnection.Close();

            return result;
        }

        public static StoredLike LoadByResponseUser(Guid respId, Guid id)
        {
            var result = new StoredLike();

            var sql = $@"SELECT
                              [LikeId]
                              ,[UserId]
                              ,[ResponseId]
                              ,[IsLike]
                              ,[CreatedOnDt]
                               FROM[dbo].[StoredReaction]
                               WHERE ResponseId = '{respId}' AND UserID = '{id}'";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            var dt = sqlCmd.ExecuteReader();

            while (dt.Read())
            {
                var likeId = (Guid)dt["LikeId"];
                var userId = (Guid)dt["UserId"];
                var responseId = (Guid)dt["ResponseId"];
                var isLike = (bool)dt["IsLike"];
                var createdOnDt = (DateTime)dt["CreatedOnDt"];

                result = new StoredLike(
                    likeId,
                    userId,
                    responseId,
                    isLike,
                    createdOnDt);
            }

            sqlConnection.Close();

            return result;
        }

        public static List<StoredLike> LoadByUser(Guid id)
        {
            var result = new List<StoredLike>();

            var sql = $@"SELECT
                              [LikeId]
                              ,[UserId]
                              ,[ResponseId]
                              ,[IsLike]
                              ,[CreatedOnDt]
                               FROM[dbo].[StoredReaction]
                               WHERE UserId = '{id}'";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            var dt = sqlCmd.ExecuteReader();

            while (dt.Read())
            {
                var likeId = (Guid)dt["LikeId"];
                var userId = (Guid)dt["UserId"];
                var responseId = (Guid)dt["ResponseId"];
                var isLike = (bool)dt["IsLike"];
                var createdOnDt = (DateTime)dt["CreatedOnDt"];

                var temp = new StoredLike(
                    likeId,
                    userId,
                    responseId,
                    isLike,
                    createdOnDt);
                result.Add(temp);
            }

            sqlConnection.Close();

            return result;
        }

        public void Insert()
        {
            var sql = $@"INSERT INTO StoredReaction
                        VALUES ('{LikeId}', 
                                '{UserId}', 
                                '{ResponseId}', 
                                '{IsLike}',
                                '{CreatedOnDt}');";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            sqlCmd.ExecuteScalar();

            sqlConnection.Close();
        }

        public void Update()
        {
            var sql = $@"UPDATE StoredReaction
                         SET IsLike = '{IsLike}'
                         WHERE LikeId = '{LikeId}';";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            sqlCmd.ExecuteScalar();

            sqlConnection.Close();
        }

        public void Delete()
        {
            var sql = $@"DELETE FROM StoredReaction
                         WHERE LikeId = '{LikeId}';";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            sqlCmd.ExecuteScalar();

            sqlConnection.Close();
        }

        public static void DeleteQuestionLike(Guid id)
        {
            var sql = $@"DELETE FROM StoredReaction
                         WHERE ResponseId = '{id}';";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            sqlCmd.ExecuteScalar();

            sqlConnection.Close();
        }
    }
}
