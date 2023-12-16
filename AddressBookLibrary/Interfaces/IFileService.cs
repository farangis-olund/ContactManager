namespace AddressBookLibrary.Interfaces
{
    public interface IFileService
    {
        /// <summary>
        /// Reads contact information from a JSON file located at the specified file path.
        /// </summary>
        /// <param name="filePath">The path to the JSON file containing contact information. 
        /// Example: "C:\Folder\contacts.json"</param>
        /// <returns>
        /// An enumerable collection of objects implementing the IContact interface, representing
        /// the contact information read from the JSON file. Returns an empty collection if the
        /// file doesn't exist, is empty, or encounters an exception during the reading process.
        /// </returns>
        IEnumerable<IContact> ReadFromJsonFile(string filePath);

        /// <summary>
        /// Writes contact information represented by an enumerable collection of objects implementing
        /// the IContact interface to a JSON file at the specified file path.
        /// </summary>
        /// <param name="data">The enumerable collection of contact information implementing the IContact interface.</param>
        /// <param name="filePath">The path to the JSON file where the contact information will be written.</param>
        /// <returns>
        /// A boolean value indicating whether the operation to write the data to the JSON file was successful (true) or encountered an exception (false).
        /// </returns>
        bool WriteToJsonFile(IEnumerable<IContact> data, string filePath);
    }
}