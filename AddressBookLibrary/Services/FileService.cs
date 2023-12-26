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
            return Enumerable.Empty<Contact>();
        }

        public bool WriteToJsonFile(IEnumerable<IContact> data, string filePath)
        {
            try
            {
                string jsonDataToWrite = JsonConvert.SerializeObject(data);

                using var sw = new StreamWriter(filePath);
                sw.Write(jsonDataToWrite);
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