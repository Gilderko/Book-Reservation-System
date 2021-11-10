using DAL;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFInfrastructure
{
    public class UnitOfWorkProvider
    {  
        private IUnitOfWork _unitOfWork;

        public IUnitOfWork GetUnitOfWork()
        {
            if (_unitOfWork == null)
            {
                _unitOfWork = new UnitOfWork(new BookRentalDbContext());                
            }

            return _unitOfWork;
        }

        public void DiscardUnitOfWork()
        {
            _unitOfWork.Dispose();
            _unitOfWork = null;
        }
    }
}
