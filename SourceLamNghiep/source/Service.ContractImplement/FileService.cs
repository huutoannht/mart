using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading.Tasks;
using Data.DataContract;
using Data.DataContract.FileDC;
using Db.Interfaces;
using Service.Contract;

namespace Service.ContractImplement
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class FileService : BaseService, IFileService
    {
        private readonly IFileRepository _repository;

        public FileService(IFileRepository repository)
        {
            _repository = repository;
        }

        public bool CheckNameExisted(string name)
        {
            return Execute(_repository, r => r.CheckNameExisted(name));
        }

        public bool CheckNameExisted2(string name, Guid id)
        {
            return Execute(_repository, r => r.CheckNameExisted(name, id));
        }

        public File GetFile(Guid id)
        {
            return Execute(_repository, r => r.GetFile(id));
        }

        public FindResponse FindFiles(FindRequest request)
        {
            return Execute(_repository, r => r.FindFiles(request));
        }

        public BaseResponse SaveFile(SaveRequest request)
        {
            return Execute(_repository, r => r.SaveFile(request));
        }

        public BaseResponse DeleteFile(Guid id, string dataFolderPath)
        {
            return Execute(_repository, r =>
            {
                var file = r.GetFile(id);

                var res = r.DeleteFile(id);

                if (res.Success && file != null)
                {
                    DeleteFile(dataFolderPath, file.PhysicName);
                }

                return res;
            });
        }
    }
}
