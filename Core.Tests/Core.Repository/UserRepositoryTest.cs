using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Model;
using Core.Repository.Interface;
using Core.Repository;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using Core.Ef.Interface;
using Core.Ef;
using System.Data.Entity.Infrastructure;

namespace Core.Tests.Core.Repository
{
    /// <summary>
    /// All Comments and Summaries written by : Behnam Alavi
    /// </summary>
    [TestClass]
    public class UserRepositoryTest
    {
        private IRepository<User> _userRepo;
        private FakeDbContext _dbContxt;
        private DbContextBase _realDbContext;
        private Mock<IRepository<User>> _mockFakeService;

        [TestInitialize]
        public void InitialiseUserRepositoryTest()
        {
            _dbContxt = new FakeDbContext();
            // _userRepo = new GenericRepository<User>();
            //Forcing the mock Service to behave in a way that if any kind of method is used before registered by setup,throws exception.
            _mockFakeService = new Mock<IRepository<User>>(MockBehavior.Strict);
        }

        [TestMethod]
        public void Can_Get_All_UsersFromDb()
        {
            /*-- Tests the functionality Of Db(without any mock Objet) --*/
            using (_realDbContext = new DbContextBase())
            {
                //1.Arrange
                _userRepo = new GenericRepository<User>();
                //2.Act
                var resultAll = _userRepo.All().ToList();
                //3.Assert
                Assert.IsNotNull(resultAll, "All Method Does not contain any result");
                Assert.IsTrue(resultAll.Count > 0);
            }
        }

        [TestMethod]
        public void Can_Insert_Any_User_In_Db()
        {
            using (_realDbContext = new DbContextBase())
            {
                //1.Arrange
                var _userRepo = new GenericRepository<User>(_realDbContext);
                var user = new User { Id = 1, FName = "Ali", LName = "Asgharpoor", UserName = "a-asgharpoor" };
                //2.Act
                var newlyInsertedUser = _userRepo.Create(user);
                //3.Assert
                Assert.IsNotNull(newlyInsertedUser);
                Assert.AreEqual(newlyInsertedUser.LName, user.LName);
            }
        }

        [TestMethod]
        public void Can_Insert_Any_User_WithSameUserName_In_Db()
        {
            using (_realDbContext = new DbContextBase())
            {
                //1.Arrange
                var _userRepo = new GenericRepository<User>(_realDbContext);
                var user = new User { Id = 1, FName = "Ali", LName = "Asgharpoor", UserName = "a-asgharpoor" };
                //2.Act
                var newlyInsertedUser = _userRepo.Create(user);
                //3.Assert
                Assert.IsNotNull(newlyInsertedUser);
                Assert.AreEqual(newlyInsertedUser.LName, user.LName);
            }
        }

        [TestMethod]
        public void Can_Update_Any_User_In_Db()
        {
            //This operation is composed from two sub operation : first Create and then Update.
            using (_realDbContext = new DbContextBase())
            {
                //1.Arrange
                var _userRepo = new GenericRepository<User>(_realDbContext);
                var user = new User { Id = 1, FName = "Hooshang", LName = "Havazi", UserName = "h-havazi" };
                //2.Act
                var tobeUpdateUser = _userRepo.Filter(u => u.UserName.Contains(user.UserName)).FirstOrDefault();
                tobeUpdateUser.FName = "HooshangUpdated";
                tobeUpdateUser.LName = "HavaziUpdated";
                tobeUpdateUser.UserName = "h-havazi-updated";
                var updatedRes = _userRepo.Update(tobeUpdateUser);
                //3.Assert
                Assert.IsTrue(updatedRes == 1);
            }
        }

        [TestMethod]
        public void Can_Update_Any_User_In_Db_With_Manipulated_Id()
        {
            //This operation is composed from two sub operation : first Create and then Update.
            using (_realDbContext = new DbContextBase())
            {
                //1.Arrange
                var _userRepo = new GenericRepository<User>(_realDbContext);
                var user = new User { Id = 1, FName = "Hooshang", LName = "Havazi", UserName = "h-havazi" };
                //2.Act
                var newlyInsertedUser = _userRepo.Create(user);
                //firstResult.Id = 1;
                newlyInsertedUser.Id = 1;
                newlyInsertedUser.FName = "Hushyar";
                newlyInsertedUser.LName = "Hekmatan";
                newlyInsertedUser.UserName = "h-hekmatan";
                var updatedRes = _userRepo.Update(newlyInsertedUser);
                //3.Assert
                Assert.IsTrue(updatedRes == 1);
            }
        }

