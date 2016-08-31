using System;
using System.IO;
using System.ServiceModel;
using ML.Common.Error;
using ML.Common.Log;
using Share.Helper;

namespace Service.ContractImplement
{
    public abstract class BaseService
    {
        private ILogger _logger;

        protected TResult Execute<TRepository, TResult>(TRepository repository, Func<TRepository, TResult> repositoryFunc)
        {
            try
            {
                return repositoryFunc(repository);
            }
            catch (Exception error)
            {
                HandleException(error);
            }

            return default(TResult);
        }

        protected TResult Execute<TResult>(Func<TResult> repositoryFunc)
        {
            try
            {
                return repositoryFunc();
            }
            catch (Exception error)
            {
                HandleException(error);
            }

            return default(TResult);
        }

        protected void Execute(Action action)
        {
            try
            {
                action();
            }
            catch (Exception error)
            {
                HandleException(error);
            }
        }

        private void HandleException(Exception error)
        {
            ErrorException errorException;
            if ((errorException = error as ErrorException) != null)
            {
                _logger.Info("ErrorException: " + errorException.Message, errorException);

                throw new FaultException<ErrorManager>(new ErrorManager
                {
                    Id = errorException.ErrorId,
                    Description = errorException.Message,
                    OriginalException = error
                }, new FaultReason(errorException.Message), new FaultCode(errorException.ErrorId.ToString()));
            }

            var lastesError = error.InnerException ?? error;
            _logger.Error("Error Handler: " + lastesError.Message, lastesError);

            throw new FaultException<ErrorManager>(new ErrorManager
            {
                Id = -1,
                Description = lastesError.Message,
                OriginalException = error,
            }, new FaultReason(lastesError.Message), new FaultCode((-1).ToString()));
        }

        protected void DeleteFile(string dataFolderPath, string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return;

            var physicalFilePath = Path.Combine(dataFolderPath, fileName);
            if (File.Exists(physicalFilePath))
            {
                File.Delete(physicalFilePath);
            }

            physicalFilePath = Path.Combine(dataFolderPath, ConstKeys.ImageThumbPrefix + fileName);
            if (File.Exists(physicalFilePath))
            {
                File.Delete(physicalFilePath);
            }

            physicalFilePath = Path.Combine(dataFolderPath, ConstKeys.ImageThumbSmallPrefix + fileName);
            if (File.Exists(physicalFilePath))
            {
                File.Delete(physicalFilePath);
            }
        }
    }
}
