using Dapper;
using DataAccess.Abstraction;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ApplicationRepository : SqlDbRepository<Application>, IApplicationRepository
    {
        public ApplicationRepository(string connectionString) : base(connectionString)
        {
        }


        public bool AddApplication(AddApplicationModel addApplication)
        {
            using (var connection = GetOpenConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int applicationId = addApplication.application.Id;
                        if (addApplication.application.Id > 0)
                        {
                            var deleteEducationSql = "DELETE FROM Education WHERE AppId = @AppId";
                            var deleteWorkSql = "DELETE FROM Work WHERE AppId = @AppId";
                            var deleteLanguageSql = "DELETE FROM Language WHERE AppId = @AppId";
                            var deleteSkillsSql = "DELETE FROM Skills WHERE AppId = @AppId";
                            connection.Execute(deleteEducationSql, new { AppId = applicationId }, transaction);
                            connection.Execute(deleteWorkSql, new { AppId = applicationId }, transaction);
                            connection.Execute(deleteLanguageSql, new { AppId = applicationId }, transaction);
                            connection.Execute(deleteSkillsSql, new { AppId = applicationId }, transaction);


                            var updateApplicationSql = @"
                                                        UPDATE Application 
                                                        SET 
                                                            Name = @Name,
                                                            Email = @Email,
                                                            Address = @Address,
                                                            Gender = @Gender,
                                                            Contact = @Contact,
                                                            PreferredLocation = @PreferredLocation,
                                                            ECTC = @ECTC,
                                                            CCTC = @CCTC,
                                                            Notice = @Notice
                                                        WHERE Id = @Id;
                                                    ";

                            connection.Execute(updateApplicationSql, addApplication.application, transaction);
                        }
                        else
                        {
                            // Add application record
                            var addApplicationSql = "INSERT INTO Application (Name, Email, Address, Gender, Contact, PreferredLocation, ECTC, CCTC, Notice) VALUES (@Name, @Email, @Address, @Gender, @Contact, @PreferredLocation, @ECTC, @CCTC, @Notice); SELECT SCOPE_IDENTITY();";
                            applicationId = connection.QuerySingle<int>(addApplicationSql, addApplication.application, transaction);
                        }

                        // Add education records
                        var addEducationSql = "INSERT INTO Education (AppId, Board, Year, CGPA) VALUES (@AppId, @Board, @Year, @CGPA);";
                        connection.Execute(addEducationSql, addApplication.educations.Select(edu => new { AppId = applicationId, edu.Board, edu.Year, edu.CGPA }), transaction);

                        // Add work records
                        var addWorkSql = "INSERT INTO Work (AppId, Company, Designation, FromDate, ToDate) VALUES (@AppId, @Company, @Designation, @FromDate, @ToDate);";
                        if (addApplication.works != null)
                        {
                            connection.Execute(addWorkSql, addApplication.works.Select(work => new { AppId = applicationId, work.Company, work.Designation, work.FromDate, work.ToDate }), transaction);
                        }

                        // Add language records
                        var addLanguageSql = "INSERT INTO Language (AppId, LanguageName, CanRead, CanWrite, CanSpeak) VALUES (@AppId, @LanguageName, @CanRead, @CanWrite, @CanSpeak);";
                        if (addApplication.languages != null)
                        {
                            connection.Execute(addLanguageSql, addApplication.languages.Select(lang => new { AppId = applicationId, lang.LanguageName, lang.CanRead, lang.CanWrite, lang.CanSpeak }), transaction);
                        }

                        // Add skills records
                        var addSkillsSql = "INSERT INTO Skills (AppId, SkillName, SkillRate) VALUES (@AppId, @SkillName, @SkillRate);";
                        if (addApplication.skills != null)
                        {
                            connection.Execute(addSkillsSql, addApplication.skills.Select(skill => new { AppId = applicationId, skill.SkillName, skill.SkillRate }), transaction);
                        }

                        // Commit the transaction
                        transaction.Commit();

                        return true;
                    }
                    catch (Exception)
                    {
                        // An error occurred, rollback the transaction
                        transaction.Rollback();
                        throw; // Rethrow the exception for logging or further handling
                    }
                }
            }
        }

        public AddApplicationModel GetApplicationData(int id)
        {
            using (var connection = GetOpenConnection())
            {
                connection.Open();

                AddApplicationModel applicationData = null;
                // Assuming there is a SQL query to retrieve application data based on ID
                var getApplicationSql = "SELECT * FROM Application WHERE Id = @Id;";
                var applicationDataObj = connection.QueryFirstOrDefault<Application>(getApplicationSql, new { Id = id });

                if (applicationDataObj != null)
                {
                    applicationData = new()
                    {
                        application = applicationDataObj,
                    };
                    // Assuming there are queries to retrieve related data (education, work, language, skills)
                    var getEducationSql = "SELECT * FROM Education WHERE AppId = @AppId;";
                    applicationData.educations = connection.Query<Education>(getEducationSql, new { AppId = id }).ToList();

                    var getWorkSql = "SELECT * FROM Work WHERE AppId = @AppId;";
                    applicationData.works = connection.Query<Work>(getWorkSql, new { AppId = id }).ToList();

                    var getLanguageSql = "SELECT * FROM Language WHERE AppId = @AppId;";
                    applicationData.languages = connection.Query<Language>(getLanguageSql, new { AppId = id }).ToList();

                    var getSkillsSql = "SELECT * FROM Skills WHERE AppId = @AppId;";
                    applicationData.skills = connection.Query<Skills>(getSkillsSql, new { AppId = id }).ToList();
                }

                return applicationData;
            }
        }

        public override void Delete(int aId)
        {
            using var connection = GetOpenConnection();
            connection.Open();
            var deleteApplicationSql = "DELETE FROM Application WHERE Id = @AppId";
            var deleteEducationSql = "DELETE FROM Education WHERE AppId = @AppId";
            var deleteWorkSql = "DELETE FROM Work WHERE AppId = @AppId";
            var deleteLanguageSql = "DELETE FROM Language WHERE AppId = @AppId";
            var deleteSkillsSql = "DELETE FROM Skills WHERE AppId = @AppId";

            using var transaction = connection.BeginTransaction();

            try
            {
                // Delete related records from child tables
                connection.Execute(deleteEducationSql, new { AppId = aId }, transaction);
                connection.Execute(deleteWorkSql, new { AppId = aId }, transaction);
                connection.Execute(deleteLanguageSql, new { AppId = aId }, transaction);
                connection.Execute(deleteSkillsSql, new { AppId = aId }, transaction);

                // Delete the application record
                connection.Execute(deleteApplicationSql, new { AppId = aId }, transaction);

                // Commit the transaction
                transaction.Commit();
            }
            catch (Exception)
            {
                // An error occurred, rollback the transaction
                transaction.Rollback();
                throw; // Rethrow the exception for logging or further handling
            }
        }


        public override IEnumerable<Application> GetAll()
        {
            using var connection = GetOpenConnection();

            var sql = "SELECT * FROM Application";
            var applications = connection.Query<Application>(sql);

            return applications;
        }

        public override IEnumerable<Application> GetAllById(int aId)
        {
            throw new NotImplementedException();
        }

        public override Application GetSingle(int aId)
        {
            throw new NotImplementedException();
        }

        public override void Insert(Application aEntityToUpdate)
        {
            throw new NotImplementedException();
        }

        public override void Update(Application aEntityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
