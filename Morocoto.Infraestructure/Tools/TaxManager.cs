using Morocoto.Domain.Models;
using Morocoto.Infraestructure.Implementations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Morocoto.Infraestructure.Tools
{
    public class TaxManager
    {
        private readonly CustomerTaxis _tax;
        private readonly CustomerTaxesRepository _taxRepository;

        public TaxManager(CustomerTaxis tax, CustomerTaxesRepository taxRepository)
        {
            this._tax = tax;
            this._taxRepository = taxRepository;
        }

        public async Task<CustomerTaxis> CalculateTax(int creditRequested)
        {
            if (creditRequested > 50 && 100 > creditRequested)
            {
                var taxApplied = await _taxRepository.FirstOrDefaultAsync(x => x.Id == 1);
                return taxApplied;
            }
            if (creditRequested > 101 && 500 > creditRequested)
            {
                var taxApplied = await _taxRepository.FirstOrDefaultAsync(x => x.Id == 2);
                return taxApplied;
            }
            if (creditRequested > 501 && 1000 > creditRequested)
            {
                var taxApplied = await _taxRepository.FirstOrDefaultAsync(x => x.Id == 3);
                return taxApplied;
            }
            if (creditRequested > 1001 && 2000 > creditRequested)
            {
                var taxApplied = await _taxRepository.FirstOrDefaultAsync(x => x.Id == 4);
                return taxApplied;
            }
            if (creditRequested > 2001 && 5000 > creditRequested)
            {
                var taxApplied = await _taxRepository.FirstOrDefaultAsync(x => x.Id == 5);
                return taxApplied;
            }
            if (creditRequested > 50 && 100 > creditRequested)
            {
                var taxApplied = await _taxRepository.FirstOrDefaultAsync(x => x.Id == 1);
                return taxApplied;
            }
            else
            {
                return null;
            }

        }


    }
}
