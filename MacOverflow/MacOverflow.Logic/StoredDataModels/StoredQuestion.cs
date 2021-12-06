using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

namespace MacOverflow.Logic.StoredDataModels
{
    public class StoredQuestion
    {
        public Guid QuestionId { get; set; }

        [Required]
        [StringLength(int.MaxValue, MinimumLength = 1, ErrorMessage = "Empty inputs are not allowed for Titles")]
        public string Title { get; set; }

        [Required]
        public string Question { get; set; }

        [Required]
        public Guid TopicId { get; set; }

        [Required]
        public string ImportanceLevel { get; set; }

        [Required]
        public string RecommendedAudience { get; set; }
        
        public bool IsApproved { get; set; }
        
        public long Likes { get; set; }
        
        public Guid CreatedByUserId { get; set; }
        
        public DateTime CreatedOnDt { get; set; }

        public Guid LastEditedByUserId { get; set; }

        public DateTime LastEditedOnDt { get; set; }

        public StoredTopic Topic { get; set; }

        public string User { get; set; }

        public bool HasLiked { get; set; }

        public bool HasDisliked { get; set; }
        public StoredQuestion(Guid questionId, 
            string title,
            string question, 
            Guid topicId, 
            string importanceLevel, 
            string recommendedAudience,
            bool isApproved,
            long likes, 
            string user,
            Guid createdByUserId, 
            DateTime createdOnDt,
            Guid lastEditedByUserId,
            DateTime lastEditedOnDt)
        {
            QuestionId = questionId;
            Title = title;
            Question = question;
            TopicId = topicId;
            ImportanceLevel = importanceLevel;
            RecommendedAudience = recommendedAudience;
            IsApproved = isApproved;
            Likes = likes;
            User = user;
            CreatedByUserId = createdByUserId;
            CreatedOnDt = createdOnDt;
            LastEditedByUserId = lastEditedByUserId;
            LastEditedOnDt = lastEditedOnDt;
        }

        public StoredQuestion(string title, string importanceLevel,
            string recommendedAudience, bool isApproved,
            long likes,
            string user)
        {
            Title = title;
            ImportanceLevel = importanceLevel;
            RecommendedAudience = recommendedAudience;
            IsApproved = isApproved;
            Likes = likes;
            User = user;
        }

        public StoredQuestion()
        {

        }

        public static List<StoredQuestion> Load()
        {
            var result = new List<StoredQuestion>();

            var sql = @"SELECT
                              [QuestionId]
                              ,[Title]
                              ,[Question]
                              ,[TopicId]
                              ,[ImportanceLevel]
                              ,[RecommendedAudience]
                              ,[IsApproved]
                              ,[Likes]
                              ,[User]
                              ,[CreatedByUserId]
                              ,[CreatedOnDt]
                              ,[LastEditedByUserId]
                              ,[LastEditedOnDt]
                               FROM[dbo].[StoredQuestions]";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            var dt = sqlCmd.ExecuteReader();

            while (dt.Read())
            {
                var questionId = (Guid)dt["QuestionId"];
                var title = (string)dt["Title"];
                var question = (string)dt["Question"];
                var topicId = (Guid)dt["TopicId"];
                var importanceLevel = (string)dt["ImportanceLevel"];
                var recommendedAudience = (string)dt["RecommendedAudience"];
                var isApproved = (bool)dt["IsApproved"];
                var likes = (long)dt["Likes"];
                var user = (string)dt["User"];
                var createdByUserId = (Guid)dt["CreatedByUserId"];
                var createdOnDt = (DateTime)dt["CreatedOnDt"];
                var lastEditedByUserId = (Guid)dt["LastEditedByUserId"];
                var lastEditedOnDt = (DateTime)dt["LastEditedOnDt"];

                var temp = new StoredQuestion(
                    questionId,
                    title,
                    question,
                    topicId,
                    importanceLevel,
                    recommendedAudience,
                    isApproved,
                    likes,
                    user,
                    createdByUserId,
                    createdOnDt,
                    lastEditedByUserId,
                    lastEditedOnDt);
                result.Add(temp);
            }

            sqlConnection.Close();

            return result;
        }