        [TestMethod]
        public void Can_Delete_Any_User_From_Db()
        {
            //This operation is composed from two sub operation : first Create and then Update.
            using (_realDbContext = new DbContextBase())
            {
                //1.Arrange
                var _userRepo = new GenericRepository<User>(_realDbContext);
                var user = new User { Id = 1, FName = "Ali", LName = "Havazi", UserName = "h-havazi-updated" };
                //2.Act
                var tobeDeleteUser = _userRepo.Filter(u => !u.UserName.Contains(user.UserName) && u.FName.Contains(user.FName)).FirstOrDefault();
                var deletedUser = _userRepo.Delete(tobeDeleteUser , true);
                //3.Assert
                Assert.IsTrue(deletedUser == 1);
            }
        }

        [TestMethod]
        public void LazyLoad_Should_Not_Be_Enabled_ByDefault()
        {
            /*-- Tests the functionality Of Db(without any mock Objet) --*/
            using (_realDbContext = new DbContextBase())
            {
                //1.Arrange
                var _userRepo = new GenericRepository<User>(_realDbContext);
                //2.Act
                var firstResult = _userRepo.All().ToList().First();
                //3.Assert
                Assert.IsTrue(firstResult.Roles.Count == 0);
            }
        }

        [TestMethod]
        public void Call_AllMethod_DoesNot_ThrowException()
        {
            //TODO:
            /* Tests with Mock fack objects which are provided by Moq framework */
            //1.Arrange
            //_dbContxt.Users.Add(new User { Id = 1, FName = "Behnam", LName = "Alavi", UserName = "b-alavi" });
            //var users = _dbContxt.Users.ToList();
            // _userRepo = new GenericRepository<User>(_dbContxt);
            //2.Act
            //var result = _userRepo.All().ToList();
            //3.Assert
            // Assert.IsNotNull(result);
            // Assert.AreEqual(result.ToList().Count, users.ToList().Count);
        }

        [TestMethod]
        public void Filter_Expresion_Return_DesiredResult_On_Each_Condition_Real()
        {
            /*-- Testing with fack objects which are provided by our own  --*/
            //1.Arrange
            _dbContxt.Users.Add(new User { Id = 1, FName = "Behnam", LName = "Alavi", UserName = "b-alavi" });
            _dbContxt.Users.Add(new User { Id = 2, FName = "هما", LName = "گچپچي", UserName = "h-gachpachi" });
            _dbContxt.Users.Add(new User { Id = 3, FName = "Ali", LName = "Ladani", UserName = "a-ladani" });
            _dbContxt.Users.Add(new User { Id = 4, FName = "Masood", LName = "Hasani", UserName = "m-hasani" });
            _userRepo = new GenericRepository<User>(_dbContxt);
            var users = new List<User> { 
                  new User { Id = 1, FName = "Behnam" , LName = "Alavi", UserName = "b-alavi" } ,
                  new User { Id = 2, FName = "هما" , LName = "گچپچي", UserName = "h-gachpachi" } ,
                  new User { Id = 3, FName = "Ali" , LName = "Ladani", UserName = "a-ladani" } ,
                  new User { Id = 4, FName = "Masood" , LName = "Hasani", UserName = "m-hasani" }};
            //2.Act
            var fakePerson = users.FirstOrDefault(u => u.LName.Contains("گچپچي")).LName;
            var result = _userRepo.Filter(u => u.LName.Contains("گچپچي")).ToList().FirstOrDefault().LName;
            //3.Assert
            Assert.AreEqual(result, fakePerson);
        }

        [TestMethod]
        public void Filter_Expresion_Return_DesiredResult_On_Each_Condition_Fake()
        {
            /*-- Testing with Mock fack objects which are provided by Moq fack objects --*/
            // 1.Arrange
            _dbContxt.Users.Add(new User { Id = 1, FName = "Behnam", LName = "Alavi", UserName = "b-alavi" });
            _dbContxt.Users.Add(new User { Id = 2, FName = "هما", LName = "گچپچي", UserName = "h-gachpachi" });
            _dbContxt.Users.Add(new User { Id = 3, FName = "Ali", LName = "Ladani", UserName = "a-ladani" });
            _dbContxt.Users.Add(new User { Id = 4, FName = "Masood", LName = "Hasani", UserName = "m-hasani" });
            _userRepo = new GenericRepository<User>(_dbContxt);
            var users = new List<User> { 
                  new User { Id = 1, FName = "Behnam" , LName = "Alavi", UserName = "b-alavi" } ,
                  new User { Id = 2, FName = "هما" , LName = "گچپچي", UserName = "h-gachpachi" } ,
                  new User { Id = 3, FName = "Ali" , LName = "Ladani", UserName = "a-ladani" } ,
                  new User { Id = 4, FName = "Masood" , LName = "Hasani", UserName = "m-hasani" }};
            // _mockFakeService.Setup(pr => pr.Filter(u => u.FName.Contains('B'))).Returns(users.AsQueryable());
            //// 2.Act
            var fakePerson = users.FirstOrDefault(u => u.LName.Contains("گچپچي")).LName;
            var result = _userRepo.Filter(u => u.LName.Contains("گچپچي")).ToList().FirstOrDefault().LName;
            // 3.Assert
            Assert.AreEqual(result, fakePerson);
        }

