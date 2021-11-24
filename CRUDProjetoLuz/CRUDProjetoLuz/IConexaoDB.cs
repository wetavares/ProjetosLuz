using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDProjetoLuz
{
    interface IConexaoDB
    {
        void Open();
        void Closed();

    }
}
