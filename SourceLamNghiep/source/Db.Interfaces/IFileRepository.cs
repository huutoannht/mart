using System;
using Data.DataContract;
using Data.DataContract.FileDC;

namespace Db.Interfaces
{
    public interface IFileRepository
    {
        bool CheckNameExisted(string name, Guid id);

        bool CheckNameExisted(string name);

        File GetFile(Guid id);

        FindResponse FindFiles(FindRequest request);

        BaseResponse SaveFile(SaveRequest request);

        BaseResponse DeleteFile(Guid id);
    }
}