        public static StoredQuestion LoadById(Guid id)
        {
            var result = new StoredQuestion();

            var sql = $@"SELECT
                              [QuestionId]
                              ,[Title]
                              ,[Question]
                              ,[TopicId]
                              ,[ImportanceLevel]
                              ,[RecommendedAudience]
                              ,[IsApproved]
                              ,[Likes]
                              ,[User]
                              ,[CreatedByUserId]
                              ,[CreatedOnDt]
                              ,[LastEditedByUserId]
                              ,[LastEditedOnDt]
                               FROM[dbo].[StoredQuestions]
                               WHERE QuestionId = '{id}'";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            var dt = sqlCmd.ExecuteReader();

            while (dt.Read())
            {
                var questionId = (Guid)dt["QuestionId"];
                var title = (string)dt["Title"];
                var question = (string)dt["Question"];
                var topicId = (Guid)dt["TopicId"];
                var importanceLevel = (string)dt["ImportanceLevel"];
                var recommendedAudience = (string)dt["RecommendedAudience"];
                var isApproved = (bool)dt["IsApproved"];
                var likes = (long)dt["Likes"];
                var user = (string)dt["User"];
                var createdByUserId = (Guid)dt["CreatedByUserId"];
                var createdOnDt = (DateTime)dt["CreatedOnDt"];
                var lastEditedByUserId = (Guid)dt["LastEditedByUserId"];
                var lastEditedOnDt = (DateTime)dt["LastEditedOnDt"];

                result = new StoredQuestion(
                    questionId,
                    title,
                    question,
                    topicId,
                    importanceLevel,
                    recommendedAudience,
                    isApproved,
                    likes,
                    user,
                    createdByUserId,
                    createdOnDt,
                    lastEditedByUserId,
                    lastEditedOnDt);
            }

            sqlConnection.Close();

            return result;
        }

        public static List<StoredQuestion> LoadByUserId(Guid id)
        {
            var result = new List<StoredQuestion>();

            var sql = $@"SELECT
                              [QuestionId]
                              ,[Title]
                              ,[Question]
                              ,[TopicId]
                              ,[ImportanceLevel]
                              ,[RecommendedAudience]
                              ,[IsApproved]
                              ,[Likes]
                              ,[User]
                              ,[CreatedByUserId]
                              ,[CreatedOnDt]
                              ,[LastEditedByUserId]
                              ,[LastEditedOnDt]
                               FROM[dbo].[StoredQuestions]
                               WHERE CreatedByUserId = '{id}'";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            var dt = sqlCmd.ExecuteReader();

            while (dt.Read())
            {
                var questionId = (Guid)dt["QuestionId"];
                var title = (string)dt["Title"];
                var question = (string)dt["Question"];
                var topicId = (Guid)dt["TopicId"];
                var importanceLevel = (string)dt["ImportanceLevel"];
                var recommendedAudience = (string)dt["RecommendedAudience"];
                var isApproved = (bool)dt["IsApproved"];
                var likes = (long)dt["Likes"];
                var user = (string)dt["User"];
                var createdByUserId = (Guid)dt["CreatedByUserId"];
                var createdOnDt = (DateTime)dt["CreatedOnDt"];
                var lastEditedByUserId = (Guid)dt["LastEditedByUserId"];
                var lastEditedOnDt = (DateTime)dt["LastEditedOnDt"];

                var temp = new StoredQuestion(
                    questionId,
                    title,
                    question,
                    topicId,
                    importanceLevel,
                    recommendedAudience,
                    isApproved,
                    likes,
                    user,
                    createdByUserId,
                    createdOnDt,
                    lastEditedByUserId,
                    lastEditedOnDt);
                result.Add(temp);
            }

            sqlConnection.Close();

            return result;
        }

        public void Insert()
        {
            var sql = $@"INSERT INTO StoredQuestions (QuestionId, Title, Question, TopicId, ImportanceLevel, RecommendedAudience, IsApproved, Likes, [User], CreatedByUserId, CreatedOnDt, LastEditedByUserId, LastEditedOnDt)
                        VALUES ('{QuestionId}', 
                                '{Title}',
                                '{Question}', 
                                '{TopicId}', 
                                '{ImportanceLevel}',
                                '{RecommendedAudience}',
                                '{IsApproved}',
                                '{Likes}',
                                '{User}',
                                '{CreatedByUserId}', 
                                '{CreatedOnDt}', 
                                '{LastEditedByUserId}',
                                '{LastEditedOnDt}');";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            sqlCmd.ExecuteScalar();

            sqlConnection.Close();
        }

        public void Update()
        {
            var sql = $@"UPDATE StoredQuestions
                         SET QuestionId = '{QuestionId}', 
                             Title = '{Title}',
                             Question = '{Question}',
                             TopicId = '{TopicId}',
                             ImportanceLevel = '{ImportanceLevel}', 
                             RecommendedAudience = '{RecommendedAudience}',
                             IsApproved = '{IsApproved}', 
                             Likes = '{Likes}', 
                             [User] = '{User}',
                             CreatedByUserId = '{CreatedByUserId}',
                             CreatedOnDt = '{CreatedOnDt}',
                             LastEditedByUserId = '{LastEditedByUserId}',
                             LastEditedOnDt = '{LastEditedOnDt}'
                         WHERE QuestionId = '{QuestionId}';";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            sqlCmd.ExecuteScalar();

            sqlConnection.Close();
        }

        public static void Delete(Guid id)
        {
            var sql = $@"DELETE FROM StoredQuestions
                         WHERE QuestionId = '{id}';";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            sqlCmd.ExecuteScalar();

            sqlConnection.Close();
        }

        public static void DeleteTopic(Guid id)
        {
            var sql = $@"DELETE FROM StoredQuestions
                         WHERE TopicId = '{id}';";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            sqlCmd.ExecuteScalar();

            sqlConnection.Close();
        }
    }
}