using AddressBookLibrary.Interfaces;
using AddressBookLibrary.Models;
using AddressBookLibrary.Services;
using Moq;
using Newtonsoft.Json;

namespace AddressBookLibrary.Tests
{
    public class FileService_Test
    {
        private readonly FileService fileServiceUnderTest;

        private readonly string filePath = @"C:\projects\contacts.json";
        
        private readonly List<IContact> testContacts;
        public FileService_Test()
        {
            fileServiceUnderTest = new FileService();

            testContacts =
            [
                new Contact { FirstName = "Elsa", LastName = "Olund", Email = "elsa@example.com", PhoneNumber = "07344344", Address = "st.Example, 4555" },
                new Contact { FirstName = "Jane", LastName = "Smith", Email = "jane@example.com", PhoneNumber = "54654656", Address = "st.Example, 4555" }
            ];
        }

       

        [Fact]
        public void WriteToJsonFile_WhenSuccessful_ReturnsTrue()
        {
            // Arrange
                       
            // Act
            var result = fileServiceUnderTest.WriteToJsonFile(testContacts, filePath);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void WriteToJsonFile_WhenFails_ReturnsFalse()
        {
            // Arrange
                       
            string nonExistentFilePath = "nonExistentDirectory/testFile.json";
           
            // Act
            var result = fileServiceUnderTest.WriteToJsonFile(testContacts, nonExistentFilePath);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ReadFromJsonFile_WhenFileExists_ReturnsContacts()
        {
            // Arrange
           
            string jsonData = JsonConvert.SerializeObject(testContacts);
            File.WriteAllText(filePath, jsonData);

            // Act
            var result = fileServiceUnderTest.ReadFromJsonFile(filePath);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(testContacts.Count, result.Count()); 
                    
            File.Delete(filePath);
        }

        [Fact]
        public void ReadFromJsonFile_WhenFileDoesNotExist_ReturnsEmptyList()
        {
            // Arrange
            string nonExistentFilePath = "nonExistentFile.json";

            // Act
            var result = fileServiceUnderTest.ReadFromJsonFile(nonExistentFilePath);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result); 
        }


    }

}
