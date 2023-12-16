using AddressBookLibrary.Interfaces;
using AddressBookLibrary.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace AddressBookLibrary.Services
{
    public class FileService : IFileService
    {
        public IEnumerable<IContact> ReadFromJsonFile(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string jsonData = File.ReadAllText(filePath);
                    var contacts = JsonConvert.DeserializeObject<List<Contact>>(jsonData);
                    if (contacts != null)
                    {
                        return contacts.Cast<IContact>();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return new List<IContact>();
        }

        public bool WriteToJsonFile(IEnumerable<IContact> data, string filePath)
        {
            try
            {
                // Serialize the updated list
                string jsonDataToWrite = JsonConvert.SerializeObject(data);

                // Write the updated data back to the file
                using (var sw = new StreamWriter(filePath))
                {
                    sw.Write(jsonDataToWrite);
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }

        }
    }

}