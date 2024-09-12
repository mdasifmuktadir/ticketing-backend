using Eapproval.Helpers.IHelpers;

namespace Eapproval.Helpers;


    public class FileHandler:IFileHandler
    {
        public string GetUniqueFileName(string fileName)
        {
            var fileName2 = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName2)
                + "_"
                + Guid.NewGuid().ToString().Substring(0, 8)
                + Path.GetExtension(fileName2);
        }

        public async Task<string> SaveFile(string path, string filename, IFormFile file)
        {

            var filePath = Path.Combine(path, filename);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
              await file.CopyToAsync(stream);
            }

            return filePath;
        }
    }

