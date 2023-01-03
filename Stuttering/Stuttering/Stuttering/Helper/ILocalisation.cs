using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stuttering.Helper
{
    public interface ILocalisation
    {
        void ChangeLocale(string cultureName);
    }
}