        [TestMethod]
        public void Filter_Expression_Has_Desired_Result_On_Each_Page_Size()
        {
            using (_realDbContext = new DbContextBase())
            {
                //1.Arrange
                var _userRepo = new GenericRepository<User>(_realDbContext);
                var users = new List<User>();
                for (var i = 0; i < 250; i++)
                {
                    var user = new User
                    {
                        Id = 1,
                        FName = "A" + "_" + i.ToString(),
                        LName = "B" + "_" + i.ToString(),
                        UserName = "a-b" + "_" + i.ToString()
                    };
                    users.Add(_userRepo.Create(user));
                }

                //2.Act
                var userIn21th = users.FirstOrDefault(u => u.UserName.Contains("_21"));
                int total = 1;
                //The method 'Skip' is only supported for sorted input in LINQ to Entities. 
                //The method 'OrderBy' must be called before the method 'Skip'.
                var usrIn21thFromDb = _userRepo.Filter((u => u.UserName.Contains("_")), out total, 2, 10).AsEnumerable().FirstOrDefault();
                //3.Assert
                Assert.AreEqual(userIn21th.FName, usrIn21thFromDb.FName);
            }
        }

        //[TestMethod]
        //public void Does_It_Allow_To_Insert_Users_With_The_Same_userName()
        //{

        //}

        //[TestMethod]
        //public void Does_It_RollBack_The_Transaction_On_AnyException_In_Bulk_Insert()
        //{

        //}

        ////[TestMethod]
        ////public void Does_It_RollBack_The_Transaction_On_AnyException_In_Bulk_Update()
        ////{
        ////    // Action act = () => 
        ////}

        //#region Concurrency Problems Tests

        [TestMethod]
        [ExpectedException(typeof(DbUpdateConcurrencyException))]
        public void Should_ConcurrencyChecking_Faild_If_The_SameRow_IsEditedTwice()
        {
            //1.Arrange
            var tobeUpdatedUser1 = new User();
            var tobeUpdatedUser2 = new User();
            //1.Insert
            //using (var contextZero = new DbContextBase())
            //{
            //    var user = new User { FName = "ConTestFirst", LName = "ConTstLast", UserName = "ContestUserName" };
            //    var repo = new GenericRepository<User>(contextZero);
            //    tobeInsertUser = repo.Create(user);
            //}
            //1.Act
            //2.Update
            /* This code will cause the test to evaluate to fale */
            //using (var contextOne = new DbContextBase())
            //{
            //    var repo = new GenericRepository<User>(contextOne);
            //    tobeUpdatedUser1 = repo.Find(u => u.Id == 566);
            //    tobeUpdatedUser1.UserName = "B";
            //    repo.Update(tobeUpdatedUser1);
            //}
            //using (var contextTwo = new DbContextBase())
            //{
            //    var repo1 = new GenericRepository<User>(contextTwo);
            //    tobeUpdatedUser2 = repo1.Find(u => u.Id == 566);
            //    tobeUpdatedUser2.UserName = "A";
            //    repo1.Update(tobeUpdatedUser2);
            //}
            /* This code will cause the test to evaluate to true */
            using (var contextOne = new DbContextBase())
            {
                var repo = new GenericRepository<User>(contextOne);
                tobeUpdatedUser1 = repo.Find(u => u.Id == 566);
                tobeUpdatedUser1.UserName = "B";
                using (var contextTwo = new DbContextBase())
                {
                    var repo1 = new GenericRepository<User>(contextTwo);
                    tobeUpdatedUser2 = repo1.Find(u => u.Id == 566);
                    tobeUpdatedUser2.UserName = "A";
                    repo.Update(tobeUpdatedUser1);
                    repo1.Update(tobeUpdatedUser2);
                }
            }
        }
       // #endregion
    }
}
