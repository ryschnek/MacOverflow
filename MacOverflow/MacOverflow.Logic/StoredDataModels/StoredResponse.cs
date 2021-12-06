using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Text;

namespace MacOverflow.Logic.StoredDataModels
{
    public class StoredResponse
    {
        public Guid ResponseId { get; set; }

        [Required]
        public string Response { get; set; }

        public long Likes { get; set; }

        public Guid ParentCommentId { get; set; }

        public bool IsApproved { get; set; }

        public Guid CreatedByUserId { get; set; }

        public DateTime CreatedOnDt { get; set; }

        public DateTime LastEditedOnDt { get; set; }

        public string User { get; set; }

        public StoredQuestion ParentQuestion { get; set; }

        public bool HasLiked { get; set; }

        public bool HasDisliked { get; set; }

        public StoredResponse(Guid responseId,
            string response,
            long likes,
            Guid parentCommentId,
            bool isApproved,
            string user,
            Guid createdByUserId,
            DateTime createdOnDt,
            DateTime lastEditedOnDt)
        {
            ResponseId = responseId;
            Response = response;
            Likes = likes;
            ParentCommentId = parentCommentId;
            IsApproved = isApproved;
            User = user;
            CreatedByUserId = createdByUserId;
            CreatedOnDt = createdOnDt;
            LastEditedOnDt = lastEditedOnDt;
        }

        public StoredResponse()
        {

        }

        public static List<StoredResponse> Load()
        {
            var result = new List<StoredResponse>();

            var sql = @"SELECT
                              ResponseId,
                              Response,
                              Likes,
                              ParentCommentId,
                              IsApproved,
                              [User],
                              CreatedByUserId,
                              CreatedOnDt,
                              LastEditedOnDt
                              FROM[dbo].[StoredResponses]";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            var dt = sqlCmd.ExecuteReader();

            while (dt.Read())
            {
                var responseId = (Guid)dt["ResponseId"];
                var response = (string)dt["Response"];
                var likes = (long)dt["Likes"];
                var parentCommentId = (Guid)dt["ParentCommentId"];
                var isApproved = (bool)dt["IsApproved"];
                var user = (string)dt["User"];
                var createdByUserId = (Guid)dt["CreatedByUserId"];
                var createdOnDt = (DateTime)dt["CreatedOnDt"];
                var lastEditedOnDt = (DateTime)dt["LastEditedOnDt"];

                var temp = new StoredResponse(
                    responseId,
                    response,
                    likes,
                    parentCommentId,
                    isApproved,
                    user,
                    createdByUserId,
                    createdOnDt,
                    lastEditedOnDt);
                result.Add(temp);
            }

            sqlConnection.Close();

            return result;
        }

        public static List<StoredResponse> LoadByQuestionId(Guid id)
        {
            var result = new List<StoredResponse>();

            var sql = $@"SELECT
                              ResponseId,
                              Response,
                              Likes,
                              ParentCommentId,
                              IsApproved,
                              [User],
                              CreatedByUserId,
                              CreatedOnDt,
                              LastEditedOnDt
                               FROM[dbo].[StoredResponses]
                               WHERE ParentCommentId = '{id}'";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            var dt = sqlCmd.ExecuteReader();

            while (dt.Read())
            {
                var responseId = (Guid)dt["ResponseId"];
                var response = (string)dt["Response"];
                var likes = (long)dt["Likes"];
                var parentCommentId = (Guid)dt["ParentCommentId"];
                var isApproved = (bool)dt["IsApproved"];
                var user = (string)dt["User"];
                var createdByUserId = (Guid)dt["CreatedByUserId"];
                var createdOnDt = (DateTime)dt["CreatedOnDt"];
                var lastEditedOnDt = (DateTime)dt["LastEditedOnDt"];

                var temp = new StoredResponse(
                    responseId,
                    response,
                    likes,
                    parentCommentId,
                    isApproved,
                    user,
                    createdByUserId,
                    createdOnDt,
                    lastEditedOnDt);
                result.Add(temp);
            }

            sqlConnection.Close();

            return result;
        }

        public static StoredResponse LoadById(Guid id)
        {
            var result = new StoredResponse();

            var sql = $@"SELECT
                              ResponseId,
                              Response,
                              Likes,
                              ParentCommentId,
                              IsApproved,
                              [User],
                              CreatedByUserId,
                              CreatedOnDt,
                              LastEditedOnDt
                               FROM[dbo].[StoredResponses]
                               WHERE ResponseId = '{id}'";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            var dt = sqlCmd.ExecuteReader();

            while (dt.Read())
            {
                var responseId = (Guid)dt["ResponseId"];
                var response = (string)dt["Response"];
                var likes = (long)dt["Likes"];
                var parentCommentId = (Guid)dt["ParentCommentId"];
                var isApproved = (bool)dt["IsApproved"];
                var user = (string)dt["User"];
                var createdByUserId = (Guid)dt["CreatedByUserId"];
                var createdOnDt = (DateTime)dt["CreatedOnDt"];
                var lastEditedOnDt = (DateTime)dt["LastEditedOnDt"];

                result = new StoredResponse(
                    responseId,
                    response,
                    likes,
                    parentCommentId,
                    isApproved,
                    user,
                    createdByUserId,
                    createdOnDt,
                    lastEditedOnDt);
            }

            sqlConnection.Close();

            return result;
        }

        public void Insert()
        {
            var sql = $@"INSERT INTO StoredResponses (ResponseId,
                              Response,
                              Likes,
                              ParentCommentId,
                              IsApproved,
                              [User],
                              CreatedByUserId,
                              CreatedOnDt,
                              LastEditedOnDt)
                        VALUES ('{ResponseId}', 
                                '{Response}',
                                '{Likes}', 
                                '{ParentCommentId}', 
                                '{IsApproved}',
                                '{User}',
                                '{CreatedByUserId}', 
                                '{CreatedOnDt}',
                                '{LastEditedOnDt}');";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            sqlCmd.ExecuteScalar();

            sqlConnection.Close();
        }

        public void Update()
        {
            var sql = $@"UPDATE StoredResponses
                         SET ResponseId = '{ResponseId}', 
                             Response = '{Response}',
                             Likes = '{Likes}',
                             ParentCommentId = '{ParentCommentId}',
                             IsApproved = '{IsApproved}', 
                             [User] = '{User}',
                             CreatedByUserId = '{CreatedByUserId}',
                             CreatedOnDt = '{CreatedOnDt}',
                             LastEditedOnDt = '{LastEditedOnDt}'
                         WHERE ResponseId = '{ResponseId}';";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            sqlCmd.ExecuteScalar();

            sqlConnection.Close();
        }

        public static void Delete(Guid id)
        {
            var sql = $@"DELETE FROM StoredResponses
                         WHERE ResponseId = '{id}';";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            sqlCmd.ExecuteScalar();

            sqlConnection.Close();
        }

        public static void DeleteQuestion(Guid id)
        {
            var sql = $@"DELETE FROM StoredResponses
                         WHERE ParentCommentId = '{id}';";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            sqlCmd.ExecuteScalar();

            sqlConnection.Close();
        }
    }
}
