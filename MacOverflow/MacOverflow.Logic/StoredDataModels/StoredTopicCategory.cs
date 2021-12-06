using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;

namespace MacOverflow.Logic.StoredDataModels
{
    public class StoredTopicCategory
    {
        public Guid TopicCategoryId { get; set; }

        public string TopicCategory { get; set; }

        public Guid CreatedByUserId { get; set; }

        public DateTime CreatedOnDt { get; set; }

        public StoredTopicCategory(Guid topicCategoryId,
            string topicCategory,
            Guid createdByUserId,
            DateTime createdOnDt)
        {
            TopicCategoryId = topicCategoryId;
            TopicCategory = topicCategory;
            CreatedByUserId = createdByUserId;
            CreatedOnDt = createdOnDt;
        }

        public StoredTopicCategory()
        {

        }

        public static List<StoredTopicCategory> Load()
        {
            var result = new List<StoredTopicCategory>();

            var sql = @"SELECT
                              [TopicCategoryId]
                              ,[TopicCategory]
                              ,[CreatedByUserId]
                              ,[CreatedOnDt]
                               FROM[dbo].[StoredTopicCategories]";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            var dt = sqlCmd.ExecuteReader();

            while (dt.Read())
            {
                var topicCategoryId = (Guid)dt["TopicCategoryId"];
                var topicCategory = (string)dt["TopicCategory"];
                var createdByUserId = (Guid)dt["CreatedByUserId"];
                var createdOnDt = (DateTime)dt["CreatedOnDt"];

                var temp = new StoredTopicCategory(
                    topicCategoryId,
                    topicCategory,
                    createdByUserId,
                    createdOnDt);
                result.Add(temp);
            }

            sqlConnection.Close();

            return result;
        }

        public static StoredTopicCategory LoadById(Guid id)
        {
            var result = new StoredTopicCategory();

            var sql = $@"SELECT
                              [TopicCategoryId]
                              ,[TopicCategory]
                              ,[CreatedByUserId]
                              ,[CreatedOnDt]
                               FROM[dbo].[StoredTopicCategories]
                               WHERE TopicCategoryId = '{id}'";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            var dt = sqlCmd.ExecuteReader();

            while (dt.Read())
            {
                var topicCategoryId = (Guid)dt["TopicCategoryId"];
                var topicCategory = (string)dt["TopicCategory"];
                var createdByUserId = (Guid)dt["CreatedByUserId"];
                var createdOnDt = (DateTime)dt["CreatedOnDt"];

                result = new StoredTopicCategory(
                    topicCategoryId,
                    topicCategory,
                    createdByUserId,
                    createdOnDt);
            }

            sqlConnection.Close();

            return result;
        }

        public void Insert()
        {
            var sql = $@"INSERT INTO StoredTopicCategories
                        VALUES ({TopicCategoryId}, 
                                {TopicCategory}, 
                                {CreatedByUserId}, 
                                {CreatedOnDt});";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            sqlCmd.ExecuteScalar();

            sqlConnection.Close();
        }

        public void Update()
        {
            var sql = $@"UPDATE StoredTopicCategories
                         SET TopicCategory = {TopicCategory}, 
                             CreatedByUserId = {CreatedByUserId},
                             CreatedOnDt = {CreatedOnDt}
                         WHERE TopicCategoryId = '{TopicCategoryId}';";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            sqlCmd.ExecuteScalar();

            sqlConnection.Close();
        }

        public void Delete()
        {
            var sql = $@"DELETE FROM StoredTopicCategories
                         WHERE TopicCategoryId = '{TopicCategoryId}';";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            sqlCmd.ExecuteScalar();

            sqlConnection.Close();
        }
    }
}