using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace MacOverflow.Logic.StoredDataModels
{
    public class StoredAppUserInTopic
    {
        public Guid UsersInTopicsId { get; set; }

        public Guid TopicId { get; set; }

        public Guid UserId { get; set; }

        public DateTime CreatedOnDt { get; set; }

        public StoredTopic Topic => StoredTopic.LoadById(TopicId);

            public StoredAppUserInTopic(Guid usersInTopicsId, Guid topicId, Guid userId, DateTime createdOnDt)
            {
                UsersInTopicsId = usersInTopicsId;
                TopicId = topicId;
                UserId = userId;
                CreatedOnDt = createdOnDt;
            }

            public StoredAppUserInTopic()
            {

            }

            public static List<StoredAppUserInTopic> LoadById(Guid id)
            {
                var result = new List<StoredAppUserInTopic>();

                var sql = $@"SELECT 
                            [UsersInTopicsId],
                            [TopicId],
                            [UserId],
                            [CreatedOnDt]
                            FROM[4HC3Project].[dbo].[StoredAppUsersInTopics]
                            WHERE UserId = '{id}'";

                var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

                sqlConnection.Open();

                var sqlCmd = new SqlCommand(sql, sqlConnection);

                var dt = sqlCmd.ExecuteReader();

                while (dt.Read())
                {
                    var usersInTopicsId = (Guid)dt["UsersInTopicsId"];
                    var topicId = (Guid)dt["TopicId"];
                    var userId = (Guid)dt["UserId"];
                    var createdOnDt = (DateTime)dt["CreatedOnDt"];

                    var temp = new StoredAppUserInTopic(
                        usersInTopicsId,
                        topicId,
                        userId,
                        createdOnDt);

                result.Add(temp);
                }

                sqlConnection.Close();

                return result;
            }

        public void Insert()
        {
            var sql = $@"INSERT INTO StoredAppUsersInTopics (UsersInTopicsId, TopicId, UserId, CreatedOnDt)
                        VALUES ('{UsersInTopicsId}', 
                                '{TopicId}', 
                                '{UserId}', 
                                '{CreatedOnDt}');";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            sqlCmd.ExecuteScalar();

            sqlConnection.Close();
        }

        public static void DeleteTopic(Guid topicId)
        {
            var sql = $@"DELETE FROM StoredAppUsersInTopics
                         WHERE TopicId = '{topicId}';";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            sqlCmd.ExecuteScalar();

            sqlConnection.Close();
        }

        public static void Delete(Guid topicId, Guid userId)
        {
            var sql = $@"DELETE FROM StoredAppUsersInTopics
                         WHERE TopicId = '{topicId}' AND userId = '{userId}';";

            var sqlConnection = new SqlConnection(ConnectionString.MyConnectionString);

            sqlConnection.Open();

            var sqlCmd = new SqlCommand(sql, sqlConnection);

            sqlCmd.ExecuteScalar();

            sqlConnection.Close();
        }

    }
    }

