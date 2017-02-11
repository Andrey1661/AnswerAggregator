using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BL.DTO.PresentationToBl;
using BL.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.BL.Services
{
    [TestClass]
    public class ServerFileManagerTest
    {
        private readonly bool _deleteTestFolders = true;

        private string _testDirectory;

        [TestInitialize]
        public void SetUp()
        {
            _testDirectory = Path.Combine(@"E:\", "ServerFileManagerTest");

            if (!Directory.Exists(_testDirectory))
                Directory.CreateDirectory(_testDirectory);
        }

        [TestCleanup]
        public void CloseTests()
        {
            if (_deleteTestFolders && Directory.Exists(_testDirectory))
                Directory.Delete(_testDirectory, true);
        }

        [TestMethod]
        public void SaveFileTest()
        {
            //Инициализация менеджера
            //_testDirectory - путь к папке для тестовых файлов
            var manager = new FileService(_testDirectory);

            var testFileName = "test.txt";
            var file = CreateTestFile(testFileName);

            //test
            var folderName = "FileManagerTest";
            var result = manager.SaveFile(folderName, file);

            var endPath = Path.Combine(_testDirectory, folderName, testFileName);      

            Assert.IsTrue(File.Exists(endPath));
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void SaveFilesTest()
        {
            var manager = new FileService(_testDirectory);
            var files = CreateTestFiles(100);

            var models = files.Select(t => new FileModel
            {
                FileName = Path.GetFileName(t),
                Data = File.ReadAllBytes(t)
            });

            var folderName = "SaveFilesTest";
            var result = manager.SaveFiles(folderName, models);

            var filesExist = files.All(t => File.Exists(Path.Combine(_testDirectory, folderName, t)));

            Assert.IsTrue(filesExist, "В результате выполнениея не все файлы были сохранены");
            Assert.IsTrue(result.Success, "В процессе выполнения было выброгено исключение");
        }

        [TestMethod]
        public void GetFilesFromServerTest()
        {
            var fileCount = 100;

            var manager = new FileService(_testDirectory);

            var files = new List<string>();
            for (var i = 0; i < fileCount; i++)
            {
                var current = "testFile" + i + ".txt";
                files.Add(current);
                File.WriteAllText(Path.Combine(_testDirectory, current), "test file " + i + " text");
            }

            var models = manager.GetFiles("");

            Assert.IsTrue(models.Count() == fileCount);
        }

        [TestMethod]
        public void SaveFileAsyncTest()
        {
            //Инициализация менеджера
            //_testDirectory - путь к папке для тестовых файлов
            var manager = new FileService(_testDirectory);

            var testFileName = "test.txt";
            var file = CreateTestFile(testFileName);

            //test
            var folderName = "FileManagerTest";
            var task = manager.SaveFileAsync(folderName, file);

            task.Wait();
            var result = task.Result;

            var endPath = Path.Combine(_testDirectory, folderName, testFileName);

            Assert.IsTrue(File.Exists(endPath));
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void SaveFilesAsyncTest()
        {
            var manager = new FileService(_testDirectory);
            var files = CreateTestFiles(100);

            var models = files.Select(t => new FileModel
            {
                FileName = Path.GetFileName(t),
                Data = File.ReadAllBytes(t)
            });

            var folderName = "SaveFilesTest";
            var task = manager.SaveFilesAsync(folderName, models);
            task.Wait();

            var result = task.Result;

            var filesExist = files.All(t => File.Exists(Path.Combine(_testDirectory, folderName, t)));

            Assert.IsTrue(filesExist);
            Assert.IsTrue(result.Success);
        }

        private FileModel CreateTestFile(string testFileName)
        {
            //Полный путь к тестовому файлу
            var fileName = Path.Combine(_testDirectory, testFileName);

            //Создание тестового файла и запись в него текста
            File.AppendAllText(fileName, "text for test file");

            return new FileModel
            {
                FileName = testFileName,
                Data = File.ReadAllBytes(fileName)
            };
        }

        private List<string> CreateTestFiles(int count)
        {
            var files = new List<string>();
            for (var i = 0; i < count; i++)
            {
                var current = "testFile" + i + ".txt";
                var fullName = Path.Combine(_testDirectory, current);
                files.Add(fullName);
                File.WriteAllText(fullName, "test file " + i + " text");
            }

            return files;
        } 
    }
}
