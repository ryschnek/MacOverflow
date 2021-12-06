using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

namespace MacOverflow.Logic.StoredDataModels
{
    public class StoredTopic
    {
        public Guid TopicId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Topic is needs to be under 50 characters and not an empty input")]
        public string Topic { get; set; }

        [Required]
        public string TopicCategory { get; set; }

        [Required]
        public bool ModCommentApproval { get; set; }

        [Required]
        public bool ModResponseApproval { get; set; }
      
        public Guid CreatedByUserId { get; set; }
        
        public DateTime CreatedOnDt { get; set; }
        
        public Guid LastEditedByUserId { get; set; }
        
        public DateTime LastEditedOnDt { get; set; }

        public bool IsSubscribed { get; set; }

        public StoredTopic(Guid topicId, 
            string topic, 
            string topicCategory, 
            bool modCommentApproval, 
            bool modResponseApproval,
            Guid createdByUserId, 
            DateTime createdOnDt, 
            Guid lastEditedByUserId, 
            DateTime lastEditedOnDt)
        {
            TopicId = topicId;
            Topic = topic;
            TopicCategory = topicCategory;
            ModCommentApproval = modCommentApproval;
            ModResponseApproval = modResponseApproval;
            CreatedByUserId = createdByUserId;
            CreatedOnDt = createdOnDt;
            LastEditedByUserId = lastEditedByUserId;
            LastEditedOnDt = lastEditedOnDt;
        }

        public StoredTopic()
        {

        }

        public static List<StoredTopic> Load()
        {
            var result = new List<StoredTopic>();

            var sql = @"SELECT
                              [TopicId]
                              ,[Topic]
                              ,[TopicCategory]
                              ,[ModCommentApproval]
                              ,[ModResponseApproval]
                              ,[CreatedByUserId]
                              ,[CreatedOnDt]
                              ,[LastEditedByUserId]
                              ,[LastEditedOnDt]
                               FROM[dbo].[StoredTopics]";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            var dt = sqlCmd.ExecuteReader();

            while (dt.Read())
            {
                var topicId = (Guid)dt["TopicId"];
                var topic = (string)dt["Topic"];
                var topicCategory = (string)dt["TopicCategory"];
                var modCommentApproval = (bool)dt["ModCommentApproval"];
                var modResponseApproval = (bool)dt["ModResponseApproval"];
                var createdByUserId = (Guid)dt["CreatedByUserId"];
                var createdOnDt = (DateTime)dt["CreatedOnDt"];
                var lastEditedByUserId = (Guid)dt["LastEditedByUserId"];
                var lastEditedOnDt = (DateTime)dt["LastEditedOnDt"];

                var temp = new StoredTopic(
                    topicId,
                    topic,
                    topicCategory,
                    modCommentApproval,
                    modResponseApproval,
                    createdByUserId,
                    createdOnDt,
                    lastEditedByUserId,
                    lastEditedOnDt);
                result.Add(temp);
            }

            sqlConnection.Close();

            return result;
        }

        public static StoredTopic LoadById(Guid id)
        {
            var result = new StoredTopic();

            var sql = $@"SELECT
                              [TopicId]
                              ,[Topic]
                              ,[TopicCategory]
                              ,[ModCommentApproval]
                              ,[ModResponseApproval]
                              ,[CreatedByUserId]
                              ,[CreatedOnDt]
                              ,[LastEditedByUserId]
                              ,[LastEditedOnDt]
                               FROM[dbo].[StoredTopics]
                               WHERE TopicId = '{id}'";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            var dt = sqlCmd.ExecuteReader();

            while (dt.Read())
            {
                var topicId = (Guid)dt["TopicId"];
                var topic = (string)dt["Topic"];
                var topicCategory = (string)dt["TopicCategory"];
                var modCommentApproval = (bool)dt["ModCommentApproval"];
                var modResponseApproval = (bool)dt["ModResponseApproval"];
                var createdByUserId = (Guid)dt["CreatedByUserId"];
                var createdOnDt = (DateTime)dt["CreatedOnDt"];
                var lastEditedByUserId = (Guid)dt["LastEditedByUserId"];
                var lastEditedOnDt = (DateTime)dt["LastEditedOnDt"];

                result = new StoredTopic(
                    topicId,
                    topic,
                    topicCategory,
                    modCommentApproval,
                    modResponseApproval,
                    createdByUserId,
                    createdOnDt,
                    lastEditedByUserId,
                    lastEditedOnDt);
            }

            sqlConnection.Close();

            return result;
        }

        public static List<StoredTopic> LoadByUserId(Guid id)
        {
            var result = new List<StoredTopic>();

            var sql = $@"SELECT
                              [TopicId]
                              ,[Topic]
                              ,[TopicCategory]
                              ,[ModCommentApproval]
                              ,[ModResponseApproval]
                              ,[CreatedByUserId]
                              ,[CreatedOnDt]
                              ,[LastEditedByUserId]
                              ,[LastEditedOnDt]
                               FROM[dbo].[StoredTopics]
                               WHERE CreatedByUserId = '{id}'";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            var dt = sqlCmd.ExecuteReader();

            while (dt.Read())
            {
                var topicId = (Guid)dt["TopicId"];
                var topic = (string)dt["Topic"];
                var topicCategory = (string)dt["TopicCategory"];
                var modCommentApproval = (bool)dt["ModCommentApproval"];
                var modResponseApproval = (bool)dt["ModResponseApproval"];
                var createdByUserId = (Guid)dt["CreatedByUserId"];
                var createdOnDt = (DateTime)dt["CreatedOnDt"];
                var lastEditedByUserId = (Guid)dt["LastEditedByUserId"];
                var lastEditedOnDt = (DateTime)dt["LastEditedOnDt"];

                var temp = new StoredTopic(
                    topicId,
                    topic,
                    topicCategory,
                    modCommentApproval,
                    modResponseApproval,
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
            var sql = $@"INSERT INTO StoredTopics (TopicId, Topic, TopicCategory, ModCommentApproval, ModResponseApproval, CreatedByUserId, CreatedOnDt, LastEditedByUserId, LastEditedOnDt)
                        VALUES ('{TopicId}', 
                                '{Topic}', 
                                '{TopicCategory}', 
                                '{ModCommentApproval}',
                                '{ModResponseApproval}', 
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
            var sql = $@"UPDATE StoredTopics
                         SET Topic = '{Topic}', 
                             TopicCategory = '{TopicCategory}',
                             ModCommentApproval = '{ModCommentApproval}',
                             ModResponseApproval = '{ModResponseApproval}', 
                             CreatedByUserId = '{CreatedByUserId}',
                             CreatedOnDt = '{CreatedOnDt}',
                             LastEditedByUserId = '{LastEditedByUserId}',
                             LastEditedOnDt = '{LastEditedOnDt}'
                         WHERE TopicId = '{TopicId}';";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            sqlCmd.ExecuteScalar();

            sqlConnection.Close();
        }

        public static void Delete(Guid id)
        {
            var sql = $@"DELETE FROM StoredTopics
                         WHERE TopicId = '{id}';";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            sqlCmd.ExecuteScalar();

            sqlConnection.Close();
        }
    }
}